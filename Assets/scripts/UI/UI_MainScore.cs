using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_MainScore : MonoBehaviour
{
    public GameObject scoreText;
    private TMP_Text Text;
    void Start()
    {
        Text = scoreText.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = string.Format("{0:000000}", KillScore.ComboScore);
    }
}
