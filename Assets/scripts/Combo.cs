using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    // コンボ数を表示する
    [SerializeField] CanvasGroup grCombo;
    [SerializeField] TextMeshProUGUI comboText;

    private bool isCombo; // コンボ数が表示されたか

    public static Combo Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // 最初は非表示にしておく
        grCombo.alpha = 0f;
        isCombo = false;
    }


    public void ShowCombo(int currentCombo)
    {
        // コンボ数をセットして表示
        comboText.SetText("{0}", currentCombo);
        grCombo.alpha = 1f;

        isCombo = true;
    }

    // Set_Bulletから呼び出す関数 (コンボを非表示)
    public void HideCombo()
    {
        comboText.SetText("0"); // テキストも0に戻す
        grCombo.alpha = 0f;
        isCombo = false;
    }

}
