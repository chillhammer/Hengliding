using Raising.Interaction;
using UnityEngine;

namespace Raising {

	public class HenBatheState : HenState {

		Vector3 beforeBathingPos;
		Bath bath;

		public HenBatheState(HenStateInput input) : base(input) { }

		override public void runOnce() {
			bath = input.hen.findNearbyItem<Bath>();
			// if (target != null) {
			// 	//this scripted movmement can hopefully be replaced by real animations later
			// 	float bathSeekAcceleration = 0.02f; //TODO make this change depending on speed stat?

			// 	input.hen.transform.LookAt(target.transform);
			// 	//TODO keep rotation parallel to ground
			// 	float yRot = input.hen.transform.rotation.eulerAngles.y;
			// 	input.hen.transform.rotation = Quaternion.Euler(0, yRot, 0);

			// 	input.hen.GetComponent<Rigidbody>().velocity += input.hen.transform.forward * bathSeekAcceleration;
			// }

			beforeBathingPos = input.hen.transform.position;
			input.hen.transform.position = bath.gameObject.transform.position + new Vector3(0, 0.5f, 0);
			input.hen.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			input.hen.GetComponent<Animator>().SetTrigger("inWater");
			Object.Instantiate(input.hen.bathingParticles, input.hen.transform.position, input.hen.bathingParticles.transform.rotation, bath.gameObject.transform);
		}

		override public void run() {
			if (timeSinceStart() > 3) {
				input.hen.finishBath(bath);
				input.hen.transform.position = beforeBathingPos;
			}
		}

		override public void updateState() {
			if (timeSinceStart() > 3) {
				input.hen.state = new HenIdleState(input);
			}
		}

	}
}
