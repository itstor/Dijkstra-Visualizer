using System.Collections.Generic;
using UnityEngine;

public class Djikstra
{
    public static IEnumerator<WaitForSeconds> FindShortestPath(Dictionary<Node, (List<EdgeData>, List<EdgeData>)> graph, Node start, Node end, float steps_delay){
        if (start.m_connectedNodes.Count == 0){
            GUIManager.Instance.showToast("Can't find path!", 1.5f);
            PathfindingManager.Instance.m_TaskState = states.PFStates.Idle;

            yield break;
        }

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
            
            if (currentNode != end){
                currentNode.m_nodeState.toggleForceGlow();
                if (currentNode != start){
                    currentNode.m_nodeState.setAccessed();
                }
            }

            yield return new WaitForSeconds(steps_delay);

            if (currentNode == end){
                if (previousList[end] != null){
                    Stack<Node> path = new Stack<Node>();
                    Node currentTrack = currentNode;
                    int step = 1;

                    path.Push(currentTrack);
                    
                    while(currentTrack != start){
                        currentTrack = previousList[currentTrack];
                        path.Push(currentTrack);
                        
                        yield return null;
                    }

                    do {
                        Node node = path.Pop();
                        node.m_nodeState.showStep(step);
                        if (node != end) node.m_nodeState.setPath();
                        node.m_nodeState.toggleForceGlow();
                        step++;

                        yield return new WaitForSeconds(0.5f);
                    } while(path.Count > 0);

                    GUIManager.Instance.showToast("Shortest path found!", 1.5f);
                    GUIManager.Instance.showToast("Distance: " + distanceList[end], 1.5f);
                    PathfindingManager.Instance.m_results = distanceList[end].ToString();
                }
                else {
                    GUIManager.Instance.showToast("Path not found!", 1.5f);
                }

                PathfindingManager.Instance.m_TaskState = states.PFStates.Finished;

                yield break;
            }

            foreach(Node neighbour in currentNode.m_connectedNodes.Keys){
                float newDistance = distanceList[currentNode] + currentNode.m_connectedNodes[neighbour].m_distance;
                
                if (neighbour != end){
                    neighbour.m_nodeState.setNeighbour();
                }
                neighbour.m_nodeState.toggleForceGlow();

                yield return new WaitForSeconds(steps_delay);

                if(newDistance < distanceList[neighbour]){
                    distanceList[neighbour] = newDistance;
                    previousList[neighbour] = currentNode;
                }

                if (neighbour != end){
                    neighbour.m_nodeState.setNeighbour();
                }
                neighbour.m_nodeState.toggleForceGlow();
            }

            if (currentNode != end && currentNode != start){
                currentNode.m_nodeState.setVisited();
            }
            currentNode.m_nodeState.toggleForceGlow();

            yield return new WaitForSeconds(steps_delay);
        }

        yield break;
    }

    private static Node GetNodeWithLowestDistance(Dictionary<Node, float> distanceList, List<Node> unvisitedList){
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
}