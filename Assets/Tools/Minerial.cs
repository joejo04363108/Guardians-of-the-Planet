using UnityEngine;

public class Mineral : MonoBehaviour
{
    public GameObject[] dropItems; // 掉落物 Prefab 列表
    public int maxDropCount = 3;   // 最大掉落數量
    public float dropSpread = 1f; // 掉落物散布範圍
    public ParticleSystem breakEffectPrefab; // 礦物破壞粒子效果
    public AudioClip breakSound;           // 礦物破壞音效

    public void Break()
    {
        // 隨機生成掉落物
        SpawnDrops();

        // 播放破壞效果（可選）
        PlayBreakEffect();

        // 銷毀礦物物件
        Destroy(gameObject);
    }

    void SpawnDrops()
    {
        int dropCount = Random.Range(1, maxDropCount + 1); // 隨機決定掉落數量

        for (int i = 0; i < dropCount; i++)
        {
            // 隨機選擇掉落物
            GameObject dropItem = dropItems[Random.Range(0, dropItems.Length)];

            // 隨機生成位置
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-dropSpread, dropSpread),
                0.5f,
                Random.Range(-dropSpread, dropSpread)
            );

            // 生成掉落物
            // 生成掉落物
            GameObject spawnedItem = Instantiate(dropItem, spawnPosition, Quaternion.identity);

            // 設置掉落物的 Layer 為 "Item"
            spawnedItem.tag = "Item";
        }
    }

    void PlayBreakEffect()
    {
        // 添加粒子效果或音效 (視需求)
        //ParticleSystem breakEffect = Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
        //breakEffect.Play();

        // 播放音效
        //AudioSource.PlayClipAtPoint(breakSound, transform.position);

        Debug.Log("礦物破壞效果播放！");
    }

    
}