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

    private void Awake()
    {
        _ballsPool = new BallsPool(_ballPrefab, _isExpandable, _isActiveByDefault, _capacity, _ballsParent);

        BoundsCalculator.Init(Camera.main);


        _ballDeathTrigger.OnDeathTriggerBallEnter += _ballsPool.ReturnObject;

        _ballSpawner.Init(_ballsPool, _spawnDelay); 
    }

    private void PauseGame() => Time.timeScale = 0f;
    private void ResumeGame() => Time.timeScale = 1f;


    private void OnValidate()
    {
        _ballSpawner.SetSpawnDelay(_spawnDelay);
    }
}
