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
        // seSourceで、attackClipを、PlayOneShotで再生
        // PlayOneShotはSEが重なって再生される（連打しても音が鳴る）
        if (seSound != null && attacSound != null)
        {
            seSound.PlayOneShot(attacSound);
        }
    }

    public void PlayAttackSE2()
    {
        // seSourceで、attackClipを、PlayOneShotで再生
        // PlayOneShotはSEが重なって再生される（連打しても音が鳴る）
        if (seSound != null && attacSound2 != null)
        {
            seSound.PlayOneShot(attacSound2);
        }
    }

    // ダメージ音を再生する関数
    public void PlayDamageSE()
    {
        if (seSound != null && playerDamageSound != null)
        {
            seSound.PlayOneShot(playerDamageSound);
        }
    }

    // ジャンプ音を再生する関数
    public void PlayJumpSE()
    {
        if (seSound != null && junpSound != null)
        {
            seSound.PlayOneShot(junpSound);
        }
    }
}
