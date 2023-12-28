using UnityEngine;

namespace Timeline 
{
    /// <summary>
    /// class used to destroy the collided asteroid and spawn the fractured
    /// </summary>
    public class AsteroidFractureStartScene : MonoBehaviour
    {
        // this is the object the asteroid will break into
        [SerializeField] private GameObject fractured; 

        /// <summary>
        /// is called when the projectile with the Tag "Bullet" collides with the asteroid
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter()
        {
            Instantiate(fractured, transform.position, transform.rotation); 
        
            Destroy(gameObject);
        }
    }
}