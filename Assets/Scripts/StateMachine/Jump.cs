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
            this.Rigidbody.AddForce(new Vector3(this.PlayerScript.SpeedX, 1.5f, this.PlayerScript.SpeedZ) * this.PlayerScript.Acc, ForceMode.Impulse);
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
        if (this.PlayerScript.ActionQueue.Count != 0)
        {
            if (this.PlayerScript.ActionQueue.Peek() == CharacterAction.Jump)
            {
                Debug.print("Lecymy do " + StateMachine.FastJump.ToString());
                StateMachine.ChangeState(StateMachine.FastJump);
            }
        }
        else
        {
            Debug.print("wychodze");
            StateMachine.ChangeState(StateMachine.Idle);
        }
    }
    public void CalculateEnd()
    {

    }
}
