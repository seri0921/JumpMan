using Unity.VisualScripting;
using UnityEngine;

public class BalloonEnemy : MonoBehaviour
{
    [SerializeField]
    public Transform playerPos;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float knockbackForce;
    private SpriteRenderer sr;

    [SerializeField]
    private bool redBalloon; //赤風船かどうかフラグ

    //エネミーマネージャースクリプト
    public EnemyManagerTest spawner;

    // エフェクト
    public GameObject enemyDestroyEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateBalloonColor();
    }

    // Update is called once per frame
    void Update()
    {
        //一定スピードでプレイヤーの方へ移動
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }

    // 風船の色を変化させる関数
    void UpdateBalloonColor()
{
    if (sr != null)
        {
        // 赤風船なら赤に白風船なら白に変化
        sr.color = redBalloon ? Color.red : Color.white;
    }
}
    private void OnTriggerStay2D(Collider2D other)
    {
        // if (other.CompareTag("Player"))
        // {
        //     //プレイヤーにダメージを与える
        //     other.gameObject.GetComponent<Player>().playerHP--;
        //     //プレイヤーにダメージを与えたらノックバック、それ以外は死亡
        //     if (other.gameObject.GetComponent<Player>().playerHP > 0)
        //     {
        //         Debug.Log(other.gameObject.GetComponent<Player>().playerHP);

        //         Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
        //         // 自分（このオブジェクト）から相手への方向ベクトルを計算
        //         Vector2 direction = (other.transform.position - transform.position).normalized;
        //         // その方向にノックバックする
        //         playerRb.AddForce(direction * knockbackPower, ForceMode2D.Impulse);

        //     }
        //     else
        //     {
        //         other.gameObject.GetComponent<Player>().Die();
        //         Destroy(gameObject);
        //     }

        // }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("sword"))
        {
            // 白風船の処理
            if (!redBalloon)
            {
                GameManager.instance.player.HandleSwordClash(knockbackForce); // プレイヤーをノックバック
                Instantiate(enemyDestroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.05f);
                spawner.EnemyDestroyed(); // エネミーの数を減らす
                KillScore.killScore++; // スコアを加算
                Debug.Log("b");
            }
            else
            {
                knockbackForce *= 2;
                GameManager.instance.player.HandleSwordClash(knockbackForce); // プレイヤーをノックバック
                redBalloon = false;
                UpdateBalloonColor();
                Debug.Log("a");
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // 白風船の処理
            if (!redBalloon)
            {
                GameManager.instance.player.playerHP--; // プレイヤーにダメージを与える
                GameManager.instance.player.HandleSwordClash(knockbackForce); // プレイヤーをノックバック
                Destroy(gameObject, 0.05f);
                spawner.EnemyDestroyed(); // エネミーの数を減らす
            }
            else
            {
                GameManager.instance.player.playerHP--;
                GameManager.instance.player.HandleSwordClash(knockbackForce); // プレイヤーをノックバック
            }

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
