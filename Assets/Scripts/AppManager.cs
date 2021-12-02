using UnityEngine;
using System.Timers;

public class AppManager : MonoBehaviour
{

    public static AppManager Instance = null;
    private GameObject m_SelectedNode;
    private NodeState m_SelectedNodeState;
    private readonly Timer m_MouseClickTimer = new Timer();
    private GameObject newNode = null;
    [SerializeField] private DialogGUI dialogGUI;

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

    void Start()
    {
        m_MouseClickTimer.Interval = 500;
        m_MouseClickTimer.Elapsed += singleClick;
    }

    void Update()
    {
        if (CursorStateManager.Instance.currentState == states.CursorState.Add)
        {
            if (newNode == null)
            {
                newNode = ObjectFactory.Instance.createNode(Utils.getMouseWorldPosition());
                newNode.GetComponent<NodeState>().onDragAdd();
            }

            if (newNode != null)
            {
                var mousePosition = Utils.getMouseWorldPosition();
                newNode.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

                if (Input.GetMouseButtonDown(0))
                {
                    CursorStateManager.Instance.currentState = states.CursorState.Select;
                    newNode.GetComponent<NodeState>().onIdle();
                    dialogGUI.displayDialog(0, (string name, GameObject node) =>
                    {
                        node.GetComponent<Node>().nodeName = name;
                    }, newNode);
                    newNode = null;
                }
            }
        }

        else if (CursorStateManager.Instance.currentState == states.CursorState.Select)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_MouseClickTimer.Enabled == false)
                {
                    m_MouseClickTimer.Start();
                    return;
                }
                else
                {
                    m_MouseClickTimer.Stop();

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    if (hit)
                    {
                        Debug.Log("Hit " + hit.transform.gameObject.name);
                        if (hit.collider.gameObject.tag == "Node")
                        {
                            m_SelectedNode = hit.collider.gameObject;
                            m_SelectedNodeState = m_SelectedNode.GetComponent<NodeState>();
                            m_SelectedNodeState.onSelected();
                        }
                    }
                    else
                    {
                        if (m_SelectedNode != null)
                        {
                            m_SelectedNodeState.onIdle();
                        }
                        m_SelectedNode = null;
                    }
                }
            }
        }

        if (m_SelectedNode != null)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                m_SelectedNode.GetComponent<Node>().deleteNode();
                m_SelectedNode = null;
            }
        }
    }

    void singleClick(object o, System.EventArgs e)
    {
        m_MouseClickTimer.Stop();
    }
}