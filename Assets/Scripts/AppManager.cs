using UnityEngine;
using System.Timers;
using System.Collections.Generic;
using System;

public class AppManager : MonoBehaviour
{

    public static AppManager Instance = null;
    private GameObject m_newNode = null;
    private NodeState m_SelectedNodeState;
    private readonly Timer m_MouseClickTimer = new Timer();
    private NodeState m_newNodeState;
    public bool onSelectedChanged;
    public GameObject m_SelectedStartNode = null;
    public GameObject m_SelectedEndNode = null;
    private GameObject m_SelectedNodeProperty;
    public GameObject m_SelectedNode
    {
        get
        {
            return this.m_SelectedNodeProperty;
        }

        set
        {
            if (this.m_SelectedNodeProperty != value)
            {
                onSelectedChanged = true;
            }
            else
            {
                onSelectedChanged = false;
            }

            this.m_SelectedNodeProperty = value;
        }
    }

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
        m_MouseClickTimer.Interval = 250;
        m_MouseClickTimer.Elapsed += singleClick;
    }

    void Update()
    {
        //When cursorstate equal to NodeState.Add. Create new node
        if (CursorStateManager.Instance.currentState == states.CursorState.Add)
        {
            //create new node when m_newnode is null
            if (m_newNode == null)
            {
                m_newNode = ObjectFactory.Instance.createNode(Utils.getMouseWorldPosition());
                m_newNodeState = m_newNode.GetComponent<NodeState>();
                m_newNodeState.setDragAdd();
                m_newNodeState.toggleForceGlow();
            }

            //when new node is not null. drag it
            if (m_newNode != null)
            {
                var mousePosition = Utils.getMouseWorldPosition();
                m_newNode.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

                //you can cancel adding node by pressing escape
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Destroy(m_newNode);
                    CursorStateManager.Instance.currentState = states.CursorState.Select;
                    m_newNode = null;
                }

                //when mouse is clicked. place node, change states to select and show dialog window
                else if (Input.GetMouseButtonDown(0))
                {
                    CursorStateManager.Instance.currentState = states.CursorState.Select;
                    GUIManager.Instance.showDialog(0, (string name, bool isInput, Dictionary<string, dynamic> Object) =>
                    {
                        if (isInput)
                        {
                            Object["m_newNode"].GetComponent<Node>().nodeName = name;
                            Object["m_newNodeState"].toggleForceGlow();
                            Object["m_newNodeState"].setIdle();
                            GUIManager.Instance.showToast($"Added {Object["m_newNode"].GetComponent<Node>().nodeName}", 2f);
                        }
                        else
                        {
                            Destroy(Object["m_newNode"]);
                        }
                    }, new Dictionary<string, dynamic>
                    {
                        ["m_newNode"] = m_newNode,
                        ["m_newNodeState"] = m_newNodeState
                    });

                    m_newNode = null;
                }
            }
        }

        //this code is for selecting node by double clicking on it
        else if (CursorStateManager.Instance.currentState == states.CursorState.Select)
        {
            doubleClickEvent(() => { }, () =>
            {
                    //this is for double click event. selecting node
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit)
                {
                    if (hit.collider.gameObject.tag == "Node")
                    {
                        if (m_SelectedNode != null)
                        {
                            m_SelectedNodeState.setIdle();
                            m_SelectedNodeState.toggleForceGlow();
                        }

                        m_SelectedNode = hit.collider.gameObject;
                        m_SelectedNodeState = m_SelectedNode.GetComponent<NodeState>();
                        m_SelectedNodeState.setSelected();
                        m_SelectedNodeState.toggleForceGlow();
                    }
                }
                else
                {
                    if (m_SelectedNode != null)
                    {
                        m_SelectedNodeState.setIdle();
                        m_SelectedNodeState.toggleForceGlow();
                        m_SelectedNode = null;
                    }
                }
            }, 0);
        }
        else if (CursorStateManager.Instance.currentState == states.CursorState.FindPath)
        {
            doubleClickEvent(() =>
            {
                //this is for double click event. selecting node
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit)
                {
                    if (hit.collider.gameObject.tag == "Node")
                    {
                        if (m_SelectedStartNode == null)
                        {
                            m_SelectedStartNode = hit.collider.gameObject;
                            m_SelectedStartNode.GetComponent<NodeState>().setStart();
                            Debug.Log("Start Node Selected");
                        }
                        else if (m_SelectedEndNode == null)
                        {
                            m_SelectedEndNode = hit.collider.gameObject;
                            m_SelectedEndNode.GetComponent<NodeState>().setEnd();
                            Debug.Log("End Node Selected");
                        }
                    }
                }
            }, () => {}, 0);
        }

        //this is for deleting node by pressing delete when node is on selected
        if (m_SelectedNode != null)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Debug.Log(m_SelectedNode.GetComponent<Node>().nodeName);
                GUIManager.Instance.showToast("Deleted " + m_SelectedNode.GetComponent<Node>().nodeName, 2f);
                m_SelectedNode.GetComponent<Node>().deleteNode();
                m_SelectedNode = null;
            }
        }
    }

    void singleClick(object o, System.EventArgs e)
    {
        m_MouseClickTimer.Stop();
    }

    void doubleClickEvent(Action onSingleClick, Action onDoubleClick, int button_index)
    {
        if (Input.GetMouseButtonDown(button_index))
        {
            if (m_MouseClickTimer.Enabled == false)
            {
                onSingleClick();
                m_MouseClickTimer.Start();
                return;
            }
            //if timer already started and the interval time did not finish. stop it and execute the double click event
            else
            {
                Debug.Log("Double Click");
                onDoubleClick();
                m_MouseClickTimer.Stop();
            }
        }
    }
}