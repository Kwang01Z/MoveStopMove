using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkin : MonoBehaviour
{
    [SerializeField] Transform m_HeadPosition;
    [SerializeField] SkinnedMeshRenderer m_PantRender;
    [SerializeField] SkinnedMeshRenderer m_BodyRender;
    GameObject currentHeadskin;
    public GameObject SetHeadSkin(GameObject a_HeadSkin)
    {
        if (currentHeadskin != null) Destroy(currentHeadskin);
        currentHeadskin = Instantiate(a_HeadSkin, m_HeadPosition);
        return currentHeadskin;
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
