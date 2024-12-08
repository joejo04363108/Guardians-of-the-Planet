using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public Animator animator;   // Player 的 Animator 元件
    public Animator swordAnimator; // Sword 的 Animator 元件
    public GameObject sword;       // Sword 物件

    private Vector2 movement;         // 儲存移動方向
    private string lastIdleAnimation = "idle_side"; // 記錄最後的 idle 動畫
    private string lastAttackAnimation = "sword_attack_side"; // 記錄最後的攻擊動畫
    private string lastSwordAnimation = "weapon_sword_side"; // 記錄最後的劍攻擊動畫
    private bool isAttacking = false; // 是否正在攻擊

    void Update()
    {
        // 攻擊處理
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking) // 滑鼠左鍵按下且未在攻擊時
        {
            PerformAttack();
        }

        if (!isAttacking) // 攻擊期間不更新移動動畫
        {
            // 取得輸入
            movement.x = Input.GetAxisRaw("Horizontal"); // 左右
            movement.y = Input.GetAxisRaw("Vertical");   // 上下

            // 移動動畫處理
            UpdateAnimation();
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking) // 攻擊期間不移動角色
        {
            // 移動角色
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }

    void UpdateAnimation()
    {
        if (movement.x > 0) // 向右移動
        {
            animator.Play("walk_side");
            lastIdleAnimation = "idle_side";
            lastAttackAnimation = "sword_attack_side";
            lastSwordAnimation = "weapon_sword_side";
            FlipCharacter(false);
        }
        else if (movement.x < 0) // 向左移動
        {
            animator.Play("walk_side");
            lastIdleAnimation = "idle_side";
            lastAttackAnimation = "sword_attack_side";
            lastSwordAnimation = "weapon_sword_side";
            FlipCharacter(true);
        }
        else if (movement.y > 0) // 向上移動
        {
            animator.Play("walk_up");
            lastIdleAnimation = "idle_up";
            lastAttackAnimation = "sword_attack_up";
            lastSwordAnimation = "weapon_sword_up";
        }
        else if (movement.y < 0) // 向下移動
        {
            animator.Play("walk_down");
            lastIdleAnimation = "idle_down";
            lastAttackAnimation = "sword_attack_down";
            lastSwordAnimation = "weapon_sword_down";
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
        scale.x = faceLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void PerformAttack()
    {
        isAttacking = true; // 設置為攻擊狀態

        // 播放 Player 攻擊動畫
        animator.Play(lastAttackAnimation);

        // 播放 Sword 攻擊動畫
        sword.transform.position = transform.position; // 將 Sword 定位到 Player 的位置
        swordAnimator.Play(lastSwordAnimation);

        // 設置協程等待動畫播放完成
        StartCoroutine(WaitForAttackAnimation());
    }

    IEnumerator WaitForAttackAnimation()
    {
        // 等待 Player 的攻擊動畫播放完成
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (stateInfo.IsName(lastSwordAnimation) && stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 更新動畫狀態
            yield return null;
        }

        // 攻擊結束後重置 Sword 狀態
        swordAnimator.Play("weapon_sword_idle");

        isAttacking = false; // 攻擊完成後解除狀態
    }
}
