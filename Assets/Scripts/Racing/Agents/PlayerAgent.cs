using UnityEngine;
using Racing.Collidables;

namespace Racing.Agents {
	public class PlayerAgent : Agent {

		public PlayerAgent(Racer racer) : base(racer) { }

		public override Vector3 getVelocityChange() {
			Vector3 velocity = new Vector3(0, 0, 0);
			if (!inputEnabled) {
				return velocity;
			}

			if (Input.GetButton("SpaceBar")) {
				//Currently, velocity limits only consider forward motion.
				if (racer.rb.velocity.x < racer.chickenStack.getMaxSpeed()) {
					velocity += racer.transform.forward * racer.chickenStack.getFlapStrength() * Time.deltaTime;
				}
			}

			return velocity;
		}


		public override float getInclineChange() {
			float inclineChange = 0;

			if(!inputEnabled) {
				return 0;
			}

			if (Input.GetAxis("Vertical") != 0) {
				//clamp angle to [0, 90]
				inclineChange = Input.GetAxis("Vertical") * racer.chickenStack.getAngularAcceleration() * Time.deltaTime;
				if( (racer.incline + inclineChange < 0) || (racer.incline + inclineChange > 90) ) {
					inclineChange = 0;
				}
			}

			return inclineChange;
		}
	}
}
