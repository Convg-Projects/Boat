using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
  public static WaveManager instance;

  public float Amplitude = 1f;
  public float Length = 2f;
  public float Speed = 1f;
  public float Offset = 0f;

  private void Awake(){
    if(instance == null){
      instance = this;
    } else if(instance != this) {
      Debug.Log("Wave manager instance already exists, destroying object!!! :O");
      Destroy(gameObject);
    }
  }

  private void Update(){
    Offset = Time.time * Speed;

    for(int i = -1000; i < 1000; ++i){
      Debug.DrawLine(new Vector3(i, GetWaveHeight(i), 0), new Vector3(i, 0, 0));
    }
  }

  public float GetWaveHeight(float x){
    return -Amplitude * Mathf.Sin(x * Speed / (Length * transform.lossyScale.x) - Speed * Offset);
  }
}
