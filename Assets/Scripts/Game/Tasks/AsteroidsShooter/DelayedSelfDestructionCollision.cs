using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    /// <summary>
    /// class used to destroy the asteroid after a given delay after collision
    /// </summary>
    public class DelayedSelfDestructionCollision : MonoBehaviour
    {
        // stores the value of the delay when the game-object gets destroyed
        [SerializeField] private float delay;
        
        // stores the value if the object is collided with something
        private bool _collided; 

        /// <summary>
        /// called every frame
        /// manages the delayed destruction of the asteroid
        /// </summary>
        private void Update()
        {
            if (!_collided) return;
            if (delay >= 0f)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// collision event
        /// sets the value of variable _collided to true
        /// </summary>
        private void OnCollisionEnter()
        {
            _collided = true;
        }
        
        
    }
}