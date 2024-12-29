using System.Collections;
using TMPro;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // 指定TextMeshPro元件
    public float revealSpeed = 0.05f;  // 每個字出現的時間間隔

    private void Start()
    {
        // 確保初始文字內容隱藏
        textMeshPro.maxVisibleCharacters = 0;
        StartCoroutine(RevealText());
    }

    private IEnumerator RevealText()
    {
        int totalCharacters = textMeshPro.text.Length;
        for (int i = 0; i <= totalCharacters; i++)
        {
            textMeshPro.maxVisibleCharacters = i; // 設置可見字符數
            yield return new WaitForSeconds(revealSpeed);
        }
    }
}
