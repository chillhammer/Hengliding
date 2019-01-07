using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Racing {
	public class RacerChickenStack {

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
			return chickenStack.Sum(chicken => chicken.drag) * forwardsSpeed * forwardsSpeed;
		}

		public float getAngularAcceleration() {
			return chickenStack.Average(chicken => chicken.turnRate);
		}

		//Net gravity is our faked gravity after accounting for lift and angle. 
		//The logic here is that lift reduces gravity by less as you enter a dive.
		public float getNetGravity(float pitch) {
			float angleLiftScalar = Mathf.Cos((Mathf.PI / 180f) * (pitch));
			return -1 * (getWeight() - (getLift() * angleLiftScalar));
		}

		private float getWeight() {
			return chickenStack.Sum(chicken => chicken.weight);
		}

		private float getLift() {
			return chickenStack.Sum(chicken => chicken.wingArea);
		}
	}
}