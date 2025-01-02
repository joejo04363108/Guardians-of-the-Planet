using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    // 按鈕點擊事件
    public void TogglButton_1()
    {
         SceneManager.LoadScene("Start1");
        
    }

    public void TogglButton_2()
    {
        Debug.Log("123");
        
    }
    public void TogglButton_3()
    {
        SceneManager.LoadScene("Start0");
        ClearAllDontDestroyOnLoad();
        
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("EarthBase");
    }


    public void ClearAllDontDestroyOnLoad()
    {
        // 獲取隱藏的 DontDestroyOnLoad 場景
        Scene dontDestroyOnLoadScene = GetDontDestroyOnLoadScene();

        if (dontDestroyOnLoadScene.IsValid())
        {
            foreach (GameObject obj in dontDestroyOnLoadScene.GetRootGameObjects())
            {
                Destroy(obj); // 刪除物件
            }
        }
    }

    private Scene GetDontDestroyOnLoadScene()
    {
        // 將一個暫時物件標記為 DontDestroyOnLoad 並取得其場景
        GameObject temp = new GameObject("Temp");
        DontDestroyOnLoad(temp);
        Scene scene = temp.scene;
        Destroy(temp); // 刪除暫時物件
        return scene;
    }

}