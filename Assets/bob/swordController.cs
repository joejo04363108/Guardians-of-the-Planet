using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordController : MonoBehaviour
{
    public GameObject sword_attack_side;
    public GameObject sword_attack_down;
    public GameObject sword_attack_up;
    public GameObject idle_side;
    public GameObject idle_down;
    public GameObject idle_up;
    public GameObject walk_side;
    public GameObject walk_down;
    public GameObject walk_up;
    public AudioSource audioSource;
    public AudioClip soundClip;

    public float attackInterval = 2f;  // 攻擊間隔
    public float attackRange = 3f;     // 攻擊範圍
    public LayerMask enemyLayer;       // 敵人所在的圖層

    private GameObject currentAnimation; // 記錄當前播放的動畫
    private Vector2 movement;           // 儲存移動方向
    private string lastAnimation = "sword_attack_side"; // 記錄最後的攻擊方向
    private string lastIdle = "idle_side";             // 記錄最後的閒置方向
    private bool isAttacking = false;   // 記錄是否正在攻擊
    private bool facingLeft = false;    // 記錄角色面向方向

    public void Start()
    {
        // 確保默認狀態
        DeactivateAllAnimations();
        idle_side.SetActive(true); // 預設為側面閒置動畫
    }

    void Update()
    {
        // 如果正在攻擊，不執行其他動畫
        if (isAttacking) return;

        // 取得輸入
        movement.x = Input.GetAxisRaw("Horizontal"); // 左右
        movement.y = Input.GetAxisRaw("Vertical");   // 上下

        // 根據輸入更新動畫
        UpdateAnimation();

        // 按下滑鼠左鍵觸發攻擊
        if (Input.GetMouseButtonDown(0))
        {
            PlayAttackAnimation();
            CheckAndDealDamage();
            audioSource.PlayOneShot(soundClip);
        }
    }

    private void CheckAndDealDamage()
    {
        // 獲取當前位置
        Vector2 attackPosition = transform.position;

        // 檢測範圍內的碰撞體
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("monster"))
            {
                sporeMonsterController enemyController = collider.GetComponent<sporeMonsterController>();
                goblinController goblin = collider.GetComponent<goblinController>();
                BoDController boss = collider.GetComponent<BoDController>();
                if (enemyController != null)
                {
                    enemyController.TakeDamage(2); // 對敵人造成傷害
                }
                if(goblin != null)
                {
                    goblin.TakeDamage(2);
                }
                if (boss != null)
                {
                    boss.TakeDamage(2);
                }
            }
        }
    }

    public void UpdateAnimation()
    {
        GameObject newAnimation = null;

        if (movement.magnitude > 0) // 檢查是否有移動
        {
            if (movement.x > 0) // 向右移動
            {
                facingLeft = false;
                newAnimation = walk_side;
                lastIdle = "idle_side";
                lastAnimation = "sword_attack_side";
            }
            else if (movement.x < 0) // 向左移動
            {
                facingLeft = true;
                newAnimation = walk_side;
                lastIdle = "idle_side";
                lastAnimation = "sword_attack_side";
            }
            else if (movement.y > 0) // 向上移動
            {
                newAnimation = walk_up;
                lastIdle = "idle_up";
                lastAnimation = "sword_attack_up";
            }
            else if (movement.y < 0) // 向下移動
            {
                newAnimation = walk_down;
                lastIdle = "idle_down";
                lastAnimation = "sword_attack_down";
            }
        }
        else
        {
            // 停止移動，播放閒置動畫
            PlayIdleAnimation();
            return;
        }

        // 切換到新的動畫
        if (currentAnimation != newAnimation)
        {
            DeactivateAllAnimations();
            currentAnimation = newAnimation;
            currentAnimation.SetActive(true);

            // 如果是側面動畫，處理翻轉
            if (currentAnimation == walk_side)
            {
                FlipCharacter(facingLeft);
            }
        }
    }

    public void PlayIdleAnimation()
    {
        DeactivateAllAnimations(); // 停用其他動畫
        currentAnimation = null;
        switch (lastIdle)
        {
            case "idle_side":
                idle_side.SetActive(true);
                FlipCharacter(facingLeft);
                break;
            case "idle_down":
                idle_down.SetActive(true);
                break;
            case "idle_up":
                idle_up.SetActive(true);
                break;
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
                FlipCharacter(facingLeft);
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

        // 播放對應的閒置動畫
        PlayIdleAnimation();

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
        walk_side.SetActive(false);
        walk_down.SetActive(false);
        walk_up.SetActive(false);
    }

    void FlipCharacter(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x); // 根據方向調整 x 軸縮放
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        // 可視化攻擊範圍
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
