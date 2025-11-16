using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bullet;
    public GameObject firepoint;

    private Rigidbody2D rb;
    [SerializeField] private float speed = 30f;
    [SerializeField] private bool AntiOn = true;
    [SerializeField] private float bulletForce = 2f;

    private PlayerInput playerInput;

    private InputAction normalAttack;
    private InputAction spAttack;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform firePoint;        // 予測線の位置
    [SerializeField] private float maxDistance = 100f;   // 予測線の最大の長さ
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Color lineStartColor = new Color(1f, 1f, 1f, 1f); // 始点の色 (デフォルトは白の不透明)
    [SerializeField] private Color lineEndColor = new Color(1f, 1f, 1f, 0f);   // 終点の色 (デフォルトは白の透明)

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();

        normalAttack = playerInput.actions["normalAt"];
        spAttack = playerInput.actions["spAt"];

        lineRenderer.enabled = false;  // 最初は予測線オフ
        lineRenderer.startColor = lineStartColor;
        lineRenderer.endColor = lineEndColor;

    }

    // Update is called once per frame
    void Update()
    {
        if (normalAttack.IsPressed()) // 通常弾で押されている間
        {
            lineRenderer.enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up, maxDistance, layerMask);
            lineRenderer.SetPosition(0, firePoint.position);

            if (hit.collider != null)
            {
                lineRenderer.SetPosition(1, hit.point);   // エネミーに当たったら貫通させない
            }
            else
            {
                Vector2 endPosition = (Vector2)firePoint.position + (Vector2)firePoint.up * maxDistance;  // 予測線の最大の長さまで
                lineRenderer.SetPosition(1, endPosition);
            }
        }
        else if (spAttack.IsPressed()) // 貫通弾のボタンが押されている間
        {
            lineRenderer.enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up, maxDistance, layerMask);
            lineRenderer.SetPosition(0, firePoint.position);
            Vector2 endPosition = (Vector2)firePoint.position + (Vector2)firePoint.up * maxDistance;  // 予測線の最大の長さまで
            lineRenderer.SetPosition(1, endPosition);
        }
        else
        {
            lineRenderer.enabled = false;   // 予測線オフ
        }


        if (normalAttack.WasReleasedThisFrame()) // ボタンが離されたとき
        {
            KillScore.LifeOrBullet--;
            Transform firepointTransform = firepoint.transform;
            Vector2 bulletPosi = firepointTransform.position;
            Vector2 direction = firepointTransform.up;
            Quaternion bulletRot = firepointTransform.rotation;
            if (AntiOn)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(-direction * bulletForce, ForceMode2D.Impulse);
            }
            GameObject newBullet = Instantiate(bullet, bulletPosi, bulletRot);
            newBullet.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
            newBullet.name = bullet.name;
            Set_Bullet bulletScript = newBullet.GetComponent<Set_Bullet>();
            if (bulletScript != null)
            {
                bulletScript.common = true;
            }
            SoundManager.Instance.PlayAttackSE();
            Destroy(newBullet, 1.5f);

        }
        if (spAttack.WasReleasedThisFrame())  // ボタンが離されたとき
        {
            KillScore.LifeOrBullet --;
            AntiOn = true;
            Transform firepointTransform = firepoint.transform;
            Vector2 bulletPosi = firepointTransform.position;
            Vector2 direction = firepointTransform.up;
            Quaternion bulletRot = firepointTransform.rotation;
            if (AntiOn)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(-direction * bulletForce, ForceMode2D.Impulse);
            }
            GameObject newBullet = Instantiate(bullet, bulletPosi, bulletRot);
            newBullet.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
            newBullet.name = bullet.name;
            SoundManager.Instance.PlayAttackSE2();
            Destroy(newBullet, 1.5f);

            AntiOn = false;
        }

        if (KillScore.LifeOrBullet <= 0)
        {
            StartCoroutine(Count());
        }
    }

    private IEnumerator Count()
    {
        yield return new WaitForSeconds(1.0f);
        if (KillScore.LifeOrBullet <=  0)
        {
            SceneManager.LoadScene("ResultScene");
        }

    }
}
