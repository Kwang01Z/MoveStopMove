using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSkinData", menuName = "ScriptableObject/ShieldSkinData")]
public class ShieldSkinData : SkinData<ShieldInfo>
{

}
[System.Serializable]
public class ShieldInfo : SkinInfo
{
    public GameObject Prefab;
    public override string GetSaveTxt()
    {
        return "ShieldSkin" + "_" + Name;
    }
}