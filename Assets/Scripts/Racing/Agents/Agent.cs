using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing.Agents {
	public abstract class Agent {

		protected static readonly Vector3 FLAP_VECTOR = new Vector3(1, 1f, 0);

		[SerializeField]
		protected Racer racer;

		protected bool inputEnabled = true;

		public Agent(Racer racer) {
			this.racer = racer;
		}

		public abstract Vector3 getVelocityChange();

		public void disableInput() {
			inputEnabled = false;
		}

		public void enableInput() {
			inputEnabled = true;
		}


	}
}
