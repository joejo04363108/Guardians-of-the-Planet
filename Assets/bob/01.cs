using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Animator animator;   // Animator 元件

    private Vector2 movement;         // 儲存移動方向
    private string lastIdleAnimation = "idle_side"; // 記錄最後的 idle 動畫

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
            animator.Play("walk_side"); // 播放走路動畫
            lastIdleAnimation = "idle_side"; // 記錄最後的 idle 動畫
            FlipCharacter(false); // 面向右
        }
        else if (movement.x < 0) // 向左移動
        {
            animator.Play("walk_side"); // 播放走路動畫
            lastIdleAnimation = "idle_side"; // 記錄最後的 idle 動畫
            FlipCharacter(true); // 面向左
        }
        else if (movement.y > 0) // 向上移動
        {
            animator.Play("walk_up"); // 播放向上走路動畫
            lastIdleAnimation = "idle_up"; // 記錄最後的 idle 動畫
        }
        else if (movement.y < 0) // 向下移動
        {
            animator.Play("walk_down"); // 播放向下走路動畫
            lastIdleAnimation = "idle_down"; // 記錄最後的 idle 動畫
        }
        else
        {
            // 停止時播放最後的 idle 動畫
            animator.Play(lastIdleAnimation);
        }
    }

    void FlipCharacter(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); // 根據方向調整 x 軸縮放
        transform.localScale = scale;
    }
}