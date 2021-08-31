using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class Player : MonoBehaviour
{
    public float SpeedX, SpeedY;
    public StateMachine.StateMachine StateMachine;
    public float acc = 0.005f;
    public Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        StateMachine = new StateMachine.StateMachine();
        StateMachine.SetState(StateMachine.Idle);
        Animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)){
            this.SpeedX = acc;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.SpeedX = -acc;
        }
        else
        {
            this.SpeedX = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.SpeedY = acc;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.SpeedY = -acc;
        }
        else
        {
            this.SpeedY = 0;
        }
        if (SpeedX != 0 || SpeedY != 0)
        {
            StateMachine.ChangeState(StateMachine.Walk);
        }
        else
        {
          StateMachine.ChangeState(StateMachine.Idle);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(SpeedX == 0 && SpeedY == 0)
            {
              StateMachine.ChangeState(StateMachine.Jump);
            }
        }
        this.transform.position += new Vector3(SpeedX, SpeedY, 0);

    }
}
