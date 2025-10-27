using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSound; // BGM

    [Header("SE")]
    [SerializeField] private AudioSource killSound; // 敵倒した時
    [SerializeField] private AudioSource playerDamageSound; // ダメージ音
    [SerializeField] private AudioSource junpSound; // ダイヤモンド音


    // コイン取得音を再生する関数
    public void KillSound()
    {
        killSound.Play();
    }

    public void PlayerDamageSound()
    {
        playerDamageSound.Play();
    }


    // ダメージ音を再生する関数
    public void JunpSound()
    {
        junpSound.Play();
    }
}
