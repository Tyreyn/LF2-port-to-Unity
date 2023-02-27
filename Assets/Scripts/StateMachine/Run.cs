using UnityEngine;

public class Run : State
{
    public Run(GameObject Player, StateMachine.StateMachine StateMachine) : base(Player, StateMachine)
    {
    }

    public override void DoState()
    {
        if(this.PlayerScript.SpeedX == 0)
        {
            OnExit();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this.StateMachine.ChangeState(StateMachine.Idle);
    }
}
