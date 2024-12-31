using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f; // 子彈的移動速度
    private Vector2 targetDirection; // 子彈的方向

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 檢測碰撞，銷毀子彈
        Destroy(gameObject);
    }
}
