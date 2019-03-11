using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Raising.Interaction;

namespace Raising {

	public class HenStateInput {

		public Hen hen;

		public HenStateInput(Hen hen) {
			this.hen = hen;
		}


		public bool foodNearby() {
			return (hen.findNearbyItem<Food>() != null);
		}

		public bool bathNearby() {
			Bath nearbyBath = hen.findNearbyItem<Bath>();

			if (nearbyBath != null && nearbyBath.filled) {
				return true;
			}

			return false;
		}


	}
}
