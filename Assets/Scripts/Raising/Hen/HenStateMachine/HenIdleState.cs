using UnityEngine;

namespace Raising {

	public class HenIdleState : HenState {

		public HenIdleState(HenStateInput input) : base(input) { }

		override public void runOnce() {}

		override public void run() {
			Rigidbody rb = input.hen.GetComponent<Rigidbody>();
			rb.velocity -= (rb.velocity * 0.02f * Time.deltaTime);
			rb.angularVelocity -= (rb.angularVelocity * 0.05f * Time.deltaTime);
		}

		override public void updateState() {
			if (input.foodNearby()) {
				input.hen.state = new HenSeekFoodState(input);
			} else if (timeSinceStart() > 2) {
				input.hen.state = new HenWanderState(input);
			}
		}
	}
}
