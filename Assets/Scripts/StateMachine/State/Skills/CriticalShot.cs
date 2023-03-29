// <copyright file="Skill3.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine.State
{
    #region Usings

    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// Character idle state.
    /// </summary>
    public class CriticalShot : TemplateState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalShot"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public CriticalShot(GameObject player, StateMachineClass stateMachine)
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
            //Debug.Log(this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            //Debug.Log(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
            if (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f
                && this.animator.GetCurrentAnimatorStateInfo(0).IsName(this.Name))
            {
                this.OnExit();
            }
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            this.playerScript.isAttacking = false;
            this.stateMachine.ChangeState(this.stateMachine.Idle);
        }

        #endregion Public Methods
    }
}