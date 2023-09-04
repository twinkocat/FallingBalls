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


        public void Init()
        {
            InitializeProperties();
            BindProperties();
        }

        private void InitializeProperties()
        {
            _currentScoreProperty = new ReactiveProperty<int>();
            _topScoreProperty = new ReactiveProperty<int>();
            _healthScoreProperty = new ReactiveProperty<int>();
        }
        private void BindProperties()
        {
            _currentScoreProperty.Subscribe(value => _currentScore.text = value.ToString());
            _topScoreProperty.Subscribe(value => _topScore.text = value.ToString());
            _healthScoreProperty.Subscribe(value => _healthScore.text = value.ToString());
        }

        private void OnDestroy()
        {
            _currentScoreProperty.Unsubscribe();
            _topScoreProperty.Unsubscribe();
            _healthScoreProperty.Unsubscribe();
        }
        
        public void SetCurrentScore(int value) => _currentScoreProperty.Value = value;
        public void SetTopScore(int value)     => _topScoreProperty.Value     = value;
        public void SetHealthScore(int value)  => _healthScoreProperty.Value  = value;
    }
}