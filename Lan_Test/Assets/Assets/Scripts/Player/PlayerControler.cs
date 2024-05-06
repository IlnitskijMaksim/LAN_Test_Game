using System.Collections;
using UnityEngine;
using Mirror;
using Cinemachine;
using Inventory.UI;
using Inventory;

public class PlayerController : NetworkBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (isLocalPlayer)
        {
            cinemachineVirtualCamera = CinemachineVirtualCamera.FindAnyObjectByType<CinemachineVirtualCamera>();
            cinemachineVirtualCamera.Follow = this.gameObject.transform;
        }
    }

    private void Update()
    {
        if (!PauseMenu.GameIsPaused && isLocalPlayer)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPosition = transform.position;

            if (mousePosition.x < playerPosition.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(inputX, inputY);

            rb.velocity = movement * speed;
        }
    }
}
