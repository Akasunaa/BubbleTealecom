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
        public virtual void MachineExecuteButtonCalled()
        {
            print("MACHINE EXECUTE CALLED");
        }
    }
}
