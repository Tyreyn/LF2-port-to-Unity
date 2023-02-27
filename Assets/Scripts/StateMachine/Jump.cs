﻿using System.Collections;
using UnityEngine;
public class Jump : State
{
    private bool m_Jumping = true;
    private bool m_Jumped = false;
    public Jump(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
        canMove = false;
    }
    public override void OnEntry()
    {
        this.PlayerScript.Rigidbody.velocity = Vector3.zero;
        this.PlayerScript.Rigidbody.angularVelocity = Vector3.zero;
        m_Jumping = true;
        m_Jumped = false;
        Player.GetComponent<Player>().Animator.Play(this.ToString());
    }
    public override void DoState()
    {
        if (m_Jumping)
        {
            this.m_Jumping = false;
            this.PlayerScript.isGround = false;
            this.Rigidbody.AddForce(new Vector3(this.PlayerScript.SpeedX*0.65f, 1.5f, this.PlayerScript.SpeedZ*0.65f) * this.PlayerScript.Acc/2, ForceMode.Impulse);
        }

        if (this.Player.transform.position.y > 1.5f)
        {
            m_Jumped = true;
        }

        if (this.PlayerScript.isGround && m_Jumped)
        {
            OnExit();
        }

    }
    public override void OnExit()
    {
        this.PlayerScript.Rigidbody.velocity = Vector3.zero;
        this.PlayerScript.Rigidbody.angularVelocity = Vector3.zero;
        if (this.PlayerScript.ActionQueue.Count != 0)
        {
            if (this.PlayerScript.ActionQueue.Peek().CharacterActionItem == 'z' && this.PlayerScript.SpeedX != 0 ||
                this.PlayerScript.ActionQueue.Peek().CharacterActionItem == 'z' && this.PlayerScript.SpeedZ != 0)
            {
                StateMachine.ChangeState(StateMachine.FastJump);
            }
            else
            {
                StateMachine.ChangeState(StateMachine.Idle);
            }
        }
        else
        {
            StateMachine.ChangeState(StateMachine.Idle);
        }
    }
    public void CalculateEnd()
    {

    }
}
