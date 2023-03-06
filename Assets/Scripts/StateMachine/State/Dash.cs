// <copyright file="Dash.cs" company="GG-GrubsGaming">
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
    /// Character dash attack state.
    /// </summary>
    public class Dash : TemplateState
    {
        #region Fields and Constants
        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dash"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Dash(GameObject player, StateMachineClass stateMachine)
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
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = Vector3.zero;
            this.rigidbody.AddForce(
                    new Vector3(
                        this.playerScript.GetPlayerSpeed().x * 1f,
                        0,
                        this.playerScript.GetPlayerSpeed().y * 1f) * this.playerScript.Acc,
                    ForceMode.Impulse);
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f
                && this.animator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            {
                this.OnExit();
            }
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            this.playerScript.isDefending = false;
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = Vector3.zero;
            this.stateMachine.ChangeState(this.stateMachine.Idle);
        }

        #endregion Public Methods
    }
}