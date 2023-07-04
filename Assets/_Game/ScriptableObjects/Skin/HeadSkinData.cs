using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HeadSkinData", menuName = "ScriptableObject/HeadSkinData")]
public class HeadSkinData : SkinData<HeadSkinInfo>
{

}
[System.Serializable]
public class HeadSkinInfo : SkinInfo
{
    public GameObject Prefab;
    public override string GetSaveTxt()
    {
        return "HeadSkin_" + Name;
    }
}