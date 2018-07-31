using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public Camera mainCam;
    float shakeAmount;

    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }


	public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("DoShake", 0, 0.01f); //Lặp phương thức BeginShake với độ trễ là 0, tỉ lệ lặp 0.01
        Invoke("StopShake", length); //Dừng rung sau 1 khoảng thời gian length;
    }
    void DoShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position; //Tạo 1 biến tạm để thay đổi vị trí camera

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount; //Công thức rung theo trục X (ngẫu nhiên)
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount; //Rung ngẫu nhiên theo trục Y

            camPos.x += offsetX; //Thay đổi
            camPos.y += offsetY;

            mainCam.transform.position = camPos; //Lưu vị trí mới
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
