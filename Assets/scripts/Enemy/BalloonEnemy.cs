using Unity.VisualScripting;
using UnityEngine;

public class BalloonEnemy : EnemyBase
{
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
    
    public void HandleHitBySword()
    {
        if (!redBalloon)
        {
            // 白風船の処理
            Instantiate(enemyDestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.05f);
            spawner.EnemyDestroyed();
            KillScore.killScore++;
        }
        else
        {
            // 赤風船の処理
            knockbackForce *= 2;
            redBalloon = false;
            UpdateBalloonColor();
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("sword"))
        {
            HandleHitBySword();

        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // 白風船の処理
            if (!redBalloon)
            {
                KillScore.LifeOrBullet--;

                Destroy(gameObject, 0.05f);
                spawner.EnemyDestroyed(); // エネミーの数を減らす
            }
            else
            {
                KillScore.LifeOrBullet--;

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
