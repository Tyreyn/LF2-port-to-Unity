using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{

    public float SpeedX, SpeedY;
    public StateMachine.StateMachine StateMachine;
    public float acc = 0.005f;
    public Animator Animator;
    // Start is called before the first frame update
    void Awake()
    {
        StateMachine = new StateMachine.StateMachine();
        StateMachine.SetState(StateMachine.Idle);
        Animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<SpriteRenderer>().enabled = false;
        if (Input.GetKey(KeyCode.D)){
            this.SpeedX = acc;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.SpeedX = -acc;
            this.GetComponent<SpriteRenderer>().flipX = true;
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

        StateMachine.DoState();
    }
}

