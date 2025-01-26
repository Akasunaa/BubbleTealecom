using NUnit.Framework;
using UI;
using UnityEngine;

namespace Machines
{
    /// <summary>
    /// Script for Molecular machine
    /// </summary>
    public class MolecularController : BaseMachine
    {
        [Header("ItemSlots")]
        [SerializeField] private EmptyableItemSlot _itemSlot1;
        [SerializeField] private EmptyableItemSlot _itemSlot2;
        [SerializeField] private EmptyableItemSlot _outputSlot;

        [Header("Tourniquet Button")]
        [SerializeField] private RectTransform _tourniquetButton;
        private Vector3 _tourniquetStartRotation;
        [SerializeField] private float _rotationDegrees = 45;

        [Header("Output")]
        [SerializeField] private GameObject _melangeIngredient;

        private void OnValidate()
        {
            Assert.IsNotNull(_tourniquetButton);
            Assert.IsNotNull(_itemSlot1);
            Assert.IsNotNull(_itemSlot2);
            Assert.IsNotNull(_outputSlot);
        }

        private void Awake()
        {
            _tourniquetStartRotation = _tourniquetButton.rotation.eulerAngles;
            print(_tourniquetStartRotation);
        }

        /// <summary>
        /// Ensures that the inputted ingredient can be moleculared
        /// </summary>
        private bool CheckItemSlotHasCorrectItem(EmptyableItemSlot itemSlot)
        {
            var item = itemSlot.GetItem();
            if (item != null && item.GetComponent<Ingredient>())
            {
                var possibleTransformations = item.GetComponent<Ingredient>().ingredientState.possibleTransformations;
                foreach(IngredientTransformation ingredientTransformation in possibleTransformations)
                {
                    if (ingredientTransformation.transformation == Transformation.MolecularAssembly)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void MachineExecuteButtonCalled()
        {
            base.MachineExecuteButtonCalled();
            if(_tourniquetButton.rotation == new Quaternion(0, 0, 0, 1) || _tourniquetButton.rotation == new Quaternion(0,0,1,0))
            {
                if(CheckItemSlotHasCorrectItem(_itemSlot1) && CheckItemSlotHasCorrectItem(_itemSlot2))
                {
                    print("YES !");
                    //we generate the combined ingredient in the outputslot
                    IngredientState ingredientState1 = _itemSlot1.GetItem().GetComponent<Ingredient>().ingredientState;
                    IngredientState ingredientState2 = _itemSlot2.GetItem().GetComponent<Ingredient>().ingredientState;
                    GameObject newMelange = Instantiate(_melangeIngredient);
                    newMelange.GetComponent<MelangeIngredientController>().CreateMelange(ingredientState1, ingredientState2);
                    newMelange.AddComponent<CanvasGroup>().blocksRaycasts = false;
                    newMelange.transform.position = _outputSlot.transform.position;
                    _outputSlot.Receive(newMelange);
                }
                else
                {
                    print("NO - wrong transformations");
                    SoundManager.PlaySound(SoundManager.Sound.Error);
                }
            }
            else
            {
                print("NO");
                SoundManager.PlaySound(SoundManager.Sound.Error);
            }
        }

        public void OnTourniquetClicked()
        {
            //we rotate the tourniquet by x degrees
            float rotZ = _tourniquetButton.rotation.eulerAngles.z;
            rotZ -= _rotationDegrees;
            _tourniquetButton.rotation = Quaternion.Euler(new Vector3(_tourniquetButton.rotation.eulerAngles.x, _tourniquetButton.rotation.eulerAngles.y, rotZ));
        }
    }
}
