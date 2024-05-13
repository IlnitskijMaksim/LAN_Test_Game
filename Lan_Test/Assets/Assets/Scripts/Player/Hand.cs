using UnityEngine;
using Mirror;
using System.Collections;

public class Hand : NetworkBehaviour
{
    public float offset;
    private bool isCodeDisabled = false;
    private float currentDelay = 0.3f;

    [SyncVar(hook = nameof(OnLookDirectionChanged))]
    private Vector3 syncLookDirection;

    void Update()
    {
        if (isLocalPlayer)
        {
            if (!isCodeDisabled)
            {
                Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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

                if (Input.GetMouseButton(0))
                {
                    currentDelay = Mathf.Clamp(currentDelay - Time.deltaTime, 0.1f, 1.0f);
                    StartCoroutine(DisableCodeForDuration(currentDelay));
                }

                // Обновляем направление взгляда на сервере только для локального игрока
                CmdUpdateLookDirection(direction.normalized);
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
    private void CmdUpdateLookDirection(Vector3 direction)
    {
        syncLookDirection = direction;
    }

    private void OnLookDirectionChanged(Vector3 oldValue, Vector3 newValue)
    {
        // Поворачиваем руку у удаленных игроков
        float rotateWeapon = Mathf.Atan2(newValue.y, newValue.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateWeapon + offset);
    }
}
