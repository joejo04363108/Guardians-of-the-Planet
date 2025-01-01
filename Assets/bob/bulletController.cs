using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f; // 子彈的移動速度
    public int damage = 4;          // 子彈造成的傷害
    private Vector2 targetDirection; // 子彈的方向
    private float timer = 3.0f;      // 子彈的生命時間

    void Start()
    {
        // 獲取滑鼠位置並計算方向
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 忽略 Z 軸
        targetDirection = (mousePosition - transform.position).normalized;

        // 旋轉子彈朝向滑鼠位置
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Update()
    {
        // 子彈持續向方向移動
        transform.Translate(targetDirection * bulletSpeed * Time.deltaTime, Space.World);

        // 倒計時結束後銷毀子彈
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰到 "monster" 標籤的物件
        if (collision.CompareTag("monster"))
        {
            // 嘗試獲取 sporeMonsterController
            sporeMonsterController enemyController = collision.GetComponent<sporeMonsterController>();
            if (enemyController != null)
            {
                // 調用 TakeDamage 方法，對敵人造成傷害
                enemyController.TakeDamage(damage);
            }
        }

        // 無論是否是 monster，都銷毀子彈
        Destroy(gameObject);
    }
}
