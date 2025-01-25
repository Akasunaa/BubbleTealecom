using UnityEngine;

namespace World
{
    public class TrashGlassReceiver : MonoBehaviour, IGlassReceiver
    {
        public void ReceiveGlass(GlassData glass)
        {
            glass.Clear();
        }
    }
}