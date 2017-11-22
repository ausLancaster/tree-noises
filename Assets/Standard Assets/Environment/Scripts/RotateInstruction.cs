using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSys
{
    public abstract class RotateInstruction : TurtleInstruction
    {

        private float x;
        private float y;
        private float z;
        private float max;
        private float min;

        public RotateInstruction(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        override
        public void Perform(Turtle turtle)
        {
            turtle.state.dir = Quaternion.Euler(x, y, z) * turtle.state.dir;
        }
    }

}
