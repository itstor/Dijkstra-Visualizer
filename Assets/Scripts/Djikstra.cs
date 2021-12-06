using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Djikstra
{
    public bool m_isFinished = false;
    public bool m_isRunning = false;
    public IEnumerator<WaitForSeconds> FindShortestPath(Dictionary<Node, (List<EdgeData>, List<EdgeData>)> graph, Node start, Node end, float steps_delay){
        //Initialitation
        Dictionary<Node, float> distanceList = new Dictionary<Node, float>();
        Dictionary<Node, Node> previousList = new Dictionary<Node, Node>();
        List<Node> unvisitedList = new List<Node>();
        
        foreach(Node node in graph.Keys){
            distanceList.Add(node, float.MaxValue);
            unvisitedList.Add(node);
            previousList.Add(node, null);
        }

        distanceList[start] = 0;

        while(unvisitedList.Count > 0){
            Node currentNode = GetNodeWithLowestDistance(distanceList, unvisitedList);
            unvisitedList.Remove(currentNode);
            if (currentNode != end && currentNode != start){
                currentNode.nodeState.setAccessed();
            }

            yield return new WaitForSeconds(steps_delay);

            if (currentNode == end){
                Node currentTrack = currentNode;
                currentTrack.nodeState.toggleForceGlow();

                if (previousList[currentNode] != null){
                    while(currentTrack != start){
                        currentTrack = previousList[currentTrack];
                        currentTrack.nodeState.setPath();
                        currentTrack.nodeState.toggleForceGlow();
                        
                        yield return new WaitForSeconds(0.5f);
                    }
                    GUIManager.Instance.showToast("Shortest path found!", 1.5f);
                    GUIManager.Instance.showToast("Distance: " + distanceList[end], 1.5f);
                    m_isFinished = true;
                    m_isRunning = false;
                    yield break;
                }
                
                else {
                    GUIManager.Instance.showToast("Path not found!", 1.5f);
                    m_isFinished = true;
                    m_isRunning = false;
                }
            }

            foreach(Node neighbour in currentNode.connectedNodes.Keys){
                float newDistance = distanceList[currentNode] + currentNode.connectedNodes[neighbour].distance;
                if(newDistance < distanceList[neighbour]){
                    distanceList[neighbour] = newDistance;
                    previousList[neighbour] = currentNode;
                }
            }

            if (currentNode != end && currentNode != start){
                currentNode.nodeState.setVisited();
            }

            yield return new WaitForSeconds(steps_delay);
        }

        yield break;
    }

    private Node GetNodeWithLowestDistance(Dictionary<Node, float> distanceList, List<Node> unvisitedList){
        Node currentNode = unvisitedList[0];
        float currentDistance = distanceList[currentNode];

        for(int i = 1; i < unvisitedList.Count; i++){
            if(distanceList[unvisitedList[i]] < currentDistance){
                currentNode = unvisitedList[i];
                currentDistance = distanceList[currentNode];
            }
        }

        return currentNode;
    }

    public void Reset(){
        m_isFinished = false;
        m_isRunning = false;
    }
}