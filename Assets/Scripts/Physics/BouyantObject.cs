using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyantObject : MonoBehaviour
{
  public Rigidbody RB;
  public float DepthBeforeSubmerged = 1f;
  public float DisplacementAmount = 3f;
  public int BouyantObjectCount = 1;

  public float WaterDrag = 0.99f;
  public float WaterAngularDrag = 0.5f;

  public float ForceMultiplier = 1f;

  void FixedUpdate(){
    RB.AddForceAtPosition(Physics.gravity / BouyantObjectCount, transform.position, ForceMode.Acceleration);

    float WaveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
    if (transform.position.y < WaveHeight){
      float DisplacementMultiplier = Mathf.Clamp01(WaveHeight - transform.position.y / DepthBeforeSubmerged) * DisplacementAmount;
      Vector3 BouyantForce = new Vector3(0f, Mathf.Abs(Physics.gravity.y) * DisplacementMultiplier, 0f);
      RB.AddForceAtPosition(BouyantForce * ForceMultiplier, transform.position, ForceMode.Acceleration);
      RB.AddForce(DisplacementMultiplier * -RB.velocity * WaterDrag / BouyantObjectCount * Time.fixedDeltaTime);
      RB.AddTorque(DisplacementMultiplier * -RB.angularVelocity * WaterAngularDrag / BouyantObjectCount * Time.fixedDeltaTime);
    }
  }
}
