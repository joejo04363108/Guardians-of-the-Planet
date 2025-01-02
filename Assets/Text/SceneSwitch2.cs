using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch2 : MonoBehaviour
{
    // Start is called before the first frame update

    public string nextSceneName;
    int cnt = 0;
    public bool is_run = false;
    public int max_cnt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 檢測空白鍵
        {
            Debug.Log("Mars");
            cnt++;
            if(cnt >= max_cnt) LoadNextScene();
        }
       
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            if(is_run) return;
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive); 
            is_run = true;
        }
        else
        {
            Debug.LogError("未設置下一個場景的名稱！");
        }
    }
}
