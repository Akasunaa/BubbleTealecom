using System.Collections;
using UI;
using UnityEngine;

namespace Machines
{
    /// <summary>
    /// Base parent class of all machines
    /// </summary>
    public class BaseMachine : MonoBehaviour, IMachineCommand
    {
        protected bool _working;
        [SerializeField] protected Transformation _machineTransformation;

        public virtual void MachineExecuteButtonCalled()
        {
            print("MACHINE EXECUTE CALLED");
        }

        /// <summary>
        /// Ensures that the inputted ingredient can be moleculared
        /// </summary>
        protected virtual bool CheckItemSlotHasCorrectItem(EmptyableItemSlot itemSlot)
        {
            var item = itemSlot.GetItem();
            if (item != null && item.GetComponent<Ingredient>())
            {
                var possibleTransformations = item.GetComponent<Ingredient>().ingredientState.possibleTransformations;
                foreach (IngredientTransformation ingredientTransformation in possibleTransformations)
                {
                    if (ingredientTransformation.transformation == _machineTransformation)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
