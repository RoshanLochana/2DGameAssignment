using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currunthealthBar;


    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.curruntHealth / 10;
    }

    private void Update()
    {
        currunthealthBar.fillAmount = playerHealth.curruntHealth / 10;
    }


}
