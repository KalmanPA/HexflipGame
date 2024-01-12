using System.Collections;
using System.Collections.Generic;


namespace StatePattern
{
    public interface IState
    {
        void OnEnter();// Happens once
        void Update();// Every frame
        void OnExit();// Happens once
    }
}
