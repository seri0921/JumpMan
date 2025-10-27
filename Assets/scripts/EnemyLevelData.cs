using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLevelData", menuName = "Game/Enemy Level Data")]
public class EnemyLevelData : ScriptableObject
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public string enemyName;               // 敵の名前（デバッグ用）
        public GameObject prefab;              // 敵プレハブ
        public float spawnChance = 1f;         // 出現確率（重み）
        public int[] spawnPointIndices;        // 生成可能な位置のインデックス
    }

    [Header("このレベルで出現する敵リスト")]
    public EnemySpawnInfo[] enemyList;

    [Header("全体の設定")]
    public float spawnInterval = 3f;           // 生成間隔
    public int maxEnemies = 5;                 // 同時出現上限
}
