using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint {

    public int id;
    public Vector3 position;
    public Joint parentJoint; //親関節
    public Vector3 initialVecFromParent; //初期状態の親からのベクトル
    public Matrix4x4 offsetMat; // 初期位置から原点に戻す行列
    public Matrix4x4 jointMat; // 頂点位置変換行列

    public Joint(int id, Vector3 position) {
        this.id = id;
        this.position = position;
        this.position.z = 0;
        this.offsetMat = Matrix4x4.Translate(position).inverse;
        UpdateJointMat();
    }

    public void SetParentJoint(Joint parentJoint) {
        this.parentJoint = parentJoint;
        initialVecFromParent = position - parentJoint.position;
    }

    // 関節位置更新
    public void UpdateJointPosition(Vector3 newPosition) {
        newPosition.z = 0;
        position = newPosition;
    }

    // jointMat更新
    // 原点に戻す -> 回す -> 現在座標に戻す 変換を施す
    public void UpdateJointMat(){
        Matrix4x4 toCurrentPositionMat = Matrix4x4.Translate(position);
        if (parentJoint != null)
        {
            Vector3 currentVecFromParent = position - parentJoint.position;
            Matrix4x4 rotMat = Matrix4x4.Rotate(Quaternion.FromToRotation(initialVecFromParent, currentVecFromParent));
            jointMat = toCurrentPositionMat * rotMat * offsetMat;
        }
        else
        {
            jointMat = toCurrentPositionMat * offsetMat;
        }
    }
}
