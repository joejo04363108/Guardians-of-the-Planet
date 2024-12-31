using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseGun : MonoBehaviour
{
     public BobController player;
    public BackPack backPack;
    bool has_gun = false;
    public bool use_gun = false;
    public string tag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detect_key(tag);
    }

     void Detect_key(string tag){
        int item_index = 0;
        for(int i = 15; i <= 19; i++){
            if(backPack.slots[i] != null && backPack.slots[i].transform.childCount > 0){
                foreach (Transform child in backPack.slots[i].transform){
                    //Debug.Log(tag);
                    if (tag != "" && child.CompareTag(tag)){
                        has_gun = true;
                        item_index = i - 14;
                    }
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1) && item_index == 1){
            Run(1,tag);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && item_index == 2){
            Run(2,tag);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && item_index == 3){
            Run(3,tag);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && item_index == 4){
            Run(4,tag);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5) && item_index == 5){
            Run(5,tag);
        }
    }

    void Run(int num, string tag){
        //Debug.Log(distance);
        if(use_gun && has_gun){
            player.TriggerActionByTag("none");
            use_gun = false;
            return;
        }
        if(has_gun){
            // 顯示隱藏的樹
            player.TriggerActionByTag("gun");
            use_gun = true;
        }
        
    }
}
