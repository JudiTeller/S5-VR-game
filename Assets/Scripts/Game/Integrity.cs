using System;
using Logging;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Class that represents the Integrity-Value which can be changed anytime.
    /// </summary>
    public class Integrity : MonoBehaviour
    {
        private readonly Logger m_LOG = new Logger(new LogHandler());
        private const string LOGTag = "Integrity";
        public event Action<int> OnIntegrityChanged;

        [SerializeField] private int integrityValue = 100;

        /// <summary>
        /// Delivers the current Integrity-value so that it can be
        /// displayed on the UI later on.
        /// </summary>
        /// <returns>The integrity-value that represents the game score</returns>
        public int GetCurrentIntegrity()
        {
            return this.integrityValue;
        }

        /// <summary>
        /// Increments the integrityValue of the game.
        /// </summary>
        /// <param name="value">The operand for the addition</param>
        public void IncrementIntegrity(int value)
        {
            integrityValue += value;
            NotifyScoreChanged();
        }

        /// <summary>
        /// Decrements the integrityValue of the game.
        /// </summary>
        /// <param name="value">The operand for the subtraction</param>
        public void DecrementIntegrity(int value)
        {
            integrityValue -= value;
            NotifyScoreChanged();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// This method is invoked if something changed on the integrityValue.
        /// </summary>
        private void NotifyScoreChanged()
        {
            m_LOG.Log(LOGTag, "new integrity value: " + integrityValue);
            OnIntegrityChanged?.Invoke(integrityValue);
        }
    }
}