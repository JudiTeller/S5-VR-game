using System;

namespace Game.Tasks.EnergyCore
{
    // control-script of the energycore-task
    public class StartEnergyCoreTask : GameTask
    {
        public int finishedEnergyCoreCounter;
        
        public StartEnergyCoreTask() : base( taskName: "Energy Core", taskDescription: "Energy Core",
            integrityValue : 10)
        {
        }

        public override void Initialize()
        {
            // no implementation needed anymore
        }

        protected override void BeforeStateCheck()
        {
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
    }
}
