using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;

namespace LSys
{

    public class Turtle
    {
        public MeshBuilder meshGenerator;

        private TurtleState state;
        private List<TurtleState> previousStates;
        private Dictionary<string, Action> grammar;
        private List<float> ratios;
        private int stemCount = 0;
        private int heightInStems;
        private float height = UnityEngine.Random.Range(6.0f, 13.0f); // was 4 - 7
        private float angle;
        private float baseRadius = 0.35f;
        private float radiusShrinkRatio = 0.88f;
        private float radiusMaxShrink = 30;
        private float branchLength;
        private float branchAngleExtraX = UnityEngine.Random.Range(-40.0f, 40.0f);
        private float branchAngleExtraZ = UnityEngine.Random.Range(-40.0f, 40.0f);
        private Vector3 twistVector = new Vector3(UnityEngine.Random.Range(-7.0f, 7.0f),
                                                  UnityEngine.Random.Range(-7.0f, 7.0f),
                                                  UnityEngine.Random.Range(-7.0f, 7.0f));
        private Quaternion twistQuaternion;
        private int stemsUntilSwitch = 10;
        private float breath;

        public Turtle(float breath)
        {
            this.breath = breath;
            //twistVector *= ((1-breath) + 0.35f); "straghtens out"
            twistVector *= 1+breath;
            meshGenerator = new MeshBuilder();
            state = new TurtleState(new Vector3(0, 0, 0),
                                    Quaternion.identity,
                                    baseRadius, 0);
            previousStates = new List<TurtleState>();

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
            this.grammar["L"] = () => Leaf();
            this.grammar["A"] = () => Branch();

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

            List<List<int>> lengths = new List<List<int>>();
            int i = 0;

            lengths.Add(new List<int>());
            lengths[i].Add(0);

            LinkedListNodeA<string> currentNode;
            for (currentNode = ll.first; currentNode != null; currentNode = currentNode.next)
            {
                if (currentNode.item == "A" || currentNode.item == "B")
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
                if (currentNode.item == "A" || currentNode.item == "B")
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
            branchLength = height / heightInStems;

            return;

        }

        private void SavePosition()
        {
            previousStates.Add(new TurtleState(state.pos,
                                              state.dir,
                                              state.currentRadius,
                                              state.extentCount));
        }

        private void ResumePosition()
        {
            state = previousStates[previousStates.Count - 1];
            previousStates.RemoveAt(previousStates.Count - 1);
        }

        private void Rotate(float x, float y, float z)
        {
            state.dir = Quaternion.Euler(x, y, z) * state.dir;
        }

        private void RandomRotate()
        {
            float minBranchAngle = -90.0f;
            float maxBranchAngle = 90.0f;
            float x = UnityEngine.Random.Range(minBranchAngle, maxBranchAngle);
            float z = UnityEngine.Random.Range(minBranchAngle, maxBranchAngle);
            Rotate(x + branchAngleExtraX*breath, 0.0f, z + branchAngleExtraZ*breath);
        }

        private void MoveForward(float length)
        {
            state.pos += (state.dir * Vector3.up) * length;
        }

        private void Draw(MeshBuilder mg)
        {
            mg.Rotate(state.dir);
            for (int i=0; i<Cylinder.verticesToBeUntwisted.Count; i++)
            {
                mg.vertices[Cylinder.verticesToBeUntwisted[i]] = Quaternion.Inverse(twistQuaternion) * mg.vertices[Cylinder.verticesToBeUntwisted[i]];
            }
            for (int i = 0; i < Cylinder.normalsToBeUntwisted.Count; i++)
            {
                mg.normals[Cylinder.normalsToBeUntwisted[i]] = Quaternion.Inverse(twistQuaternion) * mg.normals[Cylinder.normalsToBeUntwisted[i]];
            }
            mg.Translate(state.pos);
            meshGenerator.Add(mg);
        }

        private void Branch()
        {
            float oldRadius = state.currentRadius;
            state.currentRadius = baseRadius * Mathf.Pow(radiusShrinkRatio, ratios[stemCount] * radiusMaxShrink);

            Draw(Cylinder.Mesh(oldRadius, state.currentRadius, branchLength, 4, twistQuaternion));
            MoveForward(branchLength);


            if (state.extentCount % stemsUntilSwitch == 0)
            {
                twistVector = -twistVector;
                twistQuaternion = Quaternion.Euler(twistVector);
            }
            state.dir = twistQuaternion * state.dir;

            stemCount++;
            state.extentCount++;
        }

        private void Leaf()
        {
            Draw(Geometry.Leaf.Mesh(0.2f, 0.07f));
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