using UnityEngine;
using System.Collections;

public class DestroyCoin : MonoBehaviour {


    private Rigidbody2D rb;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(0f, 100f), 500f));
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, 4f);
	}
}
