using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int slotNumber = 0;

    private InputController controller;

    private void Awake()
    {
        controller = new InputController();
    }
    private void Start()
    {
        controller.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ToggleActiveHightLight(0);
    }
    private void OnEnable()
    {
        controller.Enable();
    }
    private void ToggleActiveSlot(int slot)
    {
        ToggleActiveHightLight(slot-1);
    }
    private void ToggleActiveHightLight(int index)
    {
        slotNumber = index;
        foreach(Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon()
    {
        if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            (ActiveWeapon.Instance.CurrentActiveWeapon as IWeapon)?.CleanUp();
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }
        if (!transform.GetChild(slotNumber).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }
        GameObject weaponToSpawn = transform.GetChild(slotNumber).GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
