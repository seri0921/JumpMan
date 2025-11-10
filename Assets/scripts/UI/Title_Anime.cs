using UnityEngine;
using DG.Tweening; // DOTweenを使用するために必須
using System.Collections;
using System.Linq; // LINQ (OrderBy) を使用するために必要

/// <summary>
/// ゲーム開始時から、子オブジェクトが名前順に時間差で
/// 上下にふわふわと動き続けるスクリプト。
/// </summary>
public class Title_Anime : MonoBehaviour
{
    [Header("Initial Setup")]
    [Tooltip("起動時に設定する初期スケール。エディタでの設定を優先する場合は 0 を設定")]
    [SerializeField] private float startScale = 0.7f;

    [Header("Idle Animation Settings")]
    [Tooltip("各文字のアニメーション開始の遅延時間差（秒）")]
    [SerializeField] private float perCharIdleStartDelay = 0.05f;

    [Tooltip("アイドルアニメーションでの上下移動距離")]
    [SerializeField] private float idleMoveDistance = 0.05f;

    [Tooltip("アイドルアニメーションの片道の移動にかかる時間（秒）")]
    [SerializeField] private float idleMoveDuration = 1.0f;

    [Tooltip("アイドルアニメーションのイージング（動きの種類）")]
    [SerializeField] private Ease idleEaseType = Ease.InOutSine;

    void Start()
    {
        // 子オブジェクトを名前順 (J_hide (0), U_hide (1)...) にソートして取得
        Transform[] sortedChildren = transform.Cast<Transform>()
                                             .OrderBy(t => t.name)
                                             .ToArray();

        // アイドルアニメーションをコルーチンで開始
        StartCoroutine(StartIdleAnimationSequentially(sortedChildren));
    }

    /// <summary>
    /// 子オブジェクトのアイドルアニメーションを順番に開始します。
    /// </summary>
    private IEnumerator StartIdleAnimationSequentially(Transform[] children)
    {
        // ソートされた順番で各子オブジェクトを処理
        foreach (Transform child in children)
        {
            // 既存のアニメーションを停止（安全のため）
            child.DOKill();

            // startScaleが0より大きい場合のみ、初期スケールを適用
            if (startScale > 0)
            {
                child.localScale = Vector3.one * startScale;
            }

            // 次の文字のアニメーションを開始するまで、指定された時間だけ待つ
            yield return new WaitForSeconds(perCharIdleStartDelay);

            // 現在のローカルY座標を基準に、上下への無限往復アニメーションを開始
            float startY = child.localPosition.y;

            child.DOLocalMoveY(startY + idleMoveDistance, idleMoveDuration)
                 .SetEase(idleEaseType)
                 .SetLoops(-1, LoopType.Yoyo); // -1 = 無限ループ, Yoyo = 往復
        }
    }
}