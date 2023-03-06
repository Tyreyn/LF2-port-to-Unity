// <copyright file="ComboHandler.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>
namespace Assets.Scripts.InputSystem
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion

    /// <summary>
    /// The player character combo handler.
    /// </summary>
    public class ComboHandler
    {
        /// <summary>
        /// List with all player combo move.
        /// </summary>
        private readonly List<CharacterComboItem> AllMove = new();

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboHandler"/> class.
        /// </summary>
        /// <param name="p">
        /// Player GameObject.
        /// </param>
        /// <param name="state">
        /// Run state.
        /// </param>
        public ComboHandler(TemplateState state)
        {
            AllMove.Add(new CharacterComboItem(0, state, "→→"));
            AllMove.Add(new CharacterComboItem(0, state, "←←"));
        }

        #endregion Constructors and Destructors

        #region Public Methods

        /// <summary>
        /// Find what combo player want to perform.
        /// </summary>
        /// <param name="characterActionHandler">
        /// Player input buffer.
        /// </param>
        /// <returns>
        /// State to be performed.
        /// </returns>
        public TemplateState CheckForAction(CharacterActionHandler[] characterActionHandler)
        {
            List<CharacterComboItem> availableMove = this.AllMove;
            string searchForCombo = string.Empty;
            foreach (var key in characterActionHandler)
            {
                searchForCombo += key.CharacterActionItem;
                availableMove = availableMove.Where(x => x.GetMoveKeysCode().Contains(searchForCombo)).ToList();

                if (availableMove.Count == 0)
                {
                    return null;
                }

                var result = availableMove.LastOrDefault();

                if (searchForCombo.Equals(result.GetMoveKeysCode()))
                {
                    return result.GetName();
                }
            }

            return null;
        }
        #endregion
    }
}