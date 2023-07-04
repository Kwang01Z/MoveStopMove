using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PantSkinData", menuName = "ScriptableObject/PantSkinData")]
public class PantSkinData : SkinData<PantSkinInfo>
{

}
[System.Serializable]
public class PantSkinInfo : SkinInfo
{
    public Sprite IconDisplay;
    public Material Material;
    public override string GetSaveTxt()
    {
        return "PantSkin" + "_" + Name;
    }
}