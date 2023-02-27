/// <summary>
/// Available character states
/// </summary>
public class CharacterComboItem
{
    private int Priority;
    private State MoveName;
    private string MoveKeysCode;

    /// <summary>
    /// Combo move class.
    /// </summary>
    /// <param name="priority">
    /// Move priority.
    /// </param>
    /// <param name="moveName">
    /// Move name.
    /// </param>
    /// <param name="moveKeysCode">
    /// What press to make combo.
    /// </param>
    public CharacterComboItem(int priority, State moveName, string moveKeysCode)
    {
        this.Priority = priority;
        this.MoveName = moveName;
        this.MoveKeysCode = moveKeysCode;
    }

    /// <summary>
    /// Get move priority.
    /// </summary>
    /// <returns>
    /// Move priority.
    /// </returns>
    public int getPriority() { return this.Priority; }


    /// <summary>
    /// Get move state name.
    /// </summary>
    /// <returns>
    /// Move state name.
    /// </returns>
    public State getName() { return this.MoveName; }

    /// <summary>
    /// Get keys code list.
    /// </summary>
    /// <returns>
    /// Keys code list to perform combo.
    /// </returns>
    public string getMoveKeysCode() { return this.MoveKeysCode; }
}
