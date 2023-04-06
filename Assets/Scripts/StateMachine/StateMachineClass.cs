// <copyright file="StateMachineClass.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine
{
    #region Usings

    using Assets.Scripts.StateMachine.State;
    using Assets.Scripts.Templates;
    using System.Collections.Generic;
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
        /// The player attack state.
        /// </summary>
        public JumpAttack JumpAttack;

        /// <summary>
        /// The player run state.
        /// </summary>
        public Run Run;

        /// <summary>
        /// The player dash attack state.
        /// </summary>
        public DashAttack DashAttack;

        /// <summary>
        /// The player defend state.
        /// </summary>
        public Defend Defend;

        /// <summary>
        /// The player dash state.
        /// </summary>
        public Dash Dash;

        /// <summary>
        /// The player catching state.
        /// </summary>
        public Catching Catching;

        /// <summary>
        /// The player caught state.
        /// </summary>
        public Caught Caught;

        /// <summary>
        /// The player caught falling state.
        /// </summary>
        public CaughtFalling CaughtFalling;

        /// <summary>
        /// The fast jump attack state.
        /// </summary>
        public FastJumpAttack FastJumpAttack;

        /// <summary>
        /// The player lying state.
        /// </summary>
        public Lying Lying;

        /// <summary>
        /// The player falling state.
        /// </summary>
        public Falling Falling;

        /// <summary>
        /// The player get hit state.
        /// </summary>
        public Hit Hit;

        /// <summary>
        /// The player specific skill state.
        /// </summary>
        public List<TemplateState> SkillStates;

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
        /// <param name="player">
        /// The player script.
        /// </param>
        public StateMachineClass(GameObject player)
        {
            this.player = player;
            this.SkillStates = new List<TemplateState>();
            this.Attack = new Attack(this.player, this);
            this.Catching = new Catching(this.player, this);
            this.Caught = new Caught(this.player, this);
            this.CaughtFalling = new CaughtFalling(this.player, this);
            this.Dash = new Dash(this.player, this);
            this.DashAttack = new DashAttack(this.player, this);
            this.Defend = new Defend(this.player, this);
            this.Falling = new Falling(this.player, this);
            this.FastJump = new FastJump(this.player, this);
            this.FastJumpAttack = new FastJumpAttack(this.player, this);
            this.Hit = new Hit(this.player, this);
            this.Idle = new Idle(this.player, this);
            this.Jump = new Jump(this.player, this);
            this.JumpAttack = new JumpAttack(this.player, this);
            this.Lying = new Lying(this.player, this);
            this.Run = new Run(this.player, this);
            this.Walk = new Walk(this.player, this);
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
            if (this.CurrentState.CanChangeToState(nextState))
            {
                this.OldState = this.CurrentState;
                this.CurrentState = nextState;

                if (this.CurrentState != this.OldState)
                {
                    this.CurrentState.OnEntry();
                }
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
        /// Show that player can move in current state.
        /// </summary>
        /// <returns>
        /// True if player can move in current state.
        /// </returns>
        public bool CanPlayerMove()
        {
            return this.CurrentState.CanMove;
        }

        /// <summary>
        /// Show that player can attack in current state.
        /// </summary>
        /// <returns>
        /// True if player can attack in current state.
        /// </returns>
        public bool CanPlayerAttack()
        {
            return this.CurrentState.CanAttack;
        }

        /// <summary>
        /// The main StateMachine method.
        /// </summary>
        public void DoState()
        {
            this.CurrentState.DoState();
        }

        /// <summary>
        /// Add new character skill.
        /// </summary>
        /// <param name="newState">
        /// New skill to add.
        /// </param>
        public void AddNewSkillState(TemplateState newState)
        {
            this.SkillStates.Add(newState);
        }

        #endregion Public Methods
    }
}