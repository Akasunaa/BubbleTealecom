using System.Collections;
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
        private Quaternion _tourniquetStartRotation;
        [SerializeField] private float _rotationDegrees = 45;

        [Header("Output")]
        [SerializeField] private GameObject _melangeIngredient;

        private void Awake()
        {
            _tourniquetStartRotation = _tourniquetButton.rotation;
        }

        public override void MachineExecuteButtonCalled()
        {
            base.MachineExecuteButtonCalled();
            if (_working)
            {
                //in case the machine is already working, we return immediately
                return;
            }
            if(_tourniquetButton.rotation == new Quaternion(0, 0, 0, 1) || _tourniquetButton.rotation == new Quaternion(0,0,1,0))
            {
                if(CheckItemSlotHasCorrectItem(_itemSlot1) && CheckItemSlotHasCorrectItem(_itemSlot2) && !_outputSlot.GetItem())
                {
                    print("YES !");
                    StartCoroutine(MachineFunctionDelay());
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

        private IEnumerator MachineFunctionDelay()
        {
            _working = true;
            yield return null;
#if UNITY_EDITOR
#else
            SoundManager.PlaySound(SoundManager.Sound.MolecularReassembler);
            AudioClip clip = SoundManager.GetAudioClip(SoundManager.Sound.MolecularReassembler);
#endif
            //we generate the combined ingredient in the outputslot
            var newMelange = GenerateNewMelangeItem();
            newMelange.SetActive(false);
            //we remove the original items :
            //TODO : remove original items before the timer
            _itemSlot1.DropItem();
            _itemSlot2.DropItem();
#if UNITY_EDITOR
            yield return new WaitForSeconds(2f);
#else
            yield return new WaitForSeconds(clip.length);
#endif
            //after delay, enable melange object
            newMelange.SetActive(true);
            //we reset the tourniquet
            _tourniquetButton.SetLocalPositionAndRotation(new Vector3(0f,92.8f,0f), _tourniquetStartRotation); 
            _working = false;
        }

        private GameObject GenerateNewMelangeItem()
        {
            IngredientState ingredientState1 = _itemSlot1.GetItem().GetComponent<Ingredient>().ingredientState;
            IngredientState ingredientState2 = _itemSlot2.GetItem().GetComponent<Ingredient>().ingredientState;
            GameObject newMelange = Instantiate(_melangeIngredient);
            newMelange.GetComponent<MelangeIngredientController>().CreateMelange(ingredientState1, ingredientState2);
            newMelange.AddComponent<CanvasGroup>().blocksRaycasts = false;
            newMelange.transform.position = _outputSlot.transform.position;
            _outputSlot.Receive(newMelange);
            return newMelange;
        }
    }
}
