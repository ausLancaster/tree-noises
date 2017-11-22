using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;
using UnityEngine.Profiling;

namespace LSys
{

    public class Turtle
    {
        public MeshBuilder meshGenerator;
        public TurtleState state;
        public List<TurtleState> previousStates;

        private TurteInstructionSet instructionSet;
        public List<float> ratios;
        public int heightInStems;
        private float branchAngleExtraX = UnityEngine.Random.Range(-40.0f, 40.0f);
        private float branchAngleExtraZ = UnityEngine.Random.Range(-40.0f, 40.0f);

        public Turtle(TurteInstructionSet instructionSet)
        {
            this.instructionSet = instructionSet;
            meshGenerator = new MeshBuilder();
            state = new TurtleState(new Vector3(0, 0, 0),
                                    Quaternion.identity,
                                    0.4f, 0);
            previousStates = new List<TurtleState>();

        }

        public void RenderSymbols(LinkedListA<string> ll)
        {
            CountRatios(ll);

            LinkedListNodeA<string> currentNode;
            for (currentNode = ll.first; currentNode != null; currentNode = currentNode.next)
            {
                instructionSet.Perform(currentNode.item[0]);
            }
        }

        private void CountRatios(LinkedListA<string> ll)
        {

            List<List<int>> lengths = new List<List<int>>();
            int i = 0;

            lengths.Add(new List<int>());
            lengths[i].Add(0);

            LinkedListNodeA<string> currentNode;
            for (currentNode = ll.first; currentNode != null; currentNode = currentNode.next)
            {
                if (currentNode.item == "A")
                {
                    lengths[i][lengths[i].Count-1]++;
                } else if (currentNode.item == "[")
                {
                    i++;
                    if (i >= lengths.Count) lengths.Add(new List<int>());
                    lengths[i].Add(0);
                } else if (currentNode.item == "]")
                {
                    i--;
                }
            }

            ratios = new List<float>();
            List<float> bases = new List<float>();
            List<float> counts = new List<float>();
            List<int> islands = new List<int>();
            bases.Add(0);
            counts.Add(0);
            islands.Add(0);

            for (currentNode = ll.first; currentNode != null; currentNode = currentNode.next)
            {
                if (currentNode.item == "A")
                {
                    ratios.Add(counts[i] += (1-bases[i]) / lengths[i][islands[i]]);
                }
                else if (currentNode.item == "[")
                {
                    i++;
                    if (i >= bases.Count)
                    {
                        islands.Add(0);
                        bases.Add(counts[i - 1]);
                        counts.Add(counts[i - 1]);
                    } else
                    {
                        islands[i]++;
                        bases[i] = counts[i - 1];
                        counts[i] = counts[i - 1];
                    }

                }
                else if (currentNode.item == "]")
                {
                    i--;
                }
            }

            heightInStems = lengths[0][0];

            return;

        }


        public void MoveForward(float length)
        {
            state.pos += (state.dir * Vector3.up) * length;
        }

        public void Draw(MeshBuilder mg)
        {
            mg.Rotate(state.dir);
            mg.Translate(state.pos);
            meshGenerator.Add(mg);
        }
    }
    
    public class TurtleState
    {
        public Vector3 pos;
        public Quaternion dir;
        public float currentRadius;
        public int extentCount;

        public TurtleState(Vector3 pos, Quaternion dir, float currentRadius, int extentCount)
        {
            this.pos = pos;
            this.dir = dir;
            this.currentRadius = currentRadius;
            this.extentCount = extentCount;
        }
    }
}