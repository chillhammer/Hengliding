using System.Collections;
using UnityEngine;

namespace Raising.Interaction {

	public class Food : MonoBehaviour {

		private static readonly float FLOAT_HEIGHT = 0.3f;

		// private Vector3 screenPoint;
		// private Vector3 offset;

		void OnMouseDown() {
			// screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			// offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			StartCoroutine(trackPointer());
		}

		private IEnumerator trackPointer() {
			while (Input.GetButton("LMB")) {
				RaycastHit mouseHit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out mouseHit, 1000.0f)) {
					// RaycastHit floatHit;
					// if (Physics.Raycast(mouseHit.point, transform.TransformDirection(Vector3.down), out floatHit)) {
					// }
					transform.position = new Vector3(mouseHit.point.x, mouseHit.point.y + Food.FLOAT_HEIGHT, mouseHit.point.z);
				}
				yield return null;
			}
			transform.position -= new Vector3(0, Food.FLOAT_HEIGHT, 0);
		}
	}
}