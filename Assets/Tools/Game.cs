using UnityEngine;

public class MineralCollector : MonoBehaviour
{
    public GameObject player;         // 玩家物件
    public float collectDistance = 3f; // 採集距離

    private GameObject targetMineral; // 當前可採集的礦物
    public BackPack backPack;
    public LightBar lightBar;
    public UseHammer useHammer;

    public string tag_name;
    public bool is_break = false;
    void Update()
    {
        // 檢測玩家附近是否有礦物
        DetectMineral();

        if(is_break) return;
        // 如果有可採集礦物且按下 J
        if (targetMineral != null && Input.GetMouseButtonDown(0) && useHammer.use_hammer == true)
        {
            Invoke("CollectMineral", 0.5f);
            //CollectMineral();
            is_break = true;
            lightBar.subLight(1);
            
        }
    }

    // 檢測範圍內的礦物
    void DetectMineral()
    {
        // 找出場景中所有帶有 "Mineral" 標籤的物件
        GameObject[] minerals = GameObject.FindGameObjectsWithTag(tag_name);

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
    void CollectMineral()
    {
         if (targetMineral == null) return;

        Debug.Log("成功採集礦物：" + targetMineral.name);

        // 獲取礦物的 Mineral 腳本，並執行 Break()
        Mineral mineralScript = targetMineral.GetComponent<Mineral>();
        if (mineralScript != null)
        {
            mineralScript.Break();
        }

        targetMineral = null; // 清空目標礦物
    }
        
}