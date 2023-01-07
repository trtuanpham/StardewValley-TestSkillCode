using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvartarShopTriggerCharacter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            AvatarShopChatingPopup.ShowPopup();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
        {
            AvatarShopChatingPopup.HidePopup();
        }
    }
}
