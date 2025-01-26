using UnityEngine;

namespace UI
{
    public class TabletStartupController : MonoBehaviour
    {
        [SerializeField] private GameObject _tablet;

        private void OnEnable()
        {
            _tablet.SetActive(true);
            _tablet.GetComponent<TabletController>().LoadTabletAtStartup();
        }
    }
}
