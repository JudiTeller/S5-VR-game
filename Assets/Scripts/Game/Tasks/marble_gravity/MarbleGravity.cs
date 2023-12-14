using UnityEngine;
using UnityEngine.XR;

namespace Game.Tasks.marble_gravity
{
    /// <summary>
    /// This class represents the MarbleGravityRiddle which can
    /// be done in the Unity-Game
    /// </summary>
    public class MarbleGravity : GameTask
    {
        public GameObject taskPrefab;
        private bool _isFinished;
        private const float RotationSpeed = 50.0f;
        private const float ControlDistance = 4.0f;

        public MarbleGravity() : base("Marble Gravity :)", "solve the marble riddle \n" +
                                                           "And reach the blue cube.", 10)
        {
            
        }

        public override void Initialize()
        {
            // no implementation required
        }

        protected override void BeforeStateCheck()
        {
            if (Vector3.Distance(playerProfileService.GetPlayerGameObject().transform.position, transform.position) > ControlDistance)
            {
                return;
            }
            if (!InputDevices.GetDeviceAtXRNode(XRNode.RightHand).isValid)
            {
                return;
            }
            if (Input.GetKey(KeyCode.JoystickButton0))
            {
                transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.JoystickButton1))
            {
                transform.Rotate(Vector3.up, -RotationSpeed * Time.deltaTime);
            }
        }

        protected override TaskState CheckTaskState()
        {
            
            return _isFinished ? TaskState.Successful : TaskState.Ongoing;
        }

        protected override void AfterStateCheck()
        {
            if (currentTaskState != TaskState.Ongoing)
            {
                DestroyTask();
            }
        }

        public void SetFinished(bool newFinished)
        {
            _isFinished = newFinished;
        }
    }
}