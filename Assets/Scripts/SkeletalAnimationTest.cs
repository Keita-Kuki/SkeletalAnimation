using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletalAnimationTest : MonoBehaviour
{

    Mesh mesh;
    SkeletalAnimationObject SAObject;

    // openCVから渡ってくる輪郭の頂点情報
    Vector3[] vertices = new Vector3[] {
        new Vector3 (-0.5000f, -2.2887f, 0.0f),
        new Vector3 (-0.5000f, -1.2887f, 0.0f),
        new Vector3 (-0.5000f, -0.2887f, 0.0f),
        new Vector3 (-1.3660f,  0.2113f, 0.0f),
        new Vector3 (-2.2321f,  0.7113f, 0.0f),
        new Vector3 (-1.7321f,  1.5774f, 0.0f),
        new Vector3 (-0.8660f,  1.0774f, 0.0f),
        new Vector3 ( 0.0000f,  0.5774f, 0.0f),
        new Vector3 ( 0.8660f,  1.0774f, 0.0f),
        new Vector3 ( 1.7321f,  1.5774f, 0.0f),
        new Vector3 ( 2.2321f,  0.7113f, 0.0f),
        new Vector3 ( 1.3660f,  0.2113f, 0.0f),
        new Vector3 ( 0.5000f, -0.2887f, 0.0f),
        new Vector3 ( 0.5000f, -1.2887f, 0.0f),
        new Vector3 ( 0.5000f, -2.2887f, 0.0f),
    };

    // なんかしらのアルゴリズムでverticesを三角形分割（ドロネー分割？）
    int[] triangles = new int[] {
        0, 1, 14,
        1, 13, 14,
        1, 2, 13,
        2, 12, 13,
        2, 7, 12,
        2, 6, 7,
        2, 3, 6,
        3, 5, 6,
        3, 4, 5,
        7, 8, 12,
        8, 11, 12,
        8, 9, 11,
        9, 10, 11,
    };

    // 各頂点が影響を受けるボーン
    int[][] influencialJoint = new int[][] {
        new int[4]{2, 0, 0, 0},
        new int[4]{1, 2, 0, 0},
        new int[4]{0, 0, 0, 0},
        new int[4]{3, 4, 0, 0},
        new int[4]{4, 0, 0, 0},
        new int[4]{4, 0, 0, 0},
        new int[4]{3, 4, 0, 0},
        new int[4]{0, 0, 0, 0},
        new int[4]{5, 6, 0, 0},
        new int[4]{6, 0, 0, 0},
        new int[4]{6, 0, 0, 0},
        new int[4]{5, 6, 0, 0},
        new int[4]{0, 0, 0, 0},
        new int[4]{1, 2, 0, 0},
        new int[4]{2, 0, 0, 0},
    };

    // 上記ボーンの影響度
    Vector3[] influenceWeight = new Vector3[]{
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    new Vector3(0.50f, 0.50f, 0.00f),
    new Vector3(1.00f, 0.00f, 0.00f),
    };

    Vector3[] jointPosition = new Vector3[]{
        new Vector3(0.0000f, 0.0000f, 0),
        new Vector3(0.0000f, -1.0000f, 0),
        new Vector3(0.0000f, -2.0000f, 0),
        new Vector3(-0.6830f, 0.3943f, 0),
        new Vector3(-1.5490f, 0.8943f, 0),
        new Vector3(0.6830f, 0.3943f, 0),
        new Vector3(1.5490f, 0.8943f, 0),
    };

    int jointNum = 7;
    Joint[] joints;

	void Start ()
    {
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
	void Update ()
    {
        UpdateJointsPosition();
        SAObject.UpdateJointMat();
        SAObject.UpdateVerticesPosition();
        mesh.vertices = SAObject.vertices;
	}

    private void OnDrawGizmos()
    {
        //Gizmos.DrawMesh(mesh);
        foreach(Joint joint in SAObject.joints) {
            Gizmos.DrawSphere(joint.position, 0.1f);
        }
    }

    void InitJoints()
    {
        joints = new Joint[jointNum];
        for (int i = 0; i < jointNum; i++) {
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

    void UpdateJointsPosition()
    {
        // 適当
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 pos = joints[4].position;
            pos.x += 0.01f;
            joints[4].UpdateJointPosition(pos);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 pos = joints[4].position;
            pos.x -= 0.01f;
            joints[4].UpdateJointPosition(pos);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 pos = joints[4].position;
            pos.y += 0.01f;
            joints[4].UpdateJointPosition(pos);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 pos = joints[4].position;
            pos.y -= 0.01f;
            joints[4].UpdateJointPosition(pos);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 pos = joints[3].position;
            pos.x += 0.01f;
            joints[3].UpdateJointPosition(pos);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 pos = joints[3].position;
            pos.x -= 0.01f;
            joints[3].UpdateJointPosition(pos);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 pos = joints[3].position;
            pos.y += 0.01f;
            joints[3].UpdateJointPosition(pos);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 pos = joints[3].position;
            pos.y -= 0.01f;
            joints[3].UpdateJointPosition(pos);
        }
    }
}
