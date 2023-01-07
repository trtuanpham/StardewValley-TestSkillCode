using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvartarShop : BaseMapObject
{
    [SerializeField] GameObject _chatBox;

    private void Awake()
    {
        _chatBox.SetActive(false);
    }

    public override string GetActionLabel()
    {
        return "Talk";
    }

    public override void CharacterAction(CharacterControl characterControl)
    {
        base.CharacterAction(characterControl);

        AvatarShopChatingPopup.ShowPopup();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == CharacterControl.TAG)
        {
            this.RunDelay(()=>{
                _chatBox.SetActive(true);
            },2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == CharacterControl.TAG)
        {
            _chatBox.SetActive(false);
        }
    }
}
