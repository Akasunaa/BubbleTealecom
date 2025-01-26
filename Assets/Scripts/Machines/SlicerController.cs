using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Machines
{
    /// <summary>
    /// Class for slicer machine
    /// </summary>
    public class SlicerController : BaseMachine
    {
        [SerializeField] private EmptyableItemSlot _mainItemSlot;
        [SerializeField] private Scrollbar _bladeScrollbar;
        [SerializeField] private Scrollbar _handleScrollbar;

        public override void MachineExecuteButtonCalled()
        {
            var item = _mainItemSlot.GetItem();
            SoundManager.PlaySound(SoundManager.Sound.SlicerLever, 0.6f);
            if (!item)
            {
                SoundManager.PlaySound(SoundManager.Sound.Error);
                return;
            }
            var ingredient = item.GetComponent<Ingredient>();
            if (!ingredient)
            {
                Debug.LogError("BIZARRE INGREDIENT (probably the glass)");
                SoundManager.PlaySound(SoundManager.Sound.Error);
                return;
            }

            if(CheckItemSlotHasCorrectItem(_mainItemSlot))
            {
                print("Slicer protocol enacted");
                ingredient.Transform(Transformation.Cut);
                SoundManager.PlaySound(SoundManager.Sound.Slicer);
            }
            else
            {
                print("item cannot be sliced");
                SoundManager.PlaySound(SoundManager.Sound.Error);
            }

        }

        private void Update()
        {
            _bladeScrollbar.value = _handleScrollbar.value;
        }

        public void OnSliderValueChanged(Scrollbar scrollbar)
        {
            if (scrollbar.value > 0.0)
            {        
                if (scrollbar.value > 0.9)
                {
                    MachineExecuteButtonCalled();
                }
                scrollbar.value = 0.0f;
            }
        }
    }
}
