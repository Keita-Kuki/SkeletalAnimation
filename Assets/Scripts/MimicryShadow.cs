using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicryShadow : MonoBehaviour {

    [SerializeField]
    [Header("This object mast have \"JointPositionTracker\" component")]
    private GameObject animateObject;
    private JointPositionTracker JPTracker;

    private Mesh mesh;
    private SkeletalAnimationObject SAObject;

    Vector3[] vertices = new Vector3[] {
        new Vector3 (0.2f, 2.2f, 0),
        new Vector3 (0.5f, 1.9f, 0),

        new Vector3 (0.2f, 1.5f, 0),
        new Vector3 (0.5f, 1.5f, 0),
        new Vector3 (1.0f, 1.5f, 0),
        new Vector3 (1.5f, 1.5f, 0),
        new Vector3 (2.0f, 1.5f, 0),
        new Vector3 (2.5f, 1.5f, 0),

        new Vector3 (0.2f, 0.8f, 0),
        new Vector3 (0.5f, 0.8f, 0),
        new Vector3 (1.0f, 0.8f, 0),
        new Vector3 (1.5f, 0.8f, 0),
        new Vector3 (2.0f, 0.8f, 0),
        new Vector3 (2.5f, 0.8f, 0),

        new Vector3 (-0.2f, 2.2f, 0),
        new Vector3 (-0.5f, 1.9f, 0),

        new Vector3 (-0.2f, 1.5f, 0),
        new Vector3 (-0.5f, 1.5f, 0),
        new Vector3 (-1.0f, 1.5f, 0),
        new Vector3 (-1.5f, 1.5f, 0),
        new Vector3 (-2.0f, 1.5f, 0),
        new Vector3 (-2.5f, 1.5f, 0),

        new Vector3 (-0.2f, 0.8f, 0),
        new Vector3 (-0.5f, 0.8f, 0),
        new Vector3 (-1.0f, 0.8f, 0),
        new Vector3 (-1.5f, 0.8f, 0),
        new Vector3 (-2.0f, 0.8f, 0),
        new Vector3 (-2.5f, 0.8f, 0),
    };

    int[] triangles = new int[] {
        0,1,14,
        14,1,15,
        15,1,2,
        15,2,16,
        2,8,16,
        16,8,22,

        2,3,8,
        8,3,9,
        3,4,9,
        9,4,10,
        10,4,5,
        5,11,10,
        11,5,6,
        11,6,12,
        6,7,12,
        12,7,13,

        17,16,22,
        17,22,23,
        18,17,23,
        18,23,24,
        19,18,24,
        19,24,25,
        20,19,25,
        20,25,26,
        21,20,26,
        21,26,27,
    };

    int[][] influencialJoint = new int[][] {
        new int[4]{2, 0, 0, 0},
        new int[4]{1, 2, 0, 0},

        new int[4]{1, 0, 0, 0},
        new int[4]{0, 3, 0, 0},
        new int[4]{3, 0, 0, 0},
        new int[4]{3, 4, 0, 0},
        new int[4]{4, 0, 0, 0},
        new int[4]{4, 0, 0, 0},

        new int[4]{0, 0, 0, 0},
        new int[4]{0, 3, 0, 0},
        new int[4]{3, 0, 0, 0},
        new int[4]{3, 4, 0, 0},
        new int[4]{4, 0, 0, 0},
        new int[4]{4, 0, 0, 0},

        new int[4]{2, 0, 0, 0},
        new int[4]{1, 2, 0, 0},

        new int[4]{1, 0, 0, 0},
        new int[4]{0, 5, 0, 0},
        new int[4]{5, 0, 0, 0},
        new int[4]{5, 6, 0, 0},
        new int[4]{6, 0, 0, 0},
        new int[4]{6, 0, 0, 0},

        new int[4]{0, 0, 0, 0},
        new int[4]{0, 5, 0, 0},
        new int[4]{5, 0, 0, 0},
        new int[4]{5, 6, 0, 0},
        new int[4]{6, 0, 0, 0},
        new int[4]{6, 0, 0, 0},

    };

    Vector3[] influenceWeight = new Vector3[]{
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),

    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),

    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),

    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),

    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),

    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    };

    Vector3[] jointPosition = new Vector3[]{
        new Vector3(0, 1, 0),
        new Vector3(0, 1.5f, 0),
        new Vector3(0, 2, 0),
        new Vector3(1, 1, 0),
        new Vector3(2, 1, 0),
        new Vector3(-1, 1, 0),
        new Vector3(-2, 1, 0),
    };

    int jointNum = 7;
    Joint[] joints;

	void Start () {
        JPTracker = animateObject.GetComponent<JointPositionTracker>();

        InitJoints();
        SetJointParent();

        SAObject = new SkeletalAnimationObject(vertices, triangles, influencialJoint, influenceWeight, joints);

        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateJointsPosition();
        SAObject.UpdateJointMat();
        SAObject.UpdateVerticesPosition();
        mesh.vertices = SAObject.vertices;
	}

    private void OnDrawGizmos()
    {
        foreach(Joint joint in joints) {
            Gizmos.DrawSphere(joint.position, 0.1f);
        }
        //foreach(Vector3 p in jointPosition) {
        //    Gizmos.DrawSphere(p, 0.1f);
        //}
    }

    void InitJoints()
    {
        joints = new Joint[jointNum];
        for (int i = 0; i < jointNum; i++)
        {
            joints[i] = new Joint(i, jointPosition[i]);
        }
    }

    void SetJointParent()
    {
        joints[1].SetParentJoint(joints[0]);
        joints[2].SetParentJoint(joints[1]);
        joints[4].SetParentJoint(joints[3]);
        joints[6].SetParentJoint(joints[5]);
        joints[3].SetParentJoint(joints[1]);
        joints[5].SetParentJoint(joints[3]);
    }

    void UpdateJointsPosition() {
        joints[0].UpdateJointPosition(JPTracker.chest.position);
        joints[1].UpdateJointPosition(JPTracker.neck.position);
        joints[2].UpdateJointPosition(JPTracker.head.position);
        joints[3].UpdateJointPosition(JPTracker.rightArm.position);
        joints[4].UpdateJointPosition(JPTracker.rightHand.position);
        joints[5].UpdateJointPosition(JPTracker.leftArm.position);
        joints[6].UpdateJointPosition(JPTracker.leftHand.position);
    }
}
