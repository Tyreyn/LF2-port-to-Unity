using Scripts.InputSystem;
using Scripts.Templates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.InputSystem
{
    public static class ComboHandler
    {
        public static List<CharacterComboItem> allMove = new();

        public static void OnActivate(TemplateState state)
        {
            allMove.Add(new CharacterComboItem(0, state, "→→"));
        }

        public static TemplateState CheckForAction(CharacterActionHandler[] characterActionHandler)
        {
            List<CharacterComboItem> availableMove = allMove;
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