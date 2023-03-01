// <copyright file="Jump.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine.State
{
    #region Usings

    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// Character jump state.
    /// </summary>
    public class Jump : TemplateState
    {
        #region Fields and Constants

        private bool jumping = true;
        private bool jumped = false;

        #endregion Fields and Constants

        #region Constructs and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Jump"/> class.
        /// </summary>
        /// <param name="player">
        /// The player GameObject.
        /// </param>
        /// <param name="stateMachine">
        /// The playe StateMachine.
        /// </param>
        public Jump(GameObject player, StateMachineClass stateMachine)
            : base(player, stateMachine)
        {
            this.CanMove = false;
        }

        #endregion Constructs and Destructors

        #region Public Methods

        /// <summary>
        /// State on entry method.
        /// </summary>
        public override void OnEntry()
        {
            base.OnEntry();
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = Vector3.zero;
            this.jumping = true;
            this.jumped = false;
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (this.jumping)
            {
                this.jumping = false;
                this.playerScript.isGround = false;
                this.rigidbody.AddForce(
                    new Vector3(
                        this.playerScript.GetPlayerSpeed().x * 0.65f,
                        1.5f,
                        this.playerScript.GetPlayerSpeed().y * 0.65f) * this.playerScript.Acc / 2,
                    ForceMode.Impulse);
            }

            if (this.playerScript.GetPlayerPosition().y > 1.5f)
            {
                this.jumped = true;
            }

            if (this.playerScript.isGround && this.jumped)
            {
                this.OnExit();
            }
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            this.playerScript.Rigidbody.velocity = Vector3.zero;
            this.playerScript.Rigidbody.angularVelocity = Vector3.zero;
            if (this.playerScript.ActionQueue.Count != 0)
            {
                if ((this.playerScript.ActionQueue.Peek().CharacterActionItem == 'z' && this.playerScript.GetPlayerSpeed().x != 0) ||
                    (this.playerScript.ActionQueue.Peek().CharacterActionItem == 'z' && this.playerScript.GetPlayerSpeed().y != 0))
                {
                    this.stateMachine.ChangeState(this.stateMachine.FastJump);
                }
                else
                {
                    this.stateMachine.ChangeState(this.stateMachine.Idle);
                }
            }
            else
            {
                this.stateMachine.ChangeState(this.stateMachine.Idle);
            }
        }

        #endregion Public Methods
    }
}