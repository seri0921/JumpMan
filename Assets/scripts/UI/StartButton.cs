using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(onClick);

    }

    // Update is called once per frame
    void onClick()
    {
        SceneManager.LoadScene("Main");
    }
}
