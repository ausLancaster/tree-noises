using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;

namespace LSys
{

    public class Turtle
    {
        public MeshGenerator meshGenerator;

        private TurtleState state;
        private List<TurtleState> previousStates;
        private Dictionary<string, Action> grammar;
        private float angle; // in degrees

        public Turtle(Dictionary<string, Action> grammar, float angle)
        {
            meshGenerator = new MeshGenerator();
            state = new TurtleState(new Vector3(0, 0, 0), Quaternion.identity);
            previousStates = new List<TurtleState>();

            this.grammar = new Dictionary<string, Action>();
            this.grammar["["] = () => SavePosition();
            this.grammar["]"] = () => ResumePosition();
            this.grammar["+"] = () => Rotate(0, 0, angle);
            this.grammar["-"] = () => Rotate(0, 0, -angle);
            this.grammar["F"] = () => Stem();

        }

        public void RenderSymbols(LinkedListA<string> ll)
        {
            LinkedListNodeA<string> currentNode;
            for (currentNode = ll.first; currentNode != null; currentNode = currentNode.next)
            {
                if (grammar.ContainsKey(currentNode.item))
                {
                    grammar[currentNode.item]();
                }
            }
        }

        private void SavePosition()
        {
            previousStates.Add(new TurtleState(state.pos, state.dir));
        }

        private void ResumePosition()
        {
            state = previousStates[previousStates.Count - 1];
            previousStates.RemoveAt(previousStates.Count - 1);
        }

        private void Rotate(float x, float y, float z)
        {
            state.dir *= Quaternion.Euler(x, y, z);
        }

        private void MoveForward(float length)
        {
            state.pos += (state.dir * Vector3.up) * length;
        }

        private void Stem()
        {
            float length = 0.5f;
            MeshGenerator cylinder = Cylinder.Mesh(0.02f, length, 4);
            cylinder.Rotate(state.dir);
            cylinder.Translate(state.pos);
            meshGenerator.Add(cylinder);
            MoveForward(length);
        }

    }

    public class TurtleState
    {
        public Vector3 pos;
        public Quaternion dir;

        public TurtleState(Vector3 pos, Quaternion dir)
        {
            this.pos = pos;
            this.dir = dir;
        }
    }
}