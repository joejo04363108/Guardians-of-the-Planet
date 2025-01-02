using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    public GameObject rifle_side;
    public GameObject rifle_down;
    public GameObject rifle_up;
    public GameObject idle_side;
    public GameObject idle_down;
    public GameObject idle_up;
    public GameObject walk_side;
    public GameObject walk_down;
    public GameObject walk_up;
    public GameObject bulletPrefab;
    public Transform firePoint;     // 子彈生成點
    public AudioSource audioSource;
    public LightBar bar;

    private GameObject currentAnimation; // 記錄當前播放的動畫

    private Vector2 movement;         // 儲存移動方向
    private string lastAnimation = "rifle_side"; // 記錄最後的攻擊方向
    private string lastIdle = "idle_side";             // 記錄最後的閒置方向
    private bool isAttacking = false; // 記錄是否正在攻擊
    private bool facingLeft = false;  // 記錄角色面向方向

    public bool isactive = false;

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
            DetermineAttackDirection();
            if(bar.getLight() > 0)
            {
                PlayAttackAnimation();
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);  //產生子彈
                audioSource.Play();
            }
            bar.subLight(2);
        }
    }

    void UpdateFirePointPosition()
    {
        // 重設 firePoint 的位置和旋轉
        switch (lastAnimation)
        {
            case "rifle_side":
                firePoint.localPosition = new Vector3(facingLeft ? -1.4f : -1.4f, -0.5f, 0f); // 左右位置
                firePoint.localRotation = Quaternion.Euler(0, facingLeft ? 180 : 0, 0);   // 根據方向翻轉
                break;
            case "rifle_down":
                firePoint.localPosition = new Vector3(0f, -1.8f, 0f); // 向下位置
                firePoint.localRotation = Quaternion.Euler(0, 0, -90); // 向下旋轉
                break;
            case "rifle_up":
                firePoint.localPosition = new Vector3(0f, 1.2f, 0f);  // 向上位置
                firePoint.localRotation = Quaternion.Euler(0, 0, 90); // 向上旋轉
                break;
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
                lastAnimation = "rifle_side";
            }
            else if (movement.x < 0) // 向左移動
            {
                facingLeft = true;
                newAnimation = walk_side;
                lastIdle = "idle_side";
                lastAnimation = "rifle_side";
            }
            else if (movement.y > 0) // 向上移動
            {
                newAnimation = walk_up;
                lastIdle = "idle_up";
                lastAnimation = "rifle_up";
            }
            else if (movement.y < 0) // 向下移動
            {
                newAnimation = walk_down;
                lastIdle = "idle_down";
                lastAnimation = "rifle_down";
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

            // 更新 firePoint 的位置
            UpdateFirePointPosition();
        }

    }
    private void DetermineAttackDirection()
    {
        // 獲取角色和鼠標的世界位置
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 characterPosition = transform.position;

        // 計算鼠標與角色的相對方向
        Vector3 direction = mousePosition - characterPosition;

        // 根據方向判斷攻擊方向
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // 水平優先
            if (direction.x > 0)
            {
                lastAnimation = "rifle_side";
                facingLeft = false; // 向右
            }
            else
            {
                lastAnimation = "rifle_side";
                facingLeft = true; // 向左
            }
        }
        else
        {
            // 垂直優先
            if (direction.y > 0)
            {
                lastAnimation = "rifle_up";
            }
            else
            {
                lastAnimation = "rifle_down";
            }
        }

        // 更新 firePoint 的位置
        UpdateFirePointPosition();
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
            case "rifle_side":
                rifle_side.SetActive(true);
                FlipCharacter(facingLeft);
                break;
            case "rifle_down":
                rifle_down.SetActive(true);
                break;
            case "rifle_up":
                rifle_up.SetActive(true);
                break;
        }

        // 更新 firePoint 的位置
        UpdateFirePointPosition();

        // 在觸發攻擊後添加計時器以恢復閒置動畫
        Invoke("ResetToIdle", 0.4f); // 0.4 秒後返回閒置狀態
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
        rifle_side.SetActive(false);
        rifle_down.SetActive(false);
        rifle_up.SetActive(false);
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
}
