using PlayerController;
using UnityEngine;
using UnityEngine.XR;

namespace Game.Tasks.AsteroidsShooter
{
    /// <summary>
    /// class used to track movement of the mouse for the crosshair
    /// </summary>
    public class CrosshairMouseMovement : MonoBehaviour
    {
        // objects of the canvas and the crosshair
        public RectTransform crosshairRectTransform;
        public RectTransform canvasRectTransform;
        
        // stores the reference to the player profile
        public PlayerProfileService playerProfileService; 
        
        // stores the object of the used controller
        public XRNode controller; 
        
        // value of the sensitivity, how fast the crosshair should move in vr
        // threshold to reset crosshair if the tilt is lower than this value
        public float sensitivity = 0.1f;
        
        /// <summary>
        /// is called every frame
        /// moves the crosshair depending on the selected profile
        /// vr: crosshair-movement through left joystick
        /// keyboard: crosshair-movement through mouse
        /// </summary>
        private void Update()
        {
            // if vr-player is active
            if (playerProfileService.GetIsVrPlayerActive())
            {
                var device = InputDevices.GetDeviceAtXRNode(controller);

                if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out var leftThumbstickValue))
                {
                    // calculation stuff to set the position of the crosshair (do not ask!)
                    var displacement = leftThumbstickValue * sensitivity;
                    
                    var anchoredPosition = crosshairRectTransform.anchoredPosition;
                    anchoredPosition += displacement;
                    crosshairRectTransform.anchoredPosition = anchoredPosition;

                    Vector3 clampedPosition = anchoredPosition;
                    var rect = canvasRectTransform.rect;
                    clampedPosition.x = Mathf.Clamp(clampedPosition.x, rect.min.x, rect.max.x);
                    var rect1 = canvasRectTransform.rect;
                    clampedPosition.y = Mathf.Clamp(clampedPosition.y, rect1.min.y, rect1.max.y);
                    crosshairRectTransform.anchoredPosition = clampedPosition;

                    Debug.Log($"clamped: {clampedPosition}");

                }
            }
            // if keyboard-player is active
            else
            {
                Vector2 mousePosition = Input.mousePosition;
            
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, playerProfileService.GetPlayerCamera(), out mousePosition);

                var newPosition = mousePosition;
                
                // calculates the screen-size and the ratio
                var sizeDelta = canvasRectTransform.sizeDelta;
                var delta = crosshairRectTransform.sizeDelta;

                // limits the movement of the crosshair to the canvas-size
                var minX = -sizeDelta.x / 2.0f + delta.x / 2.0f;
                var maxX = sizeDelta.x / 2.0f - delta.x / 2.0f;
                var minY = -sizeDelta.y / 2.0f + delta.y / 2.0f;
                var maxY = sizeDelta.y / 2.0f - delta.y / 2.0f;

                // calculates and sets the new position
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                crosshairRectTransform.anchoredPosition = newPosition;  
            }
        }
    }
}