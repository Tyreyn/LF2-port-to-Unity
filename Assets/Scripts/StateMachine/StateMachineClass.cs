// <copyright file="StateMachineClass.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine
{
    #region Usings

    using Assets.Scripts.StateMachine.State;
    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// The state machine handling player states.
    /// </summary>
    public class StateMachineClass
    {
        #region Fields and Constants

#pragma warning disable SA1401 // Fields should be private

        /// <summary>
        /// The player walk state.
        /// </summary>
        public Walk Walk;

        /// <summary>
        /// The player jump state.
        /// </summary>
        public Jump Jump;

        /// <summary>
        /// The player idle state.
        /// </summary>
        public Idle Idle;

        /// <summary>
        /// The player fastJump state.
        /// </summary>
        public FastJump FastJump;

        /// <summary>
        /// The player attack state.
        /// </summary>
        public Attack Attack;

        /// <summary>
        /// The player run state.
        /// </summary>
        public Run Run;

        /// <summary>
        /// The player current state.
        /// </summary>
        public TemplateState CurrentState;

        /// <summary>
        /// The player old state.
        /// </summary>
        public TemplateState OldState;

#pragma warning restore SA1401 // Fields should be private

        /// <summary>
        /// The player GameObject.
        /// </summary>
        private readonly GameObject player;

        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachineClass"/> class.
        /// </summary>
        public StateMachineClass()
        {
            this.player = GameObject.FindGameObjectWithTag("Player");
            this.Walk = new Walk(this.player, this);
            this.Jump = new Jump(this.player, this);
            this.Idle = new Idle(this.player, this);
            this.FastJump = new FastJump(this.player, this);
            this.Attack = new Attack(this.player, this);
            this.Run = new Run(this.player, this);
        }

        #endregion Constructors and Destructors

        #region Public Methods

        /// <summary>
        /// Set StateMachine state.
        /// </summary>
        /// <param name="currentState">
        /// The current state.
        /// </param>
        public void SetState(TemplateState currentState)
        {
            this.CurrentState = currentState;
        }

        /// <summary>
        /// Change StateMachine state.
        /// </summary>
        /// <param name="nextState">
        /// The state to set.
        /// </param>
        public void ChangeState(TemplateState nextState)
        {
            this.OldState = this.CurrentState;
            this.CurrentState = nextState;
            if (this.CurrentState != this.OldState)
            {
                this.CurrentState.OnEntry();
            }
        }

        /// <summary>
        /// Show current state.
        /// </summary>
        /// <returns>
        /// Current state.
        /// </returns>
        public TemplateState ShowCurrentState()
        {
            return this.CurrentState;
        }

        /// <summary>
        /// Show previous state.
        /// </summary>
        /// <returns>
        /// previous state.
        /// </returns>
        public TemplateState ShowPreviousState()
        {
            return this.OldState;
        }

        /// <summary>
        /// Show current state name.
        /// </summary>
        /// <returns>
        /// Current state name.
        /// </returns>
        public string ShowCurrentStateName()
        {
            return this.CurrentState.GetType().Name;
        }

        /// <summary>
        /// Show that player can move.
        /// </summary>
        /// <returns>
        /// True if player can move in current state.
        /// </returns>
        public bool CanPlayerMove()
        {
            return this.CurrentState.CanMove;
        }

        /// <summary>
        /// The main StateMachine method.
        /// </summary>
        public void DoState()
        {
            this.CurrentState.DoState();
        }

        #endregion Public Methods
    }
}