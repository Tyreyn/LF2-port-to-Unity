using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TextAsset jsonFile;
    public Characters Characters;
    // Start is called before the first frame update
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
