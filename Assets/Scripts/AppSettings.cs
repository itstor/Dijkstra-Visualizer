using UnityEngine;

public class AppSettings : MonoBehaviour {

    public static AppSettings Instance = null;
    public int m_targetFrameRate;                         

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
        Application.targetFrameRate = m_targetFrameRate;

        DontDestroyOnLoad(gameObject);
    }
}
