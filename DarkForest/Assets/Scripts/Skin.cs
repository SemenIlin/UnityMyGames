using UnityEngine;

public class Skin : MonoBehaviour
{
    public Animator Animator;

    public void ShowSkin()
    {
        gameObject.SetActive(true);
    }

    public void HideSkin()
    {
        gameObject.SetActive(false);
    }
}
