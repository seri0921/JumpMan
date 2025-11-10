//using UnityEngine;
//using System.Collections.Generic;
//using System.IO; // ファイルI/Oに必要
//using System.Linq; // LINQ (ソートなど) に必要

///// スコアとランキングを管理するシングルトンクラス
//public class ScoreManager : MonoBehaviour
//{
//    // シングルトンインスタンス
//    public static ScoreManager Instance { get; private set; }

//    // 現在のゲームプレイ中のスコア
//    public int CurrentScore { get; private set; }

//    // ランキングデータ
//    private RankingData rankingData;

//    // 保存先のパス
//    private string savePath;

//    // ランキングの最大保持数
//    private const int MAX_RANKING_COUNT = 10;

//    void Awake()
//    {
//        // シングルトンパターンの実装
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject); // シーン遷移しても破棄されないようにする

//            // 保存パスを決定 (プラットフォーム共通で使える場所)
//            savePath = Path.Combine(Application.persistentDataPath, "ranking.json");

//            // データをロード
//            LoadScores();
//        }
//        else
//        {
//            // 既にインスタンスが存在する場合は破棄
//            Destroy(gameObject);
//        }
//    }

//    /// <summary>
//    /// ゲーム開始時に現在のスコアをリセット
//    /// </summary>
//    public void ResetCurrentScore()
//    {
//        CurrentScore = 0;
//    }

//    /// <summary>
//    /// 現在のスコアを加算
//    /// </summary>
//    public void AddCurrentScore(int scoreToAdd)
//    {
//        CurrentScore += scoreToAdd;
//    }

//    /// <summary>
//    /// ランキングデータをファイルから読み込む
//    /// </summary>
//    private void LoadScores()
//    {
//        if (File.Exists(savePath))
//        {
//            string json = File.ReadAllText(savePath);
//            rankingData = JsonUtility.FromJson<RankingData>(json);
//        }
//        else
//        {
//            // セーブファイルがない場合は新規作成
//            rankingData = new RankingData();
//            rankingData.scores = new List<ScoreEntry>();
//        }
//    }

//    /// <summary>
//    /// ランキングデータをファイルに保存する
//    /// </summary>
//    private void SaveScores()
//    {
//        string json = JsonUtility.ToJson(rankingData, true); // trueで整形して保存
//        File.WriteAllText(savePath, json);
//    }

//    /// <summary>
//    /// 新しいスコアをランキングに追加
//    /// </summary>
//    /// <param name="playerName">プレイヤー名</param>
//    /// <param name="score">スコア</param>
//    public void AddScoreToRanking(string playerName, int score)
//    {
//        // 新しいスコアエントリーを作成
//        ScoreEntry newEntry = new ScoreEntry { playerName = playerName, score = score };

//        // リストに追加
//        rankingData.scores.Add(newEntry);

//        // スコアの降順 (高い順) にソート
//        // LINQ を使用 ( using System.Linq; が必要)
//        rankingData.scores = rankingData.scores.OrderByDescending(entry => entry.score).ToList();

//        // 上位10件のみ保持
//        if (rankingData.scores.Count > MAX_RANKING_COUNT)
//        {
//            // 11位以下を削除
//            rankingData.scores.RemoveRange(MAX_RANKING_COUNT, rankingData.scores.Count - MAX_RANKING_COUNT);
//        }

//        // 変更をファイルに保存
//        SaveScores();
//    }

//    /// <summary>
//    /// 現在のランキングリストを取得
//    /// </summary>
//    public List<ScoreEntry> GetRanking()
//    {
//        return rankingData.scores;
//    }

//}