using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnGameStartButton()
    {
        // ゲーム開始時に現在のスコアを0にリセット
        ScoreManager.Instance.ResetCurrentScore();

        // ゲームシーンに遷移
        SceneManager.LoadScene("testScene 1"); // "GameScene" は実際のゲームシーン名に置き換えてください
    }
}