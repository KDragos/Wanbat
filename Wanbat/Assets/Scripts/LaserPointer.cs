using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour {

	public LineRenderer laser;
	public GameObject wandHandle;
	public GameObject wandTip;

	void Awake (){
	}

	// Use this for initialization
	void Start () {
		Vector3[] initLaserPositions = new Vector3[2] {wandHandle.transform.position, wandTip.transform.position};
		laser.SetPositions(initLaserPositions);
		laser.SetColors(Color.blue, Color.blue);
		laser.SetWidth(0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {

//		UpdateLaser();
		Quaternion ori = GvrController.Orientation;
		gameObject.transform.localRotation = ori;
		Vector3 vector = GvrController.Orientation * Vector3.forward;

		ShootLaserFromTargetPosition(transform.position, vector, 200f);
	}

	private void UpdateLaser() {
		Vector3[] newPositions = new Vector3[2] {wandHandle.transform.position, wandTip.transform.position};
		laser.SetPositions(newPositions);

	}

	private void ShootLaserFromTargetPosition (Vector3 targetPosition, Vector3 direction, float length)
	{
		Ray ray = new Ray (targetPosition, direction);
		RaycastHit raycastHit;
		if (Physics.Raycast (ray, out raycastHit, length)) {
			GameObject cube = raycastHit.transform.gameObject;
			if (cube.tag == "button") {
				Destroy(raycastHit.transform.gameObject);
			} 
		}
//		Vector3 endPosition = targetPosition + (length * direction);
		laser.SetPosition(0, wandHandle.transform.position);
		laser.SetPosition(1, wandTip.transform.position + (length * direction));
	}
}
