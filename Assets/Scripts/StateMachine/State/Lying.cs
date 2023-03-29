// <copyright file="Lying.cs" company="GG-GrubsGaming">
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
    /// Character caught falling state.
    /// </summary>
    public class Lying : TemplateState
    {
        #region Fields and Constants
        public float LyingMaxTime = 2f;
        public float LyingStart;
        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Lying"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Lying(GameObject player, StateMachineClass stateMachine)
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
            this.LyingStart = Time.time;
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f
                && this.animator.GetCurrentAnimatorStateInfo(0).IsName(this.Name))
            {
                if (this.playerScript.ActionQueue.Count != 0)
                {
                    this.stateMachine.ChangeState(this.stateMachine.Idle);
                }
                else if (Time.time - this.LyingStart > this.LyingMaxTime)
                {
                    this.OnExit();
                }
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