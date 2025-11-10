using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnGameStartButton()
    {
        // ゲーム開始時に現在のスコアを0にリセット
        //ScoreManager.Instance.ResetCurrentScore();

        // ゲームシーンに遷移
        SceneManager.LoadScene("main"); // "GameScene" は実際のゲームシーン名に置き換えてください
    }
}