using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class bowController : MonoBehaviour
{
    public GameObject bow_side;
    public GameObject bow_down;
    public GameObject bow_up;
    public float moveSpeed = 5f; // 移動速度
    private Vector2 movement;         // 儲存移動方向

    private void Start()
    {
        // 確保默認狀態
        bow_side.SetActive(false);
        bow_down.SetActive(false);
        bow_up.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        // 取得輸入
        movement.x = Input.GetAxisRaw("Horizontal"); // 左右
        movement.y = Input.GetAxisRaw("Vertical");   // 上下

        // 根據輸入更新動畫
        UpdateAnimation();
    }
    void FixedUpdate()
    {
        // 移動角色
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        if (movement.x > 0) // 向右移動
        {
            bow_side.SetActive(true);
            bow_down.SetActive(false);
            bow_up.SetActive(false);
            FlipCharacter(false);
        }
        else if (movement.x < 0) // 向左移動
        {
            bow_side.SetActive(true);
            bow_down.SetActive(false);
            bow_up.SetActive(false);
            FlipCharacter(true);
        }
        else if (movement.y > 0) // 向上移動
        {
            bow_side.SetActive(false);
            bow_down.SetActive(false);
            bow_up.SetActive(true);
        }
        else if (movement.y < 0) // 向下移動
        {
            bow_side.SetActive(false);
            bow_down.SetActive(true);
            bow_up.SetActive(false);
        }
        
    }
    void FlipCharacter(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); // 根據方向調整 x 軸縮放
        transform.localScale = scale;
    }
}
