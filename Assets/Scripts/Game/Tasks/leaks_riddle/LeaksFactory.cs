using UnityEngine;

namespace Game.Tasks.leaks_riddle
{
    public class LeaksFactory : GameTaskFactory<TaskSpawnPoint>
    {
        [SerializeField] private GameObject leaksRiddlePrefab;
        [SerializeField] private Transform[] leakSpawnPoints;
        [SerializeField] private GameObject leakPrefab;

        private const int EasyNumberSpawnPoints = 3;
        private const int MediumNumberSpawnPoints = 5;
        private const int HardNumberSpawnPoints = 8;
        

        // ReSharper disable Unity.PerformanceAnalysis
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            var transform1 = spawnPoint.transform;
            var riddle = Instantiate(leaksRiddlePrefab, transform1.position, transform1.rotation);
            var bookRiddle = riddle.GetComponent<LeaksRiddle>();
            SpawnLeaks(leakSpawnPoints, riddle.transform);
            return bookRiddle;
        }

        /// <summary>
        /// Spawns the leaks that belong to this riddle randomly
        /// </summary>
        /// <param name="spawnPoints">The spawn-points where the leaks might spawn</param>
        /// <param name="parent">The parent transform so that everything can be removed after the riddle.</param>
        private void SpawnLeaks(Transform[] spawnPoints, Transform parent)
        {
            spawnPoints.Shuffle();

            for (var i = 0; i < ReceiveAmountOfSpawnPointsAccordingToDifficulty(); i++)
            {
                SetUpNewLeak(spawnPoints[i], parent);
            }
        }

        /// <summary>
        /// Calculates the amount of leaks that need to be spawned.
        /// </summary>
        /// <returns>The amount of leaks to be spawned according to the difficulty.</returns>
        private int ReceiveAmountOfSpawnPointsAccordingToDifficulty()
        {
            return mDifficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => EasyNumberSpawnPoints,
                SeparatedDifficulty.Medium => MediumNumberSpawnPoints,
                SeparatedDifficulty.Hard => HardNumberSpawnPoints,
                _ => EasyNumberSpawnPoints
            };
        }

        /// <summary>
        /// Sets up the spawnPoint and parent of a certain leak.
        /// </summary>
        /// <param name="spawnPoint">The spawn-point of the leak</param>
        /// <param name="parent">The parent Object</param>
        private void SetUpNewLeak(Transform spawnPoint, Transform parent)
        {
            Instantiate(leakPrefab, spawnPoint.position, spawnPoint.rotation, parent);
        }
    }
}