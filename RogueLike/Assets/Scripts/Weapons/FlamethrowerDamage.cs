using UnityEngine;

public class FlamethrowerDamage : MonoBehaviour
{
    [SerializeField] private float baseDamage = 0.5f;           
    [SerializeField] private float damageIncreaseRate = 0.2f;   
    private float currentDamage = 0f;                          


    private void Update()
    {
        if (Flamethrower.Instance.IsFiring)
        {
            currentDamage += damageIncreaseRate * Time.deltaTime;
        }
    }

    public float GetCurrentDamage()
    {
        return baseDamage + currentDamage;
    }

    public void ResetDamage()
    {
        currentDamage = 0f;
    }
}
