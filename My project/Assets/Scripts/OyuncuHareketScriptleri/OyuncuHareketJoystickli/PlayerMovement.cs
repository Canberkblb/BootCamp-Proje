using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;


    [SerializeField] private float _speed = 4f, _XSpeed = 6f, _jump = 1f, _gravity = -9.8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundLayerMask;  //objelere layer veriyoruz hangi objelere de�di�imizde z�playabilirim
    [SerializeField] Joystick movementJoystick;
    [SerializeField] Button jumpButton;
    Vector3 _velocity; //gidece�imiz, z�playaca��m�z y�n
    bool isGrounded; //havada m� yerde mi

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        jumpButton.onClick.AddListener(Jump);
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);  //zeminde olup olmad���  //belirli a��da kontrol ger�ekle�tirir //belirli aral�kta kontrol et groundLayerMaska temas ediyo musun


        if (isGrounded && _velocity.y < 0)  //velocityde z�plamaya bak�yoruz y ekseninde
        {
            _velocity.y = -2;  //yere d��erken s�z�lerek d��meyi sa�lar

        }


        float x = movementJoystick.Horizontal;                                                    //(kendime not)  bu kodu de�i�tirdim do�rudan joysticki kullanam�yorum //movementJoystick.horizontal;  ////Input.GetAxis("Horizontal");  Input.GetAxis("Vertical");   
        float z = movementJoystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;     // sa� sol / ileri geri

        if (z > 0.8f || Input.GetKey(KeyCode.LeftShift)) //h�zl� ko�ma
        {
            characterController.Move(move * _XSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * _speed * Time.deltaTime);  //y�r�me

        }

        _velocity.y += _gravity * Time.deltaTime;       //yer �ekimi
        characterController.Move(_velocity * Time.deltaTime);

    }


    public void Jump()
    {
        if (isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);
        }
    }


}
