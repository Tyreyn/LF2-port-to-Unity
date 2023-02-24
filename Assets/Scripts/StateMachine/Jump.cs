using System.Collections;
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
        m_Jumping = true;
        m_Jumped = false;
        Player.GetComponent<Player>().Animator.Play(this.ToString());
    }
    public override void DoState()
    {
        if (m_Jumping)
        {
            m_Jumping = false;
            this.PlayerScript.isGround = false;
            this.Rigidbody.AddForce(new Vector3(0, 1, 0) * this.PlayerScript.Acc, ForceMode.Impulse);
            Debug.print("jump " + Time.time);
        }
        if (this.Player.transform.position.y > 1)
        {
            m_Jumped = true;
        }
        if (this.PlayerScript.isGround && m_Jumped)
        {
            Debug.print("end jump " + Time.time);
            OnExit();
        }

    }
    public override void OnExit()
    {
        Debug.print("wychodze");
        StateMachine.ChangeState(StateMachine.Idle);
    }
    public void CalculateEnd()
    {

    }
}
