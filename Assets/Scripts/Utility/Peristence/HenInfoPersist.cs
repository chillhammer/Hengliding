using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Raising;

//saves and loads hen info
public class HenInfoPersist
{

    private static readonly string HEN_STATS_SAVE_KEY = "HensList";
    private static readonly string STATS_VERSION_NUMBER_KEY = "HenStatsVersion";

    HenInfo[] records;



    public static void saveList(List<HenInfo> list) {
        ListWrapper wraper = new ListWrapper(list);

        string json = JsonUtility.ToJson(wraper);
        PlayerPrefs.SetString(HEN_STATS_SAVE_KEY, json);
        PlayerPrefs.SetInt(STATS_VERSION_NUMBER_KEY, HenInfo.VERSION_NUMBER);
    }

    //returns the list of saved hen stats, or null if nothing was saved
    public static List<HenInfo> loadList() {
        //check version number to prevent trying to load an incompatible JSON object
        if (!PlayerPrefs.HasKey(STATS_VERSION_NUMBER_KEY)) {
            return new List<HenInfo>();
        }
        else if (PlayerPrefs.GetInt(STATS_VERSION_NUMBER_KEY) != HenInfo.VERSION_NUMBER) {
            return new List<HenInfo>();
        }
        else if (!PlayerPrefs.HasKey(HEN_STATS_SAVE_KEY)) {
            return new List<HenInfo>();
        } else {
            string json = PlayerPrefs.GetString(HEN_STATS_SAVE_KEY);
            ListWrapper wrapper = JsonUtility.FromJson<ListWrapper>(json);
            return wrapper.infoList;
        }
    }
    
    private class ListWrapper {
        public List<HenInfo> infoList;

        internal ListWrapper(List<HenInfo> list) {
            this.infoList = list;
        }


    }
}
