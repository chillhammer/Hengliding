using UnityEngine;

namespace Raising {

	public abstract class HenState {

		public HenStateInput input;
		public float startTime;

		public HenState(HenStateInput input) {
			this.input = input;
			startTime = Time.time;
			runOnce();
		}

		public abstract void runOnce();
		public abstract void run();
		public abstract void updateState();

		public float timeSinceStart() {
			return Time.time - startTime;
		}

	}

}