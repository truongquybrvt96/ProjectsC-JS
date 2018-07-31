using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AboutText : MonoBehaviour {

    private Text aboutText;
    
    void Awake()
    {
        aboutText = GetComponent<Text>();
        
    }
    void Start()
    {
        aboutText.text = "Đề tài giữa kỳ môn Lập trình C#.\nGame được phát triển trên nền tảng Unity dựa trên bài hướng dẫn của tác giả Brackeys, sử dụng UnityEngine, PathFindingEngine và ngôn ngữ C#.\nPhát triển bởi: Lê Trường Quý.\nLớp: 14SE111.\n";
    }
	
}
