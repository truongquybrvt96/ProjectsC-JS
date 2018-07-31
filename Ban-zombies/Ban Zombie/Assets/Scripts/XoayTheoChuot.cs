using UnityEngine;
using System.Collections;

public class XoayTheoChuot : MonoBehaviour {


    public Transform nguoiChoi;
    public int roOffset;
	void Update () {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); //Tối giản 3 giá trị vector mà vẫn giữ nguyên hướng cũ. 3 giá trị cộng lại = 1

        //Tìm góc giữ vector và hoành độ x
        float roZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
        if(nguoiChoi.localScale.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -roZ + roOffset);
        }
        else if(nguoiChoi.localScale.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, roZ + roOffset);
        }
        
	}
}
