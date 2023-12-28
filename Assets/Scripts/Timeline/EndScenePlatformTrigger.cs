using Game;
using UnityEngine;

namespace Timeline
{
    public class EndScenePlatformCollision : MonoBehaviour
    {
        [SerializeField] private TimelineManager timelineManager;

        [SerializeField] private GameInformation gameInformation;

        /// <summary>
        /// function called when player won the game and enters the game-object with this script
        /// to start the endscene
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player") && gameInformation.currentGameState == GameState.GameWon)
            {
                timelineManager.PlayEndSceneWin();
            }
        }
    }
}
