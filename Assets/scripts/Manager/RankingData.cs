using System;
using System.Collections.Generic;

/// <summary>
/// ランキングデータ全体をJSON保存するためのラッパークラス
/// </summary>
[Serializable]
public class RankingData
{
    public List<ScoreEntry> scores;
}