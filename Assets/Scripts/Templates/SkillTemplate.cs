// <copyright file="SkillTemplate.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.Templates
{
    #region Usings

    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// Character idle state.
    /// </summary>
    [System.Serializable]
    public class SkillTemplate
    {
        #region Fields and Constants
#pragma warning disable SA1401 // Fields should be private

        /// <summary>
        /// The player name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Key combination to perform state.
        /// </summary>
        public string Input;

        /// <summary>
        /// The mana cost of state.
        /// </summary>
        public int Mana;

#pragma warning restore SA1401 // Fields should be private

        #endregion Fields and Constants

        #region Constructors and Destructors
        #endregion Constructors and Destructors

        #region Public Methods
        #endregion Public Methods
    }
}