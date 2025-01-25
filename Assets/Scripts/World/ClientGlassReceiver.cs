using UnityEngine;

namespace World
{
    public class ClientGlassReceiver : MonoBehaviour, IGlassReceiver
    {
        public void ReceiveGlass(GlassData glass)
        {
            Debug.Log("yummy");
        }
    }
}