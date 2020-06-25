using UnityEngine;

public class Script : MonoBehaviour
{
    public static string appKey = "c888140d";

    void Start()
    {
        //Dynamic config example
        IronSourceConfig.Instance.setClientSideCallbacks(true);

        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(appKey);
    }
    
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
