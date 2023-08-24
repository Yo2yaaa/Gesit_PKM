using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorStatBarScript : MonoBehaviour
{
    //[SerializeField] 
    public Slider sliderArmor;

    public int maxStatBarArmor = 8;
    public int currentStatBarArmor;
    public ArmorStatBarScript armorStatBar;

    void Start(){
        currentStatBarArmor = maxStatBarArmor;
        armorStatBar.SetMaxStatArmor(maxStatBarArmor);
        
        //Non-Aktif ArmorStatBar
        armorStatBar.enabled = false;
    }

    private void SetMaxStatArmor(int stat)
    {
        sliderArmor.maxValue = stat;
        sliderArmor.value = stat;
    }
            
    private void SetStatArmor(int stat)
    {
        sliderArmor.value = stat;
    }

    public void DamageToArmorStatBar(int damage)
    {
        currentStatBarArmor -= damage;
        armorStatBar.SetStatArmor(currentStatBarArmor);
    }
}
