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
        private List<float> ratios;
        private int stemCount = 0;
        private int heightInStems;
        private float height = UnityEngine.Random.Range(4.0f, 7.0f);
        private float angle;

        private float baseRadius = 0.3f;
        private float radiusShrinkRatio = 0.88f;
        private float radiusMaxShrink = 30;
        private float baseLength;
        private float lengthShrinkRatio = 0.95f;

        public Turtle(Dictionary<string, Action> grammar, float angle)
        {
            meshGenerator = new MeshGenerator();
            state = new TurtleState(new Vector3(0, 0, 0),
                                    Quaternion.identity,
                                    baseRadius, baseLength);
            previousStates = new List<TurtleState>();
            this.angle = angle;

            this.grammar = new Dictionary<string, Action>();
            this.grammar["["] = () => SavePosition();
            this.grammar["]"] = () => ResumePosition();
            this.grammar["+"] = () => Rotate(0, 0, angle);
            this.grammar["-"] = () => Rotate(0, 0, -angle);
            this.grammar["|"] = () => Rotate(0, 0, 180);
            this.grammar["\\"] = () => Rotate(angle, 0, 0);
            this.grammar["/"] = () => Rotate(-angle, 0, 0);
            this.grammar["^"] = () => Rotate(0, angle, 0);
            this.grammar["&"] = () => Rotate(0, -angle, 0);
            this.grammar["#"] = () => RandomRotate();
            this.grammar["F"] = () => Stem();
            this.grammar["L"] = () => Leaf();
            this.grammar["A"] = () => CurvedStem();

        }

        public void RenderSymbols(LinkedListA<string> ll)
        {
            CountRatios(ll);

            LinkedListNodeA<string> currentNode;
            for (currentNode = ll.first; currentNode != null; currentNode = currentNode.next)
            {
                if (grammar.ContainsKey(currentNode.item))
                {
                    grammar[currentNode.item]();
                }
            }
        }

        private void CountRatios(LinkedListA<string> ll)
        {
            String str = LSystem.LLToString(ll); // TODO: delete

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
            baseLength = height / heightInStems;

            return;

        }

        private void SavePosition()
        {
            previousStates.Add(new TurtleState(state.pos,
                                              state.dir,
                                              state.currentRadius,
                                              state.currentLength));
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

        private void Draw(MeshGenerator mg)
        {
            mg.Rotate(state.dir);
            mg.Translate(state.pos);
            meshGenerator.Add(mg);
        }

        private void Stem()
        {
            float oldRadius = state.currentRadius;
            state.currentRadius = baseRadius * Mathf.Pow(radiusShrinkRatio, ratios[stemCount]*radiusMaxShrink);

            Draw(Cylinder.Mesh(oldRadius, state.currentRadius, baseLength, 4));
            MoveForward(baseLength);

            //state.currentLength *= lengthShrinkRatio;

            stemCount++;
        }

        private void Leaf()
        {
            Draw(Geometry.Leaf.Mesh(0.2f, 0.07f));
        }

        private void CurvedStem()
        {
            Stem();
        }

        private void RandomRotate()
        {
            float minBranchAngle = -90.0f;
            float maxBranchAngle = 90.0f;
            float x = UnityEngine.Random.Range(minBranchAngle, maxBranchAngle);
            float z = UnityEngine.Random.Range(minBranchAngle, maxBranchAngle);
            Rotate(x, 0.0f, z);
        }

    }

    public class TurtleState
    {
        public Vector3 pos;
        public Quaternion dir;
        public float currentRadius;
        public float currentLength;

        public TurtleState(Vector3 pos, Quaternion dir, float currentRadius, float currentLength)
        {
            this.pos = pos;
            this.dir = dir;
            this.currentRadius = currentRadius;
            this.currentLength = currentLength;
        }
    }
}