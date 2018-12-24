using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Racing {
	public class RacerChickenStack {

		private static readonly Vector3 GRAVITY = new Vector3(0, -2f, 0);

		public List<RacerChicken> chickenStack;

		public RacerChickenStack(List<RacerChicken> chickens) {
			this.chickenStack = chickens;
		}

		public float getMaxSpeed() {
			return chickenStack.Average(chicken => chicken.maxSpeed);
		}

		public float getFlapStrength() {
			return chickenStack.Average(chicken => chicken.flapStrength);
		}

		public float getDrag(float forwardsSpeed) {
			//Lift = constants * velocity^2 * sa as viewed from the front (which == racerchicken.drag, so we sum)
			return chickenStack.Sum(chicken => chicken.drag) * forwardsSpeed * forwardsSpeed;
		}

		public Vector3 getGravity() {
			return RacerChickenStack.GRAVITY * chickenStack.Sum(chicken => chicken.weight);
		}

		public float getAngularAcceleration() {
			return chickenStack.Average(chicken => chicken.turnRate);
		}

		public float getLift(float incline, float forwardsSpeed) {
			//Lift = constants * velocity^2 * wing surface area
			return chickenStack.Sum(chicken => chicken.wingArea) * forwardsSpeed * forwardsSpeed;
		}
	}
}