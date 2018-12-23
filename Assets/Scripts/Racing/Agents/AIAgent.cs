using UnityEngine;

namespace Racing.Agents {
	public class AIAgent : Agent {

		public AIAgent(Racer racer) : base(racer) { }

		//TODO
		public override Vector3 getVelocityChange() {
			return new Vector3(0, 0, 0);
		}

	}
}