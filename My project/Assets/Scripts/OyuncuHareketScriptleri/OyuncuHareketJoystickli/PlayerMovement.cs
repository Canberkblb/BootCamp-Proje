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
    [SerializeField] LayerMask groundLayerMask;  //objelere layer veriyoruz hangi objelere deðdiðimizde zýplayabilirim
    [SerializeField] Joystick movementJoystick;
    [SerializeField] Button jumpButton;
    Vector3 _velocity; //gideceðimiz, zýplayacaðýmýz yön
    bool isGrounded; //havada mý yerde mi

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        jumpButton.onClick.AddListener(Jump);
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);  //zeminde olup olmadýðý  //belirli açýda kontrol gerçekleþtirir //belirli aralýkta kontrol et groundLayerMaska temas ediyo musun


        if (isGrounded && _velocity.y < 0)  //velocityde zýplamaya bakýyoruz y ekseninde
        {
            _velocity.y = -2;  //yere düþerken süzülerek düþmeyi saðlar

        }


        float x = movementJoystick.Horizontal;                                                    //(kendime not)  bu kodu deðiþtirdim doðrudan joysticki kullanamýyorum //movementJoystick.horizontal;  ////Input.GetAxis("Horizontal");  Input.GetAxis("Vertical");   
        float z = movementJoystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;     // sað sol / ileri geri

        if (z > 0.8f || Input.GetKey(KeyCode.LeftShift)) //hýzlý koþma
        {
            characterController.Move(move * _XSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * _speed * Time.deltaTime);  //yürüme

        }

        _velocity.y += _gravity * Time.deltaTime;       //yer çekimi
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
