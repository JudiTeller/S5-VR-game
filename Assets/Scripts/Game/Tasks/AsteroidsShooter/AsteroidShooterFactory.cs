using UnityEngine;
using UnityEngine.XR;

namespace Game.Tasks.AsteroidsShooter
{
    public class AsteroidShooterFactory : GameTaskFactory<TaskSpawnPoint>
    {

        [SerializeField] private StartAsteroidShooter asteroidShooterTaskPrefab;
        [SerializeField] private XRNode leftController;
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            GameObject instance = Instantiate(asteroidShooterTaskPrefab.gameObject, spawnPoint.GetSpawnPosition(), spawnPoint.GetRotation());
            StartAsteroidShooter asteroidShooterGameTask = instance.GetComponent<StartAsteroidShooter>();
            asteroidShooterGameTask.crosshairMouseMovement.controller = leftController;
            asteroidShooterGameTask.shootProjectile.controller = leftController;
            
            return asteroidShooterGameTask;
        }
    }
}