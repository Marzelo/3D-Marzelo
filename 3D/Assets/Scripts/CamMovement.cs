using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {

    public class CamData {
        public float moveSpeed; 
        public Vector3 targetDistance;
        public Transform target;
       
        public CamData (float moveSpeed, Vector3 targetDistance, Transform target){
            this.moveSpeed = moveSpeed;
            this.targetDistance = targetDistance;
            this.target = target;
        }

        public bool CompareValue (CamData otherData) {
            return moveSpeed == otherData.moveSpeed &&
                   targetDistance == otherData.targetDistance &&
                   target == otherData.target;
        }
    }

    public float moveSpeed;
    public Vector3 targetDistance;
    public Transform target;
    Vector3 targetNode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		targetNode = target.position + (target.right * targetDistance.x) + (target.up * targetDistance.y) + (target.forward * targetDistance.z);		
        transform.position = Vector3.MoveTowards (transform.position, targetNode, moveSpeed * Time.deltaTime);
		transform.LookAt(target);

		/*transform.LookAt (lookTarget);
		float difference = Mathf.Abs(transform.position.y - lookTarget.position.y);
		if(difference != targetHeight) {
			Vector3 temp = transform.position;
			temp.y = Mathf.MoveTowards(temp.y, transform.position.y + targetHeight, 5f * Time.deltaTime);
			transform.position = temp;
		}
		if (transform.position.x != lookTarget.position.x) {
			Vector3 temp = transform.position;
			temp.x = Mathf.MoveTowards(temp.x, transform.position.x, 5f * Time.deltaTime);

		}*/

		}

		void OnDrawGizmos () {
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(targetNode, 0.5f);
		}

    public void SetCamData (CamData camData) {
        moveSpeed = camData.moveSpeed;
        targetDistance = camData.targetDistance;
        target = camData.target;
    }

    public CamData GetCamData () {
        return new CamData(moveSpeed, targetDistance, target);
    }

}
