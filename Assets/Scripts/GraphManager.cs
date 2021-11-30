using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManager: MonoBehaviour
{
    public static GraphManager Instance = null;

    // Node : (List of incoming edges, List of outgoing edges)
    public Dictionary<Node, (List<EdgeData>, List<EdgeData>)> mContainer = new Dictionary<Node, (List<EdgeData>, List<EdgeData>)>();                         

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

    public void addEdgeLine(Node from, Node to, EdgeData edge_data)
    {
        if (!mContainer.ContainsKey(from))
        {
            mContainer.Add(from, (new List<EdgeData>(), new List<EdgeData>()));
        }

        if (!mContainer.ContainsKey(to))
        {
            mContainer.Add(to, (new List<EdgeData>(), new List<EdgeData>()));
        }

        mContainer[from].Item1.Add(edge_data);
        mContainer[to].Item2.Add(edge_data);
    }

    public void deleteNode(Node node){
        if (mContainer.ContainsKey(node))
        {
            foreach (EdgeData line in mContainer[node].Item1)
            {
                Destroy(line.gameObject);
            }

            foreach (EdgeData line in mContainer[node].Item2)
            {
                Destroy(line.gameObject);
            }

            mContainer.Remove(node);
        }
    }

    public (List<EdgeData>, List<EdgeData>) getEdgeList(Node node){
        if (mContainer.ContainsKey(node))
        {
            return mContainer[node];
        }

        return (new List<EdgeData>(), new List<EdgeData>());
    }
}

