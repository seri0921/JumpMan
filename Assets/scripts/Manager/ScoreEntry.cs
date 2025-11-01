using System;

/// 1件ごとのスコアデータ
/// [Serializable] をつけないとJsonUtilityで変換できない
[Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}