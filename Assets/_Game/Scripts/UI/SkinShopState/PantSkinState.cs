using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantSkinState : ISkinShopState
{
    public string GetItemNameSave()
    {
        return UIPantSkinItem.Instance.GetCurrentSkin().GetSaveTxt();
    }

    public int GetItemPrice()
    {
        return UIPantSkinItem.Instance.GetCurrentSkin().Price;
    }

    public void OnBuyItem()
    {
        SaveManager.Instance.AddCoin(GetItemPrice());
        SaveManager.Instance.AddSkin(GetItemNameSave());
    }

    public void OnChangeItem(int a_int)
    {
        UIPantSkinItem.Instance.ChangeSkin(a_int);
    }

    public void OnSelectItem()
    {
        SaveManager.Instance.SelectPantSkin(GetItemNameSave());
    }
}
