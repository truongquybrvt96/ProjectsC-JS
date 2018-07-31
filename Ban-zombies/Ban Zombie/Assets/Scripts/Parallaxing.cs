using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

    public Transform[] backGrounds;  //Mảng transform chứa các nền
    private float[] tiLeParallax;  //
    public float doMuot = 1f;
    private Transform cam;
    private Vector3 previousCamPos;

    void Awake()
    {
        cam = Camera.main.transform;
    }


	// Use this for initialization
	void Start () {
        previousCamPos = cam.position;
        tiLeParallax = new float[backGrounds.Length];
        for(int i = 0; i < backGrounds.Length; i++)
        {
            tiLeParallax[i] = backGrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0; i < backGrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * tiLeParallax[i];
            float backGroundTargetPosX = backGrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backGroundTargetPosX, backGrounds[i].position.y, backGrounds[i].position.z);
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, backgroundTargetPos, doMuot * Time.deltaTime);

        }
        previousCamPos = cam.position;
	}
}
