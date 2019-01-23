using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raising {

	public class Hen : MonoBehaviour {

		public HenState state;
		public HenStateInput stateInput;

		public HenBreed breed;
		public Stat speed;

		void Start() {
			stateInput = new HenStateInput(this);
			state = new HenIdleState(stateInput);

			breed = HenBreed.RedStar;
			speed = new Stat(this, 0, Resources.Load<GameObject>("Prefabs/SpeedIncrease"));
		}

		void Update() {

			if (Input.GetButtonDown("SpaceBar")) {
				StartCoroutine(speed.increase(1));
			}

			state.updateState();
		}

		void OnMouseDown() {
			// GUI for stats
		}
	}

}