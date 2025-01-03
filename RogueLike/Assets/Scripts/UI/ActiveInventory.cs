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
        // Limpia el arma actual
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            (ActiveWeapon.Instance.CurrentActiveWeapon as IWeapon)?.CleanUp();
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        // Obtén el slot actual
        InventorySlot currentSlot = transform.GetChild(slotNumber).GetComponentInChildren<InventorySlot>();

        // Si no hay slot o el slot no está desbloqueado, no equipar arma
        if (currentSlot == null || !currentSlot.isUnlocked)
        {
            ActiveWeapon.Instance.WeaponNull();
            Debug.Log($"Slot {slotNumber + 1} está bloqueado o vacío.");
            return;
        }

        // Instanciar el arma si está desbloqueada
        WeaponInfo weaponInfo = currentSlot.GetWeaponInfo();
        if (weaponInfo == null) return;

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
