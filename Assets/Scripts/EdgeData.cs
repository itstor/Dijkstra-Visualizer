using UnityEngine;

public class EdgeData: MonoBehaviour
{
    private int distanceProperty;
    public int distance {get {
        return distanceProperty;
    } 
    set {
        distanceProperty = value;
        gameObject.GetComponent<EdgeLineChildController>().updateDistanceText();
    }}
    public bool isTwoWay {get; set;}
}