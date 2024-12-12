using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
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
}
