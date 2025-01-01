using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher123213 : MonoBehaviour
{
    // 指定要切換的場景名稱
    public string targetSceneName;

    // 更新方法，檢查鍵盤輸入
    void Update()
    {
        // 檢查是否按下 P 鍵
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchSceneByName(targetSceneName);
        }
    }

    // 切換到指定名稱的場景
    public void SwitchSceneByName(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName) != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
        }
    }
}