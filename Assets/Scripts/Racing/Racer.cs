using Racing.Agents;
using Racing.Collidables;
using UnityEngine;

namespace Racing {
	public class Racer : MonoBehaviour {
		private Agent agent;

		public Racer() {
			agent = new PlayerAgent(this);
		}

		void OnCollisionEnter(Collision other) {

			if (other.gameObject.GetComponent<RaceCollidable>() != null) {
				RaceCollidable collidable = other.gameObject.GetComponent<RaceCollidable>();
				collidable.applyAllEffects(this);
			}
		}
	}
}