using UnityEngine;

namespace Raising.Interaction {

	public class Bath : MonoBehaviour {

		public bool filled = false;
		public GameObject water;

		public void fill() {
			filled = true;
			water.SetActive(true);
		}

		public void unFill() {
			filled = false;
			water.SetActive(false);
		}
	}
}