using UnityEngine;

public class NodeTextController : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshPro m_nameTextMesh;
    [SerializeField] private TMPro.TextMeshPro m_stepTextMesh;
    [SerializeField] private float m_fadeSpeed;
    void Start()
    {
        m_nameTextMesh.text = GetComponent<Node>().m_nodeName;
    }

    void Update()
    {
        if (m_stepTextMesh.text != "")
        {
            m_stepTextMesh.color = Color.Lerp(m_stepTextMesh.color, Color.white, Time.deltaTime * m_fadeSpeed);
        }
        else
        {
            m_stepTextMesh.color = Color.Lerp(m_stepTextMesh.color, new Color(255, 255, 255, 0), Time.deltaTime * m_fadeSpeed);
        }
    }

    public void updateNodeName(string name)
    {
        m_nameTextMesh.text = name;
    }

    public void showNodeStep(int step)
    {
        m_stepTextMesh.text = step.ToString();
    }

    public void hideNodeStep()
    {
        m_stepTextMesh.text = "";
    }
}

