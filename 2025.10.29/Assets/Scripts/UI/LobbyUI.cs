using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button _gameStartButton;
    
    public void OnGameStartClick()
    {
        GameManager.Instance.ChangeStage("Stage1");
    }
}
