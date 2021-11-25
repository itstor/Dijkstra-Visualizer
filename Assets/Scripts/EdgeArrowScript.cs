using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeArrowScript : MonoBehaviour
{
    public void UpdateArrowPosition(){
        Vector3 parentPosition0 = gameObject.GetComponentInParent<LineRenderer>().GetPosition(0);
        Vector3 parentPosition1 = gameObject.GetComponentInParent<LineRenderer>().GetPosition(1);
        
        Vector3 centerPosition = (parentPosition0 + parentPosition1) / 2;
        float angle = Mathf.Atan2(parentPosition1.y - parentPosition0.y, parentPosition1.x - parentPosition0.x) * Mathf.Rad2Deg;
        
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gameObject.transform.position = centerPosition;
    }
}
