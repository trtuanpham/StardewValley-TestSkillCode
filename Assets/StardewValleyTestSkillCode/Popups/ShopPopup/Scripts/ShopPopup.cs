using UnityEngine;
using System.Collections;
public class ShopPopup : BasePopup
{
    public static string NAME_POPUP = "ShopPopup";
    public static ShopPopup ShowPopup()
    {
        Hashtable hashtable = new Hashtable();
        return PopupController.Instance.ShowPopup(NAME_POPUP, hashtable) as ShopPopup;
    }
    
    public static void HidePopup()
    {
        PopupController.Instance.HidePopup(NAME_POPUP);
    }
    
    //CODE_HERE
    
    protected override void OnShow(object data)
    {
        base.OnShow(data);
        if (data != null)
        {
             Hashtable hashtable = data as Hashtable;
        }
    }
    
    protected override void OnHide()
    {
        base.OnHide();
    }
}
