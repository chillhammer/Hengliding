using UnityEngine;
using Racing.Collidables;

namespace Racing.Agents {
	public class PlayerAgent : Agent {

		//Limits in degrees
		private static readonly int MIN_PITCH = 0;
		private static readonly int MAX_PITCH = 90;
		private static readonly int MIN_YAW = -60;
		private static readonly int MAX_YAW = 60;

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

			if (!inputEnabled) {
				return 0;
			}

			if (Input.GetAxis("Vertical") != 0) {
				//clamp angle to [0, 90]
				inclineChange = -1 * Input.GetAxis("Vertical") * racer.chickenStack.getAngularAcceleration() * Time.deltaTime;
				if ((racer.pitch + inclineChange < PlayerAgent.MIN_PITCH) || (racer.pitch + inclineChange > PlayerAgent.MAX_PITCH)) {
					inclineChange = 0;
				}
			}

			return inclineChange;
		}

		public override float getYawChange() {
			float yawChange = 0;

			if (!inputEnabled) {
				return 0;
			}

			//clamp angle to [45,45]
			if (Input.GetAxis("Horizontal") != 0) {
				yawChange = Input.GetAxis("Horizontal") * racer.chickenStack.getAngularAcceleration() * Time.deltaTime;
				if ((racer.yaw + yawChange < PlayerAgent.MIN_YAW) || (racer.yaw + yawChange > PlayerAgent.MAX_YAW)) {
					yawChange = 0;
				}

			}

			return yawChange;
		}
	}
}
