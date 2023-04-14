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
    /// Character Caught state.
    /// </summary>
    public class Caught : TemplateState
    {
        #region Fields and Constants
        public int AnimCount;

        public bool oneCount;
        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Caught"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Caught(GameObject player, StateMachineClass stateMachine)
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
            this.CanMove = false;
            this.AnimCount = 0;
            this.oneCount = false;
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (this.playerScript.isGettingHit)
            {
                this.animator.Play(this.Name, 0, 0);
                this.playerScript.isGettingHit = false;
                this.oneCount = true;
            }

            if (this.oneCount)
            {
                this.oneCount = false;
                this.AnimCount++;
            }

            if (!this.playerScript.isCaught)
            {
                this.OnExit();
            }

            Debug.Log("Caught " + this.AnimCount);

        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            if (this.AnimCount >= 3)
            {
                this.stateMachine.ChangeState(this.stateMachine.CaughtFalling);
            }
            else
            {
                this.stateMachine.ChangeState(this.stateMachine.Idle);
            }
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
            if (nextstate.GetType().Name == this.stateMachine.Hit.GetType().Name)
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