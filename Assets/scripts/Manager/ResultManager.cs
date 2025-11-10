//using UnityEngine;
//using UnityEngine.UI; // Textを使う場合
//using TMPro; // TextMeshProを使う場合
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;

//public class ResultManager : MonoBehaviour
//{
//    // （Inspectorなどで設定する）
//    public TMP_Text scoreText; // 今回獲得したスコア表示用
//    public TMP_Text rankingText; // ランキング一覧表示用

//    [SerializeField] private TMP_InputField playerNameInput; //プレイヤー名を登録するためのUI

//    void Start()
//    {
//        // 1. ゲームシーンでの最終スコアを取得
//        int finalScore = ScoreManager.Instance.CurrentScore;
//        scoreText.text = $"Your Score: {finalScore}";

//    }

//    void DisplayRanking()
//    {
//        rankingText.text = ""; // 表示を初期化

//        List<ScoreEntry> ranking = ScoreManager.Instance.GetRanking();

//        if (ranking.Count == 0)
//        {
//            rankingText.text = "No Scores Yet";
//            return;
//        }

//        for (int i = 0; i < ranking.Count; i++)
//        {
//            // 順位 (i+1)、名前、スコア を表示
//            rankingText.text += $"{i + 1}. {ranking[i].playerName} : {ranking[i].score}\n";
//        }
//    }

//    // タイトルに戻るボタン用
//    public void OnGoToTitleButton()
//    {
//        SceneManager.LoadScene("TestTitleScene"); // "TitleScene" は実際のタイトルシーン名に
//    }

//    public void OnSubmitName()
//{
//    string playerName = playerNameInput.text.Trim();
//    if (string.IsNullOrEmpty(playerName)) playerName = "PLAYER";

//    int finalScore = ScoreManager.Instance.CurrentScore;
//    ScoreManager.Instance.AddScoreToRanking(playerName, finalScore);

//    DisplayRanking();
//}
//}