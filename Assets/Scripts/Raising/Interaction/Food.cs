using UnityEngine;

namespace Raising.Interaction {

	public class Food : MonoBehaviour {

		private static readonly float FLOAT_HEIGHT = 0.3f;

		private Vector3 screenPoint;
		private Vector3 offset;

		void OnMouseDown() {
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}

		void OnMouseDrag() {
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPos = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

			//Float above ground
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit)) {
				curPos = new Vector3(curPos.x, hit.point.y + Food.FLOAT_HEIGHT,  curPos.z);
			}

			transform.position = curPos;
		}

		void OnMouseUp() {
			transform.position = transform.position + new Vector3(0, -Food.FLOAT_HEIGHT, 0);
		}

	}
}