namespace Raising {

	public class HenIdleState : HenState {

		public HenIdleState(HenStateInput input) : base(input) { }

		override public void updateState() {
			input.hen.state = new HenIdleState(input);
		}	

	}
}
