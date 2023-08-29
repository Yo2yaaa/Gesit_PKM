using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    int count = 0;

    public void EnemyCounter(){
        count += 1;
        Debug.Log(count);
        if (count >= 3){
            count = 0;
        }
    }
}
