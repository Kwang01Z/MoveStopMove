using System.Collections;
using UnityEngine;

public class SetFullState : ISkinShopState
{
    public string GetItemNameSave()
    {
        return UISetFullDisplay.Instance.GetCurrentSkin().GetSaveTxt();
    }

    public int GetItemPrice()
    {
        return UISetFullDisplay.Instance.GetCurrentSkin().Price;
    }

    public void OnBuyItem()
    {
        SaveManager.Instance.AddCoin(-GetItemPrice());
        SaveManager.Instance.AddSkin(GetItemNameSave());
    }

    public void OnChangeItem(int a_int)
    {
        UISetFullDisplay.Instance.ChangeSkin(a_int);
    }

    public void OnSelectItem()
    {
        SaveManager.Instance.SelectSetFull(UISetFullDisplay.Instance.GetCurrentSkin());
    }
}