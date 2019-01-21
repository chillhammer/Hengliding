using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing {
    public class RaceStateInput
    {
        public bool isPlayerAtEnd;
        public RaceControl raceControl;

        public RaceStateInput(RaceControl raceControl)
        {
            this.raceControl = raceControl;
            isPlayerAtEnd = false;
        }
    }
}
