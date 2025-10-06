using UnityEngine;
using UnityEngine.InputSystem;

// PlayerInputコンポーネントを必須にする
[RequireComponent(typeof(PlayerInput))]
public class StickAngleDebugger : MonoBehaviour
{
    [Header("入力設定")]
    [Tooltip("この値より小さいスティックの傾きは無視")]
    [Range(0.1f, 0.9f)]
    [SerializeField] private float deadzone = 0.2f;

    private PlayerInput playerInput;
    private InputAction LstickAction;
    private Rigidbody2D rb;

    private bool isTracking = false; // 角度の追跡中かどうか

    private float stickStartAngle; // 基準となる角度
    private float playerStartAngle; // プレイヤーの角度
    private bool isAngleSet;   // 回転の合計角度

    [Header("角度")]
    [SerializeField] private float Kakudo = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // 必要なコンポーネントとアクションを取得
        playerInput = GetComponent<PlayerInput>();
        LstickAction = playerInput.actions["LStick"];
    }

    void Update()
    {
        // スティック入力の読み取り
        Vector2 stickInput = LstickAction.ReadValue<Vector2>();

        // スティックがデッドゾーンの外まで倒されているかチェック
        if (stickInput.magnitude > deadzone)
        {
            // 現在のスティックの角度を度数法で計算
            float currentAngle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;

            // 基準の角度がまだ設定されていない場合 (倒し始めの瞬間)
            if (!isAngleSet)
            {
                // 現在の角度を基準として保存
                stickStartAngle = currentAngle;
                // プレイヤーの現在の角度を保存
                playerStartAngle = rb.rotation;

                isAngleSet = true; // フラグを立てる
            }

            // 時計回りをプラス
            float relativeAngle = Mathf.DeltaAngle(stickStartAngle, currentAngle) * -1f;

            if (relativeAngle < 0)
            {
                relativeAngle += 360f;
            }

            rb.rotation = (playerStartAngle - relativeAngle) * Kakudo ;
            // 右方向への移動がプラス、左方向がマイナスになります
            Debug.Log($"基準からの角度差: {relativeAngle:F2}°");
        }
        else
        {
            // スティックが中央に戻された場合
            // 基準角度をリセットする
            if (isAngleSet)
            {
                Debug.Log("--- スティックが中央に戻されたため、基準をリセット ---");
                isAngleSet = false;
            }
        }
    }
}