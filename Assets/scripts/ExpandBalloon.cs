using UnityEngine;
using System.Collections;
public class ExpandBalloon : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float time;
    [SerializeField]
    private float expandTime;

    [SerializeField]
    private float expandRadius; // 爆発後の半径
    [SerializeField]
    private float destroyDelay = 0.2f; // 爆発後に消すまでの時間
    private CircleCollider2D circleCollider;
    private bool isExpand; //　一度だけ爆発させるためのフラグ

    [SerializeField]
    private Transform playerPos;

    //点滅させるための変数↓
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float startInterval = 0.5f; // 最初の点滅間隔
    [SerializeField] private float endInterval = 0.05f;  // 最終の点滅間隔

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isExpand = true;
        circleCollider = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkFaster());
    }

    // Update is called once per frame
    void Update()
    {
        //一定スピードでプレイヤーの方へ移動
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        
        time += Time.deltaTime;
        // 一定時間経過後爆発
        if (time > expandTime)
        {
            circleCollider.radius = expandRadius;
            Destroy(gameObject, destroyDelay);
        }
    }


    // 点滅させるための処理
    private IEnumerator BlinkFaster()
    {
        bool isColor = true; // 最初は白

        float elapsed = 0f;

        while (elapsed < expandTime)
        {
            // 経過時間の割合（0〜1）
            float t = elapsed / expandTime;

            // 点滅間隔を補間（だんだん短くなる）
            float currentInterval = Mathf.Lerp(startInterval, endInterval, t);

            // 赤白切り替え
            sr.color = isColor ? Color.white : Color.red;
            isColor = !isColor;

            // 次の点滅まで待機
            yield return new WaitForSeconds(currentInterval);

            elapsed += currentInterval;
        }
    }
}
