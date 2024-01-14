using System.Collections;
using System.Collections.Generic;

namespace CommandPattern
{
    public class CommandInvoker
    {
        Stack<ICommand> _undoStack = new Stack<ICommand>();

        Stack<ICommand> _redoStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();

            _undoStack.Push(command);
            _redoStack.Clear();

        }
        public void Undo()
        {
            if (_undoStack.Count == 0) return;
            ICommand command = _undoStack.Pop();
            command.Undo();

            _redoStack.Push(command);
        }
        public void Redo()
        {
            if (_redoStack.Count == 0) { return; }
            ICommand commandToRedo = _redoStack.Pop();
            commandToRedo.Execute();
            _undoStack.Push(commandToRedo);
        }
    }
}
