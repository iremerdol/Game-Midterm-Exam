using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Health playerHealth;
    public Image totalHealthBar;
    public Image currentHealthBar;

    private void Start(){
    }

    private void Update(){
        currentHealthBar.fillAmount = playerHealth.GetCurrentHealth() / 100.0f;
    }
}