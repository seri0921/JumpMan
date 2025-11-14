using UnityEngine;
using System.Collections;
using TMPro;
public class RareEnemyDied : MonoBehaviour
{
    [Header("アニメーション設定")]
    [SerializeField] private float duration = 1.0f;    // 演出全体の時間（秒）
    [SerializeField] private float maxScale = 1.2f;    // ポップアップ時の最大スケール
    [SerializeField] private float moveUpSpeed = 1.0f; // 少し上に移動する速度
    private TextMeshProUGUI textMesh;
    private Color startColor;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        if (textMesh == null)
        {
            Debug.LogError("TextMeshPro が見つかりません！");
            Destroy(gameObject); // エラー時は即座に消滅
            return;
        }
    }

    private void Start()
    {
        startColor = textMesh.color;
        StartCoroutine(AnimatePopup());
    }

    public void SetText(string textToShow)
    {
        if (textMesh != null)
        {
            textMesh.text = textToShow;
        }
    }

    private IEnumerator AnimatePopup()
    {
        float elapsedTime = 0f;

        // 開始時・最大時・終了時のスケールを定義
        Vector3 startScaleVec = Vector3.one * 0.1f; // ほぼ見えない状態から
        Vector3 maxScaleVec = Vector3.one * maxScale;
        Vector3 finalScaleVec = Vector3.one;        // 最終的に 1.0 に戻る

        // --- 1. ポップアップ（拡大）フェーズ ---
        // 演出時間のうち、最初の30%を使って最大まで拡大
        float popUpDuration = duration * 0.3f;

        while (elapsedTime < popUpDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / popUpDuration); // 0.0 -> 1.0

            // スケールを 0.1 -> 1.2 へ変化
            transform.localScale = Vector3.Lerp(startScaleVec, maxScaleVec, progress);

            // 上昇
            transform.Translate(Vector3.up * moveUpSpeed * Time.deltaTime, Space.World);

            yield return null; // 1フレーム待つ
        }

        // --- 2. 安定＆フェードアウト フェーズ ---
        // 残りの70%の時間を使う
        float fadeDuration = duration - popUpDuration;
        elapsedTime = 0f; // フェード用のタイマーをリセット

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / fadeDuration); // 0.0 -> 1.0

            // スケールを 1.2 -> 1.0 へ変化
            transform.localScale = Vector3.Lerp(maxScaleVec, finalScaleVec, progress);

            // フェードアウト (アルファ値を 1.0 -> 0.0 へ)
            float newAlpha = Mathf.Lerp(startColor.a, 0f, progress);
            textMesh.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // 上昇
            transform.Translate(Vector3.up * moveUpSpeed * Time.deltaTime, Space.World);

            yield return null; // 1フレーム待つ
        }

        // --- 3. 演出終了＆破壊 ---
        Destroy(gameObject);
        //心折れてほぼコピペしてるンゴ...殺してくれ...
    }

    void Update()
    {
        
    }
}
