using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Game.Tasks.EnergyCore
{
    /// <summary>
    /// handles the correct behaviour for the task Energy Core
    /// </summary>
    public class EnergyCoreManager : MonoBehaviour
    {
        // reference to the StartEnergyCore-script
        private StartEnergyCoreTask _startEnergyCoreTaskScript; 
        
        // stores the color of the cells and the cores which are colliding with each other
        private readonly List<Color> _finishedCoreColors = new(); 
        
        // stores the objects of the filled cores and the empty one (last index)
        [SerializeField] private List<GameObject> energyCores = new(); 
        
        // stores the objects of the cells to put in the cores
        [SerializeField] private List<GameObject> energyCells = new();
        
        private void Start()
        {
            _startEnergyCoreTaskScript = GetComponent<StartEnergyCoreTask>(); 
        }

        /// <summary>
        /// disables GrabInteractable-script and activates isKinematic from all cells 
        /// </summary>
        public void OnEnergyCellSelected()
        {
            foreach (var energyCell in energyCells)
            {
                if (energyCell == null) return;
                if (energyCell.GetComponent<XRGrabInteractable>().isSelected) continue;
                
                energyCell.GetComponent<XRGrabInteractable>().enabled = false;
                energyCell.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        /// <summary>
        /// checks if collision between cell and core happened
        /// </summary>
        /// <param name="core"></param>
        /// <param name="cell"></param>
        public void TriggerOnCollisionEnter(GameObject core, GameObject cell)
        {
            // activates all GrabInteractable-scripts
            // and sets isKinematic to true
            foreach (var energyCell in energyCells.Where(energyCell => energyCell != null))
            {
                if (_finishedCoreColors.Contains(cell.GetComponent<Renderer>().material.color)) continue;
                energyCell.GetComponent<XRGrabInteractable>().enabled = true;
                energyCell.GetComponent<Rigidbody>().isKinematic = false;
            }
            
            // checks if core and cell have the same color
            if (!ManageEnergyCoreColors.GetColor(core)
                    .Equals(ManageEnergyCoreColors.GetColor(cell)))
                return; 

            // checks if color of the cell is in list of matching cores with cells
            if (_finishedCoreColors.Contains(cell.GetComponent<Renderer>().material.color)) return;
            
            _finishedCoreColors.Add(cell.GetComponent<Renderer>().material.color);
            _startEnergyCoreTaskScript.IncrementFinishedEnergyCoreCounter(); // adds one to the counter: 6 = win
            
            // removes the core and the cell from its list and deactivates the possibility to grab the cell out of the core
            energyCores.Remove(core);
            energyCells.Remove(cell);
            cell.GetComponent<XRGrabInteractable>().enabled = false;
            cell.GetComponent<Rigidbody>().isKinematic = true;
            core.GetComponent<MeshCollider>().convex = true;
        }
    }
}