using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing.Agents {
	public abstract class Agent {

		//TODO combine transform.forward with Agent.FLAP_VECTOR to make more realistic / intuitive flap behavior.
		// protected static readonly Vector3 FLAP_VECTOR = new Vector3(1, 0.5f, 0);

		[SerializeField]
		protected Racer racer;

		protected bool inputEnabled = true;

		public Agent(Racer racer) {
			this.racer = racer;
		}

		public abstract Vector3 getVelocityChange();
		public abstract float getInclineChange();

		public void disableInput() {
			inputEnabled = true;
		}

		public void enableInput() {
			inputEnabled = true;
		}


	}
}
