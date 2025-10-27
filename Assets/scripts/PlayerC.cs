using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerC: MonoBehaviour
{
    public CameraShake cameraShake;

    //ダメージを受けて
    [SerializeField]
    public int playerHP; //プレイヤーのHP
    private int firstPlayerHP;
    private Vector3 startpos;
    
    [Range(0.1f, 0.9f)]
    [SerializeField] private float deadzone = 0.2f;

    [Header("合計角度")]
    [SerializeField] private float totalRotation = 0f;   

    [Header("ジャンプ力")]
    private float Kakudo;
    public float jumpForce = 10f;

    private PlayerInput playerInput;
    private InputAction lStickAction;
    private Rigidbody2D rb;

    private bool isTracking = false; 
    private float startAngle = 0f;
    private float playerStartAngle = 0f;

    public float knockbackForce = 8f;



    void Awake()
    {
        KillScore.Reset();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        lStickAction = playerInput.actions["LStick"];
        Kakudo = KillScore.rotateLevel;

    }

    void Start()
    {
        firstPlayerHP = playerHP;
        startpos = transform.position;
    }

    void Update()
    {
        HandleScreenWrap();
        Kakudo = KillScore.rotateLevel;

        Vector2 stickInput = lStickAction.ReadValue<Vector2>();

        if (stickInput.magnitude > deadzone)
        {
            float currentAngle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;

            if (!isTracking)
            {
                isTracking = true;

                playerStartAngle = rb.rotation; 
                startAngle = currentAngle;

                totalRotation = 0f;
            }

            float deltaAngle = Mathf.DeltaAngle(startAngle, currentAngle);

            totalRotation += deltaAngle;

            startAngle = currentAngle;


            rb.rotation = (playerStartAngle + totalRotation) * Kakudo;

        }
        else
        {
            if (isTracking)
            {
                isTracking = false;
            }
        }
        
        if (playerHP < 0)
        {
            Die();
            playerHP = firstPlayerHP;
        }
    }


    private void HandleScreenWrap()
    {
        Vector3 newPosition = transform.position;

        if (newPosition.x > 9f)
        {
            newPosition.x = -9f;
        }

        else if (newPosition.x < -9f)
        {
            newPosition.x = 9f;
        }
        if (newPosition.y < -6f)
        {
            newPosition.y = 5f;
        }

        transform.position = newPosition;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // SoundManager.instance.JunpSound();   // ジャンプ音
            cameraShake.ShakeCamera(CameraShake.ShakeType.Light);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Enemy"))
    //    {
    //        playerHP--;
    //    }
    //}

    // プレイヤーノックバック処理
    public void HandleSwordClash(float power)
    {
        // ( ... 剣の衝突処理は変更なし ... )
        // rb.linearVelocity = Vector2.zero;
        // Vector2 backwardDir = new Vector2(-1, -1)
        // Vector2 knockbackDir = new Vector2(backwardDir.x, Mathf.Abs(backwardDir.y));
        // if (knockbackDir.y < 0.1f) { knockbackDir.y = 0.1f; }
        // rb.AddForce(knockbackDir.normalized * knockbackForce, ForceMode2D.Impulse);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(transform.up * power, ForceMode2D.Impulse);
    }

    public void Die()
    {
        cameraShake.ShakeCamera(CameraShake.ShakeType.Heavy);
        Debug.Log(gameObject.name + " は倒された！");
        // このゲームオブジェクトを非表示にする
        gameObject.transform.position = startpos;
        //SoundManager.instance.PlayerDamageSound();
    }

}

