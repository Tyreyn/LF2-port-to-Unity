using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FastJump : State
{
    public FastJump(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }
    public override void OnEntry()
    {
        Player.GetComponent<Player>().Animator.Play(this.ToString());
    }
    public override void DoState()
    {
    }
    public override void OnExit()
    {
    }
    public void CalculateEnd()
    {
    }
}
