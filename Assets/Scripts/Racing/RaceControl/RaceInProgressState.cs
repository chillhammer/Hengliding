using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Racing
{
    public class RaceInProgressState : RaceState
    {
        public RaceInProgressState(RaceStateInput raceInput) : base(raceInput) { }
        override public void updateState()
        {
            //Debug.Log("RacingState");
            if (raceInput.isPlayerAtEnd) {
                raceInput.raceControl.displayLeaderboard();
                changeState();
            }
        }
        public void changeState()
        {
            raceInput.raceControl.currentState = new RaceEndState(raceInput);
        }
    }
}
