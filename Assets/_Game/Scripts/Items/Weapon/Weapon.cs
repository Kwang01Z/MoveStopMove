using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float m_SpeedMove = 8f;
    [SerializeField] protected Collider m_Collider;
    [SerializeField] protected ParticleSystem m_ParticleSystem;
    [SerializeField] protected AudioSource m_AudioSource;
    protected Character m_CharacterUse;
    protected Vector3 m_TargetPos;
    protected Transform m_WeaponParent;
    protected Vector3 m_OldPosision;
    protected Quaternion m_OldRotation;
    protected bool m_IsAttacking;
    protected float m_MaxDistance;
    protected AudioClip throwSound;
    private void Reset()
    {
        m_Collider = GetComponent<Collider>();
    }
    private void Awake()
    {
        m_Collider.enabled = false;
        if (m_ParticleSystem != null)
        {
            ParticleSystem.MainModule module = m_ParticleSystem.main;
            module.playOnAwake = true;
            m_ParticleSystem?.transform.gameObject.SetActive(false);
        }
    }
    protected void Update()
    {
        if (!m_IsAttacking) return;
        UpdateAttack();
    }
    public virtual void StartAttack(Vector3 a_TargetPos, Transform a_WeaponPool, float a_MaxDistance, Character a_character)
    {
        if (m_IsAttacking) return;
        // Lưu vi trí cũ
        m_OldPosision = transform.localPosition;
        m_OldRotation = transform.localRotation;
        m_WeaponParent = transform.parent;
        // Thiết lập tt cơ bản
        m_TargetPos = new Vector3(a_TargetPos.x, transform.position.y, a_TargetPos.z);
        transform.parent = a_WeaponPool;
        m_IsAttacking = true;

        m_MaxDistance = a_MaxDistance;
        m_Collider.enabled = true;
        m_CharacterUse = a_character;
        if (m_ParticleSystem != null)
        {
            m_ParticleSystem?.transform.gameObject.SetActive(true);
            m_ParticleSystem.transform.position = transform.position;
            m_ParticleSystem.transform.parent = a_WeaponPool;
        }
        if (m_AudioSource != null)
        {
            m_AudioSource.Play();
        } 
    }
    protected virtual void UpdateAttack()
    {
        if (m_ParticleSystem != null)
        {
            m_ParticleSystem.transform.position = transform.position;
        }
    }
    public virtual void EndAttack()
    {
        m_IsAttacking = false;
        if (m_WeaponParent)
        {
            transform.parent = m_WeaponParent;
            if (m_ParticleSystem != null)
            {
                m_ParticleSystem.transform.parent = transform;
            }
        }
        else
        {
            if (m_ParticleSystem != null)
            {
                Destroy(m_ParticleSystem.transform.gameObject);
            }
            Destroy(gameObject);
        }
        transform.localPosition = m_OldPosision;
        transform.localRotation = m_OldRotation;
        m_Collider.enabled = false;
        if (m_ParticleSystem != null)
        {
            m_ParticleSystem.transform.gameObject.SetActive(false);
        }
    }
    public bool IsAttacking()
    {
        return m_IsAttacking;
    }
    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null && character.GetWeapon() != this)
        {
            character.Damaged(m_CharacterUse);
            EndAttack();
        }
    }
}
public enum WeaponType
{
    HAMMER = 0,
    LOLLIPOP = 1,
    KNIFE = 2,
    CANDY_CANE = 3,
    BOOMERANG = 4,
    SWERPLY_POP = 5,
    AXE = 6,
    ICE_CREAM_CONE = 7,
    BATTLE_AXE = 8,
    Z = 9,
    ARROW = 10,
    UZI = 11
}
