using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvartarShop : BaseMapObject
{
    public override string GetActionLabel()
    {
        return "Talk";
    }

    public override void CharacterAction(CharacterControl characterControl)
    {
        base.CharacterAction(characterControl);

        AvatarShopChatingPopup.ShowPopup();
    }
}
