using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateMachine
    {
        //private State State = new State(Player);
        public Walk Walk;
        public Jump Jump;
        public Idle Idle;
        public FastJump FastJump;
        public Attack Attack;
        public Run Run;
        private State CurrentState;
        private State OldState;
        public GameObject Player = GameObject.FindGameObjectWithTag("Player");
        public int cnt = 0;

        public StateMachine(){
            Walk = new Walk(Player,this);
            Jump = new Jump(Player,this);
            Idle = new Idle(Player,this);
            FastJump = new FastJump(Player, this);
            Attack = new Attack(Player, this);
            Run = new Run(Player, this);
        }

        public void SetState(State CurrentState)
        {
            this.CurrentState = CurrentState;
           // this.CurrentState.OnEntry();
        }

        public void ChangeState(State NextState)
        {
            this.OldState = this.CurrentState;
            this.CurrentState = NextState;
            if (this.CurrentState != this.OldState)
            {
                this.CurrentState.OnEntry();
                //Debug.print(cnt++);
            }
        }

        /// <summary>
        /// Show current state.
        /// </summary>
        /// <returns>
        /// Current state.
        /// </returns>
        public State ShowCurrentState()
        {
            return this.CurrentState;
        }

        /// <summary>
        /// Show previous state.
        /// </summary>
        /// <returns>
        /// previous state.
        /// </returns>
        public State ShowPreviousState()
        {
            return this.OldState;
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
    }

}
