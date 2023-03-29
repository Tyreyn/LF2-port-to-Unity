// <copyright file="TemplateState.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.Templates
{
    #region Usings

    using Assets.Scripts.StateMachine;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// The template character state class.
    /// </summary>
    [RequireComponent(typeof(StateMachineClass))]
    [RequireComponent(typeof(GameObject))]
    public abstract class TemplateState
    {
        #region Fields and Constants
#pragma warning disable SA1401 // Fields should be private

        /// <summary>
        /// Indicates whether player can move in state.
        /// </summary>
        public bool CanMove = true;

        /// <summary>
        /// Indicates whether player can attack in state.
        /// </summary>
        public bool CanAttack = true;

        /// <summary>
        /// The player rigidbody.
        /// </summary>
        public Rigidbody rigidbody;

        /// <summary>
        /// The player animator.
        /// </summary>
        public Animator animator;

        /// <summary>
        /// The player GameObject.
        /// </summary>
        public GameObject player;

        /// <summary>
        /// The player script.
        /// </summary>
        public Player playerScript;

        /// <summary>
        /// The player StateMachine.
        /// </summary>
        public StateMachineClass stateMachine;

        /// <summary>
        /// The player name.
        /// </summary>
        public string Name;

#pragma warning restore SA1401 // Fields should be private

        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateState"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        protected TemplateState(GameObject player, StateMachineClass stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.playerScript = this.player.GetComponent<Player>();
            this.rigidbody = this.playerScript.Rigidbody;
            this.animator = this.playerScript.Animator;
            this.Name = this.GetType().Name;
        }

        #endregion Constructors and Destructors

        #region Public Methods

        /// <summary>
        /// State on entry method.
        /// </summary>
        public virtual void OnEntry()
        {
            this.animator.Play(this.Name);
            this.animator.speed = 1f;
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public virtual void DoState()
        {
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public virtual void OnExit()
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
        public virtual bool CanChangeToState(TemplateState nextstate)
        {
            return true;
        }

        /// <summary>
        /// Set name of this state.
        /// </summary>
        /// <param name="newName">
        /// Name to be set.
        /// </param>
        public virtual void SetStateName(string newName)
        {
            this.Name = newName;
        }
        #endregion Public Methods
    }
}