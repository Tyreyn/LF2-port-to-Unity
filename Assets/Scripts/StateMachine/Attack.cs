using UnityEngine;

public class Attack : State
{
    private int Combo = 0;
    private int Time = 0;
    public Attack(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }
    public override void OnEntry()
    {
        Debug.print(Combo);
        Player.GetComponent<Player>().Animator.Play(this.ToString() + Combo.ToString());
    }
    public override void DoState()
    {
        Time++;
        if(Time >= 360)
        {
            OnExit();
            Combo++;
        }
    }
    public override void OnExit()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Time = 0;
            this.OnEntry();
        }
        else
        {
            Time = 0;
            Combo = 0;
            StateMachine.ChangeState(StateMachine.Idle);
        }
    }
}