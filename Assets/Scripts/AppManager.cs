using UnityEngine;

public class AppManager : MonoBehaviour {

    public static AppManager Instance = null;
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
