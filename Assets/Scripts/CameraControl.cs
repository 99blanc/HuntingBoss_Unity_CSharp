using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    public float X;
    public float Y;
    public float Z = 1.75f;
    public float Sensiti = 1.5f;
    public GameObject Player;
    private float Height = 1.25f;
    private float MaxXRange = 45f;
    private Vector3 PlayerPos;
    private float MouseX;
    private float MouseY;
    Rigidbody PlayerRigid;
    PlayerUIControl PlayerUI;
    BossUIControl BossUI;
    Gurdian1AIControl Gurdian1AI;
    Gurdian1UIControl Gurdian1UI;
    Vector3 CameraAngle;

    void Start()
    {
        PlayerUI = GameObject.Find("PlayerStatus").GetComponent<PlayerUIControl>();
        if (SceneManager.GetActiveScene().name == "Field1")
            BossUI = GameObject.Find("BossStatus").GetComponent<BossUIControl>();
        if (SceneManager.GetActiveScene().name == "Field2")
        {
            Gurdian1AI = GameObject.Find("Gurdian1").GetComponent<Gurdian1AIControl>();
            Gurdian1UI = GameObject.Find("Gurdian1Status").GetComponent<Gurdian1UIControl>();
        }
        PlayerRigid = Player.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        CameraAngle = transform.eulerAngles;
        X = CameraAngle.y;
        Y = CameraAngle.x;
    }

    void Update()
    {
        if (PlayerUI.Dead)
            return;
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        X += MouseY * Sensiti;
        Y += MouseX * Sensiti;
        X = Mathf.Clamp(X, -MaxXRange, MaxXRange);
        PlayerPos = Player.transform.position + Vector3.up * Height;
        CameraAngle = Quaternion.Euler(-X, Y, 0f) * Vector3.forward;
        PlayerRigid.MoveRotation(Quaternion.Euler(0f, Y, 0f));
        transform.position = PlayerPos + CameraAngle * -(Z);
    }

    void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        transform.LookAt(PlayerPos);
    }
}