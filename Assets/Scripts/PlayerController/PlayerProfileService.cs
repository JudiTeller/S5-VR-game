using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerController
{
    /// <summary>
    /// This Service determines whether VRPlayer or KeyboardPlayer
    /// should be activated or not
    /// </summary>
    public class PlayerProfileService : MonoBehaviour
    {
        public const string k_PlayerGameObjectTag = "Player";
        [SerializeField] private bool isVrPlayerActive;
        [SerializeField] private GameObject vrPlayer;
        [SerializeField] private GameObject xrOrigin;
        [SerializeField] private GameObject keyBoardPlayer;
        [SerializeField] private Transform leftVrController;
        [SerializeField] private Transform rightVrController;
        [SerializeField] private Camera vrCamera;
        [SerializeField] private Camera keyboardCamera;
        [SerializeField] private GameObject locomotiveSystemMove;
        [SerializeField] private HUD vrPlayerHUD;
        [SerializeField] private HUD keyboardPlayerHUD;
        
        /// <summary>
        /// Deactivates the player that should not be used during the game.
        /// </summary>
        private void OnEnable()
        {
            if (SceneManager.GetActiveScene().name.Equals("MainMenuScene"))
            {
                SetIsVrPlayerActive(isVrPlayerActive);
            }
            else
            {
                isVrPlayerActive = PlayerPrefs.GetString("CurrentPlayer").Equals("VR");
                UpdateActivePlayer();
            }
        }

        /// <summary>
        /// Updates the current active player according to the <see cref="isVrPlayerActive"/> state
        /// </summary>
        private void UpdateActivePlayer()
        {
            keyBoardPlayer.SetActive(!isVrPlayerActive);
            vrPlayer.SetActive(isVrPlayerActive);
        }

        /// <summary>
        /// Returns the current player game object, which is either the xrOrigin or the keyboardPlayer.
        /// This game object can be used to obtain the current player position, orientation, scale etc.
        /// </summary>
        public GameObject GetPlayerGameObject()
        {
            return isVrPlayerActive ? xrOrigin : keyBoardPlayer;
        }

        /// <summary>
        /// Returns the current player camera
        /// </summary>
        public Camera GetPlayerCamera()
        {
            return isVrPlayerActive ? vrCamera : keyboardCamera;
        }

        public bool GetIsVrPlayerActive()
        {
            return isVrPlayerActive;
        }

        /// <summary>
        /// Changes the current active player according to the given value
        /// </summary>
        /// <param name="vrPlayerActive">
        /// if true, the vr player will be activated otherwise the keyboard player will be activated
        /// </param>
        public void SetIsVrPlayerActive(bool vrPlayerActive)
        {
            isVrPlayerActive = vrPlayerActive;
            PlayerPrefs.SetString("CurrentPlayer", isVrPlayerActive ? "VR" : "Keyboard");

            UpdateActivePlayer();
        }

        /// <summary>
        /// Getter for left vr controller
        /// </summary>
        /// <returns>transform of the left vr controller</returns>
        public Transform GetLeftVrController()
        {
            return leftVrController;
        }

        /// <summary>
        /// Getter for right vr controller
        /// </summary>
        /// <returns>transform of the right vr controller</returns>
        public Transform GetRightVrController()
        {
            return rightVrController;
        }

        /// <summary>
        /// Actives or deactivates the vr movement. This will only be changed if the current player is a vr player.
        /// </summary>
        /// <param name="active">if true, the vr movement will be deactivated, otherwise activated</param>
        public void SetVRMovementActive(bool active)
        {
            if (isVrPlayerActive)
            {
                locomotiveSystemMove.SetActive(active);
            }
        }
        
        /// <summary>
        /// Returns the HUD reference according to the current player
        /// </summary>
        /// <returns>vr player hud, if <see cref="isVrPlayerActive"/> and the keyboard player hud otherwise</returns>
        public HUD GetHUD()
        {
            if (isVrPlayerActive)
            {
                return vrPlayerHUD;
            }
            return keyboardPlayerHUD;
        }
    }
}