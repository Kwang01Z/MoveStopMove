using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterControllerBase
{
    [SerializeField] UltimateJoystick m_Joystick;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] float m_MoveSpeed;
    
    Vector3 m_MoveDirection = Vector3.zero;
    
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        MoveUpdate();
    }
    void MoveUpdate()
    {
        m_MoveDirection = new Vector3(m_Joystick.GetHorizontalAxis(), m_MoveDirection.y, m_Joystick.GetVerticalAxis()).normalized;
        m_MoveVelocity = m_MoveDirection * m_MoveSpeed;
        m_Rigidbody.velocity = m_MoveVelocity;
        RotateObject(m_MoveDirection);
    }
    
}
