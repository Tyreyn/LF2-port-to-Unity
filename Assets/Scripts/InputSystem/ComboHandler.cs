using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

public static class ComboHandler
{
    public static List<CharacterComboItem> allMove = new List<CharacterComboItem>();

    public static void OnActivate(State state)
    {
        allMove.Add(new CharacterComboItem(0, state, "→→"));
    }

    public static State CheckForAction(CharacterActionHandler[] characterActionHandler)
    {
        List<CharacterComboItem> availableMove = allMove;
            //allMove.Where(x => x.getMoveKeysCode().Count() >= characterActionHandler.Count()).ToList();
        string searchForCombo = string.Empty;
        foreach (var key in characterActionHandler)
        {
            searchForCombo += key.CharacterActionItem;
            availableMove = availableMove.Where(x => x.getMoveKeysCode().Contains(searchForCombo)).ToList();

            var result = availableMove.LastOrDefault();
            if (searchForCombo.Equals(result.getMoveKeysCode()))
            {
                return result.getName();
            }
        }
        //foreach (CharacterComboItem action in allMove)
        //{
        //    if (action.Equals(characterActionHandler[i].CharacterActionItem))
        //    {
        //        return action.getName();
        //    }
        //    i++;
        //}

        return null;
    }
}
