using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public GameObject Player;
    public StateMachine.StateMachine StateMachine;
    public State(GameObject Player, StateMachine.StateMachine StateMachine)
    {
        this.Player = Player;
        this.StateMachine = StateMachine;
    }
    public virtual void OnEntry()
    {
        Player.GetComponent<Player>().Animator.Play(this.ToString());
     //   DoState();
    }
    public virtual void DoState()
    {

    }
    public virtual void OnExit()
    {

    }
}
