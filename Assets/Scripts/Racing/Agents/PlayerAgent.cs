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

			if (Input.GetAxis("SpaceBar") > 0) {
				//Currently, velocity limits only consider forward motion.
				if (racer.rb.velocity.x < racer.chickenStack.getMaxSpeed()) {
					Debug.Log("Asdf");
					velocity += Agent.FLAP_VECTOR * racer.chickenStack.getAcceleration() * Time.deltaTime;
				}
			}

			return velocity;
		}
	}
}
