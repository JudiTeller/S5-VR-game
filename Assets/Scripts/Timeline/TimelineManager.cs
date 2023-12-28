using Game;
using PlayerController;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Timeline 
{
    /// <summary>
    /// manages the correct play of the timelines and sets the player up
    /// before and after playing it
    /// </summary>
     public class TimelineManager : MonoBehaviour
    {
        // stores the game-objects of the used timelines 
        [SerializeField] private GameObject startSceneGameObject;
        [SerializeField] private GameObject endSceneLoseSceneGameObject;
        [SerializeField] private GameObject endSceneWinSceneGameObject;
        
        // stores the position the player should be teleported to
        [SerializeField] private Transform startScenePlayerTransform;
        [SerializeField] private Transform endSceneLosePlayerTransform;
        [SerializeField] private Transform endSceneWinPlayerTransform;

        // stores the playable director of each timeline
        private PlayableDirector _startSceneDirector;
        private PlayableDirector _endSceneLoseDirector;
        private PlayableDirector _endSceneWinDirector;
        
        [SerializeField] private PlayerProfileService playerProfileService;
        
        [SerializeField] private GameTimer gameTimer;

        [SerializeField] private GameInformation gameInformation;
        
        private void Start()
        {
            // gets each playable director in runtime
            _startSceneDirector = startSceneGameObject.GetComponent<PlayableDirector>();
            _endSceneLoseDirector = endSceneLoseSceneGameObject.GetComponent<PlayableDirector>();
            _endSceneWinDirector = endSceneWinSceneGameObject.GetComponent<PlayableDirector>();
            
            gameInformation.OnGameStateChanged += state => {
                if(state == GameState.GameLost) PlayEndSceneLose();
            };
            
            PlayStartScene(); // plays the start-scene at the begin of the game
        }
        
        /// <summary>
        /// start the timeline at the start of the game
        /// </summary>
        public void PlayStartScene()
        {
            SetupPlayerBeforeTimeline(startScenePlayerTransform.position);

            playerProfileService.transform.rotation = Quaternion.Euler(new Vector3(0, PlayerPrefs.GetFloat("CameraDirection"),0));
            
            _startSceneDirector.Play(); // starts the timeline
            
            gameTimer.PauseTimer(); // stops the game-timer for the starting timeline

            _startSceneDirector.stopped += SetupAfterStartTimeline; // adds event listener to call function when timeline is over
            _startSceneDirector.stopped += ActivateVRMovement;
        }

        /// <summary>
        /// start the timeline at the end of the game, if the player loses
        /// </summary>
        private void PlayEndSceneLose()
        {
            SetupPlayerBeforeTimeline(endSceneLosePlayerTransform.position);
            
            _endSceneLoseDirector.Play(); // starts the timeline

            _endSceneLoseDirector.stopped += SetupAfterEndTimeline; // adds event listener to call function when timeline is over
        }
        
        /// <summary>
        /// start the timeline at the end of the game, if the player wins
        /// </summary>
        public void PlayEndSceneWin()
        {
            SetupPlayerBeforeTimeline(endSceneWinPlayerTransform.position);
            
            _endSceneWinDirector.Play(); // starts the timeline

            _endSceneWinDirector.stopped += SetupAfterEndTimeline; // adds event listener to call function when timeline is over
        }

        /// <summary>
        /// sets up the player before the timeline
        /// </summary>
        /// <param name="timelinePosition"></param>
        private void SetupPlayerBeforeTimeline(Vector3 timelinePosition)
        {
            var playerTransform = playerProfileService.transform;
            
            playerTransform.position = timelinePosition; // moves the player's position to the timeline
            playerProfileService.GetHUD().GetComponent<Canvas>().enabled = false; // deactivates the hud
            playerProfileService.SetVRMovementActive(false); // deactivates the movement
        }

        /// <summary>
        /// sets up the player after the starting timeline 
        /// </summary>
        /// <param name="playableDirector"></param>
        private void SetupAfterEndTimeline(PlayableDirector playableDirector)
        {
            SceneManager.LoadScene(0); // loads the main-menu
        }
        
        /// <summary>
        /// sets up the player after the ending timeline
        /// </summary>
        /// <param name="playableDirector"></param>
        private void SetupAfterStartTimeline(PlayableDirector playableDirector)
        {
            playerProfileService.transform.position = new Vector3(0, 0, 0); // moves the player's position to its original position
            playerProfileService.GetHUD().GetComponent<Canvas>().enabled = true; // activates the hud
            gameTimer.ResumeTimer(); // resumes the timer
        }

        /// <summary>
        /// activates vr-movement after function event call
        /// </summary>
        /// <param name="playableDirector"></param>
        private void ActivateVRMovement(PlayableDirector playableDirector)
        {
            playerProfileService.SetVRMovementActive(true); // activates the movement
        }
    }
}