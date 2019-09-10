using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGOAP
{
    public interface IAction
    {
        void SetState(string key, bool value);
        void GetValue(string key);

        void AddStateChangeListener(Action onChange);
    }

    public class Action : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
