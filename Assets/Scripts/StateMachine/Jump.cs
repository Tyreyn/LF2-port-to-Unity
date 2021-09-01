using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Jump : State
{
    private float JumpHigh = 1.9f;
    private Vector2 position;
    private float t = 1f;
    private bool high;
    public Jump(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }
    public override void OnEntry()
    {
        high = false;
        position.x = Player.transform.position.x;
        position.y = Player.transform.position.y;
        t = 0.8f;
        Debug.print("x:" + position.x + "y:" + position.y);
        Player.GetComponent<Player>().Animator.Play(this.ToString() + "UP");
    }
    public override void DoState()
    {
        if (high)
        {
            OnExit();
        }
        else
        {
            if (t <= 0.1f)
            {
                t -= 0.0015f;
            }else if (t <= 0.2f)
            {
                t -= 0.0025f;
            }
            else
            {
                t -= 0.005f;
            }
            Debug.print(t);
            Player.transform.position = new Vector3(Player.transform.position.x, Mathf.Lerp(position.y + JumpHigh, position.y, t), Player.transform.position.z);
            if (Player.transform.position.y >= position.y + JumpHigh - 0.01f && Player.transform.position.y <= position.y + JumpHigh + 0.01f)
            {
                t = 0;
                high = true;
            }
        }
    }
    public override void OnExit()
    {
        t += 0.005f;
        Player.transform.position = new Vector3(Player.transform.position.x, Mathf.Lerp(position.y + JumpHigh, position.y, t), Player.transform.position.z);
        if(Player.transform.position.y >= position.y - 0.02f && Player.transform.position.y <= position.y + 0.02f)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                StateMachine.ChangeState(StateMachine.FastJump);
            }
            else
            {
                StateMachine.ChangeState(StateMachine.Idle);
            }
        }

    }

}
