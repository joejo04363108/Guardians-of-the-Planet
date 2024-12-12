using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class swordController : MonoBehaviour
{
    public GameObject sword_attack_side;
    public GameObject sword_attack_down;
    public GameObject sword_attack_up;
    public GameObject idle_side;
    public GameObject idle_down;
    public GameObject idle_up;

    private Vector2 movement;         // 儲存移動方向
    private string lastAnimation = "sword_attack_side"; // 記錄最後的移動方向
    private bool isAttacking = false; // 記錄是否正在攻擊
    private bool facingLeft = false;  // 記錄角色面向方向

    private void Start()
    {
        // 確保默認狀態
        DeactivateAllAnimations();
        idle_side.SetActive(true); // 預設為側面閒置動畫
    }

    void Update()
    {
        // 如果正在攻擊，不切換動畫
        if (!isAttacking)
        {
            // 取得輸入
            movement.x = Input.GetAxisRaw("Horizontal"); // 左右
            movement.y = Input.GetAxisRaw("Vertical");   // 上下

            // 根據輸入更新動畫
            UpdateAnimation();
        }

        // 按下滑鼠左鍵觸發攻擊
        if (Input.GetMouseButtonDown(0))
        {
            PlayAttackAnimation();
        }
    }

    void UpdateAnimation()
    {
        // 如果正在攻擊，跳過閒置動畫
        if (isAttacking) return;

        if (movement.x > 0) // 向右移動
        {
            facingLeft = false;
            idle_side.SetActive(true);
            idle_down.SetActive(false);
            idle_up.SetActive(false);
            FlipCharacter(false);
            lastAnimation = "sword_attack_side";
        }
        else if (movement.x < 0) // 向左移動
        {
            facingLeft = true;
            idle_side.SetActive(true);
            idle_down.SetActive(false);
            idle_up.SetActive(false);
            FlipCharacter(true);
            lastAnimation = "sword_attack_side";
        }
        else if (movement.y > 0) // 向上移動
        {
            idle_side.SetActive(false);
            idle_down.SetActive(false);
            idle_up.SetActive(true);
            lastAnimation = "sword_attack_up";
        }
        else if (movement.y < 0) // 向下移動
        {
            idle_side.SetActive(false);
            idle_down.SetActive(true);
            idle_up.SetActive(false);
            lastAnimation = "sword_attack_down";
        }
        else
        {
            //idle_side.SetActive(true);
        }
    }

    void PlayAttackAnimation()
    {
        // 停用所有動畫
        DeactivateAllAnimations();

        // 設置攻擊狀態
        isAttacking = true;

        // 根據最後移動方向觸發攻擊動畫
        switch (lastAnimation)
        {
            case "sword_attack_side":
                sword_attack_side.SetActive(true);
                FlipCharacter(facingLeft); // 根據方向翻轉角色
                break;
            case "sword_attack_down":
                sword_attack_down.SetActive(true);
                break;
            case "sword_attack_up":
                sword_attack_up.SetActive(true);
                break;
        }

        // 在觸發攻擊後添加計時器以恢復閒置動畫
        Invoke("ResetToIdle", 0.5f); // 0.5 秒後返回閒置狀態
    }

    void ResetToIdle()
    {
        // 停用所有動畫
        DeactivateAllAnimations();

        // 返回對應的閒置動畫
        switch (lastAnimation)
        {
            case "sword_attack_side":
                idle_side.SetActive(true);
                break;
            case "sword_attack_down":
                idle_down.SetActive(true);
                break;
            case "sword_attack_up":
                idle_up.SetActive(true);
                break;
        }

        // 重置攻擊狀態
        isAttacking = false;
    }

    void DeactivateAllAnimations()
    {
        sword_attack_side.SetActive(false);
        sword_attack_down.SetActive(false);
        sword_attack_up.SetActive(false);
        idle_side.SetActive(false);
        idle_down.SetActive(false);
        idle_up.SetActive(false);
    }

    void FlipCharacter(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); // 根據方向調整 x 軸縮放
        transform.localScale = scale;
    }
}
