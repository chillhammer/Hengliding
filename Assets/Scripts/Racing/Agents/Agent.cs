using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing.Agents {
	public abstract class Agent : MonoBehaviour {
		
		private Racer racer;

		public Agent(Racer racer) {
			this.racer = racer;
		}

	}
}
