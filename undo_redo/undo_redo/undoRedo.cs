using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace undo_redo
{
    public class undoRedo<T>
    {
        private Stack<T> undoStack = new Stack<T>();
        private Stack<T> redoStack = new Stack<T>();

        public void addState(T state)
        {
            undoStack.Push(state);
            redoStack.Clear();
        }

        public T undo(T curState)
        {
            if(undoStack.Count == 0)
            {
                throw new InvalidOperationException("Tidak bisa undo!");
            }
            redoStack.Push(curState);
            return undoStack.Pop();
        }

        public T redo(T curState)
        {
            if(redoStack.Count == 0)
            {
                throw new InvalidOperationException("Tidak bisa redo!");
            }
            undoStack.Push(curState);
            return redoStack.Pop();
        }

        public bool canUndo => undoStack.Count > 0;
        public bool canRedo => redoStack.Count > 0;
    }
}
