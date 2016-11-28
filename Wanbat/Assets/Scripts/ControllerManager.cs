using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControllerManager : MonoBehaviour {

	public GameObject messageCanvas;
	public Text messageText;
	public GameObject controllerPivot;
	public GameObject wand;

	private GameObject selectedObject;

	#if UNITY_HAS_GOOGLEVR && (UNITY_ANDROID || UNITY_EDITOR)
	void Awake (){
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
	{
		Quaternion ori = GvrController.Orientation;
		gameObject.transform.localRotation = ori;
		Vector3 vector = GvrController.Orientation * Vector3.forward;

		if (GvrController.ClickButtonUp) {
			LevelManager levelManager = FindObjectOfType<LevelManager>();
			levelManager.LoadNextScene();
		}
		UpdatePointer();
//		UpdateWandPosition();
		UpdateStatusMessage();
	}

	private void UpdatePointer ()
	{
		if (GvrController.State != GvrConnectionState.Connected) {
			UpdateWandPosition();
		}
//		controllerPivot.SetActive (true);
//		controllerPivot.transform.rotation = GvrController.Orientation;
//

////    if (dragging) {
//      if (GvrController.TouchUp) {
//        messageText.text = "Lifted your finger";
//      }
////    } else {
		RaycastHit hitInfo;
		Vector3 rayDirection = GvrController.Orientation * Vector3.forward;
		if (Physics.Raycast (Vector3.zero, rayDirection, out hitInfo)) {
			if (hitInfo.collider && hitInfo.collider.gameObject) {
//				if ((hitInfo.transform.gameObject.tag == "Button") && GvrController.ClickButtonUp) {
//					Button button = (Button) hitInfo.transform.gameObject.GetComponent<Button>();
//					button.onClick.Invoke();
//				}
          SetSelectedObject(hitInfo.collider.gameObject);
				messageText.text = "Hit something."; 
        }
      } else {
        SetSelectedObject(null);
		messageText.text = "Not sure what this does.";
      }
//      if (GvrController.TouchDown && selectedObject != null) {
//        StartDragging();
//      }
//    }
  }
	private void SetSelectedObject(GameObject obj) {
	    if (null != selectedObject) {

	    }
	    if (null != obj) {

	    }
	    selectedObject = obj;
	  }

	private void UpdateWandPosition ()
	{
		wand.transform.localRotation = GvrController.Orientation;
	}

	private void UpdateStatusMessage() {
    // This is an example of how to process the controller's state to display a status message.
    switch (GvrController.State) {
      case GvrConnectionState.Connected:
        messageCanvas.SetActive(false);
        break;
      case GvrConnectionState.Disconnected:
        messageText.text = "Controller disconnected.";
        messageText.color = Color.white;
        messageCanvas.SetActive(true);
        break;
      case GvrConnectionState.Scanning:
        messageText.text = "Controller scanning...";
        messageText.color = Color.cyan;
        messageCanvas.SetActive(true);
        break;
      case GvrConnectionState.Connecting:
        messageText.text = "Controller connecting...";
        messageText.color = Color.yellow;
        messageCanvas.SetActive(true);
        break;
      case GvrConnectionState.Error:
        messageText.text = "ERROR: " + GvrController.ErrorDetails;
        messageText.color = Color.red;
        messageCanvas.SetActive(true);
        break;
      default:
        // Shouldn't happen.
        Debug.LogError("Invalid controller state: " + GvrController.State);
        break;
    }
  }
	#endif  // UNITY_HAS_GOOGLEVR && (UNITY_ANDROID || UNITY_EDITOR)

}
