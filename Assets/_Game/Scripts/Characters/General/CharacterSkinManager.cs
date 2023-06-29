using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinManager : MonoBehaviour
{
    [SerializeField] CharacterSkin m_CharacterSkin;
    [SerializeField] CharacterControllerBase m_Character;
    public CharacterSkin GetCharacterSkin => m_CharacterSkin;
    void Start()
    {
        m_Character.SetSkinManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
