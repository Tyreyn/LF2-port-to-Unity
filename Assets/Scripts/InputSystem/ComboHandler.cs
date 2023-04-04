// <copyright file="ComboHandler.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>
namespace Assets.Scripts.InputSystem
{
    using System;
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Loader;
    using Assets.Scripts.StateMachine.State;
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


        public Player player;
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboHandler"/> class.
        /// </summary>
        /// <param name="player">
        /// Player GameObject.
        /// </param>
        public ComboHandler(Player player)
        {
            this.player = player;
            this.LoadCharacterSkills();
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
                string movers = string.Empty;
                searchForCombo += key.CharacterActionItem;
                Debug.Log("[Combo]" + searchForCombo);
                availableMove = availableMove.Where(x => x.GetMoveKeysCode().Contains(searchForCombo)).ToList();

                foreach (var move in availableMove)
                {
                    movers += move.GetMoveKeysCode();
                }

                Debug.Log("[Combo]" + movers);

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

        private void LoadCharacterSkills()
        {
            this.AllMove.Add(new CharacterComboItem(0, this.player.StateMachine.Run, "→→"));
            this.AllMove.Add(new CharacterComboItem(0, this.player.StateMachine.Run, "←←"));

            CharacterList characterList = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetCharacterList();

            Character character = characterList.characters.FirstOrDefault(x => x.Name.Contains(this.player.CharacterName));

            if (character.Combo != null)
            {
                foreach (SkillTemplate skill in character.Combo)
                {
                    TemplateState newState = (TemplateState)Activator.CreateInstance(Type.GetType(string.Format("Assets.Scripts.StateMachine.State.{0}", skill.Name)), this.player.gameObject, this.player.StateMachine);
                    this.player.StateMachine.AddNewSkillState(newState);

                    if (skill.Input.Contains("→"))
                    {
                        this.AllMove.Add(new CharacterComboItem(0, newState, skill.Input.Replace("→", "←")));
                    }
                    else if (skill.Input.Contains("←"))
                    {
                        this.AllMove.Add(new CharacterComboItem(0, newState, skill.Input.Replace("←", "→")));
                    }

                    this.AllMove.Add(new CharacterComboItem(0, newState, skill.Input));
                }
            }
        }
        #endregion
    }
}