// <copyright file="JumpAttack.cs" company="GG-GrubsGaming">
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
    /// Character attack state.
    /// </summary>
    public class JumpAttack : TemplateState
    {
        #region Fields and Constants

        /// <summary>
        /// True if player attacked;
        /// </summary>
        private bool attacked = false;

        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JumpAttack"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public JumpAttack(GameObject player, StateMachineClass stateMachine)
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
            this.attacked = false;
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                if (this.playerScript.isGround)
                {
                    this.OnExit();
                }

                if (!this.attacked)
                {
                    this.playerScript.CreateAttackObject();
                    this.attacked = true;
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

        /// <summary>
        /// Check if player can change state from one to another.
        /// </summary>
        /// <param name="nextstate">
        /// State to change.
        /// </param>
        /// <returns>
        /// True if player can change state.
        /// </returns>
        public override bool CanChangeToState(TemplateState nextstate)
        {
            if (nextstate.GetType().Name == this.stateMachine.Run.GetType().Name)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion Public Methods
    }
}