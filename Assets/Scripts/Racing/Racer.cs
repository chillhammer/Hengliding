using System.Collections;
using System.Collections.Generic;
using Racing.Agents;
using Racing.Collidables;
using UnityEngine;

namespace Racing {
	public class Racer : MonoBehaviour {

		private const float DEFAULT_TIME_TO_ADJUST_SPEED = 0.5f;

		//Shorthand
		public Rigidbody rb;
		public RacerChickenStack chickenStack;
		public float incline = 0;

		[SerializeField]
		private Agent agent;

		void Start() {
			rb = GetComponent<Rigidbody>();

			//TODO Just for testing. Assign this in the factory.
			agent = new PlayerAgent(this);
			chickenStack = new RacerChickenStack(new List<RacerChicken>(){
				new RacerChicken(50, 60, 0.004f, 2f, 2f, 50)
			});
		}

		void Update() {
			float inclineChange = agent.getInclineChange();
			this.transform.Rotate(Vector3.right * inclineChange);
			incline += inclineChange;

			applyFlightForces();

			rb.velocity += agent.getVelocityChange();
		}

		protected void applyFlightForces() {
			rb.velocity *= (1 - chickenStack.getDrag(rb.velocity.magnitude));
			//Someone better than me at 3d math could re-do this with dot products...
			rb.velocity = new Vector3(rb.velocity.x, chickenStack.getNetGravity(), rb.velocity.z);
			// Debug.Log("speed: " + rb.velocity.magnitude);
			if (rb.velocity.magnitude < chickenStack.getMaxSpeed()) {
				rb.velocity += new Vector3(chickenStack.getFlapStrength() / 150, 0, 0);
			}

			// Calculate velocity change from angle. This involves a fair amount of fudging
			float xBoost = Mathf.Sin((Mathf.PI / 180f) * incline * 2);
			float yBoost = -Mathf.Cos((Mathf.PI / 180f) * (incline * 2 + 180));
			rb.velocity += new Vector3(xBoost, yBoost, 0);
			Debug.Log("X: " + xBoost +",   Y: " + yBoost);

		}

		//Used for collision with obstacles or other adjustments we do external to the gliding "physics"
		public IEnumerator adjustSpeed(float velocityScaler, float timeToAdjustSpeed = Racer.DEFAULT_TIME_TO_ADJUST_SPEED) {
			float progress = 0;
			Vector3 originalVelocity = rb.velocity;
			Vector3 targetVelocity = originalVelocity * velocityScaler;

			agent.disableInput();

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