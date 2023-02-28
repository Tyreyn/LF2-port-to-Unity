namespace Scripts
{
    #region Usings

    using UnityEngine;

    #endregion
    public class GameController : MonoBehaviour
    {
        #region Fields and Constants

        public TextAsset jsonFile;
        public Characters Characters;

        #endregion

        #region Public Methods
        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        void Start()
        {
            Characters = JsonUtility.FromJson<Characters>(jsonFile.text);

        }

    }

    [System.Serializable]
    public class Characters
    {
        public Character[] characters;
    }

    [System.Serializable]
    public class Character
    {
        public string Name;
    }

    #endregion
}