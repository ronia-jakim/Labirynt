using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController; 
    [SerializeField] private float speed = 12f;
    private Vector3 velocity;

    private bool groundedPlayer;
    private float jumpHeight = 2.0f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove() {
        RaycastHit hit;
        groundedPlayer = Physics.Raycast(groundCheck.position, transform.TransformDirection(Vector3.down), out hit, 0.2f, groundMask);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        characterController.Move(move * speed * Time.deltaTime);

        if (groundedPlayer) {
            if (velocity.y < 0) {
                velocity.y = 0f;
            }

            if (Input.GetButtonDown("Jump")) {
                velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * -9.81f);
            }

            string terrainType = hit.collider.gameObject.tag;

            switch (terrainType) {
                case "Low":
                    speed = 3;
                    break;
                case "High": 
                    speed = 20;
                    break;
                default:
                    speed = 12;
                    break;
            }
        }

        velocity.y += -9.81f * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
