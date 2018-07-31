using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonsUI : MonoBehaviour {

    public Button play;
    public Button exit;

	// Use this for initialization
	void Start () {
        play.onClick.AddListener(() =>
        {
            Application.LoadLevel("Game");
        });
        exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
