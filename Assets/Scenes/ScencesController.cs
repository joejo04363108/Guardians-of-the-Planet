using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesController : MonoBehaviour
{
    public string targetScene; // 要傳送的目標場景名稱
    public float triggerTime = 1.0f; // 延遲時間
    private bool isTeleporting = false; // 防止重複觸發
    private float stayTimer = 0.0f;  // 記錄主角停留的時間
    void Update()
    {
        // 按下 P 鍵切換場景
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 切換到 EarthBase，如果當前在 TestScene2，否則回到 TestScene2
            string currentScene = SceneManager.GetActiveScene().name;
            string nextScene = currentScene == "TestScene2" ? "EarthBase" : "TestScene2";

            SceneManager.LoadScene(nextScene);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 確認是主角 (bob) 並且未開始傳送
        if (collision.CompareTag("bob") && !isTeleporting)
        {
            stayTimer += Time.deltaTime; // 計算主角停留的時間

            // 如果超過指定時間，開始傳送
            if (stayTimer >= triggerTime)
            {
                isTeleporting = true; // 防止多次觸發
                StartCoroutine(Teleport());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 當主角離開傳送點，重置計時器
        if (collision.CompareTag("bob"))
        {
            stayTimer = 0.0f;
        }
    }

    private IEnumerator Teleport()
    {
        // 可以在這裡添加效果（例如淡出或傳送音效）
        yield return null;

        // 切換到目標場景
        SceneManager.LoadScene(targetScene);
    }
}
