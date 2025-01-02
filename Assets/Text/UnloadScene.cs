using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour
{
    public string nextSceneName; // 設置下一個場景的名稱
    int cnt = 0;
    public GameObject test;
    public int max_cnt;
    void Update()
    {
        if(test == null) test = GameObject.FindWithTag("111");
        if (Input.GetKeyDown(KeyCode.Space)) // 檢測空白鍵
        {
            cnt++;
            if(cnt>= max_cnt){
                LoadNextScene();
                //test.SetActive(false);
                
            } 
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.UnloadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("未設置下一個場景的名稱！");
        }
    }
}
