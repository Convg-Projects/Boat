using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  [Header("Transitions")]
  private bool Transitioning = false;
  public Text AdviceText;

  [Header("Movement")]
  private bool Grounded;
  private bool Crouched = false;
  private float GroundDistance;
  private float StandHeight;
  private float CapsuleStandHeight;
  private float CamStandHeight;
  public float CrouchDistance = 0.5f;
  public float JumpForce = 25f;
  public string HorizontalMovementAxis = "Horizontal";
  public string VerticalMovementAxis = "Vertical";
  public float MovementSpeed = 5f;
  public string HorizontalLookAxis = "Mouse X";
  public string VerticalLookAxis = "Mouse Y";
  public float LookSensitivity = 20f;

  private Rigidbody RB;
  private Collider col;
  private CapsuleCollider cCol;
  public Camera Cam;

  void Start(){
    RB = GetComponent<Rigidbody>();
    col = GetComponent<Collider>();
    cCol = GetComponent<CapsuleCollider>();

    GroundDistance = col.bounds.extents.y;
    CapsuleStandHeight = cCol.center.y;
    StandHeight = cCol.height;
    CamStandHeight = Cam.transform.localPosition.y;

    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Confined;
  }

  void FixedUpdate(){
    if(!Transitioning){
      Move();
    }
  }

  void Update(){
    if(!Transitioning){
      Look();
      Jump();
      Crouch();

      Debug.DrawRay(Cam.transform.position, Cam.transform.forward * 3, Color.blue);
      AdviceText.gameObject.SetActive(false);
    }
  }

  void Move(){
    Vector3 MovementVector = Vector3.zero;
    MovementVector.x = Input.GetAxis(HorizontalMovementAxis) * MovementSpeed / (Crouched ? 2 : 1);
    MovementVector.z = Input.GetAxis(VerticalMovementAxis) * MovementSpeed / (Crouched ? 2 : 1);
    MovementVector = transform.TransformDirection(MovementVector);
    MovementVector.y = RB.velocity.y;

    RB.velocity = MovementVector;
  }

  void Jump(){
    if(Input.GetKeyDown(KeyCode.Space) && Grounded){
      RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
      RB.AddForce(new Vector3(0, JumpForce * 0.25f, 0), ForceMode.Impulse);
      Grounded = false;
    }
  }

  void Crouch(){
    if(Input.GetKey(KeyCode.LeftControl)){
      Crouched = true;
      Cam.transform.localPosition = new Vector3(Cam.transform.localPosition.x, CamStandHeight - CrouchDistance, Cam.transform.localPosition.z);
      cCol.center = new Vector3(cCol.center.x, CapsuleStandHeight - CrouchDistance/2, cCol.center.z);
      cCol.height = StandHeight - CrouchDistance;
    } else {
      Crouched = false;
      Cam.transform.localPosition = new Vector3(Cam.transform.localPosition.x, CamStandHeight, Cam.transform.localPosition.z);
      cCol.center = new Vector3(cCol.center.x, CapsuleStandHeight, cCol.center.z);
      cCol.height = StandHeight;
    }
  }

  void Look(){
    transform.Rotate(0f, Input.GetAxis(HorizontalLookAxis) * LookSensitivity * Time.deltaTime * 25f, 0f);
    Cam.transform.Rotate(-Input.GetAxis(VerticalLookAxis) * LookSensitivity * Time.deltaTime * 25f, 0f, 0f);
  }

  void OnCollisionStay(Collision collisionInfo){
    if(collisionInfo.transform.tag == "Ground"){
      Grounded = true;
    }
  }

  void OnCollisionExit(){
    Grounded = false;
  }
}
