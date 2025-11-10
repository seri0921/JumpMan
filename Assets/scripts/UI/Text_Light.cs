using UnityEngine;
using TMPro; // TextMeshProのnamespaceを必ず記述

/// <summary>
/// TextMeshProのテキストカラーを指定された2色間で
/// 滑らかに点滅（グラデーション）させるスクリプト
/// </summary>
[RequireComponent(typeof(TMP_Text))] // このスクリプトはTMP_Textを必要とします
public class Text_Light : MonoBehaviour
{
    [Header("Text Component")]
    [Tooltip("対象のTextMeshProコンポーネント。空の場合、自動で取得します。")]
    [SerializeField] private TMP_Text textMeshPro;

    [Header("Blink Settings")]
    [Tooltip("点滅の1サイクルにかかる時間（秒）")]
    [SerializeField] private float blinkDuration = 1.0f;

    [Tooltip("開始色 (RGP = 0, 255, 255)")]
    [SerializeField] private Color colorStart = new Color(0f, 1f, 1f); // シアン (R:0, G:1, B:1)

    [Tooltip("終了色 (RGP = 255, 255, 255)")]
    [SerializeField] private Color colorEnd = new Color(1f, 1f, 1f); // 白 (R:1, G:1, B:1)

    void Awake()
    {
        // もしインスペクターで設定されていなければ、
        // 同じGameObjectからコンポーネントを自動で取得します。
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        // 実行に必要なコンポーネントや設定が正しいかチェック
        if (textMeshPro == null || blinkDuration <= 0)
        {
            return; // 処理を中断
        }

        // 1. 滑らかな往復値(0.0 ~ 1.0)を計算
        // Mathf.Sin() は -1.0 から 1.0 の値を返します。
        // 時間を (2 * PI / 期間) でスケーリングすることで、周期を blinkDuration に調整します。
        float sinValue = Mathf.Sin(Time.time * (2.0f * Mathf.PI) / blinkDuration);

        // Sin波の (-1.0 ~ 1.0) を (0.0 ~ 1.0) の範囲にマッピングします。
        // これが colorStart から colorEnd への補間係数 (t) となります。
        float t = (sinValue + 1.0f) / 2.0f;

        // 2. 2色間を補間 (Lerp)
        // t の値 (0.0 ~ 1.0) に応じて、colorStart と colorEnd の間の色を計算
        Color lerpedColor = Color.Lerp(colorStart, colorEnd, t);

        // 3. テキストの色に適用
        // テキスト全体の色を、計算した色に設定します。
        textMeshPro.color = lerpedColor;
    }
}
