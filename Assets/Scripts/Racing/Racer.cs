using System.Collections;
using System.Collections.Generic;
using Racing.Agents;
using Racing.Collidables;
using UnityEngine;

namespace Racing {
	public class Racer : MonoBehaviour {

		//When a collision happens, how long does the interpolation happen over
		private const float DEFAULT_TIME_TO_ADJUST_SPEED = 0.5f;
		//How strong are chickens' passive acceleration, compared to active flapping.
		private static readonly float PASSIVE_FLAP_SCALER = 150f;

		//Shorthand
		public Rigidbody rb;
		public RacerChickenStack chickenStack;
		public float pitch = 0;
		public float yaw = 0;

		[SerializeField]
		private Agent agent;

		void Start() {
			rb = GetComponent<Rigidbody>();

			//TODO Just for testing. Assign this in the factory.
			agent = new PlayerAgent(this);
			chickenStack = new RacerChickenStack(new List<RacerChicken>(){
				new RacerChicken(50, 60, 0.004f, 2f, 1.9f, 50)
			});
		}

		void Update() {
			//Rotate the model based on what the agent has requested
			float pitchChange = agent.getInclineChange();
			this.transform.Rotate(Vector3.right * pitchChange);
			pitch += pitchChange;

			float yawChange = agent.getYawChange();
			this.transform.Rotate(Vector3.up * yawChange);
			yaw += yawChange;

			Debug.Log("Pitch: " + pitch + ". Yaw: " + yaw);

			applyFlightForces();

			rb.velocity += agent.getVelocityChange();
		}

		protected void applyFlightForces() {
			//slow for drag
			rb.velocity *= (1 - chickenStack.getDrag(rb.velocity.magnitude));

			//Applies gravity, subtracts lift, but subtracts less lift when you're diving
			float gravity = chickenStack.getNetGravity(pitch);
			rb.velocity += new Vector3(0, gravity, 0);

			//This fakes the forward acceleration lift would provide when diving.
			//The boost caps out at about 60 degrees i think. at least on my test chicken. idk how that compares to reality though :p
			float xBoost = Mathf.Cos((Mathf.PI / 180f) * pitch);
			rb.velocity += transform.forward * -2 * xBoost * gravity;

			//Passive flap acceleration
			if (rb.velocity.magnitude < chickenStack.getMaxSpeed()) {
				rb.velocity += transform.forward * (chickenStack.getFlapStrength() / Racer.PASSIVE_FLAP_SCALER);
			}

		}

		//Used for collision with obstacles or other adjustments we do external to the gliding "physics"
		public IEnumerator adjustSpeed(float velocityScaler, float timeToAdjustSpeed = Racer.DEFAULT_TIME_TO_ADJUST_SPEED) {
			float progress = 0;
			Vector3 originalVelocity = rb.velocity;
			Vector3 targetVelocity = originalVelocity * velocityScaler;

			agent.disableInput();

			//Lerp between the speeds
			while (progress > timeToAdjustSpeed) {
				float smoothProgress = Mathf.SmoothStep(0.0f, 1.0f, progress / timeToAdjustSpeed);
				rb.velocity = Vector3.Lerp(originalVelocity, targetVelocity, smoothProgress);
				progress += Time.deltaTime;
				yield return null;
			}

			agent.enableInput();
		}

		void OnCollisionEnter(Collision other) {
			if (other.gameObject.GetComponent<RaceCollidable>() != null) {
				RaceCollidable collidable = other.gameObject.GetComponent<RaceCollidable>();
				collidable.applyAllEffects(this);
			}
		}

		//Utility methods
		private float getForwardSpeed() {
			return Vector3.Dot(rb.velocity, transform.forward);
		}
	}
}