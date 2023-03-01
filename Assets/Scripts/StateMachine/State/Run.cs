// <copyright file="Run.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine.State
{
    #region Usings

    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// Character run state.
    /// </summary>
    public class Run : TemplateState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Run"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Run(GameObject player, StateMachineClass stateMachine)
            : base(player, stateMachine)
        {
        }

        #endregion Constructors and Destructors

        #region Public Methods

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (this.playerScript.GetPlayerSpeed().x == 0)
            {
                this.OnExit();
            }
        }
        #endregion Public Methods
    }
}