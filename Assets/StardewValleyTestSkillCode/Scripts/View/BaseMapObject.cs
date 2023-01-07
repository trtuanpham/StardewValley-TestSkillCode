using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMapObject : BaseMonoBehaviour
{
    public virtual string GetActionLabel() { return ""; }
    public virtual void CharacterAction(CharacterControl characterControl) { }
}
