using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public enum STATE
    {
        idle,
        walk,
        run,
        jump
    }
    public class StateMachine
    {
        private STATE CurrentState;
        public StateMachine(STATE CurrentState)
        {
            this.CurrentState = CurrentState;
        }
        public STATE ChangeState(STATE NextState)
        {
            this.CurrentState = NextState;
            return this.CurrentState;
        }
        public STATE ShowState()
        {
            return CurrentState;
        }
    }
}
