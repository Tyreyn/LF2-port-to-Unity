using UnityEngine;
public class Jump : State
{
    private bool m_Jumping = true;
    public Jump(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }
    public override void OnEntry()
    {
        m_Jumping = true;
        Player.GetComponent<Player>().Animator.Play(this.ToString());
    }
    public override void DoState()
    {
        if (m_Jumping)
        {
            m_Jumping = false;
            this.PlayerScript.isGround = false;
            this.Rigidbody.AddForce(new Vector3(0, 1, 0) * this.PlayerScript.Acc, ForceMode.Impulse);
        }
        else 
        {
            if (this.PlayerScript.isGround)
            {
                OnExit();
            }
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
