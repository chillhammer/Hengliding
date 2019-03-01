using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackSelectionListItem : MonoBehaviour
{

    public HenInfo henInfo;
    public Toggle toggle;

    private HenSelectionControl control;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void assignHen(HenInfo hen) {
        this.henInfo = hen;
        Text label = GetComponentInChildren<Text>();
        label.text = hen.name;

    }

}
