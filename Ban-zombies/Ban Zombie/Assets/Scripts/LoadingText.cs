using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LoadingText : MonoBehaviour {

    private Text loadingText;

    void Awake()
    {
        loadingText = GetComponent<Text>();
    }

    void Start()
    {
        loadingText.text = "Loading...";
    }
}
