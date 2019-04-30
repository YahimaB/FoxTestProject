using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapGrid : MonoBehaviour
{

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        float xCount = Mathf.Round(position.x);
        float zCount = Mathf.Round(position.z);

        Vector3 result = new Vector3(xCount, 0.5f, zCount);

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = -3; x < 3; x++)
        {
            for (float z = -3; z < 3; z++)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
