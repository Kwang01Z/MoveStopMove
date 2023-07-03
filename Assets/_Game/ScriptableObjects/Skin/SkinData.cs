using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinData<T> : ScriptableObject where T : SkinInfo
{
    [SerializeField] protected List<T> m_Skins = new List<T>();
    public virtual List<string> GetListSkin()
    {
        List<string> result = new List<string>();
        m_Skins.ForEach((skin) =>
        {
            result.Add(skin.GetSaveTxt());
        });
        return result;
    }
    public virtual T GetSkin(string a_name)
    {
        T result = null;
        m_Skins.ForEach((skin) =>
        {
            if (skin.GetSaveTxt().Equals(a_name))
            {
                result = skin;
            }
        });
        if (result == null) Debug.LogError("cant find skin " + a_name);
        return result;
    }
    public virtual string GetRandomSkin()
    {
        int rand = Random.Range(0, m_Skins.Count);
        return m_Skins[rand].GetSaveTxt();
    }
}
public enum SkinType
{ 
    HeadSkin,
    PantSkin,
    ShieldSkin,
    SetFull
}

[System.Serializable]
public class SkinInfo
{
    public string Name;    
    public int Price;
    public virtual string GetSaveTxt()
    {
        return "Skin" + "_" + Name;
    }
}


