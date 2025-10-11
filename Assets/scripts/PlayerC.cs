using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CumulativeRotationTracker : MonoBehaviour
{
    [Header("入力設定")]
    [Tooltip("この値より小さいスティックの傾きは無視")]
    [Range(0.1f, 0.9f)]
    [SerializeField] private float deadzone = 0.2f;

    [Header("デバッグ")]
    [SerializeField] private float totalRotation = 0f;   // 回転の合計

    [Header("角度")]
    private float Kakudo;
    public float jumpForce = 10f;

    private PlayerInput playerInput;
    private InputAction lStickAction;
    private Rigidbody2D rb;

    private bool isTracking = false; // スティック操作中かどうか
    private float startAngle = 0f;
    private float playerStartAngle = 0f;



    void Awake()
    {
        KillScore.Reset();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        lStickAction = playerInput.actions["LStick"];
        Kakudo = KillScore.rotateLevel;

    }

    void Update()
    {
        // 新しく追加した画面端のワープ処理を呼び出す
        HandleScreenWrap();
        Kakudo = KillScore.rotateLevel;

        Vector2 stickInput = lStickAction.ReadValue<Vector2>();

        if (stickInput.magnitude > deadzone)
        {
            // スティックの角度を計算
            float currentAngle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;

            // スティックを倒し始めた瞬間
            if (!isTracking)
            {
                isTracking = true;

                playerStartAngle = rb.rotation; // プレイヤーの現在の角度を最初の角度に
                // 最初の角度を「前の角度」とする
                startAngle = currentAngle;

                totalRotation = 0f;
            }

            // 前のフレームからの角度の変化量を計算
            float deltaAngle = Mathf.DeltaAngle(startAngle, currentAngle);

            // 変化量を合計に加算
            totalRotation += deltaAngle;

            // 次のフレームのために現在の角度を保存
            startAngle = currentAngle;


            rb.rotation = (playerStartAngle + totalRotation) * Kakudo;

            // デバッグ表示
           // Debug.Log($"現在の角度: {currentAngle:F2}, 差分: {deltaAngle:F2}, 回転合計: {totalRotation:F2}°");
        }
        else
        {
            // スティックが中央に戻ったらリセット
            if (isTracking)
            {
                isTracking = false;
           //     Debug.Log("--- スティックが中央に戻りました ---");
            }
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

}

