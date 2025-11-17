using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class SelectTextLight : MonoBehaviour
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

    [SerializeField] private Color normalColor = Color.white; // 通常時の色

    private GameObject parentButton;

    void Awake()
    {
        // もしインスペクターで設定されていなければ、
        // 同じGameObjectからコンポーネントを自動で取得します。
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TMP_Text>();
        }

        parentButton = transform.parent.gameObject;
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == parentButton)
        {
            float sinValue = Mathf.Sin(Time.time * (2.0f * Mathf.PI) / blinkDuration);
            float t = (sinValue + 1.0f) / 2.0f;
            Color lerpedColor = Color.Lerp(colorStart, colorEnd, t);
            textMeshPro.color = lerpedColor;
        }
        else
        {
            textMeshPro.color = normalColor;
        }

        
    }
}

