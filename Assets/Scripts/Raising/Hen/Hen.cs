using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

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

			StartCoroutine(wander());
		}

		void Update() {
			state.updateState();
		}

		private IEnumerator wander() {
			while (true) {
				yield return new WaitForSeconds(Random.Range(0.5f, 2f));
				transform.Rotate(new Vector3(0, Random.Range(0, 359), 0), Space.World);
				yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
				GetComponent<Rigidbody>().velocity += transform.right * 0.5f;
				yield return new WaitForSeconds(Random.Range(1f, 2f));
				GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 0);
				yield return new WaitForSeconds(Random.Range(3f, 6f));
			}
		}

		void OnMouseDown() {
			//TODO GUI for stats
			Debug.Log("Speed: " + speed.value);
			StartCoroutine(speed.increase(1));

		}
	}

}