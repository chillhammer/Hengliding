using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionStatsDisplay : MonoBehaviour
{

    Text textbox;

    // Start is called before the first frame update
    void Start()
    {
        textbox = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStats() {

        textbox.text = "Mass: " + SelectedRaceParameters.mass + "\n" +
            "Wingspan: " + SelectedRaceParameters.wingspan + "\n" +
            "Control Authority: " + SelectedRaceParameters.controlAuthority + "\n" +
            "Drag multiplier: " + SelectedRaceParameters.dragMultiplier;
    }
}
