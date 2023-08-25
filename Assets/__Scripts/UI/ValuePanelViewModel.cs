using UnityEngine;
using UnityEngine.UI;

namespace twinkocat
{
    public class ValuePanelViewModel : MonoBehaviour
    {
        [SerializeField] private Text           _currentScore;
        [SerializeField] private Text           _topScore;
        [SerializeField] private Text           _healthScore;

        private ReactiveProperty<int>           _currentScoreProperty;
        private ReactiveProperty<int>           _topScoreProperty;
        private ReactiveProperty<int>           _healthScoreProperty;


        private void Awake()
        {
            _currentScoreProperty = new ReactiveProperty<int>();
            _topScoreProperty     = new ReactiveProperty<int>();
            _healthScoreProperty  = new ReactiveProperty<int>();

            _currentScoreProperty.OnValueChanged += OnCurrentScoreChanged;
            _topScoreProperty.OnValueChanged     += OnTopScoreChanged;
            _healthScoreProperty.OnValueChanged  += OnHealthScoreChanged;
        }

        private void OnCurrentScoreChanged(int value)   => _currentScore.text = value.ToString();
        private void OnTopScoreChanged(int value)       => _topScore.text = value.ToString();
        private void OnHealthScoreChanged(int value)    => _healthScore.text = value.ToString();
        
        public void SetCurrentScore(int value) => _currentScoreProperty.Value = value;
        public void SetTopScore(int value)     => _topScoreProperty.Value     = value;
        public void SetHealthScore(int value)  => _healthScoreProperty.Value  = value;

        private void OnDestroy()
        {
            _currentScoreProperty.OnValueChanged -= OnCurrentScoreChanged;
            _topScoreProperty.OnValueChanged     -= OnTopScoreChanged;
            _healthScoreProperty.OnValueChanged  -= OnHealthScoreChanged;
        }
    }
}