using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public GameManagerCanvas GM;

    public AudioSource BGAS, EffectAS, RunSound;
    public AudioClip HexgonEff;
    public AudioClip JoinWithTrap;
    public AudioClip BonusUp;
    public AudioClip JoinPuddle;
    public AudioClip Falling;
    public AudioClip Jump;

    private void Awake()
    {        
        RunSound.enabled = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RefreshSoundState()
    {

        if (GM.IsSound)
        {
            BGAS.UnPause();
        }
        else
        {
            BGAS.Pause();
        }
    }

    public void PlayCoinEffect()
    {
        if (GM.IsSound)
        {
            EffectAS.PlayOneShot(HexgonEff);
        }
    }

    public void PlayTrapEffect()
    {
        if (GM.IsSound)
        {
            EffectAS.PlayOneShot(JoinWithTrap);
        }
    }

    public void PlayPuddleEffect()
    {
        if (GM.IsSound)
        {
            EffectAS.PlayOneShot(JoinPuddle);
        }
    }

    public void PlayBonusEffect()
    {
        if (GM.IsSound)
        {
            EffectAS.PlayOneShot(BonusUp);
        }
    }

    public void PlayJumpEffect()
    {
        if (GM.IsSound)
        {
            EffectAS.PlayOneShot(Jump);
        }
    }

    public void PlayFallingEffect()
    {
        if (GM.IsSound)
        {
            EffectAS.PlayOneShot(Falling);
        }
    }

    public void StopPlayRun()
    {
        RunSound.Pause();
    }

    public void PlayRun()
    {
        RunSound.UnPause();
    }

    public void PlayBackGroundSound()
    {
        BGAS.UnPause();
    }

    public void StopBackGroundSound()
    {
        BGAS.Pause();
    }

    public void EnabledRun()
    {
        RunSound.enabled = GM.IsSound ? true : false;
    }
}
