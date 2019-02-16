namespace Raising {

	public class HenWanderState : HenState {

		public HenWanderState(HenStateInput input) : base(input) { }

		override public void runOnce() {
			input.hen.wanderSync();
		}

		override public void run() {
		}


		override public void updateState() {
			if (input.foodNearby()) {
				input.hen.state = new HenSeekFoodState(input);
			} else if (timeSinceStart() > 2) {
				input.hen.state = new HenIdleState(input);
			}
		}

	}
}
