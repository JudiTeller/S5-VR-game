using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    /// <summary>
    /// class used to control the asteroid-shooter
    /// </summary>
    public class StartAsteroidShooter : GameTask
    {
        // needs the prefab of the asteroid-shooter
        public GameObject asteroidShooterScene; 
        
        // needs the script SpawnAsteroids
        public SpawnAsteroids spawnAsteroidsScript;
        
        // needs the script CountSpaceStationHits
        public CountSpaceStationHits countSpaceStationHitsScript;
        
        // stores the script reference to ShootProjectile
        public ShootProjectile shootProjectile; 
        
        // stores the script-reference to CrosshairMouseMouvement
        public CrosshairMouseMovement crosshairMouseMovement; 
        
        // stores the value of the variable if the task is started
        private bool _started;
        
        public StartAsteroidShooter() : base("AsteroidShooter", "AsteroidShooter description")
        {
        }

        public override void Initialize()
        {
            shootProjectile.camera = playerProfileService.GetPlayerCamera();
            crosshairMouseMovement.playerProfileService = playerProfileService;
            countSpaceStationHitsScript.difficulty = difficulty;
            spawnAsteroidsScript.difficulty = difficulty;

            // deactivates the asteroid-shooter at the beginning
            asteroidShooterScene.SetActive(false); 
            
            taskDescription = "Oh no!\n Asteroids are flying towards the station!\n Shoot them before they hit the space station!\n Use the left stick to control the crosshair and the right trigger to shoot a nuclear ball!";
        }

        protected override void BeforeStateCheck()
        {
            // check if player is nearby
            if (!_started && !playerProfileService.GetIsVrPlayerActive() && Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position, transform.position) < 5)
            {
                // activate shooter scene
                asteroidShooterScene.SetActive(true);
                _started = true;
            }
        }

        protected override TaskState CheckTaskState()
        {
            // checks if the time is over and the player has hits left
            if (spawnAsteroidsScript.taskOverAmount)
            {
                return TaskState.Successful;
            }

            // checks if too many asteroids hit the wall and the player has not hits left
            if (countSpaceStationHitsScript.taskOverWall)
            {
                return TaskState.Failed;
            }

            // else: returns that the task is still running
            return TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                // activates the movement of the vr-player
                playerProfileService.SetVRMovementActive(true);
                
                DestroyTask();
            }
        }

        /// <summary>
        /// starts the task Asteroid Shooter
        /// </summary>
        public void StartTask()
        {
            asteroidShooterScene.SetActive(true);
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                // deactivates the movement of the vr-player
                playerProfileService.SetVRMovementActive(false);
            }
        }
    }
}
