// <copyright file="Hit.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.StateMachine.State
{
    #region Usings

    using Assets.Scripts.StateMachine;
    using Assets.Scripts.Templates;
    using System.Collections;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngineInternal;

    #endregion Usings

    /// <summary>
    /// Character caught falling state.
    /// </summary>
    public class Hit : TemplateState
    {
        #region Fields and Constants
        private int hitCounter;
        private float hitTimeStart;
        #endregion Fields and Constants

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Hit"/> class.
        /// </summary>
        /// <param name="player">
        /// The player gameobject.
        /// </param>
        /// <param name="stateMachine">
        /// The player statemachine.
        /// </param>
        public Hit(GameObject player, StateMachineClass stateMachine)
            : base(player, stateMachine)
        {
            this.hitCounter = 0;
        }

        #endregion Constructors and Destructors

        #region Public Methods

        /// <summary>
        /// State on entry method.
        /// </summary>
        public override void OnEntry()
        {
            if (this.hitCounter == 0)
            {
                this.hitTimeStart = Time.time;
            }

            this.animator.Play(this.Name, 0, 0);
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f
                && this.animator.GetCurrentAnimatorStateInfo(0).IsName(this.Name)
                && this.playerScript.isGettingHit)
            {
                this.hitCounter++;
                this.playerScript.isGettingHit = false;
            }

            if (this.hitCounter >= 3)
            {
                this.hitCounter = 0;
                this.stateMachine.ChangeState(this.stateMachine.Falling);
            }

            if (Time.time - this.hitTimeStart >= 1f)
            {
                this.hitCounter = 0;
                this.OnExit();
            }
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            this.stateMachine.ChangeState(this.stateMachine.Idle);
        }
        #endregion Public Methods
    }
}