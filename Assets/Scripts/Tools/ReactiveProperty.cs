using System;

namespace Tools
{
    public class ReactiveProperty<T>
    {
        private T _value;
        public event Action OnChanged;
        public event Action<T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnChanged?.Invoke();
                    OnValueChanged?.Invoke(_value);
                }
            }
        }

        public ReactiveProperty()
        {
        }
        public ReactiveProperty(T initialValue)
        {
            _value = initialValue;
        }
    }
}