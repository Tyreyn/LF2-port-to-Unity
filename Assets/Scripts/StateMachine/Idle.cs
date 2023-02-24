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
        if (Mathf.Abs(PlayerScript.SpeedX) != 0
            || Mathf.Abs(PlayerScript.SpeedZ) != 0)
        {
            StateMachine.ChangeState(StateMachine.Walk);
        }
        if (Mathf.Abs(PlayerScript.SpeedX) == 2*Mathf.Abs(PlayerScript.Acc)
            || Mathf.Abs(PlayerScript.SpeedZ) == 2*Mathf.Abs(PlayerScript.Acc))
        {
            StateMachine.ChangeState(StateMachine.Run);
        }
        if (Input.GetKey(KeyCode.X))
        {
            StateMachine.ChangeState(StateMachine.Attack);
        }

    }
}

