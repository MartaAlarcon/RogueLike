using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    private bool facingLeft = false;

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private Transform weaponCollider;
    private InputController playerControls;
    private Animator myAnimator;


    private GameObject slashAnim;

    private void Awake()
    {

        myAnimator = GetComponent<Animator>();
        playerControls = new InputController();

    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        weaponCollider = PlayerMovement.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("Slash").transform;
        playerControls.Combat.Attack.started += _ => Attack();
    }
    void Update()
    {
        MouseFollowWithOffset();
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerMovement.Instance.transform.position);
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        if (mousePosition.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            facingLeft = true;
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            facingLeft = false;
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);

        }
    }
    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }
    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (facingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (facingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    private void OnDisable()
    {
        playerControls.Combat.Attack.started -= _ => Attack();
        playerControls.Disable();
        myAnimator = null;
    }

    public void CleanUp()
    {
        playerControls.Disable();
        myAnimator = null;
    }


}