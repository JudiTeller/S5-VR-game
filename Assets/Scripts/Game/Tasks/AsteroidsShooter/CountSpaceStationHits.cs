using System;
using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    /// <summary>
    /// class used to handle the collision of asteroids and the wall
    /// </summary>
    public class CountSpaceStationHits : MonoBehaviour
    {
        // stores the selected difficulty-value
        [SerializeField] public Difficulty difficulty; 
        
        // stores the reference of the SpawnAsteroid-script
        [SerializeField] private SpawnAsteroids spawnAsteroidsScript; 
        
        // stores the value how often an asteroid can he the wall to lose the task
        private int _maxHitCounter; 
        
        // stores the value how often an asteroid hit the wall
        private int _hitCounter; 
        
        // == true, when too many asteroids hit the wall 
        [HideInInspector]
        public bool taskOverWall;

        /// <summary>
        /// sets the task up depending on the difficulty of the game and
        /// sets the hit-counter to 0
        /// </summary>
        private void Start()
        {
            SetupDifficulty();
            _hitCounter = 0;
        }

        /// <summary>
        /// handles collision between asteroid and wall
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            // checks if the colliding object has the tag "Asteroid"
            if (!other.gameObject.CompareTag("Asteroid")) return;

            // highers the counter up by 1 and destroys the asteroid
            _hitCounter++;
            Destroy(other.gameObject);
            
            if (_hitCounter >= _maxHitCounter) // checks if the task is lost
            {
                spawnAsteroidsScript.StopAsteroidSpawningLost();
                
                taskOverWall = true;
            }
        }

        /// <summary>
        /// sets the value of _maxHintCounter depending on the selected difficulty
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void SetupDifficulty()
        {
            switch (difficulty.GetSeparatedDifficulty())
            {
                case SeparatedDifficulty.Easy:
                    _maxHitCounter = 5;
                    break;
                case SeparatedDifficulty.Medium:
                    _maxHitCounter = 3;
                    break;
                case SeparatedDifficulty.Hard:
                    _maxHitCounter = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
