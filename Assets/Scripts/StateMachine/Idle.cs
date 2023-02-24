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
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    if (PlayerScript.SpeedX == 0 && PlayerScript.SpeedZ == 0)
        //    {
        //        this.PlayerScript.isGround = false;
        //        StateMachine.ChangeState(StateMachine.Jump);
        //    }
        //}
        if (Mathf.Abs(PlayerScript.SpeedX) == Mathf.Abs(PlayerScript.Acc)
            || Mathf.Abs(PlayerScript.SpeedZ) == Mathf.Abs(PlayerScript.Acc))
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

