using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text healthPercent;
    public Image hb1, hb2, hb3;
    public int numHitBox = 3;
    //public int currentHealth = 100;

    public void Start(){
        numHitBox = PlayerPrefs.GetInt("hbs");
        
        if (numHitBox == 2){
            destroyHealthBox(hb3);
        }
        else if (numHitBox == 1){
            destroyHealthBox(hb3);
            destroyHealthBox(hb2);
        }

        setHealth(PlayerPrefs.GetInt("health"));
    }

    // Setting Health
    public void setMaxHealth(int health)
    {
        
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
        healthPercent.text = "100%";
    }
    
    // Updating Health
    public void setHealth(int health)
    {
        slider.value = health;
        
        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (health <= 0){
            if (hb3){
                destroyHealthBox(hb3);
                slider.value = slider.maxValue;
                fill.color = gradient.Evaluate(slider.normalizedValue);
            }
            else if (hb2){
                destroyHealthBox(hb2);
                slider.value = slider.maxValue;
                fill.color = gradient.Evaluate(slider.normalizedValue);
            }
            else{
                destroyHealthBox(hb1);
                healthPercent.text = "Dead";
                SceneManager.LoadScene("DeathScene");
            }
        }
        healthPercent.text = (slider.normalizedValue * 100) + "%";

        
    }

    void destroyHealthBox(Image hb)
    {
        Destroy(hb);
    }
}
