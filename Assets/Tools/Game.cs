using UnityEngine;

public class MineralCollector : MonoBehaviour
{
    public GameObject player;         // 玩家物件
    //public Inventory playerInventory; // 玩家物品欄
    public float collectDistance = 3f; // 採集距離

    private GameObject targetMineral; // 當前可採集的礦物

    void Update()
    {
        // 檢測玩家附近是否有礦物
        DetectMineral();

        // 如果有可採集礦物且按下 F
        if (targetMineral != null && Input.GetKeyDown(KeyCode.J))
        {
            /*
            if (playerInventory.HasItem("Pickaxe"))
            {
                CollectMineral(targetMineral);
            }
            else
            {
                Debug.Log("你需要十字稿才能採集這個礦物！");
            }
            */
            CollectMineral(targetMineral);
        }
    }

    // 檢測範圍內的礦物
    void DetectMineral()
    {
        // 找出場景中所有帶有 "Mineral" 標籤的物件
        GameObject[] minerals = GameObject.FindGameObjectsWithTag("Ore");

        targetMineral = null; // 初始化為空
        foreach (GameObject mineral in minerals)
        {
            // 確認物件未被銷毀
            if (mineral == null) continue;

            float distance = Vector3.Distance(player.transform.position, mineral.transform.position);
            if (distance <= collectDistance)
            {
                targetMineral = mineral; // 鎖定距離最近的礦物
                break;
            }
        }
    }

    // 採集礦物邏輯
    void CollectMineral(GameObject mineral)
    {
         if (mineral == null) return;

        Debug.Log("成功採集礦物：" + mineral.name);

        // 獲取礦物的 Mineral 腳本，並執行 Break()
        Mineral mineralScript = mineral.GetComponent<Mineral>();
        if (mineralScript != null)
        {
            mineralScript.Break();
        }

        targetMineral = null; // 清空目標礦物
    }
        
}