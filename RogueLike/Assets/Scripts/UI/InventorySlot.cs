using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;
    public bool isUnlocked;

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
