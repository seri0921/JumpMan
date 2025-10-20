using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;
    public Transform playerTransform;
    private float time;
    [SerializeField]
    private float spawnTime;
    private int currentEnemies = 0; // 現在の敵数
    [SerializeField] private int maxEnemies = 5; // 最大敵数

    [SerializeField] private float spawnRadius = 5f; // プレイヤーの周囲半径

    [SerializeField] private GameObject enemyPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnTime)
        {
            RandomEnemySpawn();
        }
    }


    //敵をランダムな位置に生成する関数
    private void RandomEnemySpawn()
    {
        // 敵が最大数に達していたら生成を止める
        if (currentEnemies >= maxEnemies)
        {
            return;
        }

        Vector2 randomDir = Random.insideUnitCircle.normalized; // ランダムな方向（単位円）
        float randomDist = Random.Range(2f, spawnRadius); // ランダムな距離
        Vector2 spawnPos = (Vector2)playerTransform.position + randomDir * randomDist; // プレイヤー中心に生成位置を計算
        spawnPos.y = Mathf.Max(spawnPos.y, 0.5f); // 地面より上で生成させる

        // エネミーにこのスクリプトの変数を呼び出せるようにセット
        enemyPrefab.GetComponent<BalloonEnemy>().spawner = this; 
        //エネミープレハブにプレイヤーの位置をセット
        enemyPrefab.GetComponent<BalloonEnemy>().playerPos = playerTransform; 
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        currentEnemies++; // 敵の数をカウント
        time = 0;
    }
    
    // 敵が死んだときにカウントを減らす関数
    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
 }