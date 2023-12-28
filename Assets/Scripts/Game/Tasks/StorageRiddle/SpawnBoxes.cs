using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tasks.StorageRiddle
{
    /// <summary>
    /// handles the spawning of the boxes and its position in the game
    /// </summary>
    public class SpawnBoxes : MonoBehaviour
    {
        // prefab of the box spawned in the task
        [SerializeField] private GameObject boxPrefab; 
        
        // value how many boxes should be placed on the given platform
        private int _maxAmountDeliveryBoxes; 
        
        // stores the possible spawn-points of the boxes
        [SerializeField] private Transform spawnPos1;
        [SerializeField] private Transform spawnPos2;
        [SerializeField] private Transform spawnPos3;
        [SerializeField] private Transform spawnPos4;
        [SerializeField] private Transform spawnPos5;
        [SerializeField] private Transform spawnPos6;
        [SerializeField] private Transform spawnPos7;
        [SerializeField] private Transform spawnPos8;
        [SerializeField] private Transform spawnPos9;
        [SerializeField] private Transform spawnPos10;
        [SerializeField] private Transform spawnPos11;
        [SerializeField] private Transform spawnPos12;
        private readonly List<Transform> _possibleSpawnPositions = new();
        
        /// <summary>
        /// adds all transforms of the position-game-objects to a list and selects an amount of positions random from
        /// the list depending on the value of _maxAmountDeliveryBoxes
        /// </summary>
        private void Start()
        {
            _possibleSpawnPositions.Add(spawnPos1);
            _possibleSpawnPositions.Add(spawnPos2);
            _possibleSpawnPositions.Add(spawnPos3);
            _possibleSpawnPositions.Add(spawnPos4);
            _possibleSpawnPositions.Add(spawnPos5);
            _possibleSpawnPositions.Add(spawnPos6);
            _possibleSpawnPositions.Add(spawnPos7);
            _possibleSpawnPositions.Add(spawnPos8);
            _possibleSpawnPositions.Add(spawnPos9);
            _possibleSpawnPositions.Add(spawnPos10);
            _possibleSpawnPositions.Add(spawnPos11);
            _possibleSpawnPositions.Add(spawnPos12);
            
            // places boxes (spawned amount depends on the amount set by the difficulty) on randomized positions
            for (var i = 0; i < _maxAmountDeliveryBoxes; i++)
            {
                var boxPosition = _possibleSpawnPositions[Random.Range(0, _possibleSpawnPositions.Count)];
                
                _possibleSpawnPositions.Remove(boxPosition);
                
                Instantiate(boxPrefab, boxPosition.position, new Quaternion());
            }
        }
        
        /// <summary>
        /// sets the amount of the boxes which should be placed on the delivery-platform to the given value
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAmountDeliveryBoxes(int value)
        {
            _maxAmountDeliveryBoxes = value;
        }
    }
}