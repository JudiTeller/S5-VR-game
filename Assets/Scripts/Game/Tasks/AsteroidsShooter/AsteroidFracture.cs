using UnityEngine;

namespace Game.Tasks.AsteroidsShooter
{
    /// <summary>
    /// destroys the collided asteroid and spawn the fractured
    /// </summary>
    public class AsteroidFracture : MonoBehaviour
    {
        // this is the object the asteroid will break into
        [SerializeField] private GameObject fractured;
        
        /// <summary>
        /// replaces the collided asteroid with the fractured one
        /// </summary>
        private void FractureObject()
        {
            Instantiate(fractured, transform.position, transform.rotation); 
        
            Destroy(gameObject);
        }

        /// <summary>
        /// is called when the projectile with the Tag "Bullet"
        /// collides with the asteroid
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                gameObject.GetComponent<AsteroidFracture>().FractureObject();
            }
        }
    }
}