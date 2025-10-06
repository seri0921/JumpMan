using UnityEngine;

// 剣にはCollider2Dが必須
[RequireComponent(typeof(Collider2D))]
public class SwordController : MonoBehaviour
{
    // この剣の持ち主（プレイヤー）
    private Player owner;

    /*void Start()
    {
        // 自分の親オブジェクトからPlayerControllerの情報を取得して保持する
        owner = GetComponentInParent<Player>();
        if (owner == null)
        {
            Debug.LogError("剣の親にPlayerControllerが見つかりません！");
        }
    }
    */
    private void OnTriggerStay2D(Collider2D other)
    {

        // 接触した相手のタグが "sword" かどうかをチェック
        if (other.CompareTag("sword"))
        {
            // 相手からSwordControllerコンポーネントを取得
            SwordController otherSword = other.GetComponent<SwordController>();

            // 相手が有効な剣で、かつ持ち主が自分ではないことを確認
            if (otherSword != null && otherSword.owner != this.owner)
            {
                // 持ち主（プレイヤー）に衝突を通知する
                owner.HandleSwordClash();
            }
        }  // 剣のコライダーが他のトリガーに触れたときに呼ばれる
           // 触れた相手のタグが "player" かどうかをチェック
        if (other.CompareTag("Player"))
        {
            // 念のため、自分の持ち主を攻撃しないようにチェックする
            // 相手(other)のルート階層と、自分(this)のルート階層が違う場合のみ攻撃
            
                // 相手のゲームオブジェクトをシーンから破壊（削除）する
           // Destroy(other.gameObject);
            other.gameObject.GetComponent<Player>().Die();
            
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
          //  owner.StartSpinAttack();
        }

    }
}