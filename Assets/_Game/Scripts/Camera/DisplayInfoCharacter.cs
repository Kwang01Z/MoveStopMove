using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayInfoCharacter : MonoBehaviour
{
    [SerializeField] TextMeshPro m_NameCharacter;
    [SerializeField] TextMeshPro m_LevelCharacter;
    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }
    public void SetName(string a_NameText)
    {
        m_NameCharacter.SetText(a_NameText);
    }
    public void SetLevel(int a_level)
    {
        m_LevelCharacter.SetText(a_level.ToString());
    }
}
