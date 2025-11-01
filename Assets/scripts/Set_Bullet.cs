using UnityEngine;

public class Set_Bullet : MonoBehaviour
{
    public bool common = false;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            KillScore.Combo++;

            if (Combo.Instance != null)
            {
                Combo.Instance.ShowCombo(KillScore.Combo);
            }

            if (common)
            {
                Destroy(gameObject);
                KillScore.LifeOrBullet++;
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (common) 
            {
                KillScore.ComboReset();

                if (Combo.Instance != null)
                {
                    Combo.Instance.HideCombo();
                }
            }
        }
    }
}
