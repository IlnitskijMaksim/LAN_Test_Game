using System.Collections;
using UnityEngine;
using Mirror;

public class Hand : NetworkBehaviour
{
    public float offset;
    private bool isCodeDisabled = false;
    private float currentDelay = 0.3f;
    [SerializeField] private bool facingRight = true;


    void Update()
    {
        if (!PauseMenu.GameIsPaused && isLocalPlayer)
        {
            if (!isCodeDisabled)
            {
                Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float rotateWeapon = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotateWeapon + offset);

                Vector2 scale = transform.localScale;

                if (direction.x < 0 && facingRight)
                {
                    CmdFlipWeapon(false);
                }
                else if (direction.x > 0 && !facingRight)
                {
                    CmdFlipWeapon(true);
                }

                if (Input.GetMouseButton(0))
                {
                    currentDelay = Mathf.Clamp(currentDelay - Time.deltaTime, 0.1f, 1.0f);
                    StartCoroutine(DisableCodeForDuration(currentDelay));
                }

                CmdSetMouseDirection(direction.normalized);
            }
        }
    }

    private IEnumerator DisableCodeForDuration(float duration)
    {
        isCodeDisabled = true;
        yield return new WaitForSeconds(duration);
        isCodeDisabled = false;
    }

    [Command]
    private void CmdSetMouseDirection(Vector2 direction)
    {
        RpcSetMouseDirection(direction);
    }

    [Command]
    void CmdFlipWeapon(bool facingRight)
    {
        RpcFlipWeapon(facingRight);
        this.facingRight = facingRight;
    }

    [ClientRpc]
    void RpcFlipWeapon(bool facingRight)
    {
        Vector2 scale = transform.localScale;
        scale.y = facingRight ? 1 : -1;
        transform.localScale = scale;
    }

    [ClientRpc]
    private void RpcSetMouseDirection(Vector2 direction)
    {
        if (isLocalPlayer)
        {
            float rotateWeapon = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateWeapon + offset);

            Vector2 scale = transform.localScale;

            if (direction.x < 0)
            {
                scale.y = -1;
            }
            else if (direction.x > 0)
            {
                scale.y = 1;
            }

            transform.localScale = scale;
        }
    }
}
