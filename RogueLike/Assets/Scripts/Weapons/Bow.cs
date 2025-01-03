using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    private Animator animator;
    private InputController playerControls;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControls = new InputController();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }
    public void Attack()
    {
        animator.SetTrigger(FIRE_HASH);
        GameObject arrow = Instantiate(arrowPrefab,arrowSpawnPoint.position,ActiveWeapon.Instance.transform.rotation);
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    private void OnDisable()
    {
        playerControls.Combat.Attack.started -= _ => Attack();
        playerControls.Disable();
        animator = null;
    }

    public void CleanUp()
    {
        playerControls.Disable();
        animator = null;
    }
}
