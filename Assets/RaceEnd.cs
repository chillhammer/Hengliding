using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Racing.Agents;

namespace Racing
{
    public class RaceEnd : MonoBehaviour
    {
        public RaceControl raceControl; 
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("colliding");
            if (other.gameObject.GetComponent<Racer>() != null)
            {
                if (other.gameObject.GetComponent<Racer>().agent is PlayerAgent)
                {
                    Debug.Log("Player reaches end");
                    raceControl.chickenAtEnd();
                }
            }
        }
    }
}
