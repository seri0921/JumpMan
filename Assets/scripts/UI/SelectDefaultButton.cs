using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectDefaultButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button defaultButton;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
    }
}
