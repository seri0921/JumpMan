using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Set_Bullet : MonoBehaviour
{
    public bool common = false;
    private bool  isHit = false;

    private void Update()
    {
        if (KillScore.LifeOrBullet < 0)
        {
            SceneManager.LoadScene("TestResultScene");
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //if (common == false)
            //{
            //    if (Time.timeScale == 1f) 
            //    {
            //        StartCoroutine(HitStopCoroutine(0.2f));
            //    }
            //}

            KillScore.Combo++;

            if (Combo.Instance != null)
            {
                Combo.Instance.ShowCombo(KillScore.Combo);
            }

            if (common)
            {
                Destroy(gameObject);
                if (isHit == false) {
                    KillScore.LifeOrBullet++;
                    isHit = true;
                }
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

    //IEnumerator HitStopCoroutine(float duration)
    //{
    //    Time.timeScale = 0f;
    //    yield return new WaitForSecondsRealtime(duration);
    //    Time.timeScale = 1f;
    //}
}
