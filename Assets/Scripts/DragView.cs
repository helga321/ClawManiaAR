//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragView : EventTrigger {
    public float rotSpeed;
    public Transform dragRot;

    private Rigidbody rb;

    float angleOffset;
    Vector3 screenPos;
	bool isDragging = false;

    //public override void OnBeginDrag(PointerEventData eventData)
    //{
    //       Debug.Log("Begin Drag");
    //       if (dragRot != null){
    //           screenPos = Camera.main.WorldToScreenPoint(transform.position);
    //           Vector3 targetAngle = Input.mousePosition - screenPos;
    //           //angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(targetAngle.y, targetAngle.x)) * Mathf.Rad2Deg;
    //       }
    //}

    //public override void OnDrag(PointerEventData eventData)
    //{
    //       Debug.Log("Drag");
    //       if (dragRot != null) {
    //           Vector3 targetAngle = Input.mousePosition - screenPos;
    //           //float angle = Mathf.Atan2(targetAngle.y, targetAngle.x) * Mathf.Rad2Deg;

    //           dragRot.eulerAngles = new Vector3(0f, dragRot.eulerAngles.y + angle + angleOffset, 0f);
    //       }
    //}

    public override void OnDrag(PointerEventData eventData) {
        float xAxixRot = Input.GetAxis("Mouse X") * 6f;
        float yAxisRot = Input.GetAxis("Mouse Y") * 6f;

        dragRot.Rotate(Vector3.down, xAxixRot);
        dragRot.Rotate(Vector3.right, yAxisRot);
    }

	public override void OnEndDrag(PointerEventData eventData)
	{
        Debug.Log("End Drag");
	}
}
