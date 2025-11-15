using UnityEngine;
using DG.Tweening; // DOTweenを使用するために必要

public class kabeColor : MonoBehaviour
{
    [Header("色の設定")]
    // 色が戻る速度（インスペクターで調整可能）
    float fadeSpeed = 3f;

    // ターゲットの色（ヒットした時の色）
    private Color hitColor = Color.yellow;
    // 元の色
    private Color originalColor;


    [Header("揺れの設定")]
    // 揺れる時間
    float shakeDuration = 0.2f;
    // 揺れの強さ
    float shakeStrength = 0.1f;
    // 振動数
    public int shakeVibrato = 10;


    private SpriteRenderer spriteRenderer;
    private bool isHit = false;
    // 揺れが重複しないように管理
    private bool isShaking = false;

    void Awake()
    {
        // SpriteRendererコンポーネントを取得
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // 起動時の色を「元の色」として保存
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRendererがこのオブジェクトに見つかりません。");
        }
    }

    void Update()
    {
        // isHitフラグが立っている場合（ヒットした後）
        if (isHit)
        {
            // Color.Lerpを使用して、現在の色(spriteRenderer.color)から
            // 元の色(originalColor)へ徐々に変化させます。
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, Time.deltaTime * fadeSpeed);

            // 色の差を計算するために、ColorをVector4に変換します
            Vector4 currentColorVec = spriteRenderer.color;
            Vector4 originalColorVec = originalColor;

            // 2つのベクトルの差の大きさの2乗 (sqrMagnitude) を計算します。
            float sqrDifference = (currentColorVec - originalColorVec).sqrMagnitude;

            // 差が非常に小さくなったら（ここでは 0.0001f 未満）
            if (sqrDifference < 0.0001f)
            {
                // ほぼ戻ったら、正確に元の色に戻す
                spriteRenderer.color = originalColor;
                // フラグをリセット
                isHit = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ヒット処理を実行
        StartEffects();
    }

    // 色変更と揺れを開始する共通処理
    private void StartEffects()
    {
        // 1. 色変更処理
        if (spriteRenderer != null && !isHit) // 既に戻り処理中でなければ
        {
            // 色を即座に赤（hitColor）に変更
            spriteRenderer.color = hitColor;
            // Updateでの色戻し処理を開始
            isHit = true;
        }

        // 2. 揺れ処理 (DOTween)
        // 既に揺れていなければ、新しく揺れを開始する
        if (!isShaking)
        {
            isShaking = true;

            // DOShakePosition(揺れ時間, 揺れの強さ, 振動数)
            transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato)
                .OnComplete(() => {
                    // 揺れが終わったらフラグを戻す
                    isShaking = false;
                });
        }
    }
}