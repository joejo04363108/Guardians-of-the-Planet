using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class market_button1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;

    public GameObject trade;

    public BackPack backPack;
    public int slot_num;

    public string trade_tag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void Trade(){
        //Debug.Log("111");
        GameObject slot_item1 , slot_item2, slot_item3;
        slot_item1 = FindChildWithTag(slot1, trade_tag);
        slot_item2 = FindChildWithTag(slot2, trade_tag);
        slot_item3 = FindChildWithTag(slot3, trade_tag);
        

        if ( slot_item1  != null && slot_item2 != null && slot_item3 != null){
            GameObject newObject = Instantiate(trade);
            newObject.transform.SetParent(slot4.transform, false);
            backPack.prefabs[slot_num] = trade;
            Destroy(slot_item1);
            Destroy(slot_item2);
            Destroy(slot_item3);

        }
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        // 遍歷父物件的所有子物件
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject; // 返回匹配的子物件
            }
        }
        return null; // 如果沒有找到，返回 null
    }
}
