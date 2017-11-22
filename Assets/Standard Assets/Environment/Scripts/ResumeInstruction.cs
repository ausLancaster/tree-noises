using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSys
{
    public class ResumeInstruction : TurtleInstruction
    {

        override
        public void Perform(Turtle turtle)
        {
            turtle.state = turtle.previousStates[turtle.previousStates.Count - 1];
            turtle.previousStates.RemoveAt(turtle.previousStates.Count - 1);
        }
    }

}
