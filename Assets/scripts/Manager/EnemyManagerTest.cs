using UnityEngine;
using UnityEngine.SceneManagement;

public  class EnemyManagerTest : MonoBehaviour
{
    [Header("=== レベル管理 ===")]
    private float time;
    [SerializeField] private float levelupTime = 30f;
    [SerializeField] private float limitTime = 90f;
    private int currentLevel = 1;

    [Header("=== プレイヤー ===")]
    [SerializeField] private Transform playerTransform;

    [SerializeField] private GameObject DieTextPrefab; // 敵に渡す「演出プレハブ」
    [SerializeField] private Transform uiCanvasTransform;      // 敵に渡す「CanvasのTransform」
    [Header("=== 敵生成管理 ===")]
    private float spawnTimer;                         //  生成間隔
    private int currentEnemies = 0;                   //  現在の敵数
    [SerializeField] private Transform[] spawnPoints; //  シーン上の位置をここに設定

    [SerializeField] private EnemyLevelData level1Data;
    [SerializeField] private EnemyLevelData level2Data;
    [SerializeField] private EnemyLevelData level3Data;
    [SerializeField] private EnemyLevelData level4Data;

    private EnemyLevelData currentData;

    void Start()
    {
        SetLevel(currentLevel);
    }

    void Update()
    {
        time += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        //  レベルアップ処理
        if (time >= levelupTime)
        {
            currentLevel++;
            SetLevel(currentLevel);
            time = 0;
        }

        if (time >= limitTime)
        {
            SceneManager.LoadScene("TestResultScene");
        }

        //  敵生成処理
        if (currentData != null && spawnTimer >= currentData.spawnInterval)
        {
            RandomEnemySpawn(currentData);
            spawnTimer = 0;
        }
    }

    private void SetLevel(int level)
    {
        switch (level)
        {
            case 1:
                currentData = level1Data;
                break;
            case 2:
                currentData = level2Data;
                break;
            case 3:
                currentData = level3Data;
                break;
            case 4:
                currentData = level4Data;
                break;
            default:
                Debug.Log("これ以上のレベルデータはありません");
                return;
        }

        currentEnemies = 0;
        Debug.Log($"Level {level} 開始");
    }

    private void RandomEnemySpawn(EnemyLevelData data)
    {
        if (currentEnemies >= data.maxEnemies) return;

        //  確率に基づいて敵を選ぶ
        GameObject enemyPrefab = GetEnemyByProbability(data);
        if (enemyPrefab == null) return;

        //  出現位置をランダムに選ぶ
        int[] indices = GetSpawnPointIndices(data, enemyPrefab);
        int randomIndex = Random.Range(0, indices.Length);
        Transform spawnPoint = spawnPoints[indices[randomIndex]];

        // 敵を生成
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // 共通クラスを取得
        EnemyBase script = enemy.GetComponent<EnemyBase>();
        
        script.spawner = this;
        script.playerPos = playerTransform;
        script.DieText = DieTextPrefab;
        script.uiCanvas = uiCanvasTransform;

        currentEnemies++; 
    }

    // 敵が死んだとき呼ばれる
    public void EnemyDestroyed()
    {
        currentEnemies--;
    }

    // ==== 確率的に敵を選ぶ ====
    private GameObject GetEnemyByProbability(EnemyLevelData data)
    {
        float totalWeight = 0f;
        foreach (var e in data.enemyList)
        {
            totalWeight += e.spawnChance;
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulative = 0f;

        foreach (var e in data.enemyList)
        {
            cumulative += e.spawnChance;
            if (randomValue <= cumulative)
            {
                return e.prefab;
            }
        }

        return null;
    }

    // ==== 敵に対応する出現位置 ====
   private int[] GetSpawnPointIndices(EnemyLevelData data, GameObject prefab)
    {
        foreach (var e in data.enemyList)
        {
            if (e.prefab == prefab)
                return e.spawnPointIndices;
        }
        return new int[] { 0 };
    }
}
