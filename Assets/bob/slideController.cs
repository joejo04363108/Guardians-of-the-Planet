using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlideController : MonoBehaviour
{
    private Vector2 movement;         // 儲存移動方向
    private string lastAnimation = "slide_side"; // 記錄最後的攻擊方向

    public GameObject slide_side;
    public GameObject slide_down;
    public GameObject slide_up;

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
        SlideAnimation();
    }

    void UpdateAnimation()
    {
        if (movement.magnitude > 0)
        {
            if (movement.x > 0) // 向右移動
            {
                facingLeft = false;
                lastAnimation = "slide_side";
            }
            else if (movement.x < 0) // 向左移動
            {
                facingLeft = true;
                lastAnimation = "slide_side";
            }
            else if (movement.y > 0) // 向上移動
            {
                lastAnimation = "slide_up";
            }
            else if (movement.y < 0) // 向下移動
            {
                lastAnimation = "slide_down";
            }
        }
    }
    void SlideAnimation()
    {
        DeactivateAllAnimations();
        // 根據最後移動方向觸發攻擊動畫
        switch (lastAnimation)
        {
            case "slide_side":
                slide_side.SetActive(true);
                FlipCharacter(facingLeft);
                break;
            case "slide_down":
                slide_down.SetActive(true);
                break;
            case "slide_up":
                slide_up.SetActive(true);
                break;
        }
    }
    void DeactivateAllAnimations()
    {
        slide_side.SetActive(false);
        slide_down.SetActive(false);
        slide_up.SetActive(false);
    }

    void FlipCharacter(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); // 根據方向調整 x 軸縮放
        transform.localScale = scale;
    }
}
