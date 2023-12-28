using System;
using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    /// <summary>
    /// manage the process of the task Storage Riddle
    /// </summary>
    public class StartStorageRiddle : TimerTask
    {
        // amount how many boxes should be spawned in
        private int _maxAmountDeliveryBoxes; 
        
        // references to the needed scripts to setup the amount of boxes in the task
        [SerializeField] private HandleBoxDelivery handleBoxDeliveryScript;
        [SerializeField] private SpawnBoxes spawnBoxesScript;
        
        public StartStorageRiddle() : base(initialTimerTime: 600f, taskName: "Storage Riddle", 
            taskDescription: "", integrityValue: 10)
        {
            taskDescription = "Someone misplaced the storage boxes!\n" +
                              "You need to place all of them on the green glowing platform.\n" +
                              "One of them contains the towel you need to survive the journey!";
        }
        
        public override void Initialize()
        {
            // sets the variables depending on the difficulty
            SetupDifficulty(); 
            
            // sets the variable in the subclasses
            handleBoxDeliveryScript.SetMaxAmountDeliveryBoxes(_maxAmountDeliveryBoxes);
            spawnBoxesScript.SetMaxAmountDeliveryBoxes(_maxAmountDeliveryBoxes);
        }

        protected override void BeforeStateCheck()
        {
            // no implementation needed
        }
        
        protected override TaskState CheckTaskState()
        {
            if (PlayerPrefs.GetString("CurrentPlayer").Equals("VR"))
            {
                return handleBoxDeliveryScript.IsTaskFinished() ? TaskState.Successful : TaskState.Ongoing;
            }

            return TaskState.Ongoing;
        }
        
        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }
        
        /// <summary>
        /// sets the amount how many boxes used in this task depending on the selected difficulty
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void SetupDifficulty()
        {
            switch (difficulty.GetSeparatedDifficulty())
            {
                case SeparatedDifficulty.Easy:
                    _maxAmountDeliveryBoxes = 3;
                    break;
                case SeparatedDifficulty.Medium:
                    _maxAmountDeliveryBoxes = 5;
                    break;
                case SeparatedDifficulty.Hard:
                    _maxAmountDeliveryBoxes = 7;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
