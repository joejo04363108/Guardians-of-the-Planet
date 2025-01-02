using System;
using UnityEngine;


public class UseTree : MonoBehaviour
{
    public GameObject hiddenTree; // 隱藏的樹
    //public GameObject playerHandTree; // 玩家手上的樹
    public Transform player; // 玩家 Transform

    public float revealDistance = 2f; // 顯示距離
    public BackPack backPack;
    public string tag;

    public string tree_tag;
    public Timer timer;
    bool has_tree = false;
    float distance;
    void Update(){
        // 計算玩家與隱藏樹的距離
        if(hiddenTree == null ) hiddenTree = GameObject.FindWithTag(tree_tag);
        if(hiddenTree != null){
            distance = Vector3.Distance(player.position, hiddenTree.transform.position);
            //玩家手上的樹
            Detect_key(tag);
        }
        
    }

    void Detect_key(string tag){
        int item_index = 0;
        for(int i = 15; i <= 19; i++){
            if(backPack.slots[i] != null && backPack.slots[i].transform.childCount > 0){
                foreach (Transform child in backPack.slots[i].transform){
                    if (child.CompareTag(tag)){
                        has_tree = true;
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
        Debug.Log(distance);
        if(distance <= revealDistance && has_tree){
            // 顯示隱藏的樹
            //hiddenTree.SetActive(true);
            SpriteRenderer spriteRenderer = hiddenTree.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 0;
            foreach (Transform child in backPack.slots[num+14].transform){
                if (child.CompareTag(tag)){
                    backPack.prefabs[num+14] = null;
                    Destroy(child.gameObject);
                    timer.tree_cnt++;
                }
            }
            Debug.Log("隱藏的樹已顯示，玩家手上的樹已移除！");
        }
    }
        
}