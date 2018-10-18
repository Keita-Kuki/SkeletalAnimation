using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletalAnimationObject {
    
    public Vector3[] initialVertices; // 初期位置
    public Vector3[] vertices;
    public int[] triangles;
    public int[,] influencialJoint; // 各頂点が影響を受ける関節
    public Vector3[] influenceWeight; // 各頂点が受ける影響度
    public Joint[] joints;

    public SkeletalAnimationObject(Vector3[] vertices, int[] triangles, int[,] influencialJoint, Vector3[] influenceWeight, Joint[] joints) {
        initialVertices = new Vector3[vertices.Length];
        this.vertices = new Vector3[vertices.Length];
        vertices.CopyTo(initialVertices, 0);
        vertices.CopyTo(this.vertices, 0);
        this.triangles = triangles;
        this.influencialJoint = influencialJoint;
        this.influenceWeight = influenceWeight;
        this.joints = joints;
    }

    public void UpdateJointMat() {
        foreach(Joint joint in joints) {
            joint.UpdateJointMat();
        }
    }

    public void UpdateVerticesPosition() {
        initialVertices.CopyTo(vertices, 0); // 初期位置をコピー
        for (int i = 0; i < vertices.Length; i++)
        {
            // 影響度
            float w0 = influenceWeight[i].x;
            float w1 = influenceWeight[i].y;
            float w2 = influenceWeight[i].z;
            float w3 = 1 - w0 - w1 - w2;

            Matrix4x4 matrix = joints[influencialJoint[i,0]].jointMat.MultiplyScalar(w0)
                               .Add(joints[influencialJoint[i,1]].jointMat.MultiplyScalar(w1))
                               .Add(joints[influencialJoint[i,2]].jointMat.MultiplyScalar(w2))
                               .Add(joints[influencialJoint[i,3]].jointMat.MultiplyScalar(w3));
            vertices[i] = matrix.MultiplyPoint(vertices[i]);
        }
    }

}
