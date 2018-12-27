using System.Collections;
using System.Collections.Generic;
using Racing.Agents;
using Racing.Collidables;
using UnityEngine;

namespace Racing {
	public class Racer : MonoBehaviour {

		private static readonly float TIME_TO_ADJUST_SPEED = 0.5f;

		//Shorthand
		public Rigidbody rb;
		public RacerChickenStack chickenStack;

		[SerializeField]
		private Agent agent;
		private float incline = 0;

		void Start() {
			rb = GetComponent<Rigidbody>();

			//TODO Just for testing. Assign this in the factory.
			agent = new PlayerAgent(this);
			chickenStack = new RacerChickenStack(new List<RacerChicken>(){
				new RacerChicken(5, 60, 0.0001f, 1f, 0.1f, 50)
			});
		}

		void Update() {
			rb.velocity += agent.getVelocityChange();

			float inclineChange = agent.getInclineChange();
			this.transform.Rotate(Vector3.right * inclineChange);
			incline += inclineChange;

			applyFlightForces();
		}


		protected void applyFlightForces() {
			rb.velocity *= (1 - chickenStack.getDrag(getForwardSpeed()));
			rb.AddForce(chickenStack.getGravity());

			//Speed for the purposes of calculating lift is the forward velocity
			//float lift = chickenStack.getLift(incline, getForwardSpeed());
			// Vector3 unitLift = Quaternion.AngleAxis(incline, transform.right) * transform.up ;
			//Vector3 unitLift = transform.up;
			//rb.AddForce(lift * unitLift);

			Debug.DrawLine(transform.position, transform.position + (transform.forward * getForwardSpeed()), Color.blue, 0, false);
			//Debug.DrawLine(transform.position, transform.position + unitLift * lift, Color.green, 0, false);
			Debug.DrawLine(transform.position, transform.position + chickenStack.getGravity(), Color.red, 0, false);


            Debug.DrawLine(transform.position, transform.position + rb.velocity, Color.white, 0, false);
			Debug.Log("Airspeed: " + getForwardSpeed());
		}

		//Used for collision with obstacles or other "fake physics" adjustments we do.
		public IEnumerator adjustSpeed(float boostStrength) {
			float progress = 0;
			Vector3 originalVelocity = rb.velocity;
			Vector3 targetVelocity = originalVelocity * boostStrength;

			agent.disableInput();

			while (progress > Racer.TIME_TO_ADJUST_SPEED) {
				float smoothProgress = Mathf.SmoothStep(0.0f, 1.0f, progress / TIME_TO_ADJUST_SPEED);
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