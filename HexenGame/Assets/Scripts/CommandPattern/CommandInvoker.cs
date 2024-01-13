using System.Collections;
using System.Collections.Generic;

namespace CommandPattern
{
    public class CommandInvoker
    {
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
        public void Undo()
        {
            //TODO
        }
        public void Redo()
        {
            //TODO
        }
    }
}
