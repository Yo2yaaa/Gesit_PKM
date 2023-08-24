using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBarScript : MonoBehaviour
{
    //[SerializeField] 
    public Slider slider;

    public int maxStatBar = 20;
    public int currentStatBar;
    public StatBarScript statBar;

    void Start(){
        currentStatBar = maxStatBar;
        statBar.SetMaxStat(maxStatBar);

    }
                                                                                                                        
    private void SetMaxStat(int stat)
    {
        slider.maxValue = stat;
        slider.value = stat;
    }

    private void SetStat(int stat)
    {
        slider.value = stat;
    }

    public void DamageToStatBar(int damage)
    {
        currentStatBar -= damage;
        statBar.SetStat(currentStatBar);
    }
}
