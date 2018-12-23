using UnityEngine;

namespace Racing.Agents {
	public class CheaterAgent : Agent {

		public CheaterAgent(Racer racer) : base(racer) { }

		//It's just, like, real fast
		public override Vector3 getVelocityChange() {
			return new Vector3(10, 0, 0);
		}

	}
}