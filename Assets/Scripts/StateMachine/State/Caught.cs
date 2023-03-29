// <copyright file="Catching.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine.State
{
    #region Usings

    using Assets.Scripts.StateMachine;
    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// Character Caught state.
    /// </summary>
    public class Caught : TemplateState
    {
        #region Fields and Constants
        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Caught"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Caught(GameObject player, StateMachineClass stateMachine)
            : base(player, stateMachine)
        {
        }

        #endregion Constructors and Destructors

        #region Public Methods

        /// <summary>
        /// State on entry method.
        /// </summary>
        public override void OnEntry()
        {
            base.OnEntry();
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (this.playerScript.isGettingHit)
            {
                this.animator.Play(this.Name, 0, 0);
                this.playerScript.isGettingHit = false;
            }

            if (!this.playerScript.isCaught)
            {
                this.OnExit();
            }
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            this.stateMachine.ChangeState(this.stateMachine.CaughtFalling);
        }

        #endregion Public Methods
    }
}