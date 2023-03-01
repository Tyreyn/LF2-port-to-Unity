// <copyright file="GameController.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts
{
    #region Usings

    using System.IO;
    using Assets.Scripts.Loader;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// The game controller class.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        #region Fields and Constants

        /// <summary>
        /// The characters array.
        /// </summary>
        private CharacterList CharacterList = new CharacterList();

        /// <summary>
        /// The character information JSON file.
        /// </summary>
        private readonly TextAsset jsonFile;

        /// <summary>
        /// Path to character jsonFile
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
        public void Start()
        {
            if (File.Exists(this.jsonPath))
            {
                this.jsonString = File.ReadAllText(this.jsonPath);
            }
            else
            {
                Debug.LogError(string.Format("Can't find character json file {0}", this.jsonPath));
            }

            this.CharacterList = JsonUtility.FromJson<CharacterList>(this.jsonString);
        }

        /// <summary>
        /// Get character list.
        /// </summary>
        /// <returns>
        /// Character list.
        /// </returns>
        public CharacterList GetCharacterList()
        {
            return this.CharacterList;
        }
    }

    #endregion Public Methods
}