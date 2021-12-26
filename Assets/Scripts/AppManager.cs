using UnityEngine;
using System.Timers;
using System.Collections.Generic;
using System;

public class AppManager : MonoBehaviour
{

    public static AppManager Instance = null;
    private GameObject m_newNode = null;
    private NodeState m_selectedNodeState;
    private readonly Timer m_mouseClickTimer = new Timer();
    private NodeState m_newNodeState;
    [HideInInspector] public bool m_onSelectedChanged;
    [HideInInspector] public GameObject m_selectedStartNode = null;
    [HideInInspector] public GameObject m_selectedEndNode = null;
    private GameObject m_selectedNodeProperty;
    public GameObject m_selectedNode
    {
        get
        {
            return this.m_selectedNodeProperty;
        }

        set
        {
            if (this.m_selectedNodeProperty != value)
            {
                m_onSelectedChanged = true;
            }
            else
            {
                m_onSelectedChanged = false;
            }

            this.m_selectedNodeProperty = value;
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
        m_mouseClickTimer.Interval = 250;
        m_mouseClickTimer.Elapsed += singleClick;
    }

    void Update()
    {
        //When cursorstate equal to NodeState.Add. Create new node
        if (CursorStateManager.Instance.m_currentState == states.CursorState.Add)
        {
            CursorStateManager.Instance.m_currentState = states.CursorState.Select;
            GUIManager.Instance.showDialog(0, (string name, bool isInput, Vector2 position) =>
            {
                if (isInput)
                {
                    var newNode = ObjectFactory.Instance.createNode(name, position);
                    // newNode.GetComponent<Node>()
                    GUIManager.Instance.showToast($"Added {newNode.GetComponent<Node>().m_nodeName}", 2f);
                }
            });
        }

        //this code is for selecting node by double clicking on it
        else if (CursorStateManager.Instance.m_currentState == states.CursorState.Select)
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
                        if (m_selectedNode != null)
                        {
                            m_selectedNodeState.setIdle();
                            m_selectedNodeState.toggleForceGlow();
                        }

                        m_selectedNode = hit.collider.gameObject;
                        m_selectedNodeState = m_selectedNode.GetComponent<NodeState>();
                        m_selectedNodeState.setSelected();
                        m_selectedNodeState.toggleForceGlow();
                    }
                }
                else
                {
                    if (m_selectedNode != null)
                    {
                        m_selectedNodeState.setIdle();
                        m_selectedNodeState.toggleForceGlow();
                        m_selectedNode = null;
                    }
                }
            }, 0);
        }
        else if (CursorStateManager.Instance.m_currentState == states.CursorState.FindPath)
        {
            doubleClickEvent(() =>
            {
                //this is for double click event. selecting node
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit)
                {
                    if (m_selectedStartNode == null)
                    {
                        m_selectedStartNode = hit.collider.gameObject;
                        m_selectedStartNode.GetComponent<NodeState>().setStart();
                    }
                    else if (m_selectedEndNode == null)
                    {
                        m_selectedEndNode = hit.collider.gameObject;
                        m_selectedEndNode.GetComponent<NodeState>().setEnd();
                    }
                    else if (m_selectedStartNode != null && m_selectedEndNode != null)
                    {
                        m_selectedStartNode.GetComponent<NodeState>().setIdle();
                        m_selectedStartNode = null;
                        m_selectedEndNode.GetComponent<NodeState>().setIdle();
                        m_selectedEndNode = null;
                    }
                }
            }, () => { }, 0);
        }

        //this is for deleting node by pressing delete when node is on selected
        if (m_selectedNode != null)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Debug.Log(m_selectedNode.GetComponent<Node>().m_nodeName);
                GUIManager.Instance.showToast("Deleted " + m_selectedNode.GetComponent<Node>().m_nodeName, 2f);
                m_selectedNode.GetComponent<Node>().deleteNode();
                m_selectedNode = null;
            }
        }
    }

    void singleClick(object o, System.EventArgs e)
    {
        m_mouseClickTimer.Stop();
    }

    void doubleClickEvent(Action onSingleClick, Action onDoubleClick, int button_index)
    {
        if (Input.GetMouseButtonDown(button_index))
        {
            if (m_mouseClickTimer.Enabled == false)
            {
                onSingleClick();
                m_mouseClickTimer.Start();
                return;
            }
            //if timer already started and the interval time did not finish. stop it and execute the double click event
            else
            {
                Debug.Log("Double Click");
                onDoubleClick();
                m_mouseClickTimer.Stop();
            }
        }
    }
}