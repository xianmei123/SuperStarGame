using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenClick : MonoBehaviour {

	public bool pickUp;

	public float moveSpeed;
	public float rotateSpeed;
	public float force;
	private float pitch;
	private float yaw;

	private Vector3 mouseMovement;

	public Rigidbody grabbedBody;

	public LayerMask grabingLayer;

	void Start () {
		pickUp = false;
		moveSpeed = 0.1f;
		rotateSpeed = 8f;
		force = 520f;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			pickUp = !pickUp;
		}

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, grabingLayer)){
				if (!pickUp) {
					JointFollowAnimRot jf = hit.collider.gameObject.GetComponent<JointFollowAnimRot>();
					Debug.DrawRay(hit.point, ray.origin, Color.red);
					if (jf != null) {
						jf.invert = !jf.invert;
					}
				}
			}

		} else if (Input.GetMouseButton(0)) {
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, grabingLayer)){
				if (grabbedBody == null && hit.rigidbody != null) {
					grabbedBody = hit.rigidbody;
				}
				if (pickUp && grabbedBody != null) {
					Vector3 wantedPos = new Vector3(grabbedBody.position.x, grabbedBody.position.y + Input.GetAxis("Mouse Y") * 2f, grabbedBody.position.z);

					grabbedBody.AddForce(CalcualtePullForce(grabbedBody, wantedPos, force, 0.5f));
				}
			}

		} else if (Input.GetMouseButtonUp(0)) {
			grabbedBody = null;
		}

	
		if (Input.GetMouseButton(1)) {
			// Camera rotation
			pitch += rotateSpeed * -Input.GetAxis("Mouse Y");
			yaw += rotateSpeed * Input.GetAxis("Mouse X");
			pitch = Mathf.Clamp(pitch, -90f, 90f);
			while (yaw < 0f) {
				yaw += 360f;
			}
			while (yaw >= 360f) {
				yaw -= 360f;
			}
			transform.eulerAngles = new Vector3(pitch, yaw, 0f);

			// Camera fly
			if (Input.GetKey(KeyCode.W)) {
				transform.position += (transform.forward * moveSpeed);
			}
			if (Input.GetKey(KeyCode.A)) {
				transform.position += (-transform.right * moveSpeed);
			}
			if (Input.GetKey(KeyCode.S)) {
				transform.position += (-transform.forward * moveSpeed);
			}
			if (Input.GetKey(KeyCode.D)) {
				transform.position += (transform.right * moveSpeed);
			}
			if (Input.GetKey(KeyCode.Q)) {
				transform.position += (-transform.up * moveSpeed);
			}
			if (Input.GetKey(KeyCode.E)) {
				transform.position += (transform.up * moveSpeed);
			}
		}
	}

	private Vector3 CalcualtePullForce (Rigidbody rb, Vector3 want, float pullForce, float damping) {
		return ((want - rb.position) * pullForce) + ((Vector3.zero - rb.velocity) * damping);
	}
}
