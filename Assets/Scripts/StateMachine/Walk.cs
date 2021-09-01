using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Walk : State
{

    public Walk(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player,StateMachine)
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
            StateMachine.ChangeState(StateMachine.Jump);
        }
        if (Player.GetComponent<Player>().SpeedX == 0 && Player.GetComponent<Player>().SpeedY == 0)
        {
            StateMachine.ChangeState(StateMachine.Idle);
        }
        Player.transform.position += new Vector3(Player.GetComponent<Player>().SpeedX, Player.GetComponent<Player>().SpeedY, 0);
    }
}