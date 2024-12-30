using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public float pickUpRange = 2.0f; // 可剪取範圍
    public LayerMask itemLayer; // 設置物品的層
    private GameObject currentItem; // 當前可剪取的物品

    void Update()
    {
        DetectItem();
        if (Input.GetKeyDown(KeyCode.F) && currentItem != null)
        {
            PickUpItem();
        }
    }

    void DetectItem()
    {
        currentItem = null; 
        float closestDistance = pickUpRange;

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance <= pickUpRange && distance < closestDistance)
            {
                currentItem = item;
                closestDistance = distance;
            }
        }
    } 

    //判斷prefab
    public GameObject prefab;

    public BackPack backPack;
    int cnt = 0;
    void PickUpItem(){
        if (currentItem != null){
            Debug.Log($"撿起: {currentItem.name}");
            Destroy(currentItem);
            currentItem = null;
            backPack.UpdateBackPack(prefab, cnt);
            cnt++;
        }
        /*
        if (currentItem != null)
        {
            Debug.Log($"Picked up: {currentItem.name}"); // 日誌顯示物品名稱
             GameObject newObject = Instantiate(prefab);

        // 設定父物件為 Slot，保持 RectTransform 一致
            newObject.transform.SetParent(slot.transform, false);
            Destroy(currentItem); // 銷毀物品
            currentItem = null;
        }
        */
    }
}
