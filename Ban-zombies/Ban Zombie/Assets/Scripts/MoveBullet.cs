using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour {

    public int moveSpeed = 230;
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed); //Di chuyển đường đạn với moveSpeed * Time.deltaTime (không bị ảnh hưởng bởi tốc độ khung hình)
        Destroy(gameObject, 1); //Hủy đối tượng đường đạn sau 1 giây
	}
}
