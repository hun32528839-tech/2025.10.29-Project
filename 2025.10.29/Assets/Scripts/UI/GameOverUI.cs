using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Player _player;

    private void Awake()
    {
        if (_player)
        {
            _player.PlayerDieDelegate += GameOver;
        }
    }
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_player)
        {
            _player.PlayerDieDelegate -= GameOver;
        }
    }

    public void GameOver()
    {        
        gameObject.SetActive(true);
    }

}
