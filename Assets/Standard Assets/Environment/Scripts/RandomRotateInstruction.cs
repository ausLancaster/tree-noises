using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSys
{
    public class RandomRotateInstruction : TurtleInstruction
    {
        public float breath { get; set; }

        private float min;
        private float max;
        private float branchAngleExtraX;
        private float branchAngleExtraZ;


        public RandomRotateInstruction()
        {
            breath = 1;
            min = -90;
            max = 90;
        }

        public RandomRotateInstruction(float min, float max)
        {
            breath = 1;
            this.min = min;
            this.max = max;
        }

        override
        public void Perform(Turtle turtle)
        {
            float x = UnityEngine.Random.Range(min, max);
            float z = UnityEngine.Random.Range(min, max);
            Quaternion quat = Quaternion.Euler(x + branchAngleExtraX * breath, 0.0f, z + branchAngleExtraZ * breath);
            turtle.state.dir = quat * turtle.state.dir;
        }
    }

}
