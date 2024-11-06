using UnityEngine;

public class MouseLookJoysticksiz : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform playerBody;  // Karakterin pozisyonunu alýr

    float xRotation = 0f;

    private void Update()
    {
        // Fare hareketini almak için GetAxis kullanýyoruz
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Y eksenindeki fare hareketiyle kamera yukarý/aþaðý döndürme
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);   // -90 ile 90 derece arasýnda sýnýrlandýrma (yukarý/aþaðý bakma)

        // Kameranýn yukarý/aþaðý döndürülmesi
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakterin saða ve sola döndürülmesi
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
