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
            m_Jumping = false;
            this.PlayerScript.isGround = false;
            this.Rigidbody.AddForce(this.checkDirection() * this.PlayerScript.Acc / 2, ForceMode.Impulse);
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
        this.PlayerScript.Rigidbody.velocity = Vector3.zero;
        this.PlayerScript.Rigidbody.angularVelocity = Vector3.zero;
        StateMachine.ChangeState(StateMachine.Idle);
    }
    public void CalculateEnd()
    {
    }

    /// <summary>
    /// Check if direction of fast jump is proper
    /// because player can not only up and down in this state.
    /// </summary>
    /// <returns>
    /// Proper direction.
    /// </returns>
    private Vector3 checkDirection()
    {
        float properDirection = this.PlayerScript.SpeedX;
        // If player want to jump up, change it to slant
        if (this.PlayerScript.SpeedZ != 0 && this.PlayerScript.SpeedX == 0)
        {
            properDirection = this.PlayerScript.SpriteRenderer.flipX ? -1 : 1;
        }

        return new Vector3(
            properDirection * 0.95f,
            1.25f,
            this.PlayerScript.SpeedZ * 0.95f);
    }
}
