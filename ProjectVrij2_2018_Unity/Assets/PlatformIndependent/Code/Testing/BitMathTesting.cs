using System;
using System.Collections.Generic;
using UnityEngine;


public class BitMathTesting : MonoBehaviour {

    private void Start() {

        MonoBehaviour.print(2 << 1);
        MonoBehaviour.print(4 << 1);
        MonoBehaviour.print(5 << 1);

        MonoBehaviour.print("5 is odd: " + ((5 & 1) == 1));
        MonoBehaviour.print("6 is odd: " + ((6 & 1) == 1));

        MonoBehaviour.print("(" + 0x3D.ToString("X2") + ") 0x3D = " + 0x3D);

        /*
        int loopCount = 100000000;

        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        bool result = false;
        int numToCheck = 7;
        stopwatch.Start();
        for(int i=0; i<loopCount; i++){
            result = ((numToCheck & 1) == 1);
        }
        MonoBehaviour.print("ellapsed: " + stopwatch.ElapsedMilliseconds);
        MonoBehaviour.print(numToCheck + " is odd method1: " + result);
        stopwatch.Stop();
        stopwatch.Reset();
        stopwatch.Start();
        for(int i = 0; i < loopCount; i++) {
            result = ((numToCheck % 2) == 1);
        }
        MonoBehaviour.print("ellapsed: " + stopwatch.ElapsedMilliseconds);
        MonoBehaviour.print(numToCheck + " is odd method2: " + result);
        stopwatch.Stop();
        stopwatch.Reset();
        */

    }

}

