using UnityEngine;

namespace Raising.Interaction {

	public class Feeder : MonoBehaviour {

		public Animator feederAnimator;
		public GameObject foodPrefab;

		void OnMouseEnter() {
			feederAnimator.SetBool("isOpen", true);
		}

		void OnMouseExit() {
			feederAnimator.SetBool("isOpen", false);
		}

		void OnMouseDown() {
			Instantiate(foodPrefab, gameObject.transform.position + new Vector3(0, 1, 0), foodPrefab.transform.rotation);
		}

		void OnMouseDrag() {
			//move food to mouse 
		}

		void OnMouseUp() {
			//drop the food
		}
	}
}