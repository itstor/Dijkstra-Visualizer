using UnityEngine;

public class Utils
{
    public static Vector3 getMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        var pos = Camera.main.ScreenToWorldPoint(mousePosition);
        
        return pos;
    }
}