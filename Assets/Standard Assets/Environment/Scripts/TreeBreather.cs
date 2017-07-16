using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSys
{
    public class TreeBreather : MonoBehaviour
    {
        bool breathing = false;
        float animationLength = 3.0f;
        float timePassed;

        // Use this for initialization
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timePassed += Time.deltaTime;

            if (timePassed >= animationLength)
            {
                breathing = false;
            }

            if (breathing)
            {
                float breath = timePassed / animationLength;
                breath = Mathf.SmoothStep(0.0f, 1.0f, -Mathf.Abs(breath * 2.0f - 1.0f) + 1.0f);
                GetComponent<TreeGenerator>().DoTurtle(breath);
            }
        }

        public void StartBreathing()
        {
            if (!breathing)
            {
                breathing = true;
                timePassed = 0;
            }
        }
    }

}
