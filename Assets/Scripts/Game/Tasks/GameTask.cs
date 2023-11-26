using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks
{
    
    /// <summary>
    /// Abstract game task, that provides basic game task logic and data fields for game tasks.
    /// In addition it defines methods and fields for notifying state changes using the observer pattern.
    ///
    /// Classes, which extend this class should not implement the unity update method, but instead implement
    /// the methods <see cref="BeforeStateCheck"/>, <see cref="CheckTaskState"/> and <see cref="AfterStateCheck"/>
    /// to update task logic and determine the current task state.
    /// </summary>
    public abstract class GameTask : MonoBehaviour
    {
        [SerializeField]
        [Delayed]
        public string taskName;

        [SerializeField] public int integrityValue = 5;

        protected TaskState currentTaskState;
        public event Action<GameTask> TaskSuccessful;
        public event Action<GameTask> TaskFailed;
        public event Action<GameTask> GameObjectDestroyed;

        /// <summary>
        /// List of game objects, that are linked to this task and should be destroyed, when this task is destroyed
        /// by the <see cref="DestroyTask"/> method.
        /// </summary>
        private readonly List<GameObject> m_LinkedGameObjects = new List<GameObject>();

        /// <summary>
        /// Initializes the game task. Called when the game task is created.
        /// </summary>
        public abstract void Initialize();
        
        /// <summary>
        /// This method is called every update before the <see cref="CheckTaskState"/> method invocation.
        /// 
        /// Can be used to implement task related logic and updates, that should be performed before the current task
        /// state is checked.
        /// </summary>
        protected abstract void BeforeStateCheck();
        
        /// <summary>
        /// Evaluation method, that determines the current task state.
        /// </summary>
        /// <returns>the current task state</returns>
        protected abstract TaskState CheckTaskState();
        
        /// <summary>
        /// This method is called every update after the <see cref="CheckTaskState"/> method invocation.
        /// 
        /// Can be used to update task related logic and updates, that should be performed after the current task
        /// state is checked. At this point, the task state may have changed according to the returned value of the last
        /// <see cref="CheckTaskState"/> method invocation. 
        /// </summary>
        protected abstract void AfterStateCheck();

        /// <summary>
        /// Template method based update procedure for processing game task logic and updating game task state
        /// </summary>
        protected virtual void Update()
        {
            BeforeStateCheck();
            UpdateTaskState(CheckTaskState());
            AfterStateCheck();
        }
        
        /// <summary>
        /// Updates the TaskState and performs a completion if the task is not ongoing anymore
        /// </summary>
        /// <param name="taskState">the new state of the game</param>
        protected void UpdateTaskState(TaskState taskState)
        {
            currentTaskState = taskState;
            switch (currentTaskState)
            {
                case TaskState.Failed:
                    TaskFailed?.Invoke(this);
                    break;
                case TaskState.Successful:
                    TaskSuccessful?.Invoke(this);
                    break;
                case TaskState.Ongoing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Adds the given game object to a list of game objects, which are linked to this task and will be destroyed,
        /// when this task is finished.
        /// </summary>
        /// <param name="gameObjectToLink">game object to be linked</param>
        public void AddLinkedGameObject(GameObject gameObjectToLink)
        {
            m_LinkedGameObjects.Add(gameObjectToLink);
        }
        
        /// <summary>
        /// Removes the given game object from the list of linked game objects
        /// </summary>
        /// <param name="gameObjectToUnlink">game object to be unlinked</param>
        public void RemoveLinkedGameObject(GameObject gameObjectToUnlink)
        {
            m_LinkedGameObjects.Remove(gameObjectToUnlink);
        }

        /// <summary>
        /// Removes the game object and all its linked game objects from the scene
        /// </summary>
        protected void DestroyTask()
        {
            GameObjectDestroyed?.Invoke(this);
            
            // destroy all linked game objects
            foreach (var linkedGameObject in m_LinkedGameObjects)
            {
                Destroy(linkedGameObject);
            }
            m_LinkedGameObjects.Clear();
            
            Destroy(gameObject);
        }
    }
}
