using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordController : MonoBehaviour
{
    public GameObject sword_side;
    public GameObject sword_down;
    public GameObject sword_up;
    public GameObject idle_side;
    public GameObject idle_down;
    public GameObject idle_up;

    private Vector2 movement;         // 儲存移動方向

    private void Start()
    {
        // 確保默認狀態
        sword_side.SetActive(false);
        sword_down.SetActive(false);
        sword_up.SetActive(false);
        idle_side.SetActive(false);
        idle_down.SetActive(false);
        idle_up.SetActive(false);
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

    void UpdateAnimation()
    {
        if (movement.x > 0) // 向右移動
        {
            idle_side.SetActive(true);
            idle_down.SetActive(false);
            idle_up.SetActive(false);
            FlipCharacter(false);
        }
        else if (movement.x < 0) // 向左移動
        {
            idle_side.SetActive(true);
            idle_down.SetActive(false);
            idle_up.SetActive(false);
            FlipCharacter(true);
        }
        else if (movement.y > 0) // 向上移動
        {
            idle_side.SetActive(false);
            idle_down.SetActive(false);
            idle_up.SetActive(true);
        }
        else if (movement.y < 0) // 向下移動
        {
            idle_side.SetActive(false);
            idle_down.SetActive(true);
            idle_up.SetActive(false);
        }

    }
    void FlipCharacter(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); // 根據方向調整 x 軸縮放
        transform.localScale = scale;
    }
}
