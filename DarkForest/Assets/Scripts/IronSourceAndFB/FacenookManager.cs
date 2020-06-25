using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;

public class FacenookManager : MonoBehaviour
{
    public static FacenookManager Istance;
    void Awake()
    {
        Istance = this;

        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    Debug.LogError("Couldn't initialize");
                }            
            },
            isGameShow =>
            {
                if (!isGameShow)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            });
        }
        else
        {
            FB.ActivateApp();
        }
        DontDestroyOnLoad(this);
    }

    #region Login / Logout
    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
    }
    public void FacebookLogout()
    {
        FB.LogOut();
    }
    #endregion

    public void ShowAdQuantity(int quantity)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams["Count ShowAd"] = quantity.ToString();
        
        FB.LogAppEvent(
            "QuantityShowAd",
            parameters: tutParams
        );
    }
}
