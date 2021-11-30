using UnityEngine;

public class EdgeLineChildController : MonoBehaviour
{
    private EdgeData mEdgeLineData;
    private TMPro.TextMeshPro mDistanceText;
    private GameObject mArrowGameObject;
    private LineRenderer mEdgeLineRenderer;

    void Start() {
        mEdgeLineData = GetComponent<EdgeData>();
        mDistanceText = GetComponentInChildren<TMPro.TextMeshPro>();
        mArrowGameObject = transform.Find("EdgeArrow").gameObject;    

        mDistanceText.text = mEdgeLineData.distance.ToString();
    }

    public void updateEdgeLinePosition(){
        Vector3 linePosition0 = mEdgeLineRenderer.GetPosition(0);
        Vector3 linePosition1 = mEdgeLineRenderer.GetPosition(1);

        Vector3 centerPosition = (linePosition0 + linePosition1) / 2;
        float angle = Mathf.Atan2(linePosition1.y - linePosition0.y, linePosition1.x - linePosition0.x) * Mathf.Rad2Deg;

        mArrowGameObject.transform.position = centerPosition;
        mArrowGameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        mDistanceText.transform.position = centerPosition;
        mDistanceText.transform.rotation = Quaternion.AngleAxis(angle + (90 < angle || angle < -90 ? 180 : 0), Vector3.forward);
    }

    public void updateDistanceText() => mDistanceText.text = mEdgeLineData.distance.ToString();
}