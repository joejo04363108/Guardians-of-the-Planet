using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchstart4 : MonoBehaviour
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
        SceneManager.LoadSceneAsync("ToolScene", LoadSceneMode.Additive); 
        SceneManager.LoadSceneAsync("MarsBase"); 
        //SceneManager.UnloadSceneAsync(nextSceneName);
        
    }
}
