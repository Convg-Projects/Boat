using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
  public static DayNightController instance;

  public float Speed = 1f;
  [Range(0, 2359)] public float CurrentTime = 1150;

  private void Awake(){
    if(instance == null){
      instance = this;
    } else if(instance != this) {
      Debug.Log("Day-night controller instance already exists, destroying object!!! :O");
      Destroy(gameObject);
    }
  }

  private void Update(){
    CurrentTime += Speed * Time.deltaTime;
    if(CurrentTime >= 2400){
      CurrentTime = 0;
    }

    transform.rotation = Quaternion.Euler(360 * CurrentTime / 2400 - 90, 0f, 0f);
  }
}
