using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private Stage _currentStage;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    public Stage Stage
    {
        get
        {
            if (_currentStage == null)
            {
                _currentStage = FindFirstObjectByType<Stage>();
            }
            return _currentStage;
        }
    }
    public void ChangeStage(string sceneName)
    {
        _currentStage = null;

        SceneManager.LoadScene(sceneName);
    }
}
