using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    /// <summary>
    /// class to create instances of the task Energy Core
    /// </summary>
    public class EnergyCoreFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private StartEnergyCoreTask energyCorePrefab;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(energyCorePrefab.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.transform.rotation);
            StartEnergyCoreTask energyCoreTimerTask = instance.GetComponent<StartEnergyCoreTask>();
            
            
            return energyCoreTimerTask;
        }
    }
}