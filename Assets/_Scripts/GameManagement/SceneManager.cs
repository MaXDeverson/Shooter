using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneManager : MonoBehaviour
{
    [Inject] UIController _uiController;
    [Inject] CharacterManager _palayer;
    [SerializeField] private List<Transform> _allBots;

    private void Awake()
    {
        _allBots.ForEach(bot =>
        {
            bot.GetComponent<HPSystem>().OnDamaged += (count, current, value) =>
            {
                _uiController.PlayHit();
                _uiController.DamageLog(count);
            };
            bot.GetComponent<BotInputSystem>().SetTarget(_palayer.transform);
        });
        _palayer.GetComponent<HPSystem>().OnDamaged += (count, current, value) =>
        {
            _uiController.UpdateHealth(current, value);
        };
    }

}
