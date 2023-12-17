using UnityEngine;

namespace Game.Tasks.leaks_riddle
{
    /// <summary>
    /// Listens for an CollisionEvent and Destroys the leak if it Collides with the
    /// Duct Tape.
    /// </summary>
    public class LeaksListener : MonoBehaviour
    {
        private LeaksRiddle _leaksRiddle;
        
        private void Start()
        {
            _leaksRiddle = gameObject.GetComponentInParent<LeaksRiddle>();
            _leaksRiddle.RegisterLeak(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.CompareTag("duct_tape")) return;
            _leaksRiddle.UnregisterLeak(gameObject);
            Destroy(gameObject);
        }
    }
}