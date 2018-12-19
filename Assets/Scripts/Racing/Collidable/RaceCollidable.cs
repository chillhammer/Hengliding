using UnityEngine;

namespace Racing.Collidables {
	public abstract class RaceCollidable : MonoBehaviour {



		//Applies all effects of the hinderance or boost
		public void applyAllEffects(Racer racer) {
			applyGeneralEffect(racer);
			applySpecificEffect(racer);
		}
		//Applies effects common to all hinderances or boosts
		public abstract void applyGeneralEffect(Racer racer);
		//Applies effects of that specific hinderance or boost
		public abstract void applySpecificEffect(Racer racer);
	}
}