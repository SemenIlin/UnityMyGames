using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUpController : MonoBehaviour
{
    public GameManagerCanvas GM;
    public PlayerMovement PM;

    public GameObject PowerUpPref;
    public Transform PowerUpObj;

    public struct PowerUp
    {
        public enum Type
        {
            MULTIPLIER,
            IMMORALITY,
            COINS_SPWN
        }

        public Type PowerUpType;
        public float Duration;        
    }

    public delegate void OnCoinsPowerUp(bool activate);
    public static event OnCoinsPowerUp CoinsPowerUpEvents;

    PowerUp[] powerUps = new PowerUp[3];
    Coroutine[] powerUpsCors = new Coroutine[3];
    List<PowerUpScript> powerUpScripts = new List<PowerUpScript>();
    void Start()
    {
        powerUps[0] = new PowerUp() { PowerUpType = PowerUp.Type.MULTIPLIER, Duration = 12 };
        powerUps[1] = new PowerUp() { PowerUpType = PowerUp.Type.IMMORALITY, Duration = 12 };
        powerUps[2] = new PowerUp() { PowerUpType = PowerUp.Type.COINS_SPWN, Duration = 12 };
    }

    public void PowerUpUse(PowerUp.Type type)
    {
        PowerUpReset(type);
        powerUpsCors[(int)type] = StartCoroutine(PowerUpCor(type, CreatePowerUpPref(type)));

        switch (type)
        {
            case PowerUp.Type.MULTIPLIER:
                GM.PowerUpMultiplier = 2;
                break;
            case PowerUp.Type.IMMORALITY:
                PM.IsImmortal = true;
                break;
            case PowerUp.Type.COINS_SPWN:
                CoinsPowerUpEvents?.Invoke(true);
                break;
        }
    }

    private void PowerUpReset(PowerUp.Type type)
    {
        if (powerUpsCors[(int)type] != null)
        {
            StopCoroutine(powerUpsCors[(int)type]);
            var powerUp = powerUpScripts.FirstOrDefault(numberOfType => numberOfType.NumberOfTypePowerUp == (int)type);
            if (powerUp != null)
            {
                powerUp.Destroy();
                powerUpScripts.Remove(powerUp);
            }
        }
        else
        {
            return;
        }

        powerUpsCors[(int)type] = null;

        switch (type)
        {
            case PowerUp.Type.MULTIPLIER:
                GM.PowerUpMultiplier = 1;
                break;
            case PowerUp.Type.IMMORALITY:
                PM.IsImmortal = false;
                break;
            case PowerUp.Type.COINS_SPWN:
                CoinsPowerUpEvents?.Invoke(false);
                break;
        }
    }

    public void ResetAllPowerUps()
    {
        for(int i = 0; i< powerUps.Length; ++i)
        {
            PowerUpReset(powerUps[i].PowerUpType);
        }

        foreach(var powerUp in powerUpScripts)
        {
            powerUp.Destroy();
        }

        powerUpScripts.Clear();
    }

    IEnumerator PowerUpCor(PowerUp.Type type, PowerUpScript powerUpPref)
    {
        float duration = powerUps[(int)type].Duration;
        float currentDuration = duration;

        while(currentDuration > 0)
        {
            powerUpPref.SetProgress(currentDuration / duration);
            if (PM.CanPlay)
            {
                currentDuration -= Time.deltaTime;
            }
            yield return null;
        }

        powerUpScripts.Remove(powerUpPref);
        powerUpPref.Destroy();

        PowerUpReset(type);
    }

    PowerUpScript CreatePowerUpPref(PowerUp.Type type)
    {
        GameObject go = Instantiate(PowerUpPref, PowerUpObj, false);

        var ps = go.GetComponent<PowerUpScript>();
        powerUpScripts.Add(ps);
        ps.SetData(type);
        return ps;
    }
}
