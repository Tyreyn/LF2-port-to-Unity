namespace Assets.Scripts.InputSystem
{
    #region Usings

    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// The player input action handler.
    /// </summary>
    public class CharacterActionHandler
    {
        #region Fields and Constants

        /// <summary>
        /// The performed key code action.
        /// </summary>
        public char CharacterActionItem;

        /// <summary>
        /// The time when action was performed.
        /// </summary>
        public float Timestamp;

        /// <summary>
        /// The Player Script.
        /// </summary>
        public Player PlayerScript;

        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Class is handling controller input.
        /// </summary>
        /// <param name="action">
        /// The performed action.
        /// </param>
        /// <param name="timestamp">
        /// The time when action was performed.
        /// </param>
        public CharacterActionHandler(char action, float timestamp)
        {
            this.CharacterActionItem = action;
            this.Timestamp = timestamp;
            this.PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        #endregion Constructors and Destructors

        #region Public Methods

        /// <summary>
        /// Check if action is expired.
        /// </summary>
        /// <returns>
        /// True if action is expired
        /// </returns>
        public bool CheckIfExpired()
        {
            bool isExpired = false;
            if (Time.time - this.Timestamp >= this.PlayerScript.TimeBeforActionExpire)
            {
                isExpired = true;
            }

            return isExpired;
        }

        #endregion Public Methods
    }
}