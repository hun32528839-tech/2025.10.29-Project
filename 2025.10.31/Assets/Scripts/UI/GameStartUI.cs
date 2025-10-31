using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private Button _gameStartButton;
    
    public void OnGameStartButton()
    {
        GameManager.Instance.ChangeScene("Stage1");
    }
}
