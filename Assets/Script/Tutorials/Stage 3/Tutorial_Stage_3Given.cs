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
            "if(3 > 1 && || 1 != 2)" +
            "\n  Answer = ?"
            ,

            "if(False || True)" + //this
            "\n\t print(True && True)" +
            "\nelse" +
            "\n\t print(False && True)",

            "if(True || (True && False))" + //this
            "\n\t print((False && True) || (True || False))" +
            "\nelse" +
            "\n\t print(False || True)",

            "if(False && !True)" +
            "\n\t print(!False && True)" +
            "\nelse if(False || (True && False))" +
            "\n\t print(True && True)" +
            "\nelse" +
            "\n\t print(!False || !True)",

            "if(False && True)" +
            "\n\t print(False && False)" +
            "\nelse if((True && True) || False)" + //this
            "\n\t print(!False)" +
            "\n else" +
            "\n\t print(!True && False)",

            "if(True || !False)" + //this
            "\n\t print(False || (True && !False))" +
            "\nelse" +
            "\n\t print(True && True)"

        };

        FalseGiven = new List<string>
        {
             "if(6 == 6 && || !(1 == 1))" +
            "\n  Answer = ?",

            "if(False || !False)" + //this
            "\n\t print(False && (True || False))" +
            "\n else" +
            "\n\t print(True || (False || True))",

            "if(True && (False || !True))" +
            "\n\t print(False || False)" +
            "\n else" + //this
            "\n\t print(True && (True && !True))",

            "if(True && !(True || False))" +
            "\n\t print(True || False)" +
            "\n else if(!True || (True && False))" +
            "\n\t print(!False && False)" +
            "\n else" + //this
            "\n\t print(!True && !(True || !False))",

            "if(False || False)" +
            "\n\t print(True || !True)" +
            "\n else if(False || True)" + //this
            "\n\t print(!False && (True || !False))" +
            "\n else" +
            "\n\t print(!False)",

            "if(True && (!True || !False))" + //this
            "\n\t print(!True || (False && True))" +
            "\n else" +
            "\n\t print(!False || True)",

            "if(False || !True)" +
            "\n\t print(!False || True)" +
            "\n else if(True && (!False || True))" + //this
            "\n\t print(!True && !False)" +
            "\n else" +
            "\n\t print(!True)"
        };
    }

}
