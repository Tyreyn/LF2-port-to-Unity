using UnityEngine;

public class CharacterAction
{
    public CharacterActionItem CharacterActionItem;
    public float Timestamp;
    public Player PlayerScript;

    /// <summary>
    /// The class constructor.
    /// </summary>
    /// <param name="action">
    /// The performed action.
    /// </param>
    /// <param name="timestamp">
    /// The time when action was performed.
    /// </param>
    public CharacterAction(CharacterActionItem action, float timestamp)
    {
        this.CharacterActionItem = action;
        this.Timestamp = timestamp;
        this.PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// Check if action is expired.
    /// </summary>
    /// <returns>
    /// True if action is expired
    /// </returns>
    public bool CheckIfExpired()
    {
        bool isExpired = false;
        //Debug.print(Time.time + " " + Timestamp);
        if (Time.time - this.Timestamp >= this.PlayerScript.TimeBeforActionExpire)
        {
            isExpired = true;
        }

        return isExpired;
    }
}
