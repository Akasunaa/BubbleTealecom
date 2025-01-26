using UnityEngine;

namespace Machines
{
    /// <summary>
    /// Interface for machines commands as requests of the user
    /// </summary>
    public interface IMachineCommand
    {
        /// <summary>
        /// Main execute function of the machine, will trigger the transformation - called either by UI or other code stuff
        /// </summary>
        public void MachineExecuteButtonCalled();
    }
}
