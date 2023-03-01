// <copyright file="CharacterComboItem.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.InputSystem
{
    #region Methods

    using Assets.Scripts.Templates;

    #endregion Methods

    /// <summary>
    /// Available character states.
    /// </summary>
    public class CharacterComboItem
    {
        #region Fields and Constants

        /// <summary>
        /// Priority of combo.
        /// </summary>
        private readonly int priority;

        /// <summary>
        /// Combo name.
        /// </summary>
        private readonly TemplateState moveName;

        /// <summary>
        /// List with key code to make combo.
        /// </summary>
        private readonly string moveKeysCode;

        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterComboItem"/> class.
        /// </summary>
        /// <param name="priority">
        /// Move priority.
        /// </param>
        /// <param name="moveName">
        /// Move name.
        /// </param>
        /// <param name="moveKeysCode">
        /// What press to make combo.
        /// </param>
        public CharacterComboItem(int priority, TemplateState moveName, string moveKeysCode)
        {
            this.priority = priority;
            this.moveName = moveName;
            this.moveKeysCode = moveKeysCode;
        }

        #endregion Constructors and Destructors

        #region Public Methods

        /// <summary>
        /// Get move priority.
        /// </summary>
        /// <returns>
        /// Move priority.
        /// </returns>
        public int GetPriority()
        {
            return this.priority;
        }

        /// <summary>
        /// Get move state name.
        /// </summary>
        /// <returns>
        /// Move state name.
        /// </returns>
        public TemplateState GetName()
        {
            return this.moveName;
        }

        /// <summary>
        /// Get keys code list.
        /// </summary>
        /// <returns>
        /// Keys code list to perform combo.
        /// </returns>
        public string GetMoveKeysCode()
        {
            return this.moveKeysCode;
        }

        #endregion Public Methods
    }
}