using System.Collections.Generic;
using System.Linq;

public class Djikstra
{
    static public List<Node> FindShortestPath(Dictionary<Node, (List<EdgeData>, List<EdgeData>)> graph, Node start, Node end){
        //Initialitation
        Dictionary<Node, float> distanceList = new Dictionary<Node, float>();
        Dictionary<Node, List<Node>> previousList = new Dictionary<Node, List<Node>>();
        List<Node> unvisitedList = new List<Node>();
        
        foreach(Node node in graph.Keys){
            distanceList.Add(node, float.MaxValue);
            unvisitedList.Add(node);
        }

        distanceList[start] = 0;

        while(unvisitedList.Count > 0){
            Node currentNode = GetNodeWithLowestDistance(distanceList, unvisitedList);
            unvisitedList.Remove(currentNode);

            if (currentNode == end){
                // previousList[currentNode].Insert(0, start);
                previousList[currentNode].Add(currentNode);
                return previousList[currentNode];
            }

            foreach(Node neighbour in currentNode.connectedNodes.Keys){
                float newDistance = distanceList[currentNode] + currentNode.connectedNodes[neighbour].distance;
                if(newDistance < distanceList[neighbour]){
                    distanceList[neighbour] = newDistance;
                    previousList[neighbour] = new List<Node>();
                    previousList[neighbour].Add(currentNode);
                }
            }
        }
        return null;
    }

    static private Node GetNodeWithLowestDistance(Dictionary<Node, float> distanceList, List<Node> unvisitedList){
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