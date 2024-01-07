using PlayerController;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Connects the hud of the player with the game data.
    /// Updates the hud elements after game events like OnIntegrityChanged and OnTimeChanged.
    /// </summary>
    public class GameHUDService : MonoBehaviour
    {
        [SerializeField] private PlayerProfileService playerProfileService;
        [SerializeField] private Integrity integrity;
        [SerializeField] private GameTimer gameTimer;

        private void Start()
        {
            gameTimer.OnTimeChanged += OnTimeChanged;
            // deactivated due to workaround (see below)
            integrity.OnIntegrityChanged += OnIntegrityChanged;
            ChangeNavigation();
        }

        /// <summary>
        /// Event listener for time changes. Updates the time indicator on the HUD.
        /// </summary>
        /// <param name="remainingTime">current remaining time of the game timer</param>
        private void OnTimeChanged(float remainingTime)
        {
            playerProfileService.GetHUD().UpdateTime(remainingTime);
        }

        /// <summary>
        /// Event listener for integrity changes. Updates the integrity indicator on the HUD.
        /// </summary>
        /// <param name="integrityValue">new integrity value</param>
        private void OnIntegrityChanged(int integrityValue)
        {
            playerProfileService.GetHUD().UpdateIntegrityIndicator(integrity.GetCurrentIntegrityPercentage());
        }

        private void ChangeNavigation()
        {
            playerProfileService.GetHUD().ChangeNavigation(!playerProfileService.IsAltMarkerActive());
        }
    }
}
