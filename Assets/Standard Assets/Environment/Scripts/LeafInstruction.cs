using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSys
{
    public class LeafInstruction : TurtleInstruction
    {
        public float height = 0.2f;
        public float width = 0.07f;

        override
        public void Perform(Turtle turtle)
        {
            turtle.Draw(Geometry.Leaf.Mesh(height, width));
        }
    }

}
