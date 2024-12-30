using UnityEngine;
using UnityEngine.UIElements;

public class OpenCanvasOnKeyPress : MonoBehaviour
{
    public GameObject canvas; // 要開啟的 UI 或物件
    private bool isCanvasActive = false;
    public KeyCode Keyboard; 

    void Start()
    {
        // 確保遊戲開始時隱藏 canvas
        if (canvas != null)
        {
            canvas.SetActive(false);
            isCanvasActive = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(Keyboard))
        {
            // 切換 Canvas 的顯示狀態
            isCanvasActive = !isCanvasActive;
            canvas.SetActive(isCanvasActive);
        }
    }
}