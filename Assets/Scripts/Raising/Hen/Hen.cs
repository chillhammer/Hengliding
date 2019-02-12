using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Raising {

	public class Hen : MonoBehaviour {

		public HenState state;
		public HenStateInput stateInput;

		private readonly float awarenessRadius = 1.5f; //hens will be able to detect dropped food items within this radius

		public HenBreed breed;
		public Stat speed;

		void Start() {
			stateInput = new HenStateInput(this);
			state = new HenIdleState(stateInput);

			breed = HenBreed.RedStar;
			speed = new Stat(this, 0, Resources.Load<GameObject>("Prefabs/SpeedIncrease"));

			StartCoroutine(behaviorLoop());

		}

		void Update() {
			state.updateState();

		}

		private IEnumerator behaviorLoop() {
			while (true) {
				if (state is HenIdleState) {
					//slow down when idle
					Rigidbody rb = GetComponent<Rigidbody>();
					rb.velocity = rb.velocity * 0.75f;
					rb.angularVelocity = rb.angularVelocity * 0.5f;
					yield return new WaitForSeconds(0.5f);
				}
				else if (state is HenWanderState) {
					//TODO change direction once we fix the fact that hen models are currently sideways relative to their gameobject forward vector
					yield return new WaitForSeconds(Random.Range(0.5f, 2f));
					transform.Rotate(new Vector3(0, Random.Range(0, 359), 0), Space.World);
					yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
					GetComponent<Rigidbody>().velocity += transform.right * 0.5f;
					yield return new WaitForSeconds(Random.Range(1f, 2f));
					GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 0);
					yield return new WaitForSeconds(Random.Range(3f, 6f));
				} else if (state is HenSeekFoodState) {
					
					//locate nearest food item
					Interaction.Food target = findNearbyFood();
					//this shouldnt be null when we're in HenSeekFoodState but let's check anyway
					if (target != null) {
						//this scripted movmement can hopefully be replaced by real animations later
					
						float foodSeekAcceleration = 0.05f; //TODO make this change depending on speed stat?

						transform.LookAt(target.transform);
						//TODO keep rotation parallel to ground
						float yRot = transform.rotation.eulerAngles.y;
						transform.rotation = Quaternion.Euler(0,yRot,0);


						GetComponent<Rigidbody>().velocity += transform.forward * foodSeekAcceleration;
						yield return new WaitForSeconds(0.1f);


					}
					//food is missing, wait
					yield return new WaitForSeconds(0.5f);
				
				} else {
					//gotta have this or me might fall into an infinite loop an not be able to advance the frame
					yield return null;
				}
			}
		}

		void OnMouseDown() {
			//TODO GUI for stats
			Debug.Log("Speed: " + speed.value);
			StartCoroutine(speed.increase(1));

		}

		void OnCollisionEnter (Collision col)
    	{
			GameObject go = col.gameObject;
        	if(go.GetComponent<Interaction.Food>() != null)
        	{
				Debug.Log("Hit food");
         		consumeFood(go.GetComponent<Interaction.Food>());
        	}
    	}

		void consumeFood(Interaction.Food foodItem) {
			//TODO support for different levels of food quality

			//TODO GUI for stats

			//TODO increase love stat


			Destroy(foodItem.gameObject);
		}

		//returns the closest food item within the Hen's awareness sphere, or null if there is no such food item
		public Interaction.Food findNearbyFood() {
			Collider[] detectionColliders = Physics.OverlapSphere(this.transform.position, awarenessRadius);

			List<Interaction.Food> nearbyFoodList = new List<Interaction.Food>();
			foreach (Collider c in detectionColliders) {
				Interaction.Food f = c.gameObject.GetComponent<Interaction.Food>();
				if (f != null) {
					nearbyFoodList.Add(f);
				}
			}
			if (nearbyFoodList.Count == 0) {
				return null;
			} else {
				Interaction.Food nearest = nearbyFoodList[0];
				float nearDistance = Vector3.Distance(this.transform.position, nearest.transform.position);
				foreach (Interaction.Food f in nearbyFoodList) {
					float dist =  Vector3.Distance(this.transform.position, f.transform.position);
					if (dist < nearDistance) {
						nearest = f;
						nearDistance = dist;
					}
				}
				return nearest;
			}
			
			
		}
	}

}