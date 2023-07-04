using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSkinState : ISkinShopState
{
    public string GetItemNameSave()
    {
        return UIHeadSkinItem.Instance.GetCurrentSkin().GetSaveTxt();
    }

    public int GetItemPrice()
    {
        return UIHeadSkinItem.Instance.GetCurrentSkin().Price;
    }

    public void OnBuyItem()
    {
        SaveManager.Instance.AddCoin(GetItemPrice());
        SaveManager.Instance.AddSkin(GetItemNameSave());
    }

    public void OnChangeItem(int a_int)
    {
        UIHeadSkinItem.Instance.ChangeSkin(a_int);
    }

    public void OnSelectItem()
    {
        SaveManager.Instance.SelectHeadSkin(GetItemNameSave());
    }
}
