using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    [SerializeField] private Text _playerHpText;
    [SerializeField] private Player _player;
    
    private void Awake()
    {
        if (_player)
        {
            _player.PlayerHpUpdateDelegate += PlayerHpUpdate;
        }
    }
    private void Start()
    {
        PlayerHpUpdate();
    }
    private void OnDestroy()
    {
        if (_player)
        {
            _player.PlayerHpUpdateDelegate -= PlayerHpUpdate;
        }
    }

    public void PlayerHpUpdate()
    {
        _playerHpText.text = $"HP : {_player.Hp}";
    }
}
