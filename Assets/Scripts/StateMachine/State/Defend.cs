// <copyright file="Defend.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine.State
{
    #region Usings

    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// Character defend state.
    /// </summary>
    public class Defend : TemplateState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Defend"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Defend(GameObject player, StateMachineClass stateMachine)
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
            this.animator.speed = 0.3f;
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (!this.playerScript.isDefending)
            {
                this.OnExit();
            }
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            this.stateMachine.ChangeState(this.stateMachine.Idle);
        }
        #endregion Public Methods
    }
}