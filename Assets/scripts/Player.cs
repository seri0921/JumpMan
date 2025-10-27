using UnityEngine;
using System.Collections; // コルーチンを使うために必要

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{

    private bool isSpinning = false; // スピン中かどうかを管理するフラグ
    // ( ... enumや変数の宣言は変更なし ... )
    public enum PlayerType { Player1, Player2 }
    [Header("プレイヤー設定")]
    public PlayerType playerType = PlayerType.Player1;
    [Header("アクション設定")]
    public float rotateSpeed = 200f;
    public float jumpForce = 10f;

    [SerializeField]
    public int playerHP;
    private int firstPlayerHP;

    private Rigidbody2D rb;

    private Vector3 startpos;
    void Start()
    {
        firstPlayerHP = playerHP;
        rb = GetComponent<Rigidbody2D>();
        startpos = transform.position;
    }

    void Update()
    {
        if (!isSpinning)
        {
            HandleRotation();
        }
        // 新しく追加した画面端のワープ処理を呼び出す
        HandleScreenWrap();

        if (playerHP < 0)
        {
            Die();
            playerHP = firstPlayerHP;
        }

    }
    private void HandleScreenWrap()
    {
        // 現在の位置情報を取得
        Vector3 newPosition = transform.position;

        // x座標が12より大きくなったら
        if (newPosition.x > 9f)
        {
            // x座標を-12にする
            newPosition.x = -9f;
        }
        // x座標が-12より小さくなったら
        else if (newPosition.x < -9f)
        {
            // x座標を12にする
            newPosition.x = 9f;
        }
        // x座標が12より大きくなったら
        if (newPosition.y < -6f)
        {
            // x座標を-12にする
            newPosition.y = 5f;
        }

        // 計算後の新しい位置をオブジェクトに適用
        transform.position = newPosition;
    }
    private void HandleRotation()
    {
        // ( ... 回転処理は変更なし ... )
        float rotateInput = 0f;
        switch (playerType)
        {
            case PlayerType.Player1:
                if (Input.GetKey(KeyCode.A)) rotateInput = 1f;
                else if (Input.GetKey(KeyCode.D)) rotateInput = -1f;
                break;
            case PlayerType.Player2:
                if (Input.GetKey(KeyCode.LeftArrow)) rotateInput = 1f;
                else if (Input.GetKey(KeyCode.RightArrow)) rotateInput = -1f;
                break;
        }
        transform.Rotate(0, 0, rotateInput * rotateSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         playerHP--;
    //     }
    // }


    public void HandleSwordClash(float power)
    {
        // ( ... 剣の衝突処理は変更なし ... )
        // rb.linearVelocity = Vector2.zero;
        // Vector2 backwardDir = new Vector2(-1, -1)
        // Vector2 knockbackDir = new Vector2(backwardDir.x, Mathf.Abs(backwardDir.y));
        // if (knockbackDir.y < 0.1f) { knockbackDir.y = 0.1f; }
        // rb.AddForce(knockbackDir.normalized * knockbackForce, ForceMode2D.Impulse);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(transform.up * power, ForceMode2D.Impulse);
    }
    public void StartSpinAttack()
    {
        // すでにスピン中でなければ、新しいスピンを開始する
        if (!isSpinning)
        {
            StartCoroutine(SpinCoroutine());
        }
    }

    // --- ここからが新しい追加部分 ---

    /// <summary>
    /// プレイヤー本体のコライダーが他のトリガーに触れたときに呼ばれる
    /// </summary>]
    private IEnumerator SpinCoroutine()
    {
        // 1. スピン開始の準備
        isSpinning = true; // スピン状態フラグをオンにする
        float duration = 0.3f; // 回転にかかる時間
        float elapsed = 0f; // 経過時間
        float spinSpeed = 360f / duration; // 1秒あたりの回転速度

        // 2. 回転処理のループ
        while (elapsed < duration)
        {
            int muki = 0;
            if (playerType == PlayerType.Player1) muki = -1;
            else muki = 1;
                // 1フレーム分の回転量を計算し、Z軸周りに回転させる
                transform.Rotate(0, 0, spinSpeed * Time.deltaTime * muki);

            // 経過時間を更新
            elapsed += Time.deltaTime;

            // 1フレーム待つ
            yield return null;
        }

        // 3. スピン終了処理
        isSpinning = false; // スピン状態フラグをオフに戻す
    }


    /// <summary>
    /// 死亡したときの処理
    /// </summary>
    public void Die()
    {
        Debug.Log(gameObject.name + " は倒された！");
        // このゲームオブジェクトを非表示にする
        gameObject.transform.position = startpos;
    }
    // --- 追加部分ここまで ---
}