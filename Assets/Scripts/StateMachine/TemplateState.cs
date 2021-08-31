using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    private GameObject Player = GameObject.FindGameObjectWithTag("Player");
    public void OnEntry()
    {
        Player.GetComponent<Player>().Animator.Play(this.ToString());
    }
}
