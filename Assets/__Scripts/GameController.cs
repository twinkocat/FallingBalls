using twinkocat;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BallsSpawner           _ballSpawner;
    [SerializeField] private BallDeathTrigger       _ballDeathTrigger;
    [SerializeField] private ValuePanelViewModel    _valuePanelViewModel;

    [Space]
    [Header("ObjectPool params")]
    [SerializeField] private Ball               _ballPrefab;
    [SerializeField] private Transform          _ballsParent;
    [SerializeField] private bool               _isExpandable;
    [SerializeField] private bool               _isActiveByDefault;
    [SerializeField] private int                _capacity;

    [Space]
    [Range(0.1f, 10f)]
    [SerializeField] private float              _spawnDelay;

    private BallsPool                           _ballsPool;
    private int                                 _currentScore;
    private int                                 _topScore;

    private void Awake()
    {
        _ballsPool = new BallsPool(_ballPrefab, _isExpandable, _isActiveByDefault, _capacity, _ballsParent);

        BoundsCalculator.Init(Camera.main);

        _ballDeathTrigger.OnDeathTriggerBallEnter += _ballsPool.ReturnObject;
        _ballDeathTrigger.OnDeathTriggerBallEnter += UnsubscribeBallHandler;

        _ballSpawner.Init(_ballsPool, _spawnDelay);

        _ballSpawner.OnBallSpawn += SubscribeBallHandler;

        _valuePanelViewModel.Init();
        InitScores();
    }

    private void PauseGame() => Time.timeScale = 0f;
    private void ResumeGame() => Time.timeScale = 1f;

    private void InitScores()
    {
        _topScore = PlayerPrefs.GetInt(nameof(_topScore), 0);

        _valuePanelViewModel.SetCurrentScore(_currentScore);
        _valuePanelViewModel.SetTopScore(_topScore);
    }

    private void SubscribeBallHandler(Ball ball)
    {
        ball.OnBallClicked += BallHandler;
        ball.OnBallClicked += _ballsPool.ReturnObject;
    }

    private void UnsubscribeBallHandler(Ball ball)
    {
        ball.OnBallClicked -= BallHandler;
        ball.OnBallClicked -= _ballsPool.ReturnObject;
    }

    private void BallHandler(Ball ball)
    {
        _currentScore += ball.GetBallScore();

        if (_topScore < _currentScore)
            _topScore = _currentScore;

        _valuePanelViewModel.SetCurrentScore(_currentScore);
        _valuePanelViewModel.SetTopScore(_topScore);

        UnsubscribeBallHandler(ball);
    }

    private void OnValidate()
    {
        _ballSpawner.SetSpawnDelay(_spawnDelay);
    }

    private void OnDestroy()
    {
        _ballDeathTrigger.OnDeathTriggerBallEnter -= _ballsPool.ReturnObject;
        _ballDeathTrigger.OnDeathTriggerBallEnter -= UnsubscribeBallHandler;

        _ballSpawner.OnBallSpawn -= SubscribeBallHandler;
    }
}
