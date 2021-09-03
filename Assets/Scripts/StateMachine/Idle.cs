using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Idle : State
{
    public Idle(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }
    public override void DoState()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Player.GetComponent<Player>().SpeedX == 0 && Player.GetComponent<Player>().SpeedY == 0)
            {
                StateMachine.ChangeState(StateMachine.Jump);
            }
        }
        if (Player.GetComponent<Player>().SpeedX != 0 || Player.GetComponent<Player>().SpeedY != 0)
        {
            StateMachine.ChangeState(StateMachine.Walk);
        }
        if (Input.GetKey(KeyCode.X))
        {
            StateMachine.ChangeState(StateMachine.Attack);
        }

    }
}

