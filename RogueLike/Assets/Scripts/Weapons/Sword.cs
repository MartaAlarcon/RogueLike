using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    private bool facingLeft = false;

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private InputController playerControls;
    private Animator myAnimator;
    private PlayerMovement playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

    private void Awake() {
        playerController = GetComponentInParent<PlayerMovement>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new InputController();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    void Update()
    {
        MouseFollowWithOffset();
        //AdjustSwordPosition();
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        if (mousePosition.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            facingLeft = true;
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            facingLeft = false;
        }
    }
    void AdjustSwordPosition()
    {
        if (FacingLeft)
        {
            // Si está mirando a la izquierda, debes asegurarte que la espada esté correctamente posicionada en el lado izquierdo.
            slashAnim.transform.localPosition = new Vector3(-0.5f, 0, 0); // Ajustar según sea necesario
        }
        else
        {
            // Si está mirando a la derecha, la espada debe estar en el lado derecho
            slashAnim.transform.localPosition = new Vector3(0.5f, 0, 0); // Ajustar según sea necesario
        }
    }


    private void Attack() {
        myAnimator.SetTrigger("Attack");

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }
    
    public void SwingUpFlipAnim() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (facingLeft) { 
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (facingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
}
