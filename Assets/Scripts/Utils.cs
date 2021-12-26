using UnityEngine;

public class Utils
{
    public static Vector3 getMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        var pos = Camera.main.ScreenToWorldPoint(mousePosition);
        
        return pos;
    }

    public static float calculateDistance2Point(Vector2 pointA, Vector2 pointB){
        return Mathf.Round(Mathf.Sqrt(Mathf.Pow(pointA.x - pointB.x, 2) + Mathf.Pow(pointA.y - pointB.y, 2)) * 10f) / 10f;
    }
}