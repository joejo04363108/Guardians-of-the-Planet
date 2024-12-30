using UnityEngine;

public class AddPrefabToSlot : MonoBehaviour
{
    public GameObject slot;       // 指定 Slot 物件
    public GameObject prefab;     // 指定要新增的 Prefab

    void Start()
    {
        // 確保 Slot 和 Prefab 都已指定
        if (slot == null || prefab == null)
        {
            Debug.LogError("請確保在 Inspector 中設置了 Slot 和 Prefab。");
            return;
        }

        // 實例化 Prefab
        GameObject newObject = Instantiate(prefab);

        // 設定父物件為 Slot，保持 RectTransform 一致
        newObject.transform.SetParent(slot.transform, false);
    }
}
