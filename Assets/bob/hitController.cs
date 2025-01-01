using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitController : MonoBehaviour
{
    private Vector2 movement;         // 儲存移動方向
    private string lastAnimation = "hit_side"; // 記錄最後的攻擊方向

    public GameObject hit_side;
    public GameObject hit_down;
    public GameObject hit_up;

    private bool facingLeft = false;  // 記錄角色面向方向
    private void Start()
    {
        DeactivateAllAnimations();
    }

    void Update()
    {
        // 取得輸入
        movement.x = Input.GetAxisRaw("Horizontal"); // 左右
        movement.y = Input.GetAxisRaw("Vertical");   // 上下

        // 根據輸入更新動畫
        UpdateAnimation();
        HitAnimation();
    }

    void UpdateAnimation()
    {
        if (movement.magnitude > 0)
        {
            if (movement.x > 0) // 向右移動
            {
                facingLeft = false;
                lastAnimation = "hit_side";
            }
            else if (movement.x < 0) // 向左移動
            {
                facingLeft = true;
                lastAnimation = "hit_side";
            }
            else if (movement.y > 0) // 向上移動
            {
                lastAnimation = "hit_up";
            }
            else if (movement.y < 0) // 向下移動
            {
                lastAnimation = "hit_down";
            }
        }
    }
    void HitAnimation()
    {
        DeactivateAllAnimations();
        // 根據最後移動方向觸發攻擊動畫
        switch (lastAnimation)
        {
            case "hit_side":
                hit_side.SetActive(true);
                FlipCharacter(facingLeft);
                break;
            case "hit_down":
                hit_down.SetActive(true);
                break;
            case "hit_up":
                hit_up.SetActive(true);
                break;
        }
    }
    void DeactivateAllAnimations()
    {
        hit_side.SetActive(false);
        hit_down.SetActive(false);
        hit_up.SetActive(false);
    }

    void FlipCharacter(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); // 根據方向調整 x 軸縮放
        transform.localScale = scale;
    }
}
