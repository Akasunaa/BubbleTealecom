using LevelData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TabletController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> days;
        [SerializeField] private List<GameObject> notifs;
        [SerializeField] private List<GameObject> tabButtons;

        private void Start()
        {
            if (days == null) { Debug.LogError("NO DAYS"); }
            if (notifs == null) { Debug.LogError("NO NOTIFS"); }
            if(days.Count!= notifs.Count || notifs.Count!= tabButtons.Count) { Debug.LogError("WRONG SETUP"); }
        }

        public void LoadTabletAtStartup()
        {
            int levelIterator = LevelDataHolder.CurrentDay - 1;
            for(int ite=0;ite< levelIterator + 1; ite++)
            {
                days[ite].SetActive(false);
                notifs[ite].SetActive(false);
                tabButtons[ite].SetActive(true);
            }
            notifs[levelIterator].SetActive(true);
            days[levelIterator].SetActive(true);
        }
    }
}
