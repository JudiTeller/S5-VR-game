using System;
using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    /// <summary>
    /// Script to manage the task Energy Core
    /// </summary>
    public class StartEnergyCoreTask : TimerTask
    {
        // variable to count the finished energy cores (energy core collided with right energy cell)
        [SerializeField] private int finishedEnergyCoreCounter;
        
        public StartEnergyCoreTask() : base(initialTimerTime : 70f, taskName: "Energy Core", taskDescription: "Energy Core",
            integrityValue : 10)
        {
        }

        public override void Initialize()
        {
            taskDescription = "The reactor is not producing enough energy!\n" +
                              "You need to place the energy cells in the correct cores.\n" +
                              "But be careful, you can only move one at a time!";
            
            // sets the starting time depending on the used difficulty
            remainingTime = difficulty.GetSeparatedDifficulty() switch
            {
                SeparatedDifficulty.Easy => 90f,
                SeparatedDifficulty.Medium => 70f,
                SeparatedDifficulty.Hard => 50f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override void BeforeStateCheck()
        {
            // no implementation needed
        }

        protected override TaskState CheckTaskState()
        {
            // 6 = all cells are placed in the right core
            return finishedEnergyCoreCounter >= 6 ? TaskState.Successful : TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }

        /// <summary>
        /// increments the value of finishedEnergyCoreCounter by 1
        /// </summary>
        public void IncrementFinishedEnergyCoreCounter()
        {
            finishedEnergyCoreCounter++;
        }
    }
}
