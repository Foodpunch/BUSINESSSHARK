using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	//Camera Data.
	public GameObject Camera;
	public GameObject CameraTarget;

	public float cameraFollowSpeed;

	private Vector3 offeset; //Original Distance between the camera and the target.

	//Camera Clamping
	public GameObject top_Platform;
	public GameObject bottom_Platform;

	// Use this for initialization
	void Start () 
	{
		//Setting Camera as the holder of this script.
		Camera = this.gameObject;

		offeset = transform.position - CameraTarget.transform.position;
	}
	
	void FixedUpdate()
	{
		Vector3 holdCameraPosition = new Vector3(CameraTarget.transform.position.x + offeset.x, Camera.transform.position.y, Camera.transform.position.z);

		Vector3 cameraPostion = CameraTarget.transform.position + offeset;


		if (CameraTarget.transform.position.y <= 2 && CameraTarget.transform.position.y >= -2)
		{
			Camera.transform.position = Vector3.Lerp(Camera.transform.position, cameraPostion, cameraFollowSpeed * Time.deltaTime);
		}
		else
		{
			Camera.transform.position = Vector3.Lerp(Camera.transform.position, holdCameraPosition, cameraFollowSpeed * Time.deltaTime);
		}
	}
}
