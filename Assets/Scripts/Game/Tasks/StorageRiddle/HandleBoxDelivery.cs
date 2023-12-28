using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    /// <summary>
    /// class handles what is happening when the player puts a box onto the delivery-platform
    /// </summary>
    public class HandleBoxDelivery : MonoBehaviour
    {
        // value how many boxes the player needs to bring to the game-object
        private int _maxAmountDeliveryBoxes; 
        
        // current value of the delivered boxes
        private int _amountDeliveredBoxes; 
        
        // bool to check if the win-condition is reached 
        private bool _taskDone; 

        /// <summary>
        /// collision event
        /// if Box is placed on the platform where it should be delivered to:
        /// - increments the amount of delivered boxes by 1
        /// - destroys the box
        /// if amount all boxes are delivered -> sets _taskDone to true
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (_taskDone) return;
            
            if (collision.gameObject.CompareTag("DeliveryBox")) // when the game-object touches one of the boxes
            {
                _amountDeliveredBoxes++; 
                Destroy(collision.gameObject);
            }

            // sets the task to done when the condition is true
            if (_amountDeliveredBoxes >= _maxAmountDeliveryBoxes)
            {
                _taskDone = true;
            }
        }

        /// <summary>
        /// returns if the player is done with the task
        /// </summary>
        /// <returns></returns>
        public bool IsTaskFinished()
        {
            return _taskDone;
        }

        /// <summary>
        /// sets the amount of the boxes which should be placed on the delivery-platform to the given value
        /// </summary>
        /// <param name="value"></param>
        public void SetMaxAmountDeliveryBoxes(int value)
        {
            _maxAmountDeliveryBoxes = value;
        }
    }
}
