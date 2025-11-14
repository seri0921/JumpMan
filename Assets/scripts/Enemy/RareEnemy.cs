using UnityEngine;

public class RareEnemy : EnemyBase
{

    [Header("=== 動作設定 ===")]
    [SerializeField] private float horizontalSpeed = 3f;   // 右に進む速度
    [SerializeField] private float verticalRange = 2f;     // 上下に揺れる「幅」（中心からの最大距離）
    [SerializeField] private float verticalSpeed = 1.5f;   // 揺れる速さ（ノイズの周波数）
    [SerializeField] private int returnNum = 3; //往復回数

    private float startY;            // 揺れの中心となるY座標
    private float perlinOffset;      // 各個体で揺れ方を変えるためのオフセット
    private float startX;               // 出現時のX座標
    private float targetX;              // 折り返し地点のX座標
    private float currentDestinationX;  // 現在の目標X座標
    private int currentDirection = 1;   // 現在の進行方向 (1:右, -1:左)
    private int returnCount = 0;        // 往復（方向転換）した回数
    private bool isReturning = true;    // 往復動作中か？
    private bool isHit = false; //二重実行防止bool
    void Start()
    {
        startY = transform.position.y;
        perlinOffset = Random.Range(0f, 1000f);
        startX = transform.position.x;
        targetX = -startX; // Y軸で対称な地点
        currentDestinationX = targetX;
        currentDirection = (targetX > startX) ? 1 : -1; // targetXに向かう方向
        isReturning = true;
        returnCount = 0;
        mainCamera = Camera.main;
    }


    void Update()
    {
        if (isReturning)
        {
            // === 往復チェック ===
            bool reachedTarget = false;

            if (currentDirection == 1) // 右に進んでいる場合
            {
                // 目標地点（右側）を超えたか
                if (transform.position.x >= currentDestinationX)
                {
                    reachedTarget = true;
                }
            }
            else // 左に進んでいる場合
            {
                // 目標地点（左側）を超えたか
                if (transform.position.x <= currentDestinationX)
                {
                    reachedTarget = true;
                }
            }

            // 目標地点に到達したら折り返し処理
            if (reachedTarget)
            {
                HandleReturn();
            }
        }

        transform.Translate(Vector2.right * horizontalSpeed * currentDirection * Time.deltaTime);
        float timeSeed = (Time.time * verticalSpeed) + perlinOffset;
        float noise = Mathf.PerlinNoise(timeSeed, 0f);
        float yOffset = (noise * 2f - 1f) * verticalRange;
        Vector2 newPosition = transform.position;
        newPosition.y = startY + yOffset;
        transform.position = newPosition;

    }
    private void HandleReturn()
    {
        returnCount++; // 方向転換の回数をカウント

        // 規定回数に達したか？
        if (returnCount >= returnNum)
        {
            // 往復終了。自己破壊
            Destroy(gameObject);
        }
        else
        {
            // 方向転換
            currentDirection *= -1;

            // 次の目標地点を設定
            // (現在地が targetX だったら startX へ、startX だったら targetX へ)
            currentDestinationX = (currentDestinationX == targetX) ? startX : targetX;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("sword")& !isHit)
        {
            isHit = true;
            KillScore.ComboScore += 200;
            SoundManager.Instance.RareEnemy();
            int DieText = 200 + KillScore.Combo * 10;
            ShowScorePopup(DieText.ToString());
            if (spawner != null)
            {
                spawner.EnemyDestroyed(); 
            }
            Destroy(gameObject);
        }

    }

}
