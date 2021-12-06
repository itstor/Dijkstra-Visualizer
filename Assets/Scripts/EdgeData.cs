using UnityEngine;

public class EdgeData : MonoBehaviour
{
    private int m_distanceProperty;
    private bool m_isTwoWayProperty;
    public bool m_isTwoWay
    {
        get
        {
            return m_isTwoWayProperty;
        }
        set
        {
            m_isTwoWayProperty = value;
            GetComponent<EdgeLineChildController>().updateTwoWay();
        }
    }
    public int m_distance
    {
        get
        {
            return m_distanceProperty;
        }
        set
        {
            m_distanceProperty = value;
            gameObject.GetComponent<EdgeLineChildController>().updateDistanceText();
        }
    }
}