using UnityEngine;

public class RevealTree : MonoBehaviour
{
    public GameObject hiddenTree; // 隱藏的樹
    //public GameObject playerHandTree; // 玩家手上的樹
    public Transform player; // 玩家 Transform
    public float revealDistance = 2f; // 顯示距離
    public BackPack backPack;

    void Update()
    {
        // 計算玩家與隱藏樹的距離
        float distance = Vector3.Distance(player.position, hiddenTree.transform.position);

        //玩家手上的樹
        bool has_tree = false;
        if(backPack.slots[15].transform.childCount > 0){
            foreach (Transform child in backPack.slots[15].transform){
                 if (child.CompareTag("Tree")){
                    has_tree = true;
                 }
            }
        }
        

        // 檢查距離和按鍵條件
        if (distance <= revealDistance && has_tree && Input.GetKeyDown(KeyCode.X))
        {
            RevealHiddenTree();
        }
    }

    void RevealHiddenTree()
    {
        // 顯示隱藏的樹
        hiddenTree.SetActive(true);

        foreach (Transform child in backPack.slots[15].transform){
            if (child.CompareTag("Tree")){
                Destroy(child.gameObject);
            }
        }
        // 移除玩家手上的樹
        

        Debug.Log("隱藏的樹已顯示，玩家手上的樹已移除！");
    }
}