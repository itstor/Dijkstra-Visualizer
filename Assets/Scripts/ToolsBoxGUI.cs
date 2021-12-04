using UnityEngine;

public class ToolsBoxGUI : MonoBehaviour
{
    [SerializeField] private GameObject m_NodeDetailSection;
    [SerializeField] private TMPro.TextMeshProUGUI m_NodeNameText;
    [SerializeField] private GameObject m_ConnectedNodesItemContainer;
    [SerializeField] private GameObject m_ConnectedNodesTextPrefab;

    [SerializeField] private GameObject m_FindPathSection;

    bool m_AlreadyListed = false;
    private

    void Update()
    {
        if (CursorStateManager.Instance.currentState == states.CursorState.Select
            && AppManager.Instance.m_SelectedNode != null)
        {
            if (!m_AlreadyListed || AppManager.Instance.onSelectedChanged)
            {
                resetContentItems();
                foreach (Node node in AppManager.Instance.m_SelectedNode.GetComponent<Node>().connectedNodes.Keys)
                {
                    string nodeName = node.nodeName;
                    GameObject nodeNameContainer = Instantiate(m_ConnectedNodesTextPrefab, m_ConnectedNodesItemContainer.transform);
                    nodeNameContainer.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = nodeName;
                    nodeNameContainer.transform.SetParent(m_ConnectedNodesItemContainer.transform);
                }
                m_AlreadyListed = true;
                AppManager.Instance.onSelectedChanged = false;
            }

            m_NodeDetailSection.SetActive(true);
            m_NodeNameText.text = AppManager.Instance.m_SelectedNode.GetComponent<Node>().nodeName;
        }
        else
        {
            m_NodeDetailSection.SetActive(false);
            m_AlreadyListed = false;
        }

        if (CursorStateManager.Instance.currentState == states.CursorState.FindPath)
        {
            Debug.Log("Find Path");
            m_FindPathSection.SetActive(true);
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
}