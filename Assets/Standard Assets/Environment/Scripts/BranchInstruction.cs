using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;

namespace LSys
{
    public class BranchInstruction : TurtleInstruction
    {
        public float breath { get; set; }

        private float baseRadius = UnityEngine.Random.Range(0.4f, 0.6f); // 0.45f
        private int stemCount = 0;
        private float radiusShrinkRatio = 0.88f;
        private float radiusMaxShrink = 30;
        private int straightUntil = 10; // 20
        private float twistMax = 30.0f; // 10 for no trunk straight
        private Vector3 twistVector;
        private float zagMax = 0.0f;
        private Vector3 zagVector;
        private Quaternion twistQuaternion;// = Quaternion.identity;
        private float height = UnityEngine.Random.Range(13.0f, 20.0f);
        private int stemsUntilSwitch = 5; // 20

        public BranchInstruction()
        {
            twistVector = new Vector3(UnityEngine.Random.Range(-twistMax, twistMax),
                                          UnityEngine.Random.Range(-twistMax, twistMax),
                                          UnityEngine.Random.Range(-twistMax, twistMax));
            twistVector *= 1 + breath;

            zagVector = new Vector3(UnityEngine.Random.Range(-zagMax, zagMax),
                                          UnityEngine.Random.Range(-zagMax, zagMax),
                                          UnityEngine.Random.Range(-zagMax, zagMax));
        }

        override
        public void Perform(Turtle turtle)
        {
            float branchLength = height / turtle.heightInStems;
            float oldRadius = turtle.state.currentRadius;

            turtle.state.currentRadius = baseRadius * Mathf.Pow(radiusShrinkRatio, turtle.ratios[stemCount] * radiusMaxShrink);

            //if (turtle.state.extentCount < straightUntil) twistQuaternion = Quaternion.identity;

            MeshBuilder cylinder = Cylinder.Mesh(oldRadius, turtle.state.currentRadius, branchLength, 4, twistQuaternion);

            cylinder.Rotate(turtle.state.dir);
            for (int i = 0; i < Cylinder.verticesToBeUntwisted.Count; i++)
            {
                cylinder.vertices[Cylinder.verticesToBeUntwisted[i]] = Quaternion.Inverse(twistQuaternion) * cylinder.vertices[Cylinder.verticesToBeUntwisted[i]];
            }

            for (int i = 0; i < Cylinder.normalsToBeUntwisted.Count; i++)
            {
                cylinder.normals[Cylinder.normalsToBeUntwisted[i]] = Quaternion.Inverse(twistQuaternion) * cylinder.normals[Cylinder.normalsToBeUntwisted[i]];
            }

            cylinder.Translate(turtle.state.pos);
            turtle.meshGenerator.Add(cylinder);
            turtle.MoveForward(branchLength);

            if (turtle.state.extentCount % stemsUntilSwitch == 0)
            {
                twistVector = -twistVector;
                zagVector = new Vector3(-zagVector.x, zagVector.y, -zagVector.z);
            }

            if (turtle.state.extentCount < straightUntil)
            {
                if (turtle.state.extentCount % stemsUntilSwitch != 0)
                {
                    twistQuaternion = Quaternion.identity;
                } else
                {
                    twistQuaternion = Quaternion.Euler(zagVector);
                }
            } else
            {
                twistQuaternion = Quaternion.Euler(twistVector);
            }

            turtle.state.dir = twistQuaternion * turtle.state.dir;

            stemCount++;
            turtle.state.extentCount++;
        }
    }

}
