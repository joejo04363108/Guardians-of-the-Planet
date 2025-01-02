using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AdvancedEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;          // 敵人Prefab
    public int enemyCount = 5;              // 每次生成的敵人數量
    public Vector2 spawnAreaMin;            // 生成範圍左下角
    public Vector2 spawnAreaMax;            // 生成範圍右上角
    public float respawnCheckInterval = 2f; // 重生檢測間隔
    public float playerSafeDistance = 10f; // 主角遠離範圍的安全距離

    private GameObject player;              // 主角
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // 已生成的敵人列表

    void Start()
    {
        player = GameObject.FindWithTag("bob"); // 找到主角物件
        if (player == null)
        {
            Debug.LogError("未找到Tag為 'bob' 的主角物件！");
            return;
        }

        SpawnEnemies(); // 開始時生成一次敵人
        StartCoroutine(CheckAndRespawnEnemies()); // 開始檢測是否需要重生
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // 在範圍內隨機生成位置
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            // 生成敵人並加入列表
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    IEnumerator CheckAndRespawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnCheckInterval);

            // 如果敵人列表為空，檢查主角是否已遠離範圍
            if (AllEnemiesDestroyed() && PlayerIsSafe())
            {
                SpawnEnemies(); // 再次生成敵人
            }
        }
    }

    bool AllEnemiesDestroyed()
    {
        // 移除列表中已被摧毀的敵人
        spawnedEnemies.RemoveAll(enemy => enemy == null);
        return spawnedEnemies.Count == 0; // 如果列表為空，代表敵人都被摧毀
    }

    bool PlayerIsSafe()
    {
        if (player == null) return false; // 如果找不到主角，返回false

        // 計算主角與生成範圍中心的距離
        Vector2 spawnCenter = (spawnAreaMin + spawnAreaMax) / 2;
        float distance = Vector2.Distance(player.transform.position, spawnCenter);

        return distance > playerSafeDistance; // 如果距離大於安全距離，返回true
    }

    private void OnDrawGizmos()
    {
        // 可視化生成範圍
        Gizmos.color = Color.green;
        Vector3 bottomLeft = new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0);
        Vector3 topRight = new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0);
        Vector3 size = topRight - bottomLeft;
        Gizmos.DrawWireCube(bottomLeft + size / 2, size);

        // 可視化主角安全距離
        Gizmos.color = Color.red;
        Vector2 spawnCenter = (spawnAreaMin + spawnAreaMax) / 2;
        Gizmos.DrawWireSphere(spawnCenter, playerSafeDistance);
    }
}
