
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Walk : State
{

    public Walk(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }
    public override void DoState()
    {
        if (PlayerScript.SpeedX == 0 && PlayerScript.SpeedZ == 0)
        {
            StateMachine.ChangeState(StateMachine.Idle);
        }
        if (Player.GetComponent<Player>().SpeedX > 0)
        {
            Player.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Player.GetComponent<Player>().SpeedX < 0)
        {
            Player.GetComponent<SpriteRenderer>().flipX = true;
        }

        this.Rigidbody.MovePosition(new Vector3(Player.transform.position.x + PlayerScript.SpeedX * Time.deltaTime, Player.transform.position.y, Player.transform.position.z + PlayerScript.SpeedZ * Time.deltaTime));
    }
}