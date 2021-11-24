using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateInfoUI : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _coordinateText;
    // Start is called before the first frame update
    void Start()
    {
        _coordinateText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _coordinateText.text = "X: " + Mathf.RoundToInt(mousePosition.x) + " Y: " + Mathf.RoundToInt(mousePosition.y);
    }
}
