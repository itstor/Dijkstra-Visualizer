using UnityEngine;

public class EdgeLineFactory: MonoBehaviour
{
    public static EdgeLineFactory Instance = null;
    public GameObject edgeLinePrefab;

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

    public GameObject createEdgeLine(Vector3 position){
        var edgeLine = Instantiate(edgeLinePrefab, position, Quaternion.identity);
        
        edgeLine.transform.position = position;

        return edgeLine;
    }
}