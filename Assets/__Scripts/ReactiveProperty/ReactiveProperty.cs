using System;

namespace twinkocat
{
    public class ReactiveProperty<T> where T : struct
    {
        private T _value;

        public event Action<T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}

