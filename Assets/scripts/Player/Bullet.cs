using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bullet;
    public GameObject firepoint;

    private Rigidbody2D rb;
    [SerializeField] private float speed = 30f;
    [SerializeField] private bool AntiOn = true;
    [SerializeField] private float bulletForce = 2f;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if (bullet == null)
        {
            Debug.LogError("Bullet �v���n�u���ݒ肳��Ă��܂���B");
        }
        if (firepoint == null)
        {
            Debug.LogError("Firepoint ���ݒ肳��Ă��܂���B");
        }
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log($"Combo: {KillScore.LifeOrBullet}");

        var gamepad = Gamepad.current;
        if (gamepad.aButton.wasPressedThisFrame)
        {
            KillScore.LifeOrBullet --;
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
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayAttackSE();
            }

            Destroy(newBullet, 0.8f);

        }
        if (gamepad.bButton.wasPressedThisFrame)
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
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayAttackSE2();
            }
            Destroy(newBullet, 0.8f);

            AntiOn = false;
        }

    }
}
