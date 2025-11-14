using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [HideInInspector] public EnemyManagerTest spawner;
    [HideInInspector] public Transform playerPos;
    [HideInInspector] public GameObject DieText;
    [HideInInspector] public Transform uiCanvas;
    protected Camera mainCamera;
    protected virtual void Awake()
    {
        mainCamera = Camera.main;
    }

    public void ShowScorePopup(string textToShow)
    {
        // 必要な参照がセットされていなければ何もしない
        if (DieText == null || uiCanvas == null || mainCamera == null)
        {
            Debug.LogWarning("Score Popup 実行に必要な参照がセットされていません。", this);
            return;
        }

        // 1. Canvas の子として生成
        GameObject newEffect = Instantiate(DieText, uiCanvas);

        // 2. 敵のワールド座標をスクリーン座標に変換
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);

        // 3. UIの位置をスクリーン座標に設定
        newEffect.GetComponent<RectTransform>().position = screenPos;

        // 4. テキストを設定 (RareEnemyDied スクリプト を想定)
        RareEnemyDied diedScript = newEffect.GetComponent<RareEnemyDied>();
        if (diedScript != null)
        {
            diedScript.SetText(textToShow); //
        }
        else
        {
            Debug.LogError("演出プレハブに RareEnemyDied スクリプト がありません！", newEffect);
        }
    }

}
