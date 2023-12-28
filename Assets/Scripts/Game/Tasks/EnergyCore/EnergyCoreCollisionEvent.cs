using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    /// <summary>
    /// class used to call the function TriggerOnCollisionEnter on Collision-event
    /// </summary>
    public class EnergyCoreEvent : MonoBehaviour
    {
        // stores the reference of the EnergyCoreManager-script
        [SerializeField] private EnergyCoreManager energyCoreManagerScript;
    
        /// <summary>
        /// event triggered when energy-cell collides with core
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("EnergyCell")) return;
            
            energyCoreManagerScript.TriggerOnCollisionEnter(gameObject, collision.gameObject);
        }
    }
}
