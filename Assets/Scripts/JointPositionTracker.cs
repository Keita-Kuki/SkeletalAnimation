using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointPositionTracker : MonoBehaviour {

    [HideInInspector]
    public Transform head;
    [HideInInspector]
    public Transform neck;
    [HideInInspector]
    public Transform chest;
    [HideInInspector]
    public Transform rightArm;
    [HideInInspector]
    public Transform leftArm;
    [HideInInspector]
    public Transform rightHand;
    [HideInInspector]
    public Transform leftHand;
    [HideInInspector]
    public Transform hip;
    [HideInInspector]
    public Transform rightLeg;
    [HideInInspector]
    public Transform leftLeg;
    [HideInInspector]
    public Transform rightFoot;
    [HideInInspector]
    public Transform leftFoot;

	// Use this for initialization
	void Start () {
        head = transform.FindDeep("EthanHead1").transform;
        neck = transform.FindDeep("EthanNeck").transform;
        chest = transform.FindDeep("EthanSpine2").transform;
        rightArm = transform.FindDeep("EthanRightArm").transform;
        leftArm = transform.FindDeep("EthanLeftArm").transform;
        rightHand = transform.FindDeep("EthanRightHand").transform;
        leftHand = transform.FindDeep("EthanLeftHand").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
