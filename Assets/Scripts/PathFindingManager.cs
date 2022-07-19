using UnityEngine;
using System.Collections;

public class PathfindingManager : MonoBehaviour
{

    public static PathfindingManager Instance;
    public IEnumerator m_Coroutine;
    public states.PFStates m_TaskState = states.PFStates.Idle;
    public string m_results = "";
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

        DontDestroyOnLoad(gameObject);
    }

    public void registerTask(IEnumerator task)
    {
        m_Coroutine = task;
    }

    public void start()
    {
        PathfindingManager.Instance.m_TaskState = states.PFStates.Running;

        StartCoroutine(m_Coroutine);
    }

    public void stop()
    {
        m_TaskState = states.PFStates.Stopped;
        StopCoroutine(m_Coroutine);
    }
}