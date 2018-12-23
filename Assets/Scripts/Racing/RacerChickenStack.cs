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

		public float getAcceleration() {
			return chickenStack.Average(chicken => chicken.acceleration);
		}

		public float getDrag() {
			return chickenStack.Average(chicken => chicken.drag);
		}

		public Vector3 getGravity() {
			return RacerChickenStack.GRAVITY * chickenStack.Sum(chicken => chicken.weight);
		}
	}
}