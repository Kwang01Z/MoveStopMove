using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkinShopState
{
    public void OnChangeItem(int a_int);
    public void OnSelectItem();
    public void OnBuyItem();
    public int GetItemPrice();
    public string GetItemNameSave();
}
