using UnityEngine;
using UnityEngine.UI;

public class PowerUpScript : MonoBehaviour
{
    public Image ProgressBar;
    public Color[] Colors;
    public int NumberOfTypePowerUp { get; private set; }

    public void SetData(PowerUpController.PowerUp.Type type)
    {
        ProgressBar.color = Colors[(int)type];
        NumberOfTypePowerUp = (int)type;
    }

    public void SetProgress(float progress)
    {
        ProgressBar.fillAmount = progress;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
