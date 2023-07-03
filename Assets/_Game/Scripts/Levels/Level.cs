using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Level : MonoBehaviour
{
    public NavMeshData NavMeshData;
    public Transform StartPoint;
    public int BotAmount;
    public int BotSpawnPerTime;
    public float MaxRadius;
    public float MinRadius;
    public Vector3 GetRandomNavPosion()
    {
        return Vector3.zero;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MaxRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, MinRadius);
    }
}
