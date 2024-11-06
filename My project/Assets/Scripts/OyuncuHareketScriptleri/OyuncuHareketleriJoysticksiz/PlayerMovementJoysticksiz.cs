using UnityEngine;

public class PlayerMovementJoysticksiz : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] private float _speed = 4f, _XSpeed = 6f, _jump = 1f, _gravity = -9.8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundLayerMask;  // objelere layer veriyoruz hangi objelere de�di�imizde z�playabilirim
    Vector3 _velocity; // gidece�imiz, z�playaca��m�z y�n
    bool isGrounded; // havada m� yerde mi

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);  // Zeminde olup olmad���n� kontrol eder

        if (isGrounded && _velocity.y < 0)  // E�er karakter zeminde ise ve a�a��ya do�ru bir h�z varsa
        {
            _velocity.y = -2;  // Yere d��erken s�z�lerek d��meyi sa�lar
        }

        // Klavyeden yatay ve dikey eksenleri almak
        float x = Input.GetAxis("Horizontal"); // "A" ve "D" tu�lar�yla veya sol/sa� ok tu�lar�yla kontrol
        float z = Input.GetAxis("Vertical");   // "W" ve "S" tu�lar�yla veya yukar�/a�a�� ok tu�lar�yla kontrol

        Vector3 move = transform.right * x + transform.forward * z; // Sa� sol / ileri geri hareket

        if (z > 0.8f || Input.GetKey(KeyCode.LeftShift)) // H�zl� ko�ma
        {
            characterController.Move(move * _XSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * _speed * Time.deltaTime);  // Y�r�me
        }

        // Z�plama i�lemi i�in
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();  // Space tu�una bas�ld���nda z�plamay� tetikler
        }

        _velocity.y += _gravity * Time.deltaTime;  // Yer �ekimi
        characterController.Move(_velocity * Time.deltaTime);  // Hareket ettirme
    }

    private void Jump()
    {
        if (isGrounded)  // E�er zemin �zerindeyse
        {
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);  // Z�plama i�lemi
        }
    }
}
