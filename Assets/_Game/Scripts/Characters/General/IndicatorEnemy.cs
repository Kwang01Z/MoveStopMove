using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorEnemy : MonoBehaviour
{
    [SerializeField] GameObject m_Indicator;
    [SerializeField] GameObject m_FollowTarget;
    public void SetTarget(Vector3 a_Target)
    {
        m_Indicator.SetActive(true);
        m_Indicator.transform.position = a_Target;
    }
    public void OffIndicator()
    {
        m_Indicator.SetActive(false);
    }
    public void SetFollow(float a_Range, bool a_active)
    {
        m_FollowTarget.transform.localScale = Vector3.one * a_Range * 0.42f;
        m_FollowTarget.SetActive(a_active);
    }
}
