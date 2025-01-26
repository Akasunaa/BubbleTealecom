using NUnit.Framework;
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
        private GameObject _curItemContained;

        private void Awake()
        {
            Assert.IsNotNull(_mainItemSlot);
        }

        public override void MachineExecuteButtonCalled()
        {
            base.MachineExecuteButtonCalled();
            //we recover the item
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
            yield return null;
            SoundManager.PlaySound(SoundManager.Sound.Shaker);
            AudioClip clip = SoundManager.GetAudioClip(SoundManager.Sound.Shaker);
            //TODO : Stop the glass from being picked up
            yield return new WaitForSeconds(clip.length);
            //TODO : Unstop the glass from being picked up
            glassItemSlot.ShakeGlass();
        }
    }
}
