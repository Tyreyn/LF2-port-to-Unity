using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private State State = new State();
        public Walk Walk = new Walk();
        public Jump Jump = new Jump();
        public Idle Idle = new Idle();
        private State CurrentState;
        private State OldState;

        public void SetState(State CurrentState)
        {
            this.CurrentState = CurrentState;
        }
        public void ChangeState(State NextState)
        {
            this.OldState = this.CurrentState;
            this.CurrentState = NextState;
            OnEntry();
        }
        public State ShowState()
        {
            return CurrentState;
        }
        public void OnEntry()
        {
            if(CurrentState != OldState) CurrentState.OnEntry();
        }

    }

}
