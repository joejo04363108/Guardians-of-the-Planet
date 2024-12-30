using UnityEngine;

public class pick_middle : MonoBehaviour
{
    public GameObject player; // 玩家物件
    public float pickUpRange = 2.0f; //撿取範圍
    private GameObject currentItem; //當前可撿取的物品

    public string Tag_Name;

    public Timer timer;

    void Update()
    {
        DetectItem();
        // 偵測到物品且玩家距離物品範圍內且按下 F 鍵
        if (Input.GetKeyDown(KeyCode.F) && currentItem != null)
        {
            PickUpItem();
        }
    }

    void DetectItem()
    {
        currentItem = null;
        float closestDistance = pickUpRange;

        GameObject[] items = GameObject.FindGameObjectsWithTag(Tag_Name);
        foreach (GameObject item in items)
        {
            float distance = Vector3.Distance(player.transform.position, item.transform.position); // 使用玩家的位置進行距離計算

            // 如果物品在範圍內且距離更近，設置為當前物品
            if (distance <= pickUpRange && distance < closestDistance)
            {
                currentItem = item;
                closestDistance = distance;
            }
        }
    }

    // 判斷物品並撿起
    public GameObject prefab;
    public BackPack backPack; // 背包管理
    //int cnt = 0;

    void PickUpItem()
    {
        if (currentItem != null)
        {
            Debug.Log($"撿起: {currentItem.name}");
            Destroy(currentItem); // 銷毀物品
            currentItem = null;

            // 更新背包
            backPack.UpdateBackPack(prefab, timer.cnt);
            timer.cnt++;
        }
    }
}
