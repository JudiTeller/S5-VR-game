using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tasks.AsteroidsShooter
{
    /// <summary>
    /// handles the asteroid-spawning and moving to the player
    /// </summary>
    public class SpawnAsteroids : MonoBehaviour
    {
        // stores the prefab of the used asteroid
        [SerializeField] private GameObject rock; 
        
        // possible positions for the asteroid to spawn
        [SerializeField] private Transform spawnPos1;
        [SerializeField] private Transform spawnPos2;
        [SerializeField] private Transform spawnPos3;
        [SerializeField] private Transform spawnPos4;
        [SerializeField] private Transform spawnPos5;
        [SerializeField] private Transform spawnPos6;
        [SerializeField] private Transform spawnPos7;
        [SerializeField] private Transform spawnPos8;
        [SerializeField] private Transform[] _spawnPositions = new Transform[8];

        // possible positions the asteroid could fly to
        [SerializeField] private Transform endPoint1;
        [SerializeField] private Transform endPoint2;
        [SerializeField] private Transform endPoint3;
        [SerializeField] private Transform endPoint4;
        private Transform[] _endPoints = new Transform[4];
        
        // stores the selected difficulty-value
        [SerializeField] public Difficulty difficulty;
        
        // stores the time remaining until the next asteroid will spawn
        private float _spawnTime; 
        
        // stores the value of the interval between two asteroid-spawns
        private float _spawnInterval; 
        
        // stores the speed of the projectile
        private float _projectileSpeed; 
        
        // stores the amount of asteroids spawning
        private int _amountAsteroid; 
        
        // stores the value if the task is running
        private bool _running = true; 
        
        // = true, when the time of the task is over
        [HideInInspector]
        [SerializeField] public bool taskOverAmount;
        
        /// <summary>
        /// sets up task depending on the selected difficulty
        /// adds spawn-points and end-points to each list
        /// </summary>
        private void Start()
        {
            SetupDifficulty();

            // sets the default starting values
            _spawnTime = _spawnInterval;
            _running = true;
            
            _spawnPositions = new[] { spawnPos1, spawnPos2, spawnPos3, spawnPos4, spawnPos5, spawnPos6, spawnPos7, spawnPos8 };
            _endPoints = new[] { endPoint1, endPoint2, endPoint3, endPoint4 };
        }

        private void Update()
        {
            if (_running)
            {
                _spawnTime -= Time.deltaTime;

                if (_spawnTime <= 0)
                {
                    // creates a random startpoint to create the asteroid
                    // and a random endpoint where the asteroid should fly to
                    var randomStartPoint = GetRandomSpawnPosition();
                    var randomEndPoint = GetRandomEndPoint();

                    // calculates the distance 
                    var position = randomStartPoint.position;
                    var direction = CalculateDirection(position, randomEndPoint.position);
                    direction.Normalize();

                    // instantiates an asteroid at the random selected spawnpoint
                    // and changes its flying direction
                    var newRock = Instantiate(rock, position, new Quaternion());
                    newRock.GetComponent<Rigidbody>().velocity = direction * _projectileSpeed;

                    // interval of creating new asteroids
                    _spawnTime = _spawnInterval;

                    _amountAsteroid--; // reduces the amount of asteroids spawning by one
                    
                    // Check if the playing time exceeds the game time
                    if (_amountAsteroid <= 0)
                    {
                        _running = false;
                        taskOverAmount = true;
                    }
                }
            }
        }

        /// <summary>
        /// calculates the direction through the spawn-point and the endpoint
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private static Vector3 CalculateDirection(Vector3 startPoint, Vector3 endPoint)
        {
            var direction = endPoint - startPoint;

            return direction;
        }
        
        /// <summary>
        /// selects one random spawn-point out of the array
        /// </summary>
        /// <returns></returns>
        private Transform GetRandomSpawnPosition()
        {
            return _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        }

        /// <summary>
        /// selects one random endpoint out of the array
        /// </summary>
        /// <returns></returns>
        private Transform GetRandomEndPoint()
        {
            return _endPoints[Random.Range(0, _endPoints.Length)];
        }

        /// <summary>
        /// sets up the values of _spawnInternval, _projectileSpeed, _gameTime depenmding on the difficulty
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void SetupDifficulty()
        {
            switch (difficulty.GetSeparatedDifficulty())
            {
                case SeparatedDifficulty.Easy:
                    _spawnInterval = 2.5f;
                    _projectileSpeed = 3f;
                    _amountAsteroid = 20;
                     break;
                case SeparatedDifficulty.Medium:
                    _spawnInterval = 2f;
                    _projectileSpeed = 4f;
                    _amountAsteroid = 25;
                    break;
                case SeparatedDifficulty.Hard:
                    _spawnInterval = 1.5f;
                    _projectileSpeed = 5f;
                    _amountAsteroid = 30;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /// <summary>
        /// function used to stop the task if too many asteroid hit the wall
        /// </summary>
        /// <returns></returns>
        public void StopAsteroidSpawningLost()
        {
            _running = false;
        }
    }
}