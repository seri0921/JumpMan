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
    private float knockbackPower;

    public Player playerScript;

    //エネミーマネージャースクリプト
    public EnemyManager spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //一定スピードでプレイヤーの方へ移動
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("sword"))
        {
            spawner.EnemyDestroyed();
            Destroy(gameObject);
        }
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
        if (collision.gameObject.CompareTag("Player"))
        {
            //プレイヤーにダメージを与える
            GameManager.instance.player.playerHP--;
            GameManager.instance.player.HandleSwordClash();
            //プレイヤーにダメージを与えたらノックバック、それ以外は死亡
            // Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            // // 自分（このオブジェクト）から相手への方向ベクトルを計算
            // Vector2 direction = (collision.transform.position - transform.position).normalized;
            // // その方向にノックバックする
            // playerRb.AddForce(direction * knockbackPower, ForceMode2D.Impulse);
            // if (collision.gameObject.GetComponent<Player>().playerHP > 0)
            // {
            //     Debug.Log(collision.gameObject.GetComponent<Player>().playerHP);

            //     Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            //     // 自分（このオブジェクト）から相手への方向ベクトルを計算
            //     Vector2 direction = (collision.transform.position - transform.position).normalized;
            //     // その方向にノックバックする
            //     playerRb.AddForce(direction * knockbackPower, ForceMode2D.Impulse);

            // }
            // else
            // {
            //     collision.gameObject.GetComponent<Player>().Die();
            //     Destroy(gameObject);
            // }

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
