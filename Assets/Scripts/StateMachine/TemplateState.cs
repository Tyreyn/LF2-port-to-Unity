using UnityEngine;

[RequireComponent(typeof(StateMachine.StateMachine))]
[RequireComponent(typeof(GameObject))]


public abstract class State
{
    public GameObject Player;
    public StateMachine.StateMachine StateMachine;
    public Rigidbody Rigidbody;
    public Player PlayerScript;
    public State(GameObject Player, StateMachine.StateMachine StateMachine)
    {
        this.Player = Player;
        this.StateMachine = StateMachine;
        this.PlayerScript = this.Player.GetComponent<Player>();
        this.Rigidbody = this.Player.GetComponent<Rigidbody>();
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
