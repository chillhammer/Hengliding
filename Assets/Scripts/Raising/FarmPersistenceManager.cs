using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raising {
    public class FarmPersistenceManager : MonoBehaviour
    {
        public static readonly float AUTOSAVE_INTERVAL = 5f; //time between saves in seconds
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(autosave());
        }

    private void saveFlock() {
        //TODO get chickens
        Hen[] flock = FindObjectsOfType<Hen>();
        List<HenInfo> records = new List<HenInfo>();

        foreach (Hen obj in flock) {
            records.Add(new HenInfo(obj));
        }

        HenInfoPersist.saveList(records);

    }

    private IEnumerator autosave() {
	
        while (true) {
            yield return new WaitForSeconds(AUTOSAVE_INTERVAL);
            saveFlock();
        }
	}
}
}


