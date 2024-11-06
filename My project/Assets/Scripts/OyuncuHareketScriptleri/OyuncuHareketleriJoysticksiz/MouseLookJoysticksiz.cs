using UnityEngine;

public class MouseLookJoysticksiz : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform playerBody;  // Karakterin pozisyonunu al�r

    float xRotation = 0f;

    private void Update()
    {
        // Fare hareketini almak i�in GetAxis kullan�yoruz
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Y eksenindeki fare hareketiyle kamera yukar�/a�a�� d�nd�rme
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);   // -90 ile 90 derece aras�nda s�n�rland�rma (yukar�/a�a�� bakma)

        // Kameran�n yukar�/a�a�� d�nd�r�lmesi
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakterin sa�a ve sola d�nd�r�lmesi
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
