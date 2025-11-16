using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName = "Main";

    [SerializeField] private AudioClip onSound;

    private AudioSource audioSource;

    private bool isLoding = false;

    private Button button;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        audioSource = GetComponent<AudioSource>();

        button = GetComponent<Button>();
    }

    public void LoadSceneOnClick()
    {
        if(isLoding)
        {
            return;
        }

        isLoding = true;

        if (button != null)
        {
            button.interactable = false;
        }

        StartCoroutine(LoadSceneWithSound());
    }
    private IEnumerator LoadSceneWithSound()
    {
        audioSource.PlayOneShot(onSound);

        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene(sceneName);
    }
}

