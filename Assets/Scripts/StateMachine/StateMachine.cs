using Scripts.Templates;

namespace Scripts.StateMachine
{
    #region Usings

    using UnityEngine;
    using Scripts.StateMachine.State;
    using Scripts.Templates;
    using System.Runtime.CompilerServices;

    #endregion

    /// <summary>
    /// The state machine handling player states.
    /// </summary>
    public class StateMachine
    {
        #region Fields and Constants

        public Walk Walk;
        public Jump Jump;
        public Idle Idle;
        public FastJump FastJump;
        public Attack Attack;
        public Run Run;
        public TemplateState CurrentState;
        public TemplateState OldState;
        public GameObject Player = GameObject.FindGameObjectWithTag("Player");
        public int cnt = 0;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instatnce of the <see cref="StateMachine"/> class.
        /// </summary>
        public StateMachine(){
            Walk = new Walk(Player,this);
            Jump = new Jump(Player,this);
            Idle = new Idle(Player,this);
            FastJump = new FastJump(Player, this);
            Attack = new Attack(Player, this);
            Run = new Run(Player, this);
        }

        #endregion

        #region Public Methods
        public void SetState(TemplateState CurrentState)
        {
            this.CurrentState = CurrentState;
        }

        public void ChangeState(TemplateState NextState)
        {
            this.OldState = this.CurrentState;
            this.CurrentState = NextState;
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
            return this.CurrentState.canMove;
        }

        public void DoState()
        {
            this.CurrentState.DoState();
        }

        #endregion
    }
}