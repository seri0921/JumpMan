using UnityEngine;

public class Set_Bullet : MonoBehaviour
{
    public bool common = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            KillScore.Combo++;
            if (common)
            {
                Destroy(gameObject);
                KillScore.LifeOrBullet++;
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (common) {
                KillScore.ComboReset();
            }
        }


    }
}
