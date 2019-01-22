using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing {

	public class RaceControl : MonoBehaviour {

		public Canvas uiCanvas;

		public GameObject leaderboard;

		public RaceStateInput raceStateInput;
		public RaceState currentState = null;
		
		void Start() {
			raceStateInput = new RaceStateInput(this);
			currentState = new RaceBeginState(raceStateInput);
		}

		void Update() {
			currentState.updateState();
		}

		public void chickenAtEnd() {
			raceStateInput.isPlayerAtEnd = true;
		}

		public void displayLeaderboard() {
			Instantiate(leaderboard, uiCanvas.transform);
		}
	}
}