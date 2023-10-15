using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> where T : class
{
    void OnEnter(T genericT);
    void OnExecute(T genericT);
    void OnExit(T genericT);
}
