using UnityEngine;

namespace PFramework
{
    [System.Serializable]
    public class PData<T>
    {
        [SerializeField] T _data;

        public T Data
        {
            get { return _data; }
            set
            {
                if (!_data.Equals(value))
                {
                    _data = value;
                    OnDataChanged?.Invoke(_data);
                }
            }
        }

        public event Callback<T> OnDataChanged;

        public PData(T defaultValue)
        {
            _data = defaultValue;
        }
    }
}