using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGOAP
{
    public interface IState
    {
        void SetState(string key, bool value);
        void GetValue(string key);

        void AddStateChangeListener(Action onChange);
    }

    public class State : IState
    {
        private Dictionary<string, bool> _dataTable;
        private Action _onChange;

        public State()
        {
            _dataTable = new Dictionary<string, bool>();
        }

        public void AddStateChangeListener(Action onChange)
        {
            _onChange = onChange;
        }

        public void GetValue(string key)
        {
            throw new System.NotImplementedException();
        }

        public void SetState(string key, bool value)
        {
            throw new System.NotImplementedException();
        }
    }
}

