using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointPositionTracker : MonoBehaviour {

    [HideInInspector]
    public Transform nose;
    [HideInInspector]
    public Transform neck;

    [HideInInspector]
    public Transform rShoulder;
    [HideInInspector]
    public Transform rElbow;
    [HideInInspector]
    public Transform rWrist;

    [HideInInspector]
    public Transform lShoulder;
    [HideInInspector]
    public Transform lElbow;
    [HideInInspector]
    public Transform lWrist;

    [HideInInspector]
    public Transform rHip;
    [HideInInspector]
    public Transform rKnee;
    [HideInInspector]
    public Transform rAnkle;

    [HideInInspector]
    public Transform lHip;
    [HideInInspector]
    public Transform lKnee;
    [HideInInspector]
    public Transform lAnkle;


	// Use this for initialization
	void Start () {
        nose = transform.FindDeep("EthanUpperLip").transform;
        neck = transform.FindDeep("EthanNeck").transform;

        rShoulder = transform.FindDeep("EthanRightArm").transform;
        rElbow = transform.FindDeep("EthanRightForeArm").transform;
        rWrist = transform.FindDeep("EthanRightHand").transform;

        lShoulder = transform.FindDeep("EthanLeftArm").transform;
        lElbow = transform.FindDeep("EthanLeftForeArm").transform;
        lWrist = transform.FindDeep("EthanLeftHand").transform;

        rHip = transform.FindDeep("EthanRightUpLeg").transform;
        rKnee = transform.FindDeep("EthanRightLeg").transform;
        rAnkle = transform.FindDeep("EthanRightFoot").transform;

        lHip = transform.FindDeep("EthanLeftUpLeg").transform;
        lKnee = transform.FindDeep("EthanLeftLeg").transform;
        lAnkle = transform.FindDeep("EthanLeftFoot").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
