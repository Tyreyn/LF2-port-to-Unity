// <copyright file="Walk.cs" company="GG-GrubsGaming">
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
    /// Character walk state.
    /// </summary>
    public class Walk : TemplateState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Walk"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Walk(GameObject player, StateMachineClass stateMachine)
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
            if (this.playerScript.GetPlayerSpeed().x == 0 && this.playerScript.GetPlayerSpeed().y == 0)
            {
                this.stateMachine.ChangeState(this.stateMachine.Idle);
            }

            if (this.playerScript.ActionQueue.Count != 0)
            {
                if (this.playerScript.ActionQueue.Peek().CharacterActionItem == 'J'
                        && this.playerScript.isJumping)
                {
                    this.stateMachine.ChangeState(this.stateMachine.Jump);
                }

                if (this.playerScript.ActionQueue.Peek().CharacterActionItem == 'D'
                        && this.playerScript.isDefending)
                {
                    this.stateMachine.ChangeState(this.stateMachine.Defend);
                }
            }

            Vector3 playerPosition = this.playerScript.GetPlayerPosition();
            this.rigidbody.MovePosition(
                new Vector3(
                    playerPosition.x + (this.playerScript.GetPlayerSpeed().x * Time.deltaTime * this.playerScript.Acc),
                    playerPosition.y,
                    playerPosition.z + (this.playerScript.GetPlayerSpeed().y * Time.deltaTime * this.playerScript.Acc)));
        }

        #endregion Public Methods
    }
}