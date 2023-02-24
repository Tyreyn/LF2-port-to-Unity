using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug : MonoBehaviour
{
    private GameObject Player, GameManager;
    private Player PlayerScript;
    public GameObject DebugLog,Loading;
    public Text text,text1;
    public Characters Characters;
    private List<string> Eventlog = new List<string>();
    private bool playOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<Player>();
        GameManager = GameObject.FindGameObjectWithTag("GameController");
        Characters.characters = GameManager.GetComponent<GameController>().Characters.characters;
        CreateText();
    }
    public void CreateText()
    {
        DebugLog = new GameObject("DebugLog");
        DebugLog.transform.parent = this.transform;
        DebugLog.AddComponent<Text>();
        text = DebugLog.GetComponent<Text>();
        text.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
        text.fontSize = 12;
        text.alignment = TextAnchor.MiddleCenter;
        text.resizeTextForBestFit = true;
        RectTransform rectTransform;
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.localPosition = new Vector3(-0, 135f, 0);
        rectTransform.sizeDelta = new Vector2(300f, 75f);

        Loading = new GameObject("Loading");
        Loading.transform.parent = this.transform;
        Loading.AddComponent<Text>();
        text1 = Loading.GetComponent<Text>();
        text1.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
        text1.fontSize = 12;
        text1.alignment = TextAnchor.MiddleCenter;
        RectTransform rectTransform1;
        rectTransform1 = text1.GetComponent<RectTransform>();
        rectTransform1.localScale = new Vector3(1, 1, 1);
        rectTransform1.localPosition = new Vector3(-440.5f, -37.50002f, 0);
        rectTransform1.sizeDelta = new Vector2(200f, 583f);

    }

    // Update is called once per frame
    void Update()
    {
        string combo = string.Empty;
        if (playOnce) {
            foreach (Character Character in GameManager.GetComponent<GameController>().Characters.characters)
            {
                Loading.gameObject.GetComponent<Text>().text += "\nFound: " + Character.Name;
            }
            playOnce = false;
        }
        foreach(CharacterAction ac in PlayerScript.ActionQueue)
        {
            combo += ac + " ";
        }
        DebugLog.gameObject.GetComponent<Text>().text = "Stan: " + PlayerScript.StateMachine.ShowState().ToString()
            + "\nPredkosc X: " + PlayerScript.SpeedX
            + "\nPredkosc Z: " + PlayerScript.SpeedZ
            + "\nCombo: " + combo;
    }

}