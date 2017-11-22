using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSys
{
    public class SaveInstruction : TurtleInstruction
    {

        override
        public void Perform(Turtle turtle)
        {
            turtle.previousStates.Add(new TurtleState(turtle.state.pos,
                                  turtle.state.dir,
                                  turtle.state.currentRadius,
                                  turtle.state.extentCount));
        }
    }

}
