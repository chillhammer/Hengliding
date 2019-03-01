using Raising.Interaction;
using UnityEngine;

namespace Raising {

	public class HenSeekFoodState : HenState {

		public HenSeekFoodState(HenStateInput input) : base(input) { }

		override public void runOnce() {
			Food target = input.hen.findNearbyItem<Food>();
			if (target != null) {
				//this scripted movmement can hopefully be replaced by real animations later
				float foodSeekAcceleration = 0.02f; //TODO make this change depending on speed stat?

				input.hen.transform.LookAt(target.transform);
				//TODO keep rotation parallel to ground
				float yRot = input.hen.transform.rotation.eulerAngles.y;
				input.hen.transform.rotation = Quaternion.Euler(0, yRot, 0);

				input.hen.GetComponent<Rigidbody>().velocity += input.hen.transform.forward * foodSeekAcceleration;
			}
		}

		override public void run() {
		}

		override public void updateState() {
			if (input.foodNearby()) {
				input.hen.state = new HenSeekFoodState(input);
			} else {
				input.hen.state = new HenIdleState(input);
			}
		}

	}
}
