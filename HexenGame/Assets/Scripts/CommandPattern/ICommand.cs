using System.Collections;
using System.Collections.Generic;

namespace CommandPattern
{


    public interface ICommand
    {
        void Execute();

        void Undo();
    }
}
