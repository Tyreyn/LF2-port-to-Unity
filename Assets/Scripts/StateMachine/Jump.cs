using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Jump : State
{
    private float JumpHigh = 1.2f;
    private float JumpLength = 0.5f;
    private float Timer = 1;
    private Vector2 position;
    private float t = 0.4f;
    private float tx = 0;
    private bool high;
    private float HalfX, FullX, HalfY,FullY;
    public Jump(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }
    public override void OnEntry()
    {
        high = false;
        position.x = Player.transform.position.x;
        position.y = Player.transform.position.y;
        t = 0.8f;
        tx = 0.8f;
        Timer = 1;
        //Debug.print("x:" + position.x + "y:" + position.y);
        Player.GetComponent<Player>().Animator.Play(this.ToString());
        CalculateEnd();
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
                t -= 0.0025f*Timer;
                tx -= 0.0035f * Timer;
            }
            else if (t <= 0.2f)
            {
                t -= 0.0035f * Timer;
                tx -= 0.0035f * Timer;
            }
            else
            {
                t -= 0.01f * Timer;
                tx -= 0.005f * Timer;
            }
            Debug.print(t);
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
        t += 0.01f*Timer;
        tx += 0.005f* Timer;
        Player.transform.position = new Vector3(Mathf.Lerp(position.x, FullX, tx), Mathf.Lerp(HalfY,FullY, t), Player.transform.position.z);
        if(Player.transform.position.y >= FullY - 0.02f && Player.transform.position.y <= FullY + 0.02f)
        {
            if (Input.GetKey(KeyCode.Z) && Player.GetComponent<Player>().SpeedX != 0 ||
                Input.GetKey(KeyCode.Z) && Player.GetComponent<Player>().SpeedY != 0 && Player.GetComponent<Player>().SpeedX != 0)
            {
                StateMachine.ChangeState(StateMachine.FastJump);
            }
            else
            {
                StateMachine.ChangeState(StateMachine.Idle);
            }
        }

    }
    public void CalculateEnd()
    {
        // y = jumpHT * Mathf.Sin( Mathf.PI * ((Time.time - startTime)/duration) );
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
        if(Player.GetComponent<Player>().SpeedY > 0)
        {
            HalfY = position.y + 2*JumpHigh;
            FullY = position.y + JumpHigh;
            //Timer = 2f;
        }
        else if(Player.GetComponent<Player>().SpeedY < 0)
        {
            HalfY = position.y + JumpHigh/2;
            FullY = position.y - JumpHigh;
        }
        else
        {
            HalfY = position.y+ JumpHigh;
            FullY = position.y;
        }
    }
}
