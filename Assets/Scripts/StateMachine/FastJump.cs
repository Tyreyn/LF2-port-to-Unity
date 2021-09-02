using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FastJump : State
{
    private Vector2 position;
    private float JumpHigh = 0.6f;
    private float JumpLength = 0.5f;
    private float Timer, t, tx;
    private float HalfX, FullX, HalfY, FullY;
    private bool high;
    public FastJump(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }
    public override void OnEntry()
    {
        high = false;
        position.x = Player.transform.position.x;
        position.y = Player.transform.position.y;
        Debug.print("x:" + position.x + "y:" + position.y);
        CalculateEnd();
        t = 0.6f;
        tx = 0.6f;
        Timer = 1;
        Player.GetComponent<Player>().Animator.Play(this.ToString());
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
                t -= 0.0015f * Timer;
                tx -= 0.0025f * Timer;
            }
            else if (t <= 0.2f)
            {
                t -= 0.0025f * Timer;
                tx -= 0.0025f * Timer;
            }
            else
            {
                t -= 0.005f * Timer;
                tx -= 0.0025f * Timer;
            }
            Player.transform.position = new Vector3(Mathf.Lerp(HalfX, position.x, tx), Mathf.Lerp(HalfY, position.y, t), Player.transform.position.z);
            if (Player.transform.position.y >= HalfY - 0.01f && Player.transform.position.y <= HalfY + 0.01f)
            {
                t = 0;
                tx = 0;
                position.x = Player.transform.position.x;
                position.y = Player.transform.position.y;
                high = true;
            }
        }
    }
    public override void OnExit()
    {
        t += 0.005f * Timer;
        tx += 0.0025f * Timer;
        Player.transform.position = new Vector3(Mathf.Lerp(position.x, FullX, tx), Mathf.Lerp(HalfY, FullY, t), Player.transform.position.z);
        if (Player.transform.position.y >= FullY - 0.02f && Player.transform.position.y <= FullY + 0.02f)
                StateMachine.ChangeState(StateMachine.Idle);
    }
    public void CalculateEnd()
    {
        if (Player.GetComponent<Player>().SpeedX > 0)
        {
            HalfX = position.x + 3 * JumpLength;
            FullX = position.x + 6 * JumpLength;
        }
        else if (Player.GetComponent<Player>().SpeedX < 0)
        {
            HalfX = position.x - 3 * JumpLength;
            FullX = position.x - 6 * JumpLength;
        }
        else
        {
            HalfX = position.x;
            FullX = position.x;
        }
        if (Player.GetComponent<Player>().SpeedY > 0)
        {
            HalfY = position.y + 2 * JumpHigh;
            FullY = position.y + JumpHigh;
        }
        else if (Player.GetComponent<Player>().SpeedY < 0)
        {
            HalfY = position.y + JumpHigh / 2;
            FullY = position.y - JumpHigh;
        }
        else
        {
            HalfY = position.y + JumpHigh;
            FullY = position.y;
        }
    }
}
