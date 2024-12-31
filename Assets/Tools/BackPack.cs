using UnityEngine;



public class BackPack : MonoBehaviour
{
    public GameObject[] slots = new GameObject[25];  
    public GameObject[] trade = new GameObject[25];  
    //public GameObject[] toolbar = new GameObject[5];
    public GameObject[] prefabs = new GameObject[25];
    //public GameObject[] toolbar_prefabs = new GameObject[5];

    void Start()
    {
        if (slots.Length == prefabs.Length){
            for (int i = 0; i < slots.Length; i++){
                if (prefabs[i] != null && slots[i] != null){
                    // 實例化對應的 Prefab
                    GameObject newObject = Instantiate(prefabs[i]);

                    // 設定父物件為對應的 Slot
                    newObject.transform.SetParent(slots[i].transform, false);

                }
            }
        }
    }
    public void UpdateBackPack(GameObject gameObject, int num){
        prefabs[num] = gameObject;
        // 實例化對應的 Prefab
        GameObject newObject = Instantiate(prefabs[num]);
        // 設定父物件為對應的 Slot
        newObject.transform.SetParent(slots[num].transform, false);

    }

    public void SwitchBackPackItem(int num1,int num2){
        GameObject tmp = prefabs[num1];
        prefabs[num1] = prefabs[num2];
        prefabs[num2] = tmp;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)){
            // 切換 Canvas 的顯示狀態
            for (int i = 0; i < trade.Length; i++){
                if (prefabs[i] != null && trade[i] != null){

                    if (trade[i].transform.childCount > 0)
                    {
                        continue; // 如果已有元素，跳過本次迭代
                    }
                    // 實例化對應的 Prefab
                    GameObject newObject = Instantiate(prefabs[i]);

                    // 設定父物件為對應的 Slot
                    newObject.transform.SetParent(trade[i].transform, false);

                }
                if(prefabs[i] == null && trade[i].transform.childCount > 0){
                    foreach (Transform child in trade[i].transform){
                        Destroy(child.gameObject); // 銷毀子物件
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.B)){
            // 切換 Canvas 的顯示狀態
            for (int i = 0; i < slots.Length; i++){
                if (prefabs[i] != null && slots[i] != null){

                    if (slots[i].transform.childCount > 0)
                    {
                        continue; // 如果已有元素，跳過本次迭代
                    }
                    // 實例化對應的 Prefab
                    GameObject newObject = Instantiate(prefabs[i]);

                    // 設定父物件為對應的 Slot
                    newObject.transform.SetParent(slots[i].transform, false);

                }
                if(prefabs[i] == null && slots[i].transform.childCount > 0){
                    foreach (Transform child in slots[i].transform){
                        Destroy(child.gameObject); // 銷毀子物件
                    }
                }
            }
        }
    }
}
