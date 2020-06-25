using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameManagerCanvas GM;
    public MainMenuController MMC;
    public PlayerMovement PM;

    public GameObject GamePlay;

    public void Pause()
    {
        gameObject.SetActive(true);
        PM.CanPlay = false;
        PM.Pause();
        GamePlay.SetActive(false);

        AudioManager.Instance.StopPlayRun();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        PM.CanPlay = true;
        PM.UnPause();
        GamePlay.SetActive(true);

        AudioManager.Instance.PlayRun();
        AudioManager.Instance.PlayBackGroundSound();
    }

    public void MenuBtn()
    {
        PM.UnPause();
        PM.PUController.ResetAllPowerUps();

        gameObject.SetActive(false);
        MMC.OpenMenu();

        AudioManager.Instance.StopPlayRun();
    }
}
