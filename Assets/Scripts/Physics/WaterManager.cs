using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class WaterManager : MonoBehaviour
{
  private MeshFilter MF;

  private void Awake(){
    MF = GetComponent<MeshFilter>();
  }

  private void Update(){
    Vector3[] Vertices = MF.mesh.vertices;
    for(int i = 0; i < Vertices.Length; ++i){
      Vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + Vertices[i].x);
    }

    MF.mesh.vertices = Vertices;
    MF.mesh.RecalculateNormals();
  }
}
