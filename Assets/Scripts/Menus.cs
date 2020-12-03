using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public CanvasGroup GameOver;
    public CanvasGroup ButtonTryAgain;

    public CanvasGroup PausePanel;
    public CanvasGroup PauseGameImage;
    public CanvasGroup MenuButton;
    public CanvasGroup ContinueButton;
    public CanvasGroup ExitButton;

    private bool canPause = true;
    private void Start()
    {
        Player.OnPlayerDeath += PlayerIsDead;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canPause)
            {
                StartCoroutine(ShowPauseGameMenu());
            }
            else
            {
                ContinueGame();
            }
        }
    }
    public void PlayerIsDead()
    {
        StartCoroutine(ShowGameOverMenu());
    }

    IEnumerator ShowGameOverMenu()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.alphaCanvas(GameOver, 1, 2f);
        yield return new WaitForSeconds(2f);
        ButtonTryAgain.gameObject.SetActive(true);
        LeanTween.alphaCanvas(ButtonTryAgain, 1, 1f);
    }

    IEnumerator ShowPauseGameMenu()
    {
        Time.timeScale = 0;
        canPause = false;
        MenuButton.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        LeanTween.alphaCanvas(PausePanel, 1f, 1f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(PauseGameImage, 1f, 1f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(MenuButton, 1f, 1f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(ContinueButton, 1f, 1f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(ExitButton, 1f, 1f).setIgnoreTimeScale(true);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator HidePauseGameMenu()
    {
        LeanTween.alphaCanvas(PausePanel, 0f, 1f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(PauseGameImage, 0f, 1f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(MenuButton, 0f, 1f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(ContinueButton, 0f, 1f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(ExitButton, 0f, 1f).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        MenuButton.gameObject.SetActive(false);
        ContinueButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        
        canPause = true;
    }

    public void ContinueGame()
    {
        StartCoroutine(HidePauseGameMenu());
    }

    public void GoBackMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Retry()
    {
        SceneManager.LoadScene("PlayScene");
    }

    private void OnDestroy()
    {
        Player.OnPlayerDeath -= PlayerIsDead;
    }
}
