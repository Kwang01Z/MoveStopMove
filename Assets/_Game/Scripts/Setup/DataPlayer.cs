using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class DataPlayer : MonoBehaviour
{
    public struct SkinOwnedData
    {
        public SkinType Type;
        public string Name;
    }
    public static DataPlayer Instance;
    int m_Level;
    public int Level => m_Level;
    public int m_Coin;
    public int Coin => m_Coin;
    [SerializeField] PlayerController m_Player;
    [SerializeField] List<WeaponType> m_OwnedWeapons;
    WeaponType m_EquipedWeapon = WeaponType.HAMMER;
    public WeaponType EquipedWeapon => m_EquipedWeapon;
    List<SkinOwnedData> m_OwnedSkins = new List<SkinOwnedData>();
    CharacterData m_MainData = new CharacterData();
    KeyValuePair<SkinType, string> m_CurrentSkin = new KeyValuePair<SkinType, string>();
    public KeyValuePair<SkinType, string> CurrentSkin => m_CurrentSkin;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        LoadOrCreateNewData();
    }

    public bool IsOwnedWeapon(WeaponType a_Type)
    {
        bool result = false;
        if (m_OwnedWeapons.Count > 0)
        {
            m_OwnedWeapons.ForEach((type) =>
            {
                if (type.Equals(a_Type)) result = true;
            });
        }
        return result;
    }
    public void AddWeapon(WeaponType a_weapon)
    {
        if (!IsOwnedWeapon(a_weapon))
        {
            m_OwnedWeapons.Add(a_weapon);
        }
    }
    public bool IsOwnedSkin(SkinType a_type, string a_name)
    {
        bool result = false;
        for (int i = 0; i < m_OwnedSkins.Count; i++)
        {
            if (m_OwnedSkins[i].Type.Equals(a_type) && m_OwnedSkins[i].Name.Equals(a_name))
            {
                result = true;
            }
        }
        return result;
    }
    public void AddSkin(SkinType a_type, string a_name)
    {
        if (!IsOwnedSkin(a_type, a_name))
        {
            SkinOwnedData data = new SkinOwnedData();
            data.Type = a_type;
            data.Name = a_name;
            m_OwnedSkins.Add(data);
        }
    }
    public void AddCoin(int a_coin)
    {
        m_Coin += a_coin;
    }
    public void ChangeWeapon(WeaponType a_WeaponType)
    {
        m_EquipedWeapon = a_WeaponType;
        m_Player.LoadWeapon();
    }
    public void ChangeSkin(KeyValuePair<SkinType, string> valuePair)
    {
        m_CurrentSkin = valuePair;
    }
    public void Save()
    {
        m_MainData.Level = m_Level;
        m_MainData.Coin = m_Coin;
        m_MainData.CurrentWeapon = m_EquipedWeapon;
        m_MainData.OwnedWeapons = m_OwnedWeapons;
        Debug.Log("Saving!");
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, m_MainData);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Saving error: " + e);
        }
        finally
        {
            file.Close();
        }
    }
    public void LoadOrCreateNewData()
    {
        if (!File.Exists(Application.persistentDataPath + "/Player.dat"))
        {
            Debug.LogWarning("Do not Exits file save Player.dat");
            Debug.Log("Begin create new data");
            m_MainData = CharacterData.CreateNew();
        }
        else
        {
            Debug.Log("Loading data!");
            FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                m_MainData = (CharacterData)formatter.Deserialize(file);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Loading data error: " + e);
            }
            finally
            {
                file.Close();
            }
        }
        m_Level = m_MainData.Level;
        m_Coin = m_MainData.Coin;
        m_EquipedWeapon = m_MainData.CurrentWeapon;
        m_OwnedWeapons = m_MainData.OwnedWeapons;
    }
#if UNITY_EDITOR
    [MenuItem("Game Editor/Clear Save")]
    public static void ClearSave()
    {
        Debug.Log("Clear Save!");
        try
        {
            File.Delete(Application.persistentDataPath + "/Player.dat");
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
    public WeaponType CurrentWeapon;
    public List<WeaponType> OwnedWeapons = new List<WeaponType>();
    public static CharacterData CreateNew()
    {
        CharacterData data = new CharacterData();
        data.Level = 0;
        data.Coin = 0;
        data.CurrentWeapon = WeaponType.HAMMER;
        data.OwnedWeapons.Add(WeaponType.HAMMER);
        return data;
    }
}
