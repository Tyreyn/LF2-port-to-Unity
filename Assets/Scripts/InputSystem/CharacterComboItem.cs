namespace Scripts.InputSystem
{
    #region Methods

    using Scripts.Templates;

    #endregion

    /// <summary>
    /// Available character states
    /// </summary>
    public class CharacterComboItem
    {
        #region Fields and Constants

        private readonly int Priority;
        private readonly TemplateState MoveName;
        private readonly string MoveKeysCode;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instatnce of the <see cref="CharacterComboItem"/> class.
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
        public CharacterComboItem(int priority, TemplateState moveName, string moveKeysCode)
        {
            this.Priority = priority;
            this.MoveName = moveName;
            this.MoveKeysCode = moveKeysCode;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get move priority.
        /// </summary>
        /// <returns>
        /// Move priority.
        /// </returns>
        public int GetPriority() { return this.Priority; }

        /// <summary>
        /// Get move state name.
        /// </summary>
        /// <returns>
        /// Move state name.
        /// </returns>
        public TemplateState GetName() { return this.MoveName; }

        /// <summary>
        /// Get keys code list.
        /// </summary>
        /// <returns>
        /// Keys code list to perform combo.
        /// </returns>
        public string GetMoveKeysCode() { return this.MoveKeysCode; }

        #endregion
    }
}