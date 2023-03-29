// <copyright file="GameController.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts
{
    using System.Drawing.Design;
    #region Usings

    using System.IO;
    using System.Linq;
    using Assets.Scripts.Loader;
    using UnityEditor.Animations;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// The game controller class.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        #region Fields and Constants

        /// <summary>
        /// Reference to the Prefab. Drag a Prefab into this field in the Inspector.
        /// </summary>
        public GameObject characterPrefab;

        public AnimatorOverrideController animatorOverrideController;

        public AnimatorController controller;

        /// <summary>
        /// The characters array.
        /// </summary>
        public CharacterList characterList = new CharacterList();

        /// <summary>
        /// Path to character jsonFile.
        /// </summary>
        private readonly string jsonPath = Path.Combine(Application.dataPath + "/Scripts/Variables/Characters.json");

        /// <summary>
        /// Raw json file to string.
        /// </summary>
        private string jsonString;

        #endregion Fields and Constants

        #region Public Methods

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        public void Awake()
        {
            if (File.Exists(this.jsonPath))
            {
                this.jsonString = File.ReadAllText(this.jsonPath);
            }
            else
            {
                Debug.LogError(string.Format("Can't find character json file {0}", this.jsonPath));
            }

            this.characterList = JsonUtility.FromJson<CharacterList>(this.jsonString);
        }

        /// <summary>
        /// Get character list.
        /// </summary>
        /// <returns>
        /// Character list.
        /// </returns>
        public CharacterList GetCharacterList()
        {
            return this.characterList;
        }

        public void Start()
        {
            //this.SetSelectedCharacter("Dummy");
        }

        private void SetSelectedCharacter(string playerSelectedName)
        {
            string pathToCharacterSprites = Path.Combine(string.Format("/Sprite/Character/{0}/", playerSelectedName));
            GameObject tmp = Instantiate(this.characterPrefab, new Vector3(0, 0.4f, 13f), Quaternion.identity);

            string tmpPath = string.Format("Sprite/Character/{0}/Animator", playerSelectedName);

            controller = Resources.Load<AnimatorController>(tmpPath);

            tmp.SendMessage("setAnimator", this.controller);


        }
    }

    #endregion Public Methods
}