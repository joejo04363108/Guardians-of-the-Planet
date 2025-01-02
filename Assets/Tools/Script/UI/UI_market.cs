using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class UI_Market : MonoBehaviour
{
    public GameObject market_package;
    public GameObject market_trade;
    private bool is_market_package_Active = false;

    private bool is_market_trade_Active = false;
    public KeyCode Keyboard; 

    bool is_close = false;

    void Start()
    {
        // 確保遊戲開始時隱藏 canvas
        if (market_package != null && market_trade != null )
        {
            
            is_market_package_Active = false;
            is_market_trade_Active = false;

            //Canvas canvas1 = market_package.GetComponentInParent<Canvas>();
            //Canvas canvas2 = market_trade.GetComponentInParent<Canvas>();
            //canvas1.enabled = false;
            //canvas2.enabled = false;

            market_package.SetActive(is_market_package_Active);
            market_trade.SetActive(is_market_trade_Active);
            
            
        }
    }

    void Update()
    {

           
        if (Input.GetKeyDown(Keyboard))
        {
            // 切換 Canvas 的顯示狀態
            //isCanvasActive = !isCanvasActive;
            is_market_package_Active = !is_market_package_Active;
            is_market_trade_Active = !is_market_trade_Active;
            market_package.SetActive(is_market_package_Active);
            market_trade.SetActive(is_market_trade_Active);

            //Canvas canvas1 = market_package.GetComponentInParent<Canvas>();
            //Canvas canvas2 = market_trade.GetComponentInParent<Canvas>();
            //canvas1.enabled = !canvas1.enabled;
            //canvas2.enabled = !canvas2.enabled;
        }
    }

    public void martet_setactive(){
        is_market_package_Active = !is_market_package_Active;
        is_market_trade_Active = !is_market_trade_Active;
        market_package.SetActive(is_market_package_Active);
        market_trade.SetActive(is_market_trade_Active);
    }

    bool IsSceneLoaded(string sceneName)
    {
    Scene scene = SceneManager.GetSceneByName(sceneName);
    return scene.isLoaded; // 返回場景是否已載入
    }
}