using UnityEngine;
using Mirror;

public class WeaponSwitch : NetworkBehaviour
{
    public Sword[] swords;

    [SyncVar(hook = nameof(OnChangeSelectedWeapon))]
    public int selectedWeapon = 0;

    private void Start()
    {
        swords = GetComponentsInChildren<Sword>();
        SelectWeapon();
    }

    public void Update()
    {
        if (!isLocalPlayer) return;

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CmdSelectWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            CmdSelectWeapon(1);
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    [Command]
    void CmdSelectWeapon(int weaponIndex)
    {
        selectedWeapon = weaponIndex;
    }

    void OnChangeSelectedWeapon(int oldWeapon, int newWeapon)
    {
        SelectWeapon();
    }

    public void SelectWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == selectedWeapon)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
                if (swords != null && i < swords.Length)
                {
                    swords[i].CancelAttack();
                }
            }
        }
    }
}
