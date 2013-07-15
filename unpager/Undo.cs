using System;
using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApplication1 {
    class UndoPopException : Exception {
        public UndoPopException(string message)
            : base(message) {
        }
    }

    class Undo {
        static List<Bitmap> steps = new List<Bitmap>();

        static public void push(Bitmap a) {
            steps.Add(new Bitmap(a));
        }

        static public Bitmap pop() {
            if (steps.Count > 0) {
                Bitmap a = steps[steps.Count - 1];
                steps.RemoveAt(steps.Count - 1);
                return a;
            } else {
                throw new UndoPopException("Undo stack is empty");
            }
        }

        static public void clear() {
            steps.Clear();
        }
    }
}
