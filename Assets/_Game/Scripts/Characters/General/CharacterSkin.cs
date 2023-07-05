using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkin : MonoBehaviour
{
    [SerializeField] Character m_Character;
    [SerializeField] Transform m_HeadPosition;
    [SerializeField] Transform m_WingPosition;
    [SerializeField] Transform m_TailPosition;
    [SerializeField] Transform m_LeftHandPosition;
    [SerializeField] SkinnedMeshRenderer m_PantRender;
    [SerializeField] SkinnedMeshRenderer m_BodyRender;
    GameObject m_CurrentHeadSkin;
    GameObject m_CurrentWingSkin;
    GameObject m_CurrentTailSkin;
    GameObject m_CurrentWeaponSkin;
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
    public GameObject SetShieldSkin(string a_ShieldSkin)
    {
        if (m_CurrentWeaponSkin != null) Destroy(m_CurrentWeaponSkin);
        m_CurrentWeaponSkin = SkinManager.Instance.GetShieldSkin(a_ShieldSkin, m_LeftHandPosition);
        return m_CurrentWeaponSkin;
    }
    public void SetPant(Material a_PantMat)
    {
        m_PantRender.material = a_PantMat;
    }
    public void SetBody(Material a_BodyMat)
    {
        m_BodyRender.material = a_BodyMat;
    }
    public void SetFull(SetFullInfo set)
    {
        if (m_CurrentHeadSkin != null) Destroy(m_CurrentHeadSkin);
        if (m_CurrentWingSkin != null) Destroy(m_CurrentWingSkin);
        if (m_CurrentTailSkin != null) Destroy(m_CurrentTailSkin);
        if (m_CurrentWeaponSkin != null) Destroy(m_CurrentWeaponSkin);
        if (set.HeadSkin != null)
        {
            m_CurrentHeadSkin = Instantiate(set.HeadSkin, m_HeadPosition);
        }
        if (set.BodyMat != null)
        {
            m_BodyRender.material = set.BodyMat;
        }
        if (set.PantSkin != null)
        {
            m_PantRender.material = set.PantSkin;
        }
        if (set.WingSkin != null)
        {
            m_CurrentWingSkin = Instantiate(set.WingSkin, m_WingPosition);
        }
        if (set.TailSkin != null)
        {
            m_CurrentTailSkin = Instantiate(set.TailSkin, m_TailPosition);
        }
        if (set.WeaponSkin != null)
        {
            m_CurrentWeaponSkin = Instantiate(set.WeaponSkin, m_LeftHandPosition);
        }
    }
}
