namespace Raising {

	public class HenIdleState : HenState {

		public HenIdleState(HenStateInput input) : base(input) { }

		override public void updateState() {

			if (input.foodNearby()) {
				input.hen.state = new HenSeekFoodState(input);
			} else {
				input.hen.state = new HenWanderState(input);
			}

			
		}
	}
}
