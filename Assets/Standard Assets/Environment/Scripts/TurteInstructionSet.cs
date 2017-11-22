using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometry;

namespace LSys {

    public class TurteInstructionSet
    {
        private Turtle turtle;
        private Dictionary<char, TurtleInstruction> instructions;

        public TurteInstructionSet()
        {
            instructions = new Dictionary<char, TurtleInstruction>();
            turtle = new Turtle(this);
        }

        public void AddInstruction(char symbol, TurtleInstruction instruction)
        {
            instructions[symbol] = instruction;
            instruction.turtle = this.turtle;
        }

        public void Perform (char symbol)
        {
            if (instructions.ContainsKey(symbol))
            {
                instructions[symbol].Perform(turtle);
            }
        }
        
        public void RenderSymbols(LinkedListA<string> ll)
        {
            turtle.RenderSymbols(ll);
        }

        public MeshBuilder GetMesh()
        {
            return turtle.meshGenerator;
        }
    }
}


