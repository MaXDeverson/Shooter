using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animator _hitAnimation;
    [SerializeField] private Animation _hitAnim;
    [SerializeField] private TextMeshProUGUI _damageLog;
    [Header("Health")]
    [SerializeField] private TextMeshProUGUI _healthPoints;
    [SerializeField] private Slider _healthValue;

    public void PlayHit()
    {
        _hitAnimation.SetTrigger("On");
    }

    public void DamageLog(int damage)
    {
        _damageLog.text = "DAMAGE:" + damage.ToString();
    }

    public void UpdateHealth(int healthPoints, float healthValue)
    {
        _healthPoints.text = healthPoints.ToString();
        _healthValue.value = healthValue;
    }
}
