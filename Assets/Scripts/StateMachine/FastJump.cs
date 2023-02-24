using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FastJump : State
{
    private bool m_Jumping = true;
    private bool m_Jumped = false;

    public FastJump(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
        canMove = false;
    }
    public override void OnEntry()
    {
        m_Jumping = true;
        m_Jumped = false;
        Player.GetComponent<Player>().Animator.Play(this.ToString());
    }
    public override void DoState()
    {
        if (m_Jumping)
        {
            Debug.print("zrób to kurwa raz");
            m_Jumping = false;
            this.PlayerScript.isGround = false;
            this.PlayerScript.Rigidbody.velocity = Vector3.zero;
            this.PlayerScript.Rigidbody.angularVelocity = Vector3.zero;
            this.Rigidbody.AddForce(
                new Vector3(
                    this.PlayerScript.SpeedX*0.95f, 1.25f, this.PlayerScript.SpeedZ * 0.95f) * this.PlayerScript.Acc,
                ForceMode.Impulse);
        }
        if (this.Player.transform.position.y > 1f)
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
        Debug.print("szybko wychodzę");
        StateMachine.ChangeState(StateMachine.Idle);
    }
    public void CalculateEnd()
    {
    }
}
