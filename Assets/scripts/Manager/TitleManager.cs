using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction normalAttack;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        normalAttack = playerInput.actions["normalAt"];
    }
    public void OnGameReStartButton()
    {
        // ゲーム開始時に現在のスコアを0にリセット
        //ScoreManager.Instance.ResetCurrentScore();
        if (normalAttack.WasPerformedThisFrame())
        {
            // ゲームシーンに遷移
            SceneManager.LoadScene("TestTitleScene"); // "GameScene" は実際のゲームシーン名に置き換えてください
        }
    }
}