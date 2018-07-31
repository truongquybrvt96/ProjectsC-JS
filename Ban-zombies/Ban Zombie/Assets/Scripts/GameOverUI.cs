using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverUI : MonoBehaviour {


    public Button retry;
    public Button exit;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("Khong co audioManager trong GameOverUI!");
        }
        retry.onClick.AddListener(() =>
        {
            Application.LoadLevel(1);
            audioManager.PlaySound("GameMusic");
        });
        exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

}
