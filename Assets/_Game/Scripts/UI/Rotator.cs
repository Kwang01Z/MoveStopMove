using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] RectTransform objectTransform;
    [SerializeField] float rotationSpeed = 8f;
    float rotationAngle = 0f;

    public float rotZ ;
    private void Start()
    {
        rotZ = objectTransform.localRotation.eulerAngles.z;
    }
    void Update()
    {
        rotationAngle += rotationSpeed;
        objectTransform.localRotation = Quaternion.Euler(0,rotationAngle, rotZ);
    }
}
