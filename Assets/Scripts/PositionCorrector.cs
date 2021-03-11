using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCorrector : MonoBehaviour
{
    public Transform mesh;

    public void AlignWithMesh()
    {
        transform.position = mesh.position;
        mesh.localPosition = new Vector3(0,0,0);
    }
}
