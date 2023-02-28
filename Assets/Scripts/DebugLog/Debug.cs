namespace Scripts.DebugLog
{
    #region Usings

    using Scripts.InputSystem;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    #endregion

    /// <summary>
    /// The debug class.
    /// </summary>
    public class Debug : MonoBehaviour
    {
        #region Fields and Constants

        private GameObject Player, GameManager;
        private Player PlayerScript;
        public GameObject DebugLog, Loading, Combo;
        public Text text, text1, text2;
        public Characters Characters;
        private bool playOnce = true;
        private RectTransform rectTransform;
        private RectTransform rectTransform1;
        private RectTransform rectTransform2;

        #endregion

        #region Public Methods

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerScript = Player.GetComponent<Player>();
            GameManager = GameObject.FindGameObjectWithTag("GameController");
            Characters.characters = GameManager.GetComponent<GameController>().Characters.characters;
            CreateText();
        }

        /// <summary>
        /// Creates gameobject responsible for showing debug log.
        /// </summary>
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
            rectTransform = text.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.localPosition = new Vector3(-0, 75f, 0);
            rectTransform.sizeDelta = new Vector2(300f, 75f);

            Loading = new GameObject("Loading");
            Loading.transform.parent = this.transform;
            Loading.AddComponent<Text>();
            text1 = Loading.GetComponent<Text>();
            text1.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
            text1.fontSize = 12;
            text1.alignment = TextAnchor.MiddleCenter;
            rectTransform1 = text1.GetComponent<RectTransform>();
            rectTransform1.localScale = new Vector3(1, 1, 1);
            rectTransform1.localPosition = new Vector3(-917f, 438f, 0);
            rectTransform1.sizeDelta = new Vector2(200f, 583f);

            Combo = new GameObject("Combo");
            Combo.transform.parent = this.transform;
            Combo.AddComponent<Text>();
            text2 = Combo.GetComponent<Text>();
            text2.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
            text2.fontSize = 12;
            text2.alignment = TextAnchor.MiddleCenter;
            rectTransform2 = text2.GetComponent<RectTransform>();
            rectTransform2.localScale = new Vector3(1, 1, 1);
            rectTransform2.localPosition = new Vector3(-917f, 0, 0);
            rectTransform2.sizeDelta = new Vector2(200f, 583f);
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        void Update()
        {
            string combo = string.Empty;
            if (playOnce)
            {
                foreach (Character Character in GameManager.GetComponent<GameController>().Characters.characters)
                {
                    Loading.GetComponent<Text>().text += "\nFound: " + Character.Name;
                }
                playOnce = false;
            }

            foreach (CharacterActionHandler ac in PlayerScript.ActionQueue)
            {
                combo += (ac.CharacterActionItem) + " " + ac.Timestamp + " " + ac.CheckIfExpired().ToString() + " \n";
            }

            text2.text = combo;
            rectTransform.localPosition = new Vector3((this.Player.transform.position.x * 200) + 20f, (this.Player.transform.position.z * 20) + 30f, 0);
            text.text = "Stan: " + PlayerScript.StateMachine.ShowCurrentStateName()
                + "\nKierunek X: " + PlayerScript.SpeedX
                + "\nKierunek Z: " + PlayerScript.SpeedZ;
        }

        #endregion

    }
}