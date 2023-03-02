// <copyright file="FastJump.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine.State
{
    #region Usings

    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// Character FastJump state.
    /// </summary>
    public class FastJump : TemplateState
    {
        #region Fields and Constants

        private bool jumping = true;
        private bool jumped = false;

        #endregion Fields and Constants

        /// <summary>
        /// Initializes a new instance of the <see cref="FastJump"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public FastJump(GameObject player, StateMachineClass stateMachine)
            : base(player, stateMachine)
        {
            this.CanMove = false;
        }

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
                this.rigidbody.AddForce(this.CheckDirection() * this.playerScript.Acc / 2, ForceMode.Impulse);
            }

            if (this.playerScript.GetPlayerPosition().y > 1f)
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
            this.stateMachine.ChangeState(this.stateMachine.Idle);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Check if direction of fast jump is proper
        /// because player can not only up and down in this state.
        /// </summary>
        /// <returns>
        /// Proper direction.
        /// </returns>
        private Vector3 CheckDirection()
        {
            float properDirection = this.playerScript.GetPlayerSpeed().x;

            // If player want to jump up, change it to slant
            if (this.playerScript.GetPlayerSpeed().y != 0 && this.playerScript.GetPlayerSpeed().x == 0)
            {
                properDirection = this.playerScript.SpriteRenderer.flipX ? -1 : 1;
            }

            return new Vector3(
                properDirection * 0.95f,
                1.25f,
                this.playerScript.GetPlayerSpeed().y * 0.95f);
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
        #endregion Private Methods
    }
}