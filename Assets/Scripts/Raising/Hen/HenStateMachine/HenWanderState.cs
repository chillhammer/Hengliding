namespace Raising {

	public class HenWanderState : HenState {

		public HenWanderState(HenStateInput input) : base(input) { }

		override public void updateState() {
			if (input.foodNearby()) {
				input.hen.state = new HenSeekFoodState(input);
			} else {
				input.hen.state = new HenWanderState(input);
			}
		}	

	}
}
