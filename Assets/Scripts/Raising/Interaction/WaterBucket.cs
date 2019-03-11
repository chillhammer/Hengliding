using UnityEngine;

namespace Raising.Interaction {

	public class WaterBucket : Draggable {

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

		void Update() {
			GameObject under = getObjectUnder();
			if (under != null) {
				if (under.GetComponent<Bath>() != null && under.GetComponent<Bath>().filled == false) {
					if (this.filled) {
						Debug.Log("emptying bucket");
						Bath bath = under.GetComponent<Bath>();
						bath.fill();
						this.unFill();
					}
				}
				if (under.GetComponent<WaterSource>() != null) {
					if (!this.filled) {
						Debug.Log("filling bucket");
						WaterSource waterSource = under.GetComponent<WaterSource>();
						this.fill();
					}
				}
			}
		}
	}
}