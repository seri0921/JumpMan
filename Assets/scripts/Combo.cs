using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    // コンボ数を表示する
    [SerializeField] CanvasGroup grCombo;
    [SerializeField] TextMeshProUGUI comboText;

    private int combo;  // コンボ数

    private float elapsedTime; // 経過時間

    private float displayTime = 1.0f; // コンボ数を表示する時間

    private bool isCombo; // コンボ数が表示されたか

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // コンボ数が表示されたら計測開始
        if (isCombo == true)
        {
            elapsedTime += Time.deltaTime;
            // 一定時間経過したら
            if (elapsedTime >= displayTime);
            {
                // コンボ数を非表示
                grCombo.alpha = 0f;
                isCombo = false;
            }
        }

    }

    public void ShowCombo()
    {
        // コンボ数をセットして表示
        comboText.SetText("{0}", combo);
        grCombo.alpha = 1f;
        elapsedTime = 0;  // 経過時間
        isCombo = true;   // 計測開始
    }

}
