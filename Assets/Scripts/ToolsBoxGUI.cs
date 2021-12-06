using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolsBoxGUI : MonoBehaviour
{
    [SerializeField] private GameObject m_NodeDetailSection;
    [SerializeField] private TMPro.TextMeshProUGUI m_NodeNameText;
    [SerializeField] private GameObject m_ConnectedNodesItemContainer;
    [SerializeField] private GameObject m_ConnectedNodesTextPrefab;

    [SerializeField] private GameObject m_FindPathSection;
    [SerializeField] private TMPro.TextMeshProUGUI m_NodeFromText;
    [SerializeField] private TMPro.TextMeshProUGUI m_NodeToText;
    [SerializeField] private TMPro.TMP_InputField m_StepsDelayInputField;
    [SerializeField] private GameObject m_StartButtonObject;
    [SerializeField] private GameObject m_StopButtonObject;
    [SerializeField] private GameObject m_ResetButtonObject;
    [SerializeField] private Button m_StartButton;

    void Update()
    {
        if (CursorStateManager.Instance.currentState == states.CursorState.Select
            && AppManager.Instance.m_SelectedNode != null)
        {
            if (AppManager.Instance.onSelectedChanged)
            {
                resetContentItems();
                var selectedNode = AppManager.Instance.m_SelectedNode.GetComponent<Node>();
                foreach (Node node in selectedNode.connectedNodes.Keys)
                {
                    string nodeName = node.nodeName;
                    GameObject nodeNameContainer = Instantiate(m_ConnectedNodesTextPrefab, m_ConnectedNodesItemContainer.transform);
                    var childrenText = nodeNameContainer.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

                    childrenText[0].text = nodeName;
                    childrenText[1].text = selectedNode.connectedNodes[node].distance.ToString();
                    nodeNameContainer.transform.SetParent(m_ConnectedNodesItemContainer.transform);
                }
                AppManager.Instance.onSelectedChanged = false;
            }

            m_NodeDetailSection.SetActive(true);
            m_NodeNameText.text = AppManager.Instance.m_SelectedNode.GetComponent<Node>().nodeName;
        }
        else
        {
            m_NodeDetailSection.SetActive(false);
        }

        if (CursorStateManager.Instance.currentState == states.CursorState.FindPath)
        {
            try {
                m_NodeFromText.text = AppManager.Instance.m_SelectedStartNode.GetComponent<Node>().nodeName;
                m_NodeToText.text = AppManager.Instance.m_SelectedEndNode.GetComponent<Node>().nodeName;
            }
            catch {
                m_NodeFromText.text = "";
                m_NodeToText.text = "";
            }
            m_FindPathSection.SetActive(true);

            if (PathFindingManager.Instance.isRunning()){
                m_StartButtonObject.SetActive(false);
                m_ResetButtonObject.SetActive(false);
                m_StopButtonObject.SetActive(true);
            }
            else if (PathFindingManager.Instance.isFinished()){
                m_StartButtonObject.SetActive(false);
                m_ResetButtonObject.SetActive(true);
                m_StopButtonObject.SetActive(false);
            }
            else{
                m_StartButtonObject.SetActive(true);
                m_StopButtonObject.SetActive(false);
                m_ResetButtonObject.SetActive(false);
            }

            
        }
        else
        {
            m_FindPathSection.SetActive(false);
        }
    }

    private void resetContentItems()
    {
        foreach (Transform child in m_ConnectedNodesItemContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void startButton(){
        float stepsDelay;
        if (m_StepsDelayInputField.text != ""){
            stepsDelay = float.Parse(m_StepsDelayInputField.text)/1000;
        }
        else {
            return;
        }
        var selectedStartNode = AppManager.Instance.m_SelectedStartNode.GetComponent<Node>();
        var selectedEndNode = AppManager.Instance.m_SelectedEndNode.GetComponent<Node>();
        var graphContainer = GraphManager.Instance.mContainer;
        
        PathFindingManager.Instance.registerTask(new DjikstraTask(selectedStartNode, selectedEndNode, stepsDelay, graphContainer));

        PathFindingManager.Instance.start();
    }

    public void stopButton(){
        PathFindingManager.Instance.stop();
    }

    public void resetButton(){
        foreach(Node node in GraphManager.Instance.mContainer.Keys){
            node.GetComponent<NodeState>().reset();
        }

        AppManager.Instance.m_SelectedStartNode = null;
        AppManager.Instance.m_SelectedEndNode = null;
        PathFindingManager.Instance.m_Djikstra.Reset();

    }
}