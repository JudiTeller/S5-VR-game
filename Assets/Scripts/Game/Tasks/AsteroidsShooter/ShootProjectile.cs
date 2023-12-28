using UnityEngine;
using UnityEngine.XR;

namespace Game.Tasks.AsteroidsShooter
{
    /// <summary>
    /// class used to shoot projectiles from the crosshair
    /// </summary>
    public class ShootProjectile : MonoBehaviour
    {
        // stores the prefab of the projectile
        [SerializeField] private GameObject projectilePrefab; 
        
        // stores the object of the camera
        [SerializeField] public new Camera camera;  
        
        // movement-speed of the bullet
        [SerializeField] private float projectileSpeed = 20f;  

        // stores the reference of the selected controller (left hand)
        [SerializeField] public XRNode controller;
        
        private bool _lastTrigger;
        
        private void Update()
        {
            // checks if current player is keyboard-profile and player uses left-click
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("Keyboard"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CalculateProjectileDirection();
                    
                }
            } 
            // checks if current player is vr-profile and player uses trigger-button
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                bool triggered = false;

                var device = InputDevices.GetDeviceAtXRNode(controller);
                triggered = (device.TryGetFeatureValue(CommonUsages.triggerButton,
                    out var triggerState) && triggerState);
                 
                
                if (triggered != _lastTrigger)
                {
                    if (triggered)
                    {
                        CalculateProjectileDirection();
                    }

                    _lastTrigger = triggered;
                }
            } 
        }

        /// <summary>
        /// calculates the direction the projectile should be flying to
        /// </summary>
        private void CalculateProjectileDirection()
        {
            var crosshairPosition = transform.position; // stores the position of the crosshair-image

            var cameraPosition = camera.transform.position; // stores the position of the camera

            var direction = crosshairPosition - cameraPosition; // calculates the direction

            direction.Normalize();

            // instantiates a new projectile moving away from the crosshair position
            var projectile = Instantiate(projectilePrefab, crosshairPosition, Quaternion.identity);

            // moves the projectile in the calculated direction with given speed
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        }
    }
}