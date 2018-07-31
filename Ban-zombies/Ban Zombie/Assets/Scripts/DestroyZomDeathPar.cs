using UnityEngine;
using System.Collections;

public class DestroyZomDeathPar : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, 0.8f);
	}
}
