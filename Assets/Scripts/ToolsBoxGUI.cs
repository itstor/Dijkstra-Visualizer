using UnityEngine;
using System;

public class ToolsBoxGUI : MonoBehaviour
{
    [SerializeField] private GameObject m_nodeDetailSection;
    [SerializeField] private TMPro.TextMeshProUGUI m_nodeNameText;
    [SerializeField] private GameObject m_connectedNodesItemContainer;
    [SerializeField] private GameObject m_connectedNodesTextPrefab;

    [SerializeField] private GameObject m_findPathSection;
    [SerializeField] private TMPro.TextMeshProUGUI m_nodeFromText;
    [SerializeField] private TMPro.TextMeshProUGUI m_nodeToText;
    [SerializeField] private TMPro.TextMeshProUGUI m_pathDistanceText;
    [SerializeField] private TMPro.TMP_InputField m_stepsDelayInputField;
    [SerializeField] private GameObject m_startButtonObject;
    [SerializeField] private GameObject m_stopButtonObject;
    [SerializeField] private GameObject m_resetButtonObject;

    void Update()
    {
        if (CursorStateManager.Instance.m_currentState == states.CursorState.Select
            && AppManager.Instance.m_selectedNode != null)
        {
            if (AppManager.Instance.m_onSelectedChanged)
            {
                resetContentItems();
                var selectedNode = AppManager.Instance.m_selectedNode.GetComponent<Node>();
                foreach (Node node in selectedNode.m_connectedNodes.Keys)
                {
                    string nodeName = node.m_nodeName;
                    GameObject nodeNameContainer = Instantiate(m_connectedNodesTextPrefab, m_connectedNodesItemContainer.transform);
                    var childrenText = nodeNameContainer.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

                    childrenText[0].text = nodeName;
                    childrenText[1].text = selectedNode.m_connectedNodes[node].m_distance.ToString();
                    nodeNameContainer.transform.SetParent(m_connectedNodesItemContainer.transform);
                }
                AppManager.Instance.m_onSelectedChanged = false;
            }

            m_nodeDetailSection.SetActive(true);
            m_nodeNameText.text = AppManager.Instance.m_selectedNode.GetComponent<Node>().m_nodeName;
        }
        else
        {
            m_nodeDetailSection.SetActive(false);
        }

        if (CursorStateManager.Instance.m_currentState == states.CursorState.FindPath)
        {
            m_findPathSection.SetActive(true);

            try
            {
                m_nodeFromText.text = AppManager.Instance.m_selectedStartNode.GetComponent<Node>().m_nodeName;
                m_nodeToText.text = AppManager.Instance.m_selectedEndNode.GetComponent<Node>().m_nodeName;
            }
            catch
            {
                m_nodeFromText.text = "";
                m_nodeToText.text = "";
            }

            m_pathDistanceText.text = PathfindingManager.Instance.m_results;

            switch (PathfindingManager.Instance.m_TaskState)
            {
                case states.PFStates.Idle: showStartButton(); break;
                case states.PFStates.Running: showStopButton(); break;
                case states.PFStates.Finished: case states.PFStates.Stopped: showResetButton(); break;
                default: break;
            }

        }
        else
        {
            m_findPathSection.SetActive(false);
        }
    }

    private void resetContentItems()
    {
        foreach (Transform child in m_connectedNodesItemContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void startButton()
    {
        float stepsDelay;
        var graphContainer = GraphManager.Instance.m_graphContainer;
        Node selectedStartNode = null;
        Node selectedEndNode = null;

        try
        {
            selectedStartNode = AppManager.Instance.m_selectedStartNode.GetComponent<Node>();
            selectedEndNode = AppManager.Instance.m_selectedEndNode.GetComponent<Node>();
        }
        catch
        {
            GUIManager.Instance.showToast("Select a start and end node", 2f);
            return;
        }


        if (!string.IsNullOrEmpty(m_stepsDelayInputField.text))
        {
            stepsDelay = float.Parse(m_stepsDelayInputField.text) / 1000f;
        }
        else
        {
            GUIManager.Instance.showToast("Please enter a valid delay", 2f);
            return;
        }

        PathfindingManager.Instance.registerTask(Djikstra.FindShortestPath(graphContainer, selectedStartNode, selectedEndNode, stepsDelay));

        PathfindingManager.Instance.start();
    }

    public void stopButton()
    {
        PathfindingManager.Instance.stop();
    }

    public void resetButton()
    {
        foreach (Node node in GraphManager.Instance.m_graphContainer.Keys)
        {
            node.m_nodeState.reset();
            node.m_nodeState.hideStep();
        }

        PathfindingManager.Instance.m_TaskState = states.PFStates.Idle;
        PathfindingManager.Instance.m_Coroutine = null;
        PathfindingManager.Instance.m_results = "";
        AppManager.Instance.m_selectedStartNode = null;
        AppManager.Instance.m_selectedEndNode = null;
    }

    private void showStartButton()
    {
        m_startButtonObject.SetActive(true);
        m_resetButtonObject.SetActive(false);
        m_stopButtonObject.SetActive(false);
    }

    private void showStopButton()
    {
        m_startButtonObject.SetActive(false);
        m_resetButtonObject.SetActive(false);
        m_stopButtonObject.SetActive(true);
    }

    private void showResetButton()
    {
        m_startButtonObject.SetActive(false);
        m_resetButtonObject.SetActive(true);
        m_stopButtonObject.SetActive(false);
    }
}