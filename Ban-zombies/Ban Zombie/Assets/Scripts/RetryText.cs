using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RetryText : MonoBehaviour {

    private Text retryText;
	// Use this for initialization
	void Start () {
        retryText = GetComponent<Text>();
        retryText.text = "Chơi lại";
	}
	
	// Update is called once per frame
}
