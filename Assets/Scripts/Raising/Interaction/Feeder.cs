using UnityEngine;

namespace Raising.Interaction {

	public class Feeder : MonoBehaviour {

		public Animator feederAnimator;

		void OnMouseEnter() {
			feederAnimator.SetBool("isOpen", true);
		}

		void OnMouseExit() {
			feederAnimator.SetBool("isOpen", false);
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