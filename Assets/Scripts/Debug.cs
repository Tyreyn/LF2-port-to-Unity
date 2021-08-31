using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "Stan: " + Player.GetComponent<Player>().StateMachine.ShowState().ToString()
            + "\nPredkosc X: " + Player.GetComponent<Player>().SpeedX
            + "\nPredkosc Y: " + Player.GetComponent<Player>().SpeedY;
    }

}