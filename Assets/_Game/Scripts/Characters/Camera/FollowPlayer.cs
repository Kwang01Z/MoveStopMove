using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform m_Target = null;
    Vector3 m_Distance = Vector3.zero;
    private void Start()
    {
        m_Distance = transform.position - m_Target.position;
    }

    void LateUpdate()
    {
        Interpolate();
    }

    void Interpolate()
    {
        if (m_Target == null)
        {
            return;
        }
        Vector3 diff = m_Target.position - transform.position;
        transform.position += diff + m_Distance;
    }
}
