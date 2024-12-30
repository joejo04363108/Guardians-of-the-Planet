using UnityEngine;



public class BackPack : MonoBehaviour
{
    public GameObject[] slots = new GameObject[15];  
    public GameObject[] toolbar = new GameObject[5];
    public GameObject[] prefabs = new GameObject[15];
    public GameObject[] toolbar_prefabs = new GameObject[5];

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
}
