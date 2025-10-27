// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyManager : MonoBehaviour
// {
//     public static EnemyManager instance = null;
//     public Transform playerTransform;
//     private float time;
//     [SerializeField]
//     private float spawnTime;
//     private int currentEnemies = 0; // 現在の敵数
//     [SerializeField] private int maxEnemies = 5; // 最大敵数

//     [SerializeField] private Transform[] spawnPoints;  // 敵が出現する4つの位置

//     [SerializeField] private GameObject[] enemyPrefabs;

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(this.gameObject);
//         }
//         else
//         {
//             Destroy(this.gameObject);
//         }
//     }

//     void Update()
//     {
//         time += Time.deltaTime;
//         if (time >= spawnTime)
//         {
//             RandomEnemySpawn();
//         }
//     }


//     //敵をランダムな位置に生成する関数
//     private void RandomEnemySpawn()
//     {
//         // 敵が最大数に達していたら生成を止める
//         if (currentEnemies >= maxEnemies)
//         {
//             return;
//         }

//         // 4つのランダムな位置から選ぶ
//         int index = Random.Range(0, spawnPoints.Length);
//         // 生成する敵を選出
//         int select = Random.Range(1, 5);
//         Transform spawnPoint = spawnPoints[index];

//         if (select == 1)
//         {
//             GameObject enemyPrefab = enemyPrefabs[0];
//             // エネミーにこのスクリプトの変数を呼び出せるようにセット
//             enemyPrefab.GetComponent<BalloonEnemy>().spawner = this; 
//             //エネミープレハブにプレイヤーの位置をセット
//             enemyPrefab.GetComponent<BalloonEnemy>().playerPos = playerTransform; 
//             GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
//             currentEnemies++; // 敵の数をカウント
//             time = 0;

//         }
//         else
//         {
//             GameObject enemyPrefab = enemyPrefabs[1];
//             // エネミーにこのスクリプトの変数を呼び出せるようにセット
//             enemyPrefab.GetComponent<BalloonEnemy>().spawner = this; 
//             //エネミープレハブにプレイヤーの位置をセット
//             enemyPrefab.GetComponent<BalloonEnemy>().playerPos = playerTransform; 
//             GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
//             currentEnemies++; // 敵の数をカウント
//             time = 0;
//         }
//     }
    
//     // 敵が死んだときにカウントを減らす関数
//     public void EnemyDestroyed()
//     {
//         currentEnemies--;
//     }
//  }