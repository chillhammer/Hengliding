using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing {

	public abstract class RaceState {

		public RaceStateInput raceInput;

		public RaceState(RaceStateInput raceInput) {
			this.raceInput = raceInput;
		}

		public abstract void updateState();
	}

}