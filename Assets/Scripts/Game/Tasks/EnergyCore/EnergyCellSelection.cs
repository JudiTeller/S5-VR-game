using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    /// <summary>
    /// class used to call function OnEnergyCellSelected() when the player grabbed the energy-cell
    /// </summary>
    public class EnergyCellSelection : MonoBehaviour
    {
        // reference to the EnergyCoreManager-script
        [SerializeField] private EnergyCoreManager energyCoreManager;

        /// <summary>
        /// event triggered when a energy cell is picked up by the player
        /// </summary>
        public void OnEnergyCellSelection()
        {
            energyCoreManager.OnEnergyCellSelected();
        }
    }
}