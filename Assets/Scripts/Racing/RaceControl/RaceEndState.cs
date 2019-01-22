using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing
{
    public class RaceEndState : RaceState
    {
        public RaceEndState(RaceStateInput raceInput) : base(raceInput) { }
        override public void updateState()
        {
            Debug.Log("RaceEndState");
        }
    }

}