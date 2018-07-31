using UnityEngine;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    AudioManager audioManager;

    [SerializeField]
    private GameObject aboutUIObj;

    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("MenuManager: Ko tim thay audioManager");
        }
        audioManager.PlaySound("Music");
    }

	public void Play()
    {
        audioManager.PlaySound(pressButtonSound);
        Application.LoadLevel("Game");
    }
    public void Quit()
    {
        audioManager.PlaySound(pressButtonSound);
        Application.Quit();
    }
    public void About()
    {
        audioManager.PlaySound(pressButtonSound);
        aboutUIObj.SetActive(true);
        Debug.Log("About");
    }
    public void Back()
    {
        aboutUIObj.SetActive(false);
    }
    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }
}
