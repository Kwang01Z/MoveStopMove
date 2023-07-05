using System.Collections;
using UnityEngine;

public class ShieldSkinState : ISkinShopState
{
    public string GetItemNameSave()
    {
        return UIShieldDisplay.Instance.GetCurrentSkin().GetSaveTxt();
    }

    public int GetItemPrice()
    {
        return UIShieldDisplay.Instance.GetCurrentSkin().Price;
    }

    public void OnBuyItem()
    {
        SaveManager.Instance.AddCoin(GetItemPrice());
        SaveManager.Instance.AddSkin(GetItemNameSave());
    }

    public void OnChangeItem(int a_int)
    {
        UIShieldDisplay.Instance.ChangeSkin(a_int);
    }

    public void OnSelectItem()
    {
        SaveManager.Instance.SelectShielSkin(GetItemNameSave());
    }
}