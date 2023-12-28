using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    /// <summary>
    /// class used to start the task if the button in the prefab is pressed
    /// </summary>
    public class StartAsteroidShooterTaskButton : MonoBehaviour
    {
        [SerializeField] private StartAsteroidShooter startAsteroidShooterScript;

        public void StartTaskAsteroidShooter()
        {
            startAsteroidShooterScript.StartTask();
        }
    }
}
