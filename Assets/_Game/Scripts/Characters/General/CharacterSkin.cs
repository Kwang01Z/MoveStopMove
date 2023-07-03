using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkin : MonoBehaviour
{
    [SerializeField] Character m_Character;
    [SerializeField] Transform m_HeadPosition;
    [SerializeField] SkinnedMeshRenderer m_PantRender;
    [SerializeField] SkinnedMeshRenderer m_BodyRender;
    GameObject m_CurrentHeadSkin;
    private void Reset()
    {
        m_Character = GetComponentInParent<Character>();
    }
    private void Awake()
    {
        m_Character.SetCharacterSkin(this);
    }
    public GameObject SetHeadSkin(string a_HeadSkin)
    {
        if (m_CurrentHeadSkin != null) Destroy(m_CurrentHeadSkin);
        m_CurrentHeadSkin = SkinManager.Instance.GetHeadSkin(a_HeadSkin, m_HeadPosition);
        return m_CurrentHeadSkin;
    }
    public void SetPant(Material a_PantMat)
    {
        m_PantRender.material = a_PantMat;
    }
    public void SetBody(Material a_BodyMat)
    {
        m_BodyRender.material = a_BodyMat;
    }
}
