using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LSys
{
    public class LSystem
    {

        string axiom;
        Dictionary<string, List<Rule>> grammar;

        public LSystem()
        {
            axiom = "FX";
            grammar = new Dictionary<string, List<Rule>>();
            grammar["X"] = new List<Rule>();
            grammar["X"].Add(new Rule(1.0f, "[-FX][+FX]"));
        }

        public static LinkedListA<string> StringToLL(string inputString)
        {
            LinkedListA<string> result = new LinkedListA<string>();
            for (int i = 0; i < inputString.Length; i++)
            {
                result.Enqueue(inputString[i].ToString());
            }
            return result;
        }

        public static string LLToString(LinkedListA<string> linkedList)
        {

            string result = "";
            for (LinkedListNodeA<string> c = linkedList.first; c != null; c = c.next)
            {
                result += c.item;
            }
            return result;
        }

        public LinkedListA<string> DoIterations(int n)
        {
            LinkedListA<string> ll = StringToLL(axiom);
            for (int i = 0; i < n; i++)
            {
                for (LinkedListNodeA<string> c = ll.first; c != null; c = c.next)
                {
                    replaceNode(ll, c);
                }
            }

            return ll;
        }

        private void replaceNode(LinkedListA<string> ll, LinkedListNodeA<string> node)
        {
            if (Regex.IsMatch(node.item, @"^[a-zA-Z]+$"))
            {
                if (grammar.ContainsKey(node.item))
                {
                    List<Rule> rules = grammar[node.item];
                    float totalProbability = 0;

                    // roll dice against each rule
                    for (var i = 0; i < rules.Count; i++)
                    {
                        totalProbability += rules[i].probability;

                        if (totalProbability >= UnityEngine.Random.value)
                        {
                            // apply rule
                            ll.Replace(node, StringToLL(rules[i].str));
                        }
                    }
                }
            }
        }
    }
}

public class Rule
{
    public float probability;
    public string str;

    public Rule(float probability, string str)
    {
        this.probability = probability;
        this.str = str;
    }
}

public class LinkedListA<T>
{
    public LinkedListNodeA<T> first = null;
    public LinkedListNodeA<T> last = null;
    public int n = 0;

    public bool IsEmpty()
    {
        return first == null;
    }

    public void Enqueue(T item)
    {
        LinkedListNodeA<T> oldLast = last;
        last = new LinkedListNodeA<T>();
        last.item = item;
        last.next = null;
        if (IsEmpty())
        {
            first = last;
        }
        if (oldLast != null)
        {
            oldLast.next = last;
        }
        last.previous = oldLast;
        n++;
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            return default(T);
        }
        T item = first.item;
        first = first.next;
        n--;
        if (IsEmpty())
        {
            last = null;
        } else
        {
            first.previous = null;
        }
        return item;
    }

    private void Link(LinkedListNodeA<T> a, LinkedListNodeA<T> b)
    {
        a.next = b;
        b.previous = a;
    }

    // repalce node with linkedlistA
    public void Replace(LinkedListNodeA<T> node, LinkedListA<T> linkedList)
    {
        if (linkedList.IsEmpty()) return;
        if (node.previous == null)
        {
            first = linkedList.first;
        } else
        {
            Link(node.previous, linkedList.first);
        }
        if (node.next != null)
        {
            Link(linkedList.last, node.next);
        }
    }
}

public class LinkedListNodeA<T>
{
    public T item;
    public LinkedListNodeA<T> next;
    public LinkedListNodeA<T> previous;
    public LinkedListNodeA()
    {

    }
}