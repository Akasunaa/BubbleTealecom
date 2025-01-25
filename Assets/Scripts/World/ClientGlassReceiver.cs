using UI;
using UnityEngine;

namespace World
{
    public class ClientGlassReceiver : MonoBehaviour, IGlassReceiver
    {
        public void ReceiveGlass(GameObject glass)
        {
            var cpnt = glass.GetComponent<GlassItemSlot>();
            if (cpnt)
            {
                GetComponent<Client>().GiveGlass(cpnt.GetRecipe());
                cpnt.Clear();
            }
        }
    }
}