using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    public GameObject fScore;
    public GameObject sScore;
    public GameObject tScore;
    public GameObject grade;
    public GameObject item;

    private int score = 0;
    private TMP_Text firstScore;
    private TMP_Text secondScore;
    private TMP_Text thirdScore;
    private TMP_Text yourScore;
    private TMP_Text itemScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstScore = fScore.GetComponent<TMP_Text>();
        secondScore = sScore.GetComponent<TMP_Text>();
        thirdScore = tScore.GetComponent<TMP_Text>();
        yourScore = grade.GetComponent<TMP_Text>();
        itemScore = item.GetComponent<TMP_Text>();

        score += KillScore.Combo * 200;
        score += +KillScore.timeScore * 10;
        if (KillScore.first < score)
        {
            KillScore.third = KillScore.second;
            KillScore.second = KillScore.first;
            KillScore.first = score;
        }
        else if (KillScore.second < score)
        {
            KillScore.third = KillScore.second;
            KillScore.second = score;
        }
        else if (KillScore.third < score)
        {
            KillScore.third = score;
        }

    }

    // Update is called once per frame
    void Update(){
        yourScore.text = string.Format("{0:000000}", score);
        itemScore.text = "Combo " + KillScore.Combo + "× 200 = " + KillScore.Combo*200;
        itemScore.text += "\nTime " + KillScore.timeScore + "× 10 = " + KillScore.timeScore * 10;
        firstScore.text = "First: " + KillScore.first.ToString();
        secondScore.text = "Second: " + KillScore.second.ToString();
        thirdScore.text = "Third: " + KillScore.third.ToString();
    }

    public void OnGoToTitleButton()
    {
        SceneManager.LoadScene("TestTitleScene"); // "TitleScene" は実際のタイトルシーン名に
    }
}
