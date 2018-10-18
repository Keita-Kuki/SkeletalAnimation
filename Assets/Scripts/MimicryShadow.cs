using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class MimicryShadow : MonoBehaviour {

    [SerializeField]
    [Header("This object mast have \"JointPositionTracker\" component")]
    private GameObject animateObject;
    private JointPositionTracker JPTracker;

    private Mesh mesh;
    private SkeletalAnimationObject SAObject;
    private MeshData meshData;

    Vector3[] vertices;
    int[] triangles;
    Vector3[] normals;
    int[,] influencialJoint;
    Vector3[] influenceWeight;
    Vector3[] jointPosition;

    int jointNum = 14;
    Joint[] joints;

	void Start () {
        JPTracker = animateObject.GetComponent<JointPositionTracker>();
        meshData = GetComponent<MeshData>();

        initMeshData();

        InitJoints();
        SetJointParent();

        SAObject = new SkeletalAnimationObject(vertices, triangles, influencialJoint, influenceWeight, joints);

        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        //mesh.RecalculateNormals();

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
            Gizmos.DrawSphere(joint.position, 10f);
        }
    }

    void initMeshData() {
        vertices = meshData.vertices;
        triangles = Enumerable.Range(0, vertices.Length).ToArray();
        normals = new Vector3[vertices.Length];
        jointPosition = meshData.jointPosition;
        influencialJoint = new int[vertices.Length,4];
        influenceWeight = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++) {
            normals[i] = new Vector3(0, 0, 1);
            Vector3 v = vertices[i];
            float[] dists = new float[jointPosition.Length]; //関節からの距離
            int[] idx = new int[jointPosition.Length];
            for (int j = 0; j < jointPosition.Length; j++) {
                dists[j] = Vector3.Distance(v, jointPosition[j]);
                idx[j] = j;
            }
            Array.Sort(dists, idx);
            //influencialJoint[i,0] = idx[0];
            //influencialJoint[i,1] = idx[1];
            //influencialJoint[i,2] = 0;
            //influencialJoint[i,3] = 0;
            //float s = dists[0] + dists[1];
            //influenceWeight[i] = new Vector3(dists[1]/s, dists[0]/s, 0);
            influencialJoint[i, 0] = idx[0];
            influencialJoint[i, 1] = 0;
            influencialJoint[i, 2] = 0;
            influencialJoint[i, 3] = 0;
            influenceWeight[i] = new Vector3(1, 0, 0);
        }
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
        joints[0].SetParentJoint(joints[1]);
        joints[2].SetParentJoint(joints[1]);
        joints[5].SetParentJoint(joints[1]);
        joints[8].SetParentJoint(joints[1]);
        joints[11].SetParentJoint(joints[1]);

        joints[3].SetParentJoint(joints[2]);
        joints[4].SetParentJoint(joints[3]);

        joints[6].SetParentJoint(joints[5]);
        joints[7].SetParentJoint(joints[6]);

        joints[9].SetParentJoint(joints[8]);
        joints[10].SetParentJoint(joints[9]);

        joints[12].SetParentJoint(joints[11]);
        joints[13].SetParentJoint(joints[12]);
    }

    void UpdateJointsPosition() {
        joints[0].UpdateJointPosition(JPTracker.nose.position);
        joints[1].UpdateJointPosition(JPTracker.neck.position);

        joints[2].UpdateJointPosition(JPTracker.rShoulder.position);
        joints[3].UpdateJointPosition(JPTracker.rElbow.position);
        joints[4].UpdateJointPosition(JPTracker.rWrist.position);

        joints[5].UpdateJointPosition(JPTracker.lShoulder.position);
        joints[6].UpdateJointPosition(JPTracker.lElbow.position);
        joints[7].UpdateJointPosition(JPTracker.lWrist.position);

        joints[8].UpdateJointPosition(JPTracker.rHip.position);
        joints[9].UpdateJointPosition(JPTracker.rKnee.position);
        joints[10].UpdateJointPosition(JPTracker.rAnkle.position);

        joints[11].UpdateJointPosition(JPTracker.lHip.position);
        joints[12].UpdateJointPosition(JPTracker.lKnee.position);
        joints[13].UpdateJointPosition(JPTracker.lAnkle.position);
    }
}
