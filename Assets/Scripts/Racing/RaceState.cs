using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Racing
{
    public abstract class RaceState
    {
        private RaceStateInput raceInput;
        public RaceStateInput RaceInput
        {
            get { return raceInput; }
            set { raceInput = value; }
        }
        public RaceState(RaceStateInput raceInput)
        {
            RaceInput = raceInput;
        }
        public abstract void updateState();
    }

}