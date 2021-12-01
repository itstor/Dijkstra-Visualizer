using UnityEngine;

public class AppSettings : MonoBehaviour {

    public static AppSettings Instance = null;
    public int targetFrameRate;                         

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;

        DontDestroyOnLoad(gameObject);
    }
}
