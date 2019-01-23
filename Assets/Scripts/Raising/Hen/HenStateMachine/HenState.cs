namespace Raising {

	public abstract class HenState {

		public HenStateInput input;

		public HenState(HenStateInput input) {
			this.input = input;
		}

		public abstract void updateState();
	}

}