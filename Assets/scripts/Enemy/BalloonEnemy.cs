using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class BalloonEnemy : EnemyBase
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float refrectForce;
    [SerializeField]
    private float knockbackForce;
    private SpriteRenderer sr;

    [SerializeField]
    private bool redBalloon; //赤風船かどうかフラグ

    private bool isMuteki = false; // 無敵状態かどうか
    [SerializeField]
    private float mutekiTime = 0.2f;

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
        if (isMuteki)
        {
            return;
        }

        if (!redBalloon)
        {
            // 白風船の処理
            Instantiate(enemyDestroyEffect, transform.position, Quaternion.identity);
            SoundManager.Instance.EnemyDamage();
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

            StartCoroutine(MutekiCoroutine());
        }
    }

    private IEnumerator MutekiCoroutine()
    {
        isMuteki = true; // 無敵にする

        yield return new WaitForSeconds(mutekiTime); // まつ

        isMuteki = false; // 無敵解除
    }

    private IEnumerator StopAfterDelay()
    {
        yield return new WaitForSeconds(1.0f); // 0.3秒後に発動
        rb.linearVelocity = Vector2.zero;
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
                
                Vector2 dir = (transform.position - collision.gameObject.transform.position).normalized;
                rb.AddForce(dir * refrectForce, ForceMode2D.Impulse);
                StartCoroutine(StopAfterDelay());
            }

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 敵同士が触れたら反射する
            Vector2 dir = (transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(dir * refrectForce, ForceMode2D.Impulse);
            StartCoroutine(StopAfterDelay());
        }      
    }
}