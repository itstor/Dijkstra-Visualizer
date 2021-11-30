using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateInfoGUI : MonoBehaviour
{
    private TMPro.TextMeshProUGUI mCoordinateText;
    // Start is called before the first frame update
    void Start()
    {
        mCoordinateText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mCoordinateText.text = "X: " + Mathf.RoundToInt(mousePosition.x) + " Y: " + Mathf.RoundToInt(mousePosition.y);
    }
}
