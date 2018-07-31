using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoresCounter : MonoBehaviour
{

    private Text scoresText;

    void Awake()
    {
        scoresText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoresText.text = "Điểm: " + GameMaster.gm.score.ToString();
    }
}
