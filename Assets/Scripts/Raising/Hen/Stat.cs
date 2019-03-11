using System.Collections;
using UnityEngine;
using Utilities;

namespace Raising {

	public class Stat {

		private static readonly float STAT_INCREASE_ICON_TIME = 1.0f;

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

			Quaternion startRot = increaseIcon.transform.rotation * Quaternion.Euler(0, 0, Random.Range(0, 360));
			Quaternion endRot = startRot * Quaternion.Euler(0, 0, Random.Range(0, 360));

			GameObject icon = Object.Instantiate(
				increaseIcon,
				startPos,
				increaseIcon.transform.rotation
			);

			yield return Util.Lerp(Stat.STAT_INCREASE_ICON_TIME, progress => {
				icon.transform.position = Vector3.Lerp(startPos, endPos, Mathf.Pow(progress, 0.33f));
				icon.transform.rotation = Quaternion.Lerp(startRot, endRot, Mathf.Pow(progress, 0.33f));
			});

			yield return new WaitForSeconds(0.5f);

			Object.Destroy(icon);
		}
	}
}
