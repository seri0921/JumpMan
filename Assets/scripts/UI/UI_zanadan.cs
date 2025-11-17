using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
public class UI_zandan : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    // === 追従用 ===
    public Transform target; // 追従対象（自機）
    public Vector3 offset = new Vector3(0, 50, 0); // 画面上のオフセット

    private RectTransform rectTransform;
    private Camera mainCamera;

    void Start()
    {
        // 追従用の初期化
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main; // ★ mainCamera をここで取得します

        // テキスト用の初期化 (もしInspectorで設定されていなければ)
        if (textComponent == null)
        {
            textComponent = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        // --- 1. テキスト更新処理 ---
        if (textComponent != null)
        {
            int point = KillScore.LifeOrBullet;
            textComponent.text = string.Format("{0:0}", point);
        }

        // --- 2. 追従処理 ---
        if (target == null)
        {
            // 追従対象が（シーン切り替えなどで）見つからない場合
            // NullReferenceExceptionを避けるために処理を中断
            return;
        }

        // mainCamera が Start() で取得できているか確認
        if (mainCamera != null && rectTransform != null)
        {
            // ターゲットの3D/2Dワールド座標を2Dスクリーン座標に変換
            Vector2 screenPos = mainCamera.WorldToScreenPoint(target.position);

            // UI(RectTransform)の位置を更新
            rectTransform.position = screenPos + (Vector2)offset;
        }
        else if (mainCamera == null)
        {
            // mainCamera が見つからない場合（エラー防止）
            Debug.LogWarning("zandan.cs: Main Camera が見つかりません。");
            mainCamera = Camera.main; // 毎フレーム探しに行く（非推奨だが安全）
        }
    }
}
