namespace Raising {

	public class HenSeekFoodState : HenState {

		public HenSeekFoodState(HenStateInput input) : base(input) { }

		override public void updateState() {
			if (input.foodNearby()) {
				input.hen.state = new HenSeekFoodState(input);
			} else {
				input.hen.state = new HenIdleState(input);
			}
		}	

	}
}
