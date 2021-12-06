using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateInfoGUI : MonoBehaviour
{
    private TMPro.TextMeshProUGUI m_coordinateText;
    // Start is called before the first frame update
    void Start()
    {
        m_coordinateText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Utils.getMouseWorldPosition();
        
        m_coordinateText.text = "x: " + Mathf.RoundToInt(mousePosition.x) + " y: " + Mathf.RoundToInt(mousePosition.y);
    }
}
