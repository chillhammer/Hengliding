using UnityEngine;

namespace Racing.Agents {
	public class CheaterAgent : Agent {

		public CheaterAgent(Racer racer) : base(racer) { }

		//It just, like, goes really fast forwards 
		public override Vector3 getVelocityChange() {
			return racer.transform.forward * 10;
		}

		public override float getInclineChange() {
			return 0;
		}


	}
}