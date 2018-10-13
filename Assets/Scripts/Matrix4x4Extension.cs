using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Matrix4x4Extension {
    
    public static Matrix4x4 MultiplyScalar(this Matrix4x4 matrixA, float x)
    {
        Matrix4x4 answer;
        answer = Matrix4x4.zero;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                answer[i, j] = matrixA[i, j] * x;
            }
        }
        return answer;
    }

    public static Matrix4x4 Add(this Matrix4x4 matrixA, Matrix4x4 matrixB)
    {
        Matrix4x4 answer;
        answer = Matrix4x4.zero;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                answer[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }
        return answer;
    }

    public static Matrix4x4 Subtract(this Matrix4x4 matrixA, Matrix4x4 matrixB)
    {
        Matrix4x4 answer;
        answer = Matrix4x4.zero;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                answer[i, j] = matrixA[i, j] - matrixB[i, j];
            }
        }
        return answer;
    }
}
