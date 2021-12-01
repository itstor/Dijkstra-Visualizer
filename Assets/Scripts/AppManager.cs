using UnityEngine;
using System.Collections;

public class AppManager : MonoBehaviour
{

    public static AppManager Instance = null;
    public GameObject m_SelectedNode;
    private GameObject newNode = null;

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
                    newNode = null;
                }
            } 
        }
    }
}