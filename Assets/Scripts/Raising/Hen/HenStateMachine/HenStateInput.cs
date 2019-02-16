using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Raising {

	public class HenStateInput {

		public Hen hen;

		public HenStateInput(Hen hen) {
			this.hen = hen;
		}


		public bool foodNearby() {
			return (hen.findNearbyFood() != null);
		}


	}
}
