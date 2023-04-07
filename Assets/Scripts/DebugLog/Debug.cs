// <copyright file="Debug.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.DebugLog
{
    #region Usings

    using Assets.Scripts.InputSystem;
    using Assets.Scripts.Loader;
    using UnityEngine;
    using UnityEngine.UI;

    #endregion Usings

    /// <summary>
    /// The debug class.
    /// </summary>
    public class Debug : MonoBehaviour
    {
        #region Fields and Constants

        /// <summary>
        /// The player GameObject.
        /// </summary>
        private GameObject player;

        /// <summary>
        /// The GameManager GameObject.
        /// </summary>
        private GameObject gameManager;

        /// <summary>
        /// The player script.
        /// </summary>
        private Player playerScript;

        /// <summary>
        /// The GameObject which display DebugLog.
        /// </summary>
        private GameObject debugLog;

        /// <summary>
        /// The text field for DebugLog.
        /// </summary>
        private Text text;

        /// <summary>
        /// The RectTransform for DebugLog.
        /// </summary>
        private RectTransform rectTransform;

        /// <summary>
        /// The GameObject which display Loading.
        /// </summary>
        private GameObject loading;

        /// <summary>
        /// The text field for Loading.
        /// </summary>
        private Text text1;

        /// <summary>
        /// The RectTransform for Combo.
        /// </summary>
        private RectTransform rectTransform1;

        /// <summary>
        /// The GameObject which display Combo.
        /// </summary>
        private GameObject combo;

        /// <summary>
        /// The text field for Combo.
        /// </summary>
        private Text text2;

        /// <summary>
        /// The RectTransform for Combo.
        /// </summary>
        private RectTransform rectTransform2;

        /// <summary>
        /// The flag that indicates loading available characters from file.
        /// </summary>
        private bool playOnce = true;

        /// <summary>
        /// The loaded characters.
        /// </summary>
        private CharacterList characterList;

        #endregion Fields and Constants

        #region Public Methods

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        public void Start()
        {
            this.player = GameObject.FindGameObjectWithTag("Player");
            this.playerScript = this.player.GetComponent<Player>();
            this.gameManager = GameObject.FindGameObjectWithTag("GameController");
            this.characterList = this.gameManager.GetComponent<GameController>().GetCharacterList();
            this.CreateText();
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        public void Update()
        {
            string comboLog = string.Empty;

            if (this.playOnce && this.characterList != null)
            {
                foreach (Character character in this.characterList.characters)
                {
                    this.loading.GetComponent<Text>().text += "\nFound: "
                        + character.Name;
                }

                this.playOnce = false;
            }

            foreach (CharacterActionHandler ac in this.playerScript.ActionQueue)
            {
                comboLog += ac.CharacterActionItem + " " + ac.Timestamp + " " + ac.CheckIfExpired().ToString() + " \n";
            }

            this.text2.text = comboLog;
            this.rectTransform.localPosition = new Vector3((this.player.transform.position.x * 200) + 20f, (this.player.transform.position.z * 20) + 30f, 0);
            this.text.text = "State: " + this.playerScript.StateMachine.ShowCurrentStateName()
                + "\nDirection X: " + this.playerScript.GetPlayerSpeed().x
                + "\nDirection Z: " + this.playerScript.GetPlayerSpeed().y;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates gameobject responsible for showing debug log.
        /// </summary>
        private void CreateText()
        {
            this.debugLog = new GameObject("DebugLog");
            this.debugLog.transform.parent = this.transform;
            this.debugLog.AddComponent<Text>();
            this.text = this.debugLog.GetComponent<Text>();
            this.text.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
            this.text.fontSize = 12;
            this.text.alignment = TextAnchor.MiddleCenter;
            this.text.resizeTextForBestFit = true;
            this.rectTransform = this.text.GetComponent<RectTransform>();
            this.rectTransform.localScale = new Vector3(1, 1, 1);
            this.rectTransform.localPosition = new Vector3(-0, 75f, 0);
            this.rectTransform.sizeDelta = new Vector2(300f, 75f);

            this.loading = new GameObject("Loading");
            this.loading.transform.parent = this.transform;
            this.loading.AddComponent<Text>();
            this.text1 = this.loading.GetComponent<Text>();
            this.text1.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
            this.text1.fontSize = 12;
            this.text1.alignment = TextAnchor.MiddleCenter;
            this.rectTransform1 = this.text1.GetComponent<RectTransform>();
            this.rectTransform1.localScale = new Vector3(1, 1, 1);
            this.rectTransform1.localPosition = new Vector3(-917f, 438f, 0);
            this.rectTransform1.sizeDelta = new Vector2(200f, 583f);

            this.combo = new GameObject("Combo");
            this.combo.transform.parent = this.transform;
            this.combo.AddComponent<Text>();
            this.text2 = this.combo.GetComponent<Text>();
            this.text2.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;
            this.text2.fontSize = 12;
            this.text2.alignment = TextAnchor.MiddleCenter;
            this.rectTransform2 = this.text2.GetComponent<RectTransform>();
            this.rectTransform2.localScale = new Vector3(1, 1, 1);
            this.rectTransform2.localPosition = new Vector3(-917f, 0, 0);
            this.rectTransform2.sizeDelta = new Vector2(200f, 583f);
        }

        #endregion Private Methods
    }
}