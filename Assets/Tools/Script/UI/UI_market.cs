using UnityEngine;
using UnityEngine.UIElements;

public class UI_Market : MonoBehaviour
{
    public GameObject market_package;
     public GameObject market_trade;
    private bool is_market_package_Active = false;

    private bool is_market_trade_Active = false;
    public KeyCode Keyboard; 

    void Start()
    {
        // 確保遊戲開始時隱藏 canvas
        if (market_package != null && market_trade != null)
        {
            market_package.SetActive(false);
            market_trade.SetActive(false);
            is_market_package_Active = false;
            is_market_trade_Active = false;
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
        }
    }
}