using System.Collections;
using UnityEngine;
using Utilities;

namespace Raising {

	public class Stat {

		private static readonly float STAT_INCREASE_ICON_TIME = 1.5f;

		public Hen hen;
		public float value;
		public GameObject increaseIcon;

		public Stat(Hen hen, float value, GameObject increaseIcon) {
			this.hen = hen;
			this.value = value;
			this.increaseIcon = increaseIcon;
		}

		//Call this when you don't want a little icon to float up
		public void increaseNoUI(float amount) {
			value += amount;
		}

		//You'll have to call StartCoroutine on this guy
		public IEnumerator increase(float amount) {
			increaseNoUI(amount);

			//Animate the icon. Maybe switch this over to an actual animation clip?? idk?
			Vector3 startPos = hen.transform.position + new Vector3(0, 0.2f, 0);
			Vector3 endPos = startPos + new Vector3(0, 0.4f, 0);

			GameObject icon = Object.Instantiate(
				increaseIcon,
				startPos,
				increaseIcon.transform.rotation,
				hen.transform
			);

			//TODO this doesn't work....
			yield return Util.Lerp(Stat.STAT_INCREASE_ICON_TIME, progress => {
				Debug.Log(progress);
				icon.transform.position = Vector3.Lerp(startPos, endPos, progress);
			});

			Object.Destroy(icon);
		}
	}
}
