using NUnit.Framework;
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
            if (_curItemContained != null)
            {
                print("Current item : "+_curItemContained.name);
                if(_curItemContained.GetComponent<GlassItemSlot>())
                {
                    print("It's a glass ! ");
                    GlassItemSlot glassItemSlot = _curItemContained.GetComponent<GlassItemSlot>();
                    glassItemSlot.ShakeGlass();
                }
                else
                {
                    print("It is not a glass !");
                    //TODO : Play failure sound !
                }
            }
            else
            {
                print("There is nothing !");
                //TODO : Play failure sound !
            }

        }
    }
}
