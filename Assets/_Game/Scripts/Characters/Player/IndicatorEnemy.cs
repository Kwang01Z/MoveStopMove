using System.Collections;
using UnityEngine;

public class IndicatorEnemy : MonoBehaviour
{

    [SerializeField] Transform m_Indicator;
    [SerializeField] Transform m_FollowTarget;
    public void SetTarget(Vector3 a_Target)
    {
        m_Indicator.gameObject.SetActive(true);
        m_Indicator.transform.position = a_Target;
    }
    public void OffIndicator()
    {
        m_Indicator.gameObject.SetActive(false);
    }
    public void SetFollow(float a_Range, bool a_active)
    {
        m_FollowTarget.transform.localScale = Vector3.one * a_Range * 0.84f;
        m_FollowTarget.gameObject.SetActive(a_active);
    }
}