using UnityEngine;

public class SmoothFollow2D : MonoBehaviour
{
    [Header("ターゲット")]
    public Transform target; // 追従する対象のTransform

    [Header("位置の追従設定")]
    [Tooltip("位置の追従の滑らかさ。値が小さいほど遅れ、大きいほど素早い。")]
    public float positionSmoothSpeed = 5.0f;

    [Header("角度の追従設定")]
    [Tooltip("角度の追従の滑らかさ。値が小さいほど遅れ、大きいほど素早い。")]
    public float rotationSmoothSpeed = 5.0f;

    // このオブジェクト（カメラなど）のZ座標を維持するための変数
    private float zPosition;

    void Start()
    {
        // 起動時のZ座標を保存しておく
        zPosition = transform.position.z;
    }

    void LateUpdate()
    {
        // ターゲットが設定されていなければ、何もしない
        if (target == null)
        {
            Debug.LogWarning("ターゲットが設定されていません。", this);
            return;
        }

        // --- 1. 位置の追従 (Vector3.Lerp) ---

        // 目的地の座標を計算 (Z座標は自分自身の値を維持)
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            target.position.y,
            zPosition
        );

        // Lerpを使って滑らかに移動
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            positionSmoothSpeed * Time.deltaTime
        );

        transform.position = smoothedPosition;

        // --- 2. 角度の追従 (Quaternion.Lerp) ---

        // 目的の角度を取得
        Quaternion desiredRotation = target.rotation;

        // Quaternion.Lerp を使って滑らかに回転
        Quaternion smoothedRotation = Quaternion.Lerp(
            transform.rotation,   // A: 現在の角度
            desiredRotation,      // B: 目的の角度
            rotationSmoothSpeed * Time.deltaTime // t: 補間係数
        );

        transform.rotation = smoothedRotation;
    }
}