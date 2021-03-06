using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    public static ObjectFactory Instance = null;
    [SerializeField] private GameObject m_edgeLinePrefab;
    [SerializeField] private GameObject m_nodePrefab;

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

    public GameObject createEdgeLine(Vector3 position)
    {
        var edgeLine = Instantiate(m_edgeLinePrefab, position, Quaternion.identity);

        edgeLine.SetActive(false);

        edgeLine.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
            new Vector3(position.x, position.y, 0),
            new Vector3(position.x, position.y, 0)
        });

        edgeLine.transform.position = position;

        return edgeLine;
    }

    public GameObject createNode(Vector3 position)
    {
        return Instantiate(m_nodePrefab, position, Quaternion.identity);
    }
}