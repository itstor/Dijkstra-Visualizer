using UnityEngine;

public class EdgeLineChildController : MonoBehaviour
{
    private EdgeData mEdgeData;
    private TMPro.TextMeshPro mDistanceText;
    private GameObject mArrowGameObject;
    private SpriteRenderer mArrowSpriteRenderer;
    private LineRenderer mEdgeLineRenderer;
    public Sprite singleArrowSprite;
    public Sprite doubleArrowSprite;

    void Awake() {
        mEdgeData = GetComponent<EdgeData>();
        mDistanceText = GetComponentInChildren<TMPro.TextMeshPro>();
        mEdgeLineRenderer = GetComponent<LineRenderer>();
        mArrowSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        mArrowGameObject = transform.Find("EdgeArrow").gameObject;    

        mDistanceText.text = mEdgeData.distance.ToString();
    }

    void Update(){
        if(mEdgeData.isTwoWay){
            mArrowSpriteRenderer.sprite = doubleArrowSprite;
        }
        else{
            mArrowSpriteRenderer.sprite = singleArrowSprite;
        }
    }

    public void updateEdgeLinePosition(){
        if (mEdgeLineRenderer == null) {Debug.Log("Null"); return;}
        Vector3 linePosition0 = mEdgeLineRenderer.GetPosition(0);
        Vector3 linePosition1 = mEdgeLineRenderer.GetPosition(1);

        Vector3 centerPosition = (linePosition0 + linePosition1) / 2;
        float angle = Mathf.Atan2(linePosition1.y - linePosition0.y, linePosition1.x - linePosition0.x) * Mathf.Rad2Deg;

        mArrowGameObject.transform.position = centerPosition;
        mArrowGameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        mDistanceText.transform.position = centerPosition;
        mDistanceText.transform.rotation = Quaternion.AngleAxis(angle + (90 < angle || angle < -90 ? 180 : 0), Vector3.forward);
    }

    public void updateDistanceText() => mDistanceText.text = mEdgeData.distance.ToString();
}