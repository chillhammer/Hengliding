using UnityEngine;

namespace Raising.Interaction {

	public class Feeder : MonoBehaviour {

		public Animator feederAnimator;

		void OnMouseEnter() {
			feederAnimator.SetTrigger("Open");
		}

		void OnMouseExit() {
			feederAnimator.SetTrigger("Close");

		}

		void OnMouseDown() {
		}

		void OnMouseDrag() {
			//move food to mouse 
		}

		void OnMouseUp() {
			//drop the food
		}
	}
}