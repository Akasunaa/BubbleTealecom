using System.Collections;
using UI;
using UnityEngine;

namespace Machines
{
    /// <summary>
    /// Class of the Shaker machine
    /// </summary>
    public class ShakerController : BaseMachine
    {
        [SerializeField] private EmptyableItemSlot _mainItemSlot;
        [SerializeField] private GameObject _blocker;
        private GameObject _curItemContained;

        private void Start()
        {
            _blocker.SetActive(false);
        }

        public override void MachineExecuteButtonCalled()
        {
            base.MachineExecuteButtonCalled();
            if (_working)
            {
                return;
            }
            //we recover the item

            SoundManager.PlaySound(SoundManager.Sound.SwitchButton, 0.6f);
            _curItemContained = _mainItemSlot.GetItem();
            if (_curItemContained != null && _curItemContained.GetComponent<GlassItemSlot>() 
                && !_curItemContained.GetComponent<GlassItemSlot>().GetIsShaken()
                && _curItemContained.GetComponent<GlassItemSlot>().GetRecipe().finalIngredientStates.Count != 0)
            {
                print("Current item : "+_curItemContained.name);
                print("It's a glass ! ");
                GlassItemSlot glassItemSlot = _curItemContained.GetComponent<GlassItemSlot>();
                StartCoroutine(MachineFunctionDelay(glassItemSlot));
            }
            else
            {
                print("There is nothing !");
                SoundManager.PlaySound(SoundManager.Sound.Error);
            }
        }

        public IEnumerator MachineFunctionDelay(GlassItemSlot glassItemSlot)
        {
            _working = true;
            yield return null;
#if !UNITY_EDITOR
            SoundManager.PlaySound(SoundManager.Sound.Shaker);
            AudioClip clip = SoundManager.GetAudioClip(SoundManager.Sound.Shaker);
#endif
            _blocker.SetActive(true);
            print(glassItemSlot.GetItem());
#if !UNITY_EDITOR
            yield return new WaitForSeconds(clip.length);
#else
            yield return new WaitForSeconds(2f);
#endif
            _blocker.SetActive(false);
            glassItemSlot.ShakeGlass();
            _working = false;
        }
    }
}
