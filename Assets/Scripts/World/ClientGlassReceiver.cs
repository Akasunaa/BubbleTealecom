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
                bool isGoodRecipe = GetComponent<Client>().CompareRecipe(cpnt.GetRecipe());
                Debug.Log("Is Good Recipe: " + isGoodRecipe);
            }
        }
    }
}