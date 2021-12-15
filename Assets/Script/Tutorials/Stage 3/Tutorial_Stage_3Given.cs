using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial_Stage_3Given : MonoBehaviour
{
    public static List<string> TrueGiven = new List<string>();
    public static List<string> FalseGiven = new List<string>();

    public static void InitializeGiven()
    {
        TrueGiven = new List<string>
        {
            "if(True && False)" +
            "\n\t print((False || True) &&  False)",

            "if((3 < (6 + 2)) && True)" +
            "\n\t print true" +
            "\nelse" +
            "\n\tprint false",

            "if(!False && False)" +
            "\n\tprint false" +
            "\nelse" +
            "\n\tprint true",

            "if(5 <= (2 + 5))",

            "if((2 + 5) < (2 * 2))" +
            "\n\tprint false" +
            "\nelse" +
            "\n\tprint true",

            "if(True || !False)" + //this
            "\n\t print true" +
            "\nelse" +
            "\n\t print false"

        };

        FalseGiven = new List<string>
        {
            "if(False && False)" +
            "\n\t print(True && True)" +
            "\n else" + //this
            "\n\t print(!True)",

            "if(True && (!True || !False))" + //this
            "\n\t print(!True || (False && True))" +
            "\n else" +
            "\n\t print(!False || True)",

            "if((10 % 5) == 0)" +
            "\n\tprint true" +
            "\nelse" +
            "\n\tprint false",

            "if((2 * 2) > (2 + 2))" +
            "\n\tprint true" +
            "\nelse" +
            "\n\tprint false",

            "if(5 > 2)" +
            "\n\tprint false" +
            "\nelse" +
            "\n\tprint true"
        };
    }

}
