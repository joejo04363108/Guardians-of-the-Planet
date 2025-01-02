using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trade : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    public GameObject trade1;

    public float revealDistance = 2f;

    //private bool isCanvasActive = false;

    public GameObject market_package;
    public GameObject market_trade;
    private bool is_market_package_Active = false;
    private bool is_market_trade_Active = false;

    //public GameObject UI;


    //public GameObject canvas; // 要開啟的 UI 或物件
    void Start()
    {
         player = GameObject.FindWithTag("bob"); // 假設 Player 物件有 "Player" 標籤
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        //market_package = GameObject.FindWithTag("trade_package");
        //market_trade = GameObject.FindWithTag("trade1");

        market_package.SetActive(is_market_package_Active);
        market_trade.SetActive(is_market_trade_Active);
    }

    // Update is called once per frame
    void Update()
    {
        //if(UI == null) UI = GameObject.FindWithTag("UI");
         if(player == null) player = GameObject.FindWithTag("bob");
         if(trade1 == null) trade1 = GameObject.FindWithTag("trade_member1");
         //if(market_package  == null) market_package = GameObject.FindWithTag("trade_package");
         //if(market_trade == null) market_trade = GameObject.FindWithTag("trade1");
        //if(canvas == null) canvas = GameObject.FindWithTag("Bed");
        // 檢查是否按下 F 鍵
        if (Input.GetKeyDown(KeyCode.F))
        {
            float distance = Vector3.Distance(player.transform.position, trade1.transform.position);
            Debug.Log(distance);
            if(revealDistance >= distance){
                is_market_package_Active = !is_market_package_Active;
                is_market_trade_Active = !is_market_trade_Active;
                market_package.SetActive(is_market_package_Active);
                market_trade.SetActive(is_market_trade_Active);
            }
            
        }
    }


    public static GameObject FindInactiveByTag(string tag)
    {
        // 找到所有 "DontDestroyOnLoad" 場景中的根物件
        GameObject[] rootObjects = GetDontDestroyOnLoadObjects();
        foreach (GameObject root in rootObjects)
        {
            // 遍歷子物件，包括非激活
            Transform[] children = root.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if (child.CompareTag(tag)) // 比對標籤
                {
                    return child.gameObject; // 找到返回
                }
            }
        }
        return null; // 如果沒有找到則返回 null
    }

    private static GameObject[] GetDontDestroyOnLoadObjects()
    {
        // 利用新場景裝載來模擬提取 "DontDestroyOnLoad" 場景的根物件
        Scene dontDestroyScene = new GameObject().scene; // 創建一個臨時物件的場景
        SceneManager.MoveGameObjectToScene(new GameObject(), dontDestroyScene);

        List<GameObject> dontDestroyObjects = new List<GameObject>();

        foreach (GameObject obj in dontDestroyScene.GetRootGameObjects())
        {
            dontDestroyObjects.Add(obj);
        }

        return dontDestroyObjects.ToArray();
    }
}
