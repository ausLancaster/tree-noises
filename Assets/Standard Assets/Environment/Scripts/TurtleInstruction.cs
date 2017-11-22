using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSys
{
    public abstract class TurtleInstruction
    {
        public Turtle turtle { get;  set; }

        public abstract void Perform(Turtle turtle);

    }
}
