using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherand_tp : MonoBehaviour
{
    // 指定要切換的場景名稱
    public string targetSceneName;
    public GameObject player;

    public GameObject box;

    public float x;

    public float y;
    public float z;

    public float revealDistance;


    void Start(){
        player = GameObject.FindWithTag("bob"); // 假設 Player 物件有 "Player" 標籤
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
    }
    // 更新方法，檢查鍵盤輸入
    void Update()
    {
        if(player == null) player = GameObject.FindWithTag("bob");
        // 檢查是否按下 F 鍵
        if (Input.GetKeyDown(KeyCode.F))
        {
            float distance = Vector3.Distance(player.transform.position, box.transform.position);
            Debug.Log(distance);
            if(revealDistance >= distance){
                SwitchSceneByName(targetSceneName);
            }
            
        }
    }

    // 切換到指定名稱的場景
    public void SwitchSceneByName(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName) != null)
        {
            SceneManager.LoadScene(sceneName);
            Vector3 targetPosition = new Vector3(x, y, z);
            player.transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
        }
    }
}