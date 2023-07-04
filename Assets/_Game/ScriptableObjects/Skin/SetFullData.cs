using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "SetFullData", menuName = "ScriptableObject/SetFullData")]
public class SetFullData : SkinData<SetFullInfo>
{

}
[System.Serializable]
public class SetFullInfo : SkinInfo
{
    public Sprite IconDisplay;
    public GameObject HeadSkin;
    public Material BodyMat;
    public Material PantSkin;
    public GameObject WingSkin;
    public GameObject TailSkin;
    public GameObject WeaponSkin;
    public override string GetSaveTxt()
    {
        return "SetFull" + "_" + Name;
    }
}