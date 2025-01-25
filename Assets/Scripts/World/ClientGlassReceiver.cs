using UnityEngine;

namespace World
{
    public class ClientGlassReceiver : MonoBehaviour, IGlassReceiver
    {
        public void ReceiveGlass(GlassData glass)
        {
            bool isGoodRecipe = GetComponent<Client>().CompareRecipe(glass.recipe);
            Debug.Log("Is Good Recipe: " + isGoodRecipe);
        }
    }
}