using UnityEngine;

public class Flamethrower : MonoBehaviour, IWeapon
{
    [SerializeField] private ParticleSystem fireParticles;
    private InputController playerControls;
    private bool isFiring = false;
    private FlamethrowerDamage flamethrowerDamage;
    public static Flamethrower Instance; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        playerControls = new InputController();
        flamethrowerDamage = GetComponent<FlamethrowerDamage>();
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

        if (isFiring)
        {
            ApplyDamageToEnemies();
        }
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

        if (flamethrowerDamage != null)
        {
            flamethrowerDamage.ResetDamage();
        }
    }

    private void ApplyDamageToEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (var enemyCollider in hitEnemies)
        {
            if (enemyCollider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
              
                if (enemyHealth != null)
                {
                    // Aplica el daño acumulado
                    enemyHealth.Hurt(flamethrowerDamage.GetCurrentDamage());
                }
            }
        }
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

    public bool IsFiring => isFiring;
}
