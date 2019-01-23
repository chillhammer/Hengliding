using UnityEngine;

namespace Racing {

	public class RacerChicken {
		
		//At what velocity can you no longer flap?
		public float maxSpeed;
		//How much speed do you get per flap?
		public float flapStrength;
		public float drag;
		public float weight;
		//Proportional to lift
		public float wingArea;
		public float turnRate;

		public RacerChicken(float maxSpeed, float flapStrength, float drag, float weight, float wingArea, float turnRate) {
			this.maxSpeed = maxSpeed;
			this.flapStrength = flapStrength;
			this.drag = drag;
			this.weight = weight;
			this.wingArea = wingArea;
			this.turnRate = turnRate;
		}
	}
}