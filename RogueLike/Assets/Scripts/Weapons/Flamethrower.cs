using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, IWeapon
{
    [SerializeField] private ParticleSystem fireParticles; 
    private InputController playerControls;
    private bool isFiring = false;

    private void Awake()
    {
        playerControls = new InputController();

    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
        playerControls.Combat.Attack.canceled += _ => StopAttack(); 
    }
    private void Update()
    {
        fireParticles.transform.rotation = transform.rotation;
    }

    public void Attack()
    {
        isFiring = true;
        fireParticles.Play();
    }

    public void StopAttack()
    {
        isFiring = false;
        fireParticles.Stop();
    }
    private void OnDisable()
    {
        playerControls.Combat.Attack.started -= _ => Attack();
        playerControls.Disable();
    }

    public void CleanUp()
    {
        playerControls.Disable();
    }
}
