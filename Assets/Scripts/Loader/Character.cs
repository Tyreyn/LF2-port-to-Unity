// <copyright file="Character.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

using Assets.Scripts.Templates;
using System.Collections.Generic;

namespace Assets.Scripts.Loader
{
    /// <summary>
    /// Character class.
    /// </summary>
    [System.Serializable]
    public class Character
    {
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

        /// <summary>
        /// Character description.
        /// </summary>
        public string Description;

        public List<SkillTemplate> Combo;

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
