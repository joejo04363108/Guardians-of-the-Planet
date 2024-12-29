using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string nextSceneName; // 設置下一個場景的名稱

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 檢測空白鍵
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // 加載指定場景
        }
        else
        {
            Debug.LogError("未設置下一個場景的名稱！");
        }
    }
}