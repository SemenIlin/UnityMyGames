using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public PauseMenuController PMC;
    public GameObject BackGround;
    public GameManagerCanvas GM;
    public Sprite SoundOn, SoundOff;
    public Image SoundBtnImage;

    public void PlayBtn()
    {
        PMC.GamePlay.SetActive(true);
        BackGround.SetActive(true);
        gameObject.SetActive(false);
        GM.StartGame();
        AudioManager.Instance.EnabledRun();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        BackGround.SetActive(false);
        gameObject.SetActive(true);
    }

    public void SoundBtn()
    {
        GM.IsSound = !GM.IsSound;
        SoundBtnImage.sprite = GM.IsSound ? SoundOn : SoundOff;
        AudioManager.Instance.RefreshSoundState();
    }
}
