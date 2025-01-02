using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeController : MonoBehaviour
{
    public GameObject[] treeParts; // 將 t01 ~ t06 拖到此陣列
    public mapController mapController; // 將 MapController 的物件拖到此處

    public GameObject player;
    void Start()
    {
        // 初始隱藏所有 TilemapRenderer
        foreach (var part in treeParts)
        {
            var renderer = part.GetComponent<TilemapRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false; // 隱藏物件渲染
            }
        }
        if(player == null) player = GameObject.FindWithTag("bob");
    }

    void Update()
    {
        if(player == null) player = GameObject.FindWithTag("bob");
        // 按下 F 鍵並檢測是否有碰撞的子物件
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (var part in treeParts)
            {
                float distance = Vector3.Distance(player.transform.position, part.transform.position);
                if(2f < distance) return;
                var renderer = part.GetComponent<TilemapRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = !renderer.enabled; // 切換顯示狀態
                }
            }

            // 檢查顯示狀態並呼叫對應方法
            CheckTreeStates();
        }
    }

    private void CheckTreeStates()
    {
        if (mapController == null)
        {
            Debug.LogError("MapController 未設定！");
            return;
        }

        // 檢查 t01 和 t02 是否顯示
        bool t01Visible = treeParts[0].GetComponent<TilemapRenderer>().enabled;
        bool t02Visible = treeParts[1].GetComponent<TilemapRenderer>().enabled;

        if (t01Visible || t02Visible)
        {
            mapController.HidePolluted1();
        }

        // 檢查 t03 和 t04 是否顯示
        bool t03Visible = treeParts[2].GetComponent<TilemapRenderer>().enabled;
        bool t04Visible = treeParts[3].GetComponent<TilemapRenderer>().enabled;

        if (t03Visible || t04Visible)
        {
            mapController.HidePolluted2();
        }

        // 檢查 t05 和 t06 是否顯示
        bool t05Visible = treeParts[4].GetComponent<TilemapRenderer>().enabled;
        bool t06Visible = treeParts[5].GetComponent<TilemapRenderer>().enabled;

        if (t05Visible || t06Visible)
        {
            mapController.HidePolluted3();
        }
    }
}
