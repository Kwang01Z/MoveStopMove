using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShop : MonoBehaviour
{
    public virtual void ChangeSkin(int a_SkinIndex)
    { }
    public virtual int SkinCount() { return 0; }
    public virtual int SkinPrice() { return 0; }
    public virtual string SkinName() { return "skinname"; }
    public virtual void SetSkinForChar(CharacterSkin characterSkin) { }
}
