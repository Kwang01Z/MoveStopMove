using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] CharacterData m_MainData = new CharacterData();
    [SerializeField] Player m_Player;
    public int Coin => m_MainData.Coin;
    public int CoinClaim => m_Player.CoinClaim;
    const string m_SavePath = "/Player.json";
    public CharacterData CharacterData => m_MainData;
    private void Awake()
    {
        LoadOrCreateNewData();
    }
    public void AddCoin(int a_coin)
    {
        m_MainData.Coin += a_coin;
    }
    public void TotleEndGame()
    {
        m_MainData.Coin += m_Player.CoinClaim;
    }
    public bool IsOwnedWeapon(string a_name)
    {
        return m_MainData.OwnedWeapons.Contains(a_name);
    }
    public bool IsOwnedSkin(string a_name)
    {
        return m_MainData.OwnedSkins.Contains(a_name);
    }
    public void AddSkin(string a_name)
    {
        m_MainData.OwnedSkins.Add(a_name);
    }
    public void AddWeapon(string a_weapon)
    {
        m_MainData.AddWeapon(a_weapon);
    }
    public void Reborn()
    {
        m_MainData.Coin -= 200;
        m_Player.Reborn();
    }
    public void SelectWeapon(string a_weapon)
    {
        m_MainData.EquippedWeapon = a_weapon;
        m_Player.UpdateWeapon();
    }
    public void SelectHeadSkin(string a_skin)
    {
        m_MainData.EquippedHeadSkin = a_skin;
        m_Player.UpdateHeadSkin();
    }
    public void SelectPantSkin(string a_skin)
    {
        m_MainData.EquippedPantSkin = a_skin;
        m_Player.UpdatePantSkin();
    }
    public void SelectSetFull(SetFullInfo a_set)
    {
        m_MainData.EquippedSetFull = a_set.GetSaveTxt();
        m_Player.UpdateSetFull();
    }
    public void SelectShielSkin(string a_skin)
    {
        m_MainData.EquippedShieldSkin = a_skin;
        m_Player.UpdateShieldSkin();
    }
    public void Save()
    {
        Debug.Log("Saving!");
        string json = JsonUtility.ToJson(m_MainData);
        File.WriteAllText(Application.persistentDataPath + m_SavePath, json);
    }
    public void LoadOrCreateNewData()
    {
        if (!File.Exists(Application.persistentDataPath + m_SavePath))
        {
            Debug.LogWarning("Do not Exits file save Player.json");
            Debug.Log("Begin create new data");
            m_MainData = CharacterData.CreateNew();
        }
        else
        {
            Debug.Log("Loading data!");
            try
            {
                string json = File.ReadAllText(Application.persistentDataPath + m_SavePath);
                m_MainData = JsonUtility.FromJson<CharacterData>(json);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Loading data error: " + e);
            }
        }
    }
#if UNITY_EDITOR
    [MenuItem("Game Editor/Clear Save")]
    public static void ClearSave()
    {
        Debug.Log("Clear Save!");
        try
        {
            File.Delete(Application.persistentDataPath + m_SavePath);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Clear Save error: " + e);
        }
    }
#endif 
}
[System.Serializable]
public class CharacterData
{
    public int Level;
    public int Coin;
    public string EquippedWeapon;
    public string EquippedHeadSkin;
    public string EquippedPantSkin;
    public string EquippedShieldSkin;
    public string EquippedSetFull;
    public List<string> OwnedWeapons = new List<string>();
    public List<string> OwnedSkins = new List<string>();
    public static CharacterData CreateNew()
    {
        CharacterData data = new CharacterData();
        data.Level = 0;
        data.Coin = 0;
        data.EquippedWeapon = WeaponManager.Instance.GetWeaponTxtByIndex(0);
        data.EquippedHeadSkin = "";
        data.EquippedPantSkin = "";
        data.EquippedSetFull = "";
        data.OwnedWeapons.Add(WeaponManager.Instance.GetWeaponTxtByIndex(0));
        return data;
    }
    public void AddWeapon(string a_weapon)
    {
        OwnedWeapons.Add(a_weapon);
    }
}
