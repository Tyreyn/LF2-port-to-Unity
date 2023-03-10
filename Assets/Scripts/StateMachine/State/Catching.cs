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
    /// Character attack state.
    /// </summary>
    public class Catching : TemplateState
    {
        #region Fields and Constants
        public int AnimCount;
        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Catching"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Catching(GameObject player, StateMachineClass stateMachine)
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
            this.animator.speed = 0f;
            this.animator.Play(this.name, 0, 0);
            this.AnimCount = 0;
            this.playerScript.isAttacking = false;
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            Debug.Log(this.AnimCount);

            if (this.playerScript.isAttacking
                && this.playerScript.ActionQueue.Count != 0
                && this.playerScript.ActionQueue.Peek().CharacterActionItem == 'A')
            {
                this.animator.speed = 0.6f;
            }

            if (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f
                && this.animator.GetCurrentAnimatorStateInfo(0).IsName(this.name))
            {
                this.AnimCount++;
                this.animator.speed = 0;
                this.animator.Play(this.name, 0, 0);
                this.playerScript.isAttacking = false;
                if (this.AnimCount >= 3)
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
            this.playerScript.isAttacking = false;
            this.playerScript.isCatching = false;
            this.stateMachine.ChangeState(this.stateMachine.Idle);
        }

        #endregion Public Methods
    }
}