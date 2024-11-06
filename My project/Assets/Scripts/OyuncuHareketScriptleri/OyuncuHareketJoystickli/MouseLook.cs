using Unity.Mathematics;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensivity = 100f;
    [SerializeField] Transform Player;  //karakterin pozisyonunu alýr
    [SerializeField] Joystick LookJoystick;

    float xRotation = 0f;

    private void Update()
    {
        float X = LookJoystick.Horizontal * sensivity * Time.deltaTime;
        float Y = LookJoystick.Vertical * sensivity * Time.deltaTime;


        xRotation -= Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);   //180derece toplam (aþaðý yukarý bakma)

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        Player.Rotate(Vector3.up * X);

    }
}
