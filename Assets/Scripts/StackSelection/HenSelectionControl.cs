using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HenSelectionControl : MonoBehaviour
{

    public GameObject listItemPrefab;
    public VerticalLayoutGroup verticalLayout;

    public SelectionStatsDisplay displayBox;

    List<StackSelectionListItem> listItems;

    public StartRaceButton StartButton;

    // Start is called before the first frame update
    void Start()
    {
        listItems = new List<StackSelectionListItem>();

        List<HenInfo> henList = HenInfoPersist.loadList();
        foreach (HenInfo info in henList) {
            GameObject listEntry = Instantiate(listItemPrefab) as GameObject;
            StackSelectionListItem item = listEntry.GetComponent<StackSelectionListItem>();
            listItems.Add(item);
            item.assignHen(info);
            item.toggle.isOn = false;
            item.toggle.onValueChanged.AddListener(delegate {updateStats();});
            

            listEntry.transform.SetParent(verticalLayout.transform, false);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStats() {
        //compute selection
        List<HenInfo> selectedHens = new List<HenInfo>();
        foreach (StackSelectionListItem item in listItems) {
            if (item.gameObject.GetComponentInChildren<Toggle>().isOn) {
                selectedHens.Add(item.henInfo);
            }
        }

        RaceStatsCalculator.calculateStats(selectedHens);
        displayBox.updateStats();
        StartButton.updateButton();
    }

    

    


}
