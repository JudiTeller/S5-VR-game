using System;
using Logging;
using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    // class used to manage the process of the task
    public class StartStorageRiddle : GameTask
    {
        // Logger
        private Logger m_LOG = new (new LogHandler());
        private const string LOGTag = "StorageRiddle";
        
        private int _maxAmountDeliveryBoxes; // amount how many boxes should be spawned in
        
        public HandleBoxDelivery handleBoxDeliveryScript;
        public SpawnBoxes spawnBoxesScript;
        
        public StartStorageRiddle() : base(taskName: "Storage Riddle", 
            taskDescription: "Bring the boxes to the green light.", integrityValue: 10)
        {
        }
        
        public override void Initialize()
        {
            SetupDifficulty(); // sets the variables depending on the difficulty
            
            // sets the variable in the subclasses
            handleBoxDeliveryScript.maxAmountDeliveryBoxes = _maxAmountDeliveryBoxes;
            spawnBoxesScript.maxAmountDeliveryBoxes = _maxAmountDeliveryBoxes;
        }

        protected override void BeforeStateCheck()
        {
        }

        // sets the state of the task
        // if isTaskedFinished() returns true: TaskState.Successful
        // else: TaskState.Ongoing
        protected override TaskState CheckTaskState()
        {
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                return handleBoxDeliveryScript.IsTaskFinished() ? TaskState.Successful : TaskState.Ongoing;
            }

            return TaskState.Ongoing;
        }

        // destroys task if it is done
        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }
        
        // sets the amount how many boxes used in this task depending on the selected difficulty
        private void SetupDifficulty()
        {
            switch (difficulty.GetSeparatedDifficulty())
            {
                case SeparatedDifficulty.Easy:
                    _maxAmountDeliveryBoxes = 3;
                    m_LOG.Log(LOGTag,"Amount of boxes: " + _maxAmountDeliveryBoxes);
                    break;
                case SeparatedDifficulty.Medium:
                    _maxAmountDeliveryBoxes = 5;
                    m_LOG.Log(LOGTag,"Amount of boxes: " + _maxAmountDeliveryBoxes);
                    break;
                case SeparatedDifficulty.Hard:
                    _maxAmountDeliveryBoxes = 7;
                    m_LOG.Log(LOGTag,"Amount of boxes: " + _maxAmountDeliveryBoxes);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
