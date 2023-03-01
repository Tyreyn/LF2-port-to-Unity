// <copyright file="ComboHandler.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>
namespace Assets.Scripts.InputSystem
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Templates;

    #endregion

    /// <summary>
    /// The player character combo handler.
    /// </summary>
    public static class ComboHandler
    {
        /// <summary>
        /// List with all player combo move.
        /// </summary>
        private static readonly List<CharacterComboItem> AllMove = new ();

        /// <summary>
        /// On Activate method. Perform once per player.
        /// </summary>
        /// <param name="state">
        /// Run state.
        /// </param>
        public static void OnActivate(TemplateState state)
        {
            AllMove.Add(new CharacterComboItem(0, state, "→→"));
            AllMove.Add(new CharacterComboItem(0, state, "←←"));
        }

        /// <summary>
        /// Find what combo player want to perform.
        /// </summary>
        /// <param name="characterActionHandler">
        /// Player input buffer.
        /// </param>
        /// <returns>
        /// State to be performed.
        /// </returns>
        public static TemplateState CheckForAction(CharacterActionHandler[] characterActionHandler)
        {
            List<CharacterComboItem> availableMove = AllMove;
            string searchForCombo = string.Empty;
            foreach (var key in characterActionHandler)
            {
                searchForCombo += key.CharacterActionItem;
                availableMove = availableMove.Where(x => x.GetMoveKeysCode().Contains(searchForCombo)).ToList();

                var result = availableMove.LastOrDefault();

                if (searchForCombo.Equals(result.GetMoveKeysCode()))
                {
                    return result.GetName();
                }
            }

            return null;
        }
    }
}