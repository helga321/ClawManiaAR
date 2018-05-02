using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
	public class UnityARHitScreen : MonoBehaviour
	{
		public Transform m_HitTransform;
		public Claw m_Claw;
		public GameObject startButton;

        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    //Debug.Log ("Got hit!");
					if (startButton.activeSelf) {
	                    m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
	                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
					} else {
						//m_Claw.Move(ConvertToClawArea (point));
					}
                    //Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
                    return true;
                }
            }
            return false;
        }
		
		// Update is called once per frame
		void Update () {
			if (Input.touchCount > 0 && m_HitTransform != null)
			{
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
				{
					var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					Debug.Log("ScreenPos: "+screenPosition.x+","+screenPosition.y);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

					if ((point.y>0.2f)&&(point.y<0.9f)) {

	                    // prioritize reults types
	                    ARHitTestResultType[] resultTypes = {
	                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
	                        // if you want to use infinite planes use this:
	                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
	                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
	                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
	                    }; 
						
	                    foreach (ARHitTestResultType resultType in resultTypes)
	                    {
	                        if (HitTestWithResultType (point, resultType))
	                        {
	                            return;
	                        }
	                    }
					}
				}
			}
		}

	
	}
}

