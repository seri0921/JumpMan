# JumpMan

## 概要
JumpManは、Unityで開発された2Dアクションシューティングゲームです。プレイヤーは回転しながら敵を倒し、高スコアを目指します。

## ゲーム説明
プレイヤーは画面内を自由に回転移動し、風船型の敵を撃破していきます。敵を倒すとスコアが加算され、レアな敵も出現します。画面端に到達すると反対側にワープする仕組みになっています。

### 主な機能
- **回転移動システム**: 左右回転で移動方向を制御
- **射撃システム**: 通常攻撃と特殊攻撃の2種類
- **敵システム**: 通常の風船敵とレア敵が出現
- **スコアシステム**: 敵撃破によるスコア加算
- **コンボシステム**: 連続撃破でコンボ発生
- **HPシステム**: プレイヤーの体力管理
- **カメラシェイク**: 攻撃時の臨場感演出

## 開発環境
- **Unity バージョン**: 6000.0.58f2
- **プラットフォーム**: PC (Windows/Mac/Linux)
- **使用アセット**: 
  - DOTween (アニメーション)
  - Universal Render Pipeline (URP)
  - Input System (新しい入力システム)

## セットアップ手順

### 必要なもの
1. Unity Hub
2. Unity Editor 6000.0.58f2 以上

### インストール方法
1. このリポジトリをクローン
   ```bash
   git clone https://github.com/seri0921/JumpMan.git
   ```

2. Unity Hubでプロジェクトを開く
   - Unity Hubを起動
   - 「開く」ボタンをクリック
   - クローンしたプロジェクトフォルダを選択

3. プロジェクトが開いたら、Scenesフォルダ内の「TitleScene」を開く

4. Playボタンを押してゲームを開始

## 操作方法
- **左回転**: 左回転キー（Input Systemで設定）
- **右回転**: 右回転キー（Input Systemで設定）
- **通常攻撃**: 通常攻撃キー（Input Systemで設定）
- **特殊攻撃**: 特殊攻撃キー（Input Systemで設定）

※具体的なキー設定は `Assets/InputSystem_Actions.inputactions` で確認・変更できます

## プロジェクト構造
```
Assets/
├── Scenes/           # ゲームシーン
│   ├── TitleScene.unity     # タイトル画面
│   ├── Main.unity           # メインゲーム
│   ├── ResultScene.unity    # リザルト画面
│   └── SousaSetu.unity      # 操作説明画面
├── scripts/          # ゲームスクリプト
│   ├── Player/              # プレイヤー関連
│   ├── Enemy/               # 敵関連
│   ├── Manager/             # ゲーム管理
│   ├── UI/                  # UI関連
│   └── Effect/              # エフェクト関連
├── Prefabs/          # プレハブ
├── Materials/        # マテリアル
├── Resources/        # リソース
└── Plugins/          # プラグイン（DOTween等）
```

## ゲームシーン
- **TitleScene**: タイトル画面
- **Main**: メインゲームプレイシーン
- **ResultScene**: リザルト画面とランキング表示
- **SousaSetu**: 操作説明画面

## スクリプト概要
- `PlayerC.cs`: プレイヤーの移動と回転制御
- `Bullet.cs`: 弾の発射と制御
- `BalloonEnemy.cs`: 風船型敵の挙動
- `GameManager.cs`: ゲーム全体の管理
- `ScoreManager.cs`: スコアとランキングの管理（現在コメントアウト）
- `ResultManager.cs`: リザルト画面の管理

## ライセンス
このプロジェクトは教育目的で作成されました。

## クレジット
- 開発: seri0921
- 使用アセット: DOTween (Demigiant)

## 更新履歴
- 2025年最終版: デバッグ完了