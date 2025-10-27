using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
    void Start()
    {
        // ParticleSystemの寿命を調べて自動削除
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(gameObject, ps.main.duration);
        }
        else
        {
            Destroy(gameObject, 1.0f);
        }
    }
}
