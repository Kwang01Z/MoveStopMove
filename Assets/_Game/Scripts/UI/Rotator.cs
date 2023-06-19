using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Lấy tham chiếu đến thành phần Transform của đối tượng
    [SerializeField] RectTransform objectTransform;

    // Xác định tốc độ xoay (ví dụ: 90 độ/giây)
    [SerializeField] float rotationSpeed = 8f;
    float rotationAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        // Tính góc xoay dựa trên thời gian và tốc độ
        rotationAngle += rotationSpeed;

        // Xoay đối tượng theo trục Y với tốc độ cho trước
        objectTransform.localRotation = Quaternion.Euler(0,rotationAngle,130);
    }
}
