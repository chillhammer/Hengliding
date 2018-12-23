using UnityEngine;

namespace Racing {
	public class RacerChicken {
		public float maxSpeed;
		public float acceleration;
		public float drag;
		public float weight;

		public RacerChicken(float maxSpeed, float acceleration, float drag, float weight) {
			this.maxSpeed = maxSpeed;
			this.acceleration = acceleration;
			this.drag = drag;
			this.weight = weight;
		}
	}
}