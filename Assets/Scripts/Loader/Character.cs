namespace Assets.Scripts.Loader
{

    /// <summary>
    /// Character class.
    /// </summary>
    [System.Serializable]
    public class Character
    {

#pragma warning disable RCS1169 // Make field read-only.
#pragma warning disable SA1306 // Field names should begin with lower-case letter
#pragma warning disable IDE0044 // Dodaj modyfikator tylko do odczytu

        /// <summary>
        /// Character name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Character health.
        /// </summary>
        public int Health;

        /// <summary>
        /// Character mana.
        /// </summary>
        public int Mana;

#pragma warning restore IDE0044 // Dodaj modyfikator tylko do odczytu
#pragma warning restore SA1306 // Field names should begin with lower-case letter
#pragma warning restore RCS1169 // Make field read-only.

        /// <summary>
        /// Get character name.
        /// </summary>
        /// <returns>
        /// Character name.
        /// </returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// Get character name.
        /// </summary>
        /// <returns>
        /// Character name.
        /// </returns>
        public int GetHealth()
        {
            return this.Health;
        }

        /// <summary>
        /// Get character name.
        /// </summary>
        /// <returns>
        /// Character name.
        /// </returns>
        public int GetMana()
        {
            return this.Mana;
        }
    }
}
