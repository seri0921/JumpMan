using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CumulativeRotationTracker : MonoBehaviour
{
    //ダメージを受けて



    [Header("���͐ݒ�")]
    [Tooltip("���̒l��菬�����X�e�B�b�N�̌X���͖���")]
    [Range(0.1f, 0.9f)]
    [SerializeField] private float deadzone = 0.2f;

    [Header("�f�o�b�O")]
    [SerializeField] private float totalRotation = 0f;   // ��]�̍��v

    [Header("�p�x")]
    private float Kakudo;
    public float jumpForce = 10f;

    private PlayerInput playerInput;
    private InputAction lStickAction;
    private Rigidbody2D rb;

    private bool isTracking = false; // �X�e�B�b�N���쒆���ǂ���
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

    void Update()
    {
        // �V�����ǉ�������ʒ[�̃��[�v�������Ăяo��
        HandleScreenWrap();
        Kakudo = KillScore.rotateLevel;

        Vector2 stickInput = lStickAction.ReadValue<Vector2>();

        if (stickInput.magnitude > deadzone)
        {
            // �X�e�B�b�N�̊p�x���v�Z
            float currentAngle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;

            // �X�e�B�b�N��|���n�߂��u��
            if (!isTracking)
            {
                isTracking = true;

                playerStartAngle = rb.rotation; // �v���C���[�̌��݂̊p�x���ŏ��̊p�x��
                // �ŏ��̊p�x���u�O�̊p�x�v�Ƃ���
                startAngle = currentAngle;

                totalRotation = 0f;
            }

            // �O�̃t���[������̊p�x�̕ω��ʂ��v�Z
            float deltaAngle = Mathf.DeltaAngle(startAngle, currentAngle);

            // �ω��ʂ����v�ɉ��Z
            totalRotation += deltaAngle;

            // ���̃t���[���̂��߂Ɍ��݂̊p�x��ۑ�
            startAngle = currentAngle;


            rb.rotation = (playerStartAngle + totalRotation) * Kakudo;

            // �f�o�b�O�\��
           // Debug.Log($"���݂̊p�x: {currentAngle:F2}, ����: {deltaAngle:F2}, ��]���v: {totalRotation:F2}��");
        }
        else
        {
            // �X�e�B�b�N�������ɖ߂����烊�Z�b�g
            if (isTracking)
            {
                isTracking = false;
           //     Debug.Log("--- �X�e�B�b�N�������ɖ߂�܂��� ---");
            }
        }
    }


    private void HandleScreenWrap()
    {
        // ���݂̈ʒu�����擾
        Vector3 newPosition = transform.position;

        // x���W��12���傫���Ȃ�����
        if (newPosition.x > 9f)
        {
            // x���W��-12�ɂ���
            newPosition.x = -9f;
        }
        // x���W��-12��菬�����Ȃ�����
        else if (newPosition.x < -9f)
        {
            // x���W��12�ɂ���
            newPosition.x = 9f;
        }
        // x���W��12���傫���Ȃ�����
        if (newPosition.y < -6f)
        {
            // x���W��-12�ɂ���
            newPosition.y = 5f;
        }

        // �v�Z��̐V�����ʒu���I�u�W�F�N�g�ɓK�p
        transform.position = newPosition;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }


    // プレイヤーノックバック処理
    public void HandleSwordClash()
    {
        // ( ... 剣の衝突処理は変更なし ... )
        rb.linearVelocity = Vector2.zero;
        Vector2 backwardDir = -transform.up;
        Vector2 knockbackDir = new Vector2(backwardDir.x, Mathf.Abs(backwardDir.y));
        if (knockbackDir.y < 0.1f) { knockbackDir.y = 0.1f; }
        rb.AddForce(knockbackDir.normalized * knockbackForce, ForceMode2D.Impulse);
    }

}

