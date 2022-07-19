using UnityEngine;

public class EdgeLineChildController : MonoBehaviour
{
    private EdgeData m_edgeData;
    private TMPro.TextMeshPro m_distanceText;
    private GameObject m_arrowGameObject;
    private SpriteRenderer m_arrowSpriteRenderer;
    private LineRenderer m_edgeLineRenderer;
    public Sprite m_singleArrowSprite;
    public Sprite m_doubleArrowSprite;

    void Awake()
    {
        m_edgeData = GetComponent<EdgeData>();
        m_distanceText = GetComponentInChildren<TMPro.TextMeshPro>();
        m_edgeLineRenderer = GetComponent<LineRenderer>();
        m_arrowSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_arrowGameObject = transform.Find("EdgeArrow").gameObject;

        m_distanceText.text = m_edgeData.m_distance.ToString();
    }

    public void updateTwoWay() => m_arrowSpriteRenderer.sprite = m_edgeData.m_isTwoWay ? m_doubleArrowSprite : m_singleArrowSprite;

    public void updateEdgeLinePosition()
    {
        if (m_edgeLineRenderer == null) { return; }
        Vector3 linePosition0 = m_edgeLineRenderer.GetPosition(0);
        Vector3 linePosition1 = m_edgeLineRenderer.GetPosition(1);

        Vector3 centerPosition = (linePosition0 + linePosition1) / 2;
        float angle = Mathf.Atan2(linePosition1.y - linePosition0.y, linePosition1.x - linePosition0.x) * Mathf.Rad2Deg;

        m_arrowGameObject.transform.position = centerPosition;
        m_arrowGameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        m_distanceText.transform.position = centerPosition;
        m_distanceText.transform.rotation = Quaternion.AngleAxis(angle + (90 < angle || angle < -90 ? 180 : 0), Vector3.forward);
    }

    public void updateDistanceText() => m_distanceText.text = m_edgeData.m_distance.ToString();
}