using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJointFollowScript : MonoBehaviour {

	public string limbTag = "TrackedLimb";

	public Transform animRoot;

	public Transform[] allAnimTrans;
	public ConfigurableJoint[] confJoints;

	void Start () {
		PopulateArrays();
		AddJointFollowScript();
	}

	private void PopulateArrays () {
		Transform[] animTransArr = animRoot.GetComponentsInChildren<Transform>();
		Transform[] ragTransArr = transform.GetComponentsInChildren<Transform>();
		List<Transform> transList = new List<Transform>();
		List<ConfigurableJoint> jointList = new List<ConfigurableJoint>();

		foreach (Transform trans in animTransArr) {
			if (trans.tag == limbTag) {
				transList.Add(trans);
			}
		}
		allAnimTrans = transList.ToArray();

		foreach (Transform trans in ragTransArr) {
			ConfigurableJoint cj = trans.GetComponent<ConfigurableJoint>();
			if (cj != null) {
				jointList.Add(cj);
			}
		}
		confJoints = jointList.ToArray();
	}

	private void AddJointFollowScript () {
		foreach (ConfigurableJoint cj in confJoints) {
			cj.gameObject.AddComponent<JointFollowAnimRot>();
			cj.connectedBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
			for (int t = 0; t < allAnimTrans.Length; t++) {
				if (allAnimTrans[t].name == cj.gameObject.name) {
					cj.GetComponent<JointFollowAnimRot>().target = allAnimTrans[t];
				}
			}
		}
	}

}
