using System.Collections;
using System.Collections.Generic;
using Racing.Agents;
using Racing.Collidables;
using UnityEngine;

namespace Racing {
	public class Racer : MonoBehaviour {

		private static readonly float TIME_TO_ADJUST_SPEED = 0.5f;

		[SerializeField]
		private Agent agent;
		public RacerChickenStack chickenStack;

		//Shorthand
		public Rigidbody rb;

		void Start() {
			rb = GetComponent<Rigidbody>();

			//TODO Just for testing. Assign this in the factory.
			agent = new PlayerAgent(this);
			chickenStack = new RacerChickenStack(new List<RacerChicken>(){
				new RacerChicken(5, 10, 0.01f, 1)
			});
		}

		void Update() {
			rb.velocity += agent.getVelocityChange();
			applyFlightForces();
			Debug.Log(rb.velocity.magnitude);
		}


		protected void applyFlightForces() {
			rb.velocity *= (1 - chickenStack.getDrag());
			rb.AddForce(chickenStack.getGravity());
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
	}
}