using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("BGM")]
    [SerializeField] AudioSource bgmSound; // BGM用

    [Header("SE")]
    [SerializeField] AudioSource seSound; // SE用

    [Header("BGMのオーディオクリップ")]
    [SerializeField] AudioClip bgmClip; // 

    [Header("SEのオーディオクリップ")]
    [SerializeField] AudioClip attacSound; // 攻撃音
    [SerializeField] AudioClip attacSound2; // 攻撃音2
    [SerializeField] AudioClip playerDamageSound; // ダメージ音
    [SerializeField] AudioClip junpSound;  // ジャンプ音
    [SerializeField] AudioClip enemyDamage; // 敵を倒した時

    public static SoundManager Instance { get; private set; } // シングルトンインスタンス

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 破壊されないように
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (bgmSound != null && bgmClip != null)
        {
            bgmSound.clip = bgmClip;
            bgmSound.loop = true;
            bgmSound.Play();
        }
    }

    // 攻撃音を再生する関数
    public void PlayAttackSE()
    {
        seSound.PlayOneShot(attacSound);
    }

    public void PlayAttackSE2()
    {
        seSound.PlayOneShot(attacSound2);
    }

    // ダメージ音を再生する関数
    public void PlayerDamageSE()
    {
        seSound.PlayOneShot(playerDamageSound);
    }

    // ジャンプ音を再生する関数
    public void PlayJumpSE()
    {
        seSound.PlayOneShot(junpSound);
    }

    public void EnemyDamage()
    {

        seSound.PlayOneShot(enemyDamage);
    }
}
