using System.Collections;
using UnityEngine;

namespace Raising.Interaction {

	public class Draggable : MonoBehaviour {

		protected virtual float getFloatHeight() {
			return 0.3f;
		}

		void OnMouseDown() {
			StartCoroutine(trackPointer());
		}

		protected IEnumerator trackPointer() {
			while (Input.GetButton("LMB")) {
				RaycastHit mouseHit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out mouseHit, 1000.0f)) {
					transform.position = new Vector3(mouseHit.point.x, mouseHit.point.y + this.getFloatHeight(), mouseHit.point.z);
				}
				yield return null;
			}
			transform.position -= new Vector3(0, this.getFloatHeight(), 0);
		}

		protected GameObject getObjectUnder() {
			RaycastHit under;
			if (Physics.Raycast(gameObject.transform.position, Vector3.down, out under)) {
				return under.collider.gameObject;
			}
			return null;
		}

	}

}