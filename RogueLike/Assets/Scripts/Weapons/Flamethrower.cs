using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour,IWeapon
{
    public void Attack()
    {
        Debug.Log("Flame Attack");
    }
    public void CleanUp()
    {
       
    }
}
