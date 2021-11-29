using UnityEngine;

public class EdgeLineScript : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private GameObject _arrowGameObject;
    [SerializeField] private GameObject _textDistanceGameObject;

    void Start()
    {
        _textDistanceGameObject.GetComponent<TMPro.TextMeshPro>().text = _distance.ToString();
    }

    public void updateEdgeLinePos()
    {
        Vector3 linePosition0 = gameObject.GetComponent<LineRenderer>().GetPosition(0);
        Vector3 linePosition1 = gameObject.GetComponent<LineRenderer>().GetPosition(1);

        Vector3 centerPosition = (linePosition0 + linePosition1) / 2;
        float angle = Mathf.Atan2(linePosition1.y - linePosition0.y, linePosition1.x - linePosition0.x) * Mathf.Rad2Deg;

        _arrowGameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _arrowGameObject.transform.position = centerPosition;

        _textDistanceGameObject.transform.position = centerPosition;
        _textDistanceGameObject.transform.rotation = Quaternion.AngleAxis(angle + (90 < angle || angle < -90 ? 180 : 0), Vector3.forward);
    }

    public void setEdgeDistance(float distance)
    {
        _distance = distance;
        _textDistanceGameObject.GetComponent<TMPro.TextMeshPro>().text = distance.ToString();
    }
}
