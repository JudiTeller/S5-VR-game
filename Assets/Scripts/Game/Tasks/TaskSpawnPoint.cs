using PlayerController;
using UnityEngine;

namespace Game.Tasks
{
    /// <summary>
    /// Organizes a spawn point to keep track of the current state, whether it is occupied or not.
    /// As this script extends from <see cref="MonoBehaviour"/>, the position of the according game object can serve
    /// as the spawn point position. The position vector can be obtained via <see cref="GetSpawnPosition"/>
    /// This class also manages the player nearby check using a custom or default collider to display the task
    /// description on the HUD, when the player is nearby.
    /// </summary>
    public class TaskSpawnPoint : MonoBehaviour
    {
        /// <summary>
        /// Default size of the box collider, that is used for player nearby check.
        /// If no custom collider is set this vector determines the size of the default box collider.
        /// </summary>
        private static readonly Vector3 DefaultPlayerNearbyColliderSize = new Vector3(3, 3, 3);

        /// <summary>
        /// Can be used to setup a custom collider, that should be used for player nearby check.
        /// If the player is entering this collider, the task description will be shown on the HUD.
        /// If the player is leaving this collider, the task description will be dismissed.
        /// If not set, a default box collider will be set at the position of the game task with a fixed size, which is
        /// determined by the constant <see cref="DefaultPlayerNearbyColliderSize"/>.
        /// </summary>
        [SerializeField] protected Collider playerNearbyCollider;
        
        /// <summary>
        /// Keeps track of the current allocated task. Can be null, if no task is allocated currently.
        /// </summary>
        private GameTask m_AllocatedTask;

        private float m_Timeout;
        private System.DateTime lastDeallocateTime;

        /// <summary>
        /// Returns true, if this spawn point is not occupied by a game task and is not blocked by the
        /// <see cref="m_Timeout"/>.
        /// </summary>
        public bool CanBeAllocated()
        {
            // check if blocked time is passed since last deallocate time
            var isBlocked = (System.DateTime.UtcNow - lastDeallocateTime).TotalSeconds < m_Timeout;
            var isAllocated = m_AllocatedTask != null;

            return !isAllocated && !isBlocked;
        }
        
        private void Start()
        {
            // Set up the collider for the player nearby check. Add a default box collider with a fixed size, if no 
            // custom collider is setup for this spawn point
            if (playerNearbyCollider == null)
            {
                playerNearbyCollider = gameObject.AddComponent<BoxCollider>();
                ((BoxCollider) playerNearbyCollider).size = DefaultPlayerNearbyColliderSize;
            }
            
            // set collider to trigger to not block player movement
            playerNearbyCollider.isTrigger = true;
        }
        

        /// <summary>
        /// Marks the spawn point as occupied and registers the <see cref="GameTask.GameObjectDestroyed"/> action to be
        /// observed by the <see cref="Deallocate"/> function. This allows this spawn point to be automatically
        /// deallocated, when the game task object is destroyed using the <see cref="GameTask.DestroyTask"/> method.
        /// </summary>
        /// <param name="gameTask">game task, which occupies this spawn point</param>
        public void Allocate(GameTask gameTask)
        {
            m_AllocatedTask = gameTask;
            gameTask.spawnPoint = this;
            gameTask.GameObjectDestroyed += Deallocate;
        }

        /// <summary>
        /// Marks the spawn point as not-occupied.
        /// </summary>
        public void Deallocate(GameTask gameTask)
        {
            m_AllocatedTask = null;
            gameTask.spawnPoint = null;
            lastDeallocateTime = System.DateTime.UtcNow;
        }
        
        /// <summary>
        /// Provides the position vector of this spawn point
        /// </summary>
        /// <returns>position vector where to spawn the game task</returns>
        public Vector3 GetSpawnPosition()
        {
            return transform.position;
        }

        /// <summary>
        /// Provides the rotation vector of this spawn point
        /// </summary>
        /// <returns>rotation of this spawn point</returns>
        public Quaternion GetRotation()
        {
            return transform.rotation;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // check if other collider is player
            if (m_AllocatedTask != null && other.CompareTag(PlayerProfileService.k_PlayerGameObjectTag))
            {
                // show task description on HUD
                m_AllocatedTask.playerProfileService.GetHUD().ChangeText(m_AllocatedTask.taskDescription);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // check if other collider is player
            if (m_AllocatedTask != null && other.CompareTag(PlayerProfileService.k_PlayerGameObjectTag))
            {
                // dismiss task description on HUD
                m_AllocatedTask.playerProfileService.GetHUD().DismissText();
            }
        }

        /// <summary>
        /// Sets the timout for this spawn point. The timeout determines, how long a spawn point will be blocked
        /// before a new task can be allocated.
        /// </summary>
        /// <param name="timeout"></param>
        public void SetTimeout(float timeout)
        {
            m_Timeout = timeout;
        }
        
    }
}