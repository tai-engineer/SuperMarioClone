using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputController: MonoBehaviour
{
    [Serializable]
    public class InputButton
    {
        public bool Down { get; protected set; }
        public KeyCode _key;

        public InputButton(KeyCode key)
        {
            _key = key;
        }

        public void GetInput()
        {
            Down = Input.GetKeyDown(_key);
        }
    }

    [Serializable]
    public class InputAxis
    {
        public float Value { get; protected set; }
        public bool Down { get; protected set; }
        public KeyCode _negative;
        public KeyCode _positive;

        public InputAxis(KeyCode negative, KeyCode positive)
        {
            _negative = negative;
            _positive = positive;
        }

        public void GetInput()
        {
            bool negativeHeld = false;
            bool positiveHeld = false;

            negativeHeld = Input.GetKey(_negative);
            positiveHeld = Input.GetKey(_positive);

            if(negativeHeld == positiveHeld)
            {
                Value = 0f;
            }
            else if(negativeHeld)
            {
                Value = -1.0f;
            }
            else
            {
                Value = 1.0f;
            }

            Down = negativeHeld != positiveHeld;
        }
    }
}
