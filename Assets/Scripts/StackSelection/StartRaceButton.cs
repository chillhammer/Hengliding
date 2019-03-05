using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartRaceButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate {startRace();});
    }

    public void startRace() {
        Debug.Log("Start");
        SceneManager.LoadScene("Race");
    }

    public void updateButton() {
        bool canRace = (SelectedRaceParameters.mass > 0);

        Button button = GetComponent<Button>();

        if (canRace) {
            button.interactable = true;
        } else {
            button.interactable = false;
        }
    }
}
