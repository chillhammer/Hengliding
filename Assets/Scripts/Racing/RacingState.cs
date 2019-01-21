using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Racing
{
    public class RacingState : RaceState
    {
        public RacingState(RaceStateInput raceInput) : base(raceInput) { }
        override public void updateState()
        {
            //Debug.Log("RacingState");
            if (RaceInput.isPlayerAtEnd) {
                RaceInput.raceControl.displayLeaderboard();
                changeState();
            }
        }
        public void changeState()
        {
            RaceInput.raceControl.currentState = new RaceEndState(RaceInput);
        }
    }
}
