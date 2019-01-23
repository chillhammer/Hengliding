using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Racing
{
    public class RaceBeginState : RaceState
    {
        public RaceBeginState(RaceStateInput raceInput) : base(raceInput) { }
        override public void updateState()
        {
            Debug.Log("RaceBeginState");
            changeState();
        }
        public void changeState()
        {
            raceInput.raceControl.currentState = new RaceInProgressState(raceInput);
        }
    }
}
