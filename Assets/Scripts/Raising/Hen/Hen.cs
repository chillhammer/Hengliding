using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Raising.Interaction;
using UnityEngine;
using Utilities;

namespace Raising {

	public class Hen : MonoBehaviour {

		private static readonly float AWARENESS_RADIUS = 1.5f; //hens will be able to detect dropped food items within this radius

		public HenState state;
		public HenStateInput stateInput;

		public string henName;

		public HenBreed breed;
		public Stat love;
		public Stat size;
		public Stat fitness;
		public Stat featherQuality;


		void Start() {
			stateInput = new HenStateInput(this);
			state = new HenIdleState(stateInput);

			breed = HenBreed.RedStar;


			//reinitialize these only if they were not already assigned by the spawner
			if (henName == null) {
				henName = "Hen McHenface";
			}
			if (love == null) {
				love = new Stat(this, 0, Resources.Load<GameObject>("Prefabs/SpeedIncrease"));
			}
			if (size == null) {
				size = new Stat(this, 0, Resources.Load<GameObject>("Prefabs/SpeedIncrease"));
			}
			if (fitness == null) {
				fitness = new Stat(this, 0, Resources.Load<GameObject>("Prefabs/SpeedIncrease"));
			}
			if (featherQuality == null) {
				featherQuality = new Stat(this, 0, Resources.Load<GameObject>("Prefabs/SpeedIncrease"));
			}			
		}

		void Update() {
			state.run();
			state.updateState();

			//prevent flying away
			// Rigidbody rb = GetComponent<Rigidbody>();
			// rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
		}


		public void wanderSync() {
			StartCoroutine(wander());
		}
		private IEnumerator wander() {
			//TODO change direction once we fix the fact that hen models are currently sideways relative to their gameobject forward vector
			yield return new WaitForSeconds(Random.Range(0.5f, 2f));
			transform.Rotate(new Vector3(0, Random.Range(0, 359), 0), Space.World);
			yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
			GetComponent<Rigidbody>().velocity += transform.right * 0.5f;
			yield return new WaitForSeconds(Random.Range(1f, 2f));
			GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 0);
			yield return new WaitForSeconds(Random.Range(3f, 6f));
		}

		void OnCollisionEnter(Collision col) {
			GameObject go = col.gameObject;
			if (go.GetComponent<Interaction.Food>() != null) {
				consumeFood(go.GetComponent<Interaction.Food>());
			}
		}

		void consumeFood(Interaction.Food foodItem) {
			//TODO support for different levels of food quality

			//TODO GUI for stats

			//TODO increase love stat

			StartCoroutine(size.increase(1));
			Debug.Log(size.value);
			Destroy(foodItem.gameObject);
		}

		//returns the closest food item within the Hen's awareness sphere, or null if there is no such food item
		public Food findNearbyFood() {
			Collider[] detectionColliders = Physics.OverlapSphere(this.transform.position, Hen.AWARENESS_RADIUS);

			List<Food> nearbyFoodList = new List<Food>();
			foreach (Collider c in detectionColliders) {
				Food f = c.gameObject.GetComponent<Food>();
				if (f != null) {
					nearbyFoodList.Add(f);
				}
			}

			if (nearbyFoodList.Count == 0) {
				return null;
			}

			float nearestDist = nearbyFoodList.Min(food => Vector3.Distance(this.transform.position, food.transform.position));
			return nearbyFoodList.First(food => nearestDist == Vector3.Distance(this.transform.position, food.transform.position));

		}
	}

}