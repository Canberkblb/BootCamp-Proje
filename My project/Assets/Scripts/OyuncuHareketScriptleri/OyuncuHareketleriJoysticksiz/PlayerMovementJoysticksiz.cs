using UnityEngine;

public class PlayerMovementJoysticksiz : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] private float _speed = 4f, _XSpeed = 6f, _jump = 1f, _gravity = -9.8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundLayerMask;  // objelere layer veriyoruz hangi objelere deðdiðimizde zýplayabilirim
    Vector3 _velocity; // gideceðimiz, zýplayacaðýmýz yön
    bool isGrounded; // havada mý yerde mi

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);  // Zeminde olup olmadýðýný kontrol eder

        if (isGrounded && _velocity.y < 0)  // Eðer karakter zeminde ise ve aþaðýya doðru bir hýz varsa
        {
            _velocity.y = -2;  // Yere düþerken süzülerek düþmeyi saðlar
        }

        // Klavyeden yatay ve dikey eksenleri almak
        float x = Input.GetAxis("Horizontal"); // "A" ve "D" tuþlarýyla veya sol/sað ok tuþlarýyla kontrol
        float z = Input.GetAxis("Vertical");   // "W" ve "S" tuþlarýyla veya yukarý/aþaðý ok tuþlarýyla kontrol

        Vector3 move = transform.right * x + transform.forward * z; // Sað sol / ileri geri hareket

        if (z > 0.8f || Input.GetKey(KeyCode.LeftShift)) // Hýzlý koþma
        {
            characterController.Move(move * _XSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * _speed * Time.deltaTime);  // Yürüme
        }

        // Zýplama iþlemi için
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();  // Space tuþuna basýldýðýnda zýplamayý tetikler
        }

        _velocity.y += _gravity * Time.deltaTime;  // Yer çekimi
        characterController.Move(_velocity * Time.deltaTime);  // Hareket ettirme
    }

    private void Jump()
    {
        if (isGrounded)  // Eðer zemin üzerindeyse
        {
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);  // Zýplama iþlemi
        }
    }
}
