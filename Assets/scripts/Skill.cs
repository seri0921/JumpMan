using UnityEngine;

public class Skill : MonoBehaviour
{


        [Header("以上の角度")]
        [SerializeField] private float largeRt = 180f;

        [Header("以下の角度")]
        [SerializeField] private float smallRt = 30f;

        private bool isGrounded = false;      // 接地しているかどうかのフラグ
        private float TakeoffRt;     // 離陸した瞬間のプレイヤーのZ角度
        private Rigidbody2D rb;

        private void Start()
        {
             rb = GetComponent<Rigidbody2D>();
        }

        // 他のコライダーに接触したとき
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // タグが"Ground"かつ、空中にいたときか
            if (collision.gameObject.CompareTag("Ground") && !isGrounded)
            {
                isGrounded = true;

                float AirRt = Mathf.Abs(Mathf.DeltaAngle(rb.rotation, TakeoffRt));  // Mathf.DeltaAnglseで現在の角度とジャンプした時の角度を差を出し、Mathf.Absで絶対値にする。

                Debug.Log($"着地！ 空中での総回転角度: {AirRt:F2}°");

                // 1. 前回の着地から次の着地までに〇°以上回転
                if (AirRt >= largeRt)
                {
                    Debug.Log($"条件達成: {largeRt}°以上の回転！");
                KillScore.rotateLevel ++;
                }

                // 2. 前回の着地から次の着地までに〇°以下回転
                if (AirRt <= smallRt)
                {
                    Debug.Log($"条件達成: {smallRt}°以下の回転でした。");
                }

                // 3. 前回の着地から次の着地までの回転が85~95°以内
                if (AirRt >= 85f && AirRt <= 95f)
                {
                    Debug.Log("条件達成: 85°～95°");

                }
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {
            // タグが "Ground" から離れたとき
            if (collision.gameObject.CompareTag("Ground"))
            {
                // 接地状態から離れた瞬間（離陸の瞬間）の角度を記録
                if (isGrounded)
                {
                    TakeoffRt = rb.rotation;
                    Debug.Log($"離陸！ この瞬間の角度を記録: {TakeoffRt:F2}°");
                }
                isGrounded = false; // 空中状態に更新
            }
        }
    
}
