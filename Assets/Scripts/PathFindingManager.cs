using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct DjikstraTask {
    public Node start;
    public Node end;
    public float stepsDelay;
    public Dictionary<Node, (List<EdgeData>, List<EdgeData>)> graph;

    public DjikstraTask(Node start, Node end, float stepsDelay, Dictionary<Node, (List<EdgeData>, List<EdgeData>)> graph) {
        this.start = start;
        this.end = end;
        this.stepsDelay = stepsDelay;
        this.graph = graph;
    }
}

public class PathFindingManager : MonoBehaviour {
    
    public static PathFindingManager Instance;
    
    public IEnumerator m_Coroutine;

    public Djikstra m_Djikstra = new Djikstra();
    
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

    public void registerTask(DjikstraTask task)
    {
        m_Coroutine = m_Djikstra.FindShortestPath(task.graph, task.start, task.end, task.stepsDelay);
    }

    public void start(){
        StartCoroutine(m_Coroutine);
        m_Djikstra.m_isRunning = true;
        m_Djikstra.m_isFinished = false;
    }

    public void stop(){
        StopCoroutine(m_Coroutine);
        m_Djikstra.m_isRunning = false;
    }

    public bool isRunning(){
        return m_Djikstra.m_isRunning;
    }

    public bool isFinished(){
        return m_Djikstra.m_isFinished;
    }
}