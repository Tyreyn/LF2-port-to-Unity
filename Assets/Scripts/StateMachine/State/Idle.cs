// <copyright file="Idle.cs" company="GG-GrubsGaming">
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
    public class Idle : TemplateState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Idle"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Idle(GameObject player, StateMachineClass stateMachine)
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
            this.playerScript.isJumping = false;
            this.playerScript.Rigidbody.velocity = Vector3.zero;
            this.playerScript.Rigidbody.angularVelocity = Vector3.zero;

            if (Mathf.Abs(this.playerScript.GetPlayerSpeed().x) != 0
                || Mathf.Abs(this.playerScript.GetPlayerSpeed().y) != 0)
            {
                this.stateMachine.ChangeState(this.stateMachine.Walk);
            }

            if (this.playerScript.ActionQueue.Count != 0)
            {
                if (this.playerScript.ActionQueue.Peek().CharacterActionItem == 'A'
                    && this.playerScript.isAttacking)
                {
                    if (this.playerScript.CheckHeadRaycast.collider != null
                        && this.playerScript.CheckLegRaycast.collider != null)
                    {
                        if (this.playerScript.CheckHeadRaycast.collider.gameObject == this.playerScript.CheckLegRaycast.collider.gameObject)
                        {
                            this.playerScript.isCatching = true;
                            this.stateMachine.ChangeState(this.stateMachine.Catching);
                        }
                    }
                    else if (this.playerScript.isObject)
                    {
                        // TODO
                    }
                    else
                    {
                        this.stateMachine.ChangeState(this.stateMachine.Attack);
                    }

                }
                else if (this.playerScript.ActionQueue.Peek().CharacterActionItem == 'J'
                    && this.playerScript.isGround
                    && this.playerScript.canJump)
                {
                    this.stateMachine.ChangeState(this.stateMachine.Jump);
                }
                else if (this.playerScript.ActionQueue.Peek().CharacterActionItem == 'D'
                    && this.playerScript.isDefending)
                {
                    this.stateMachine.ChangeState(this.stateMachine.Defend);
                }
            }
        }

        #endregion Public Methods
    }
}