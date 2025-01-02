using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    public GameObject bed;

    public float revealDistance = 2f;

    private bool isCanvasActive = false;


    public GameObject canvas; // 要開啟的 UI 或物件
     void Start(){
        player = GameObject.FindWithTag("bob"); // 假設 Player 物件有 "Player" 標籤
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) player = GameObject.FindWithTag("bob");
        //if(canvas == null) canvas = GameObject.FindWithTag("Bed");
        // 檢查是否按下 F 鍵
        if (Input.GetKeyDown(KeyCode.F))
        {
            float distance = Vector3.Distance(player.transform.position, bed.transform.position);
            //Debug.Log(distance);
            if(revealDistance >= distance){
                isCanvasActive = !isCanvasActive;
                canvas.SetActive(isCanvasActive);
            }
            
        }
    }
}
