using UI;
using UnityEngine;

namespace World
{
    public class TrashGlassReceiver : MonoBehaviour, IGlassReceiver
    {
        public void ReceiveGlass(GameObject glass)
        {
            var cpnt = glass.GetComponent<GlassItemSlot>();
            if (cpnt)
            {
                SoundManager.PlaySound(SoundManager.Sound.Trash);
                cpnt.Clear();
            }
        }
    }
}