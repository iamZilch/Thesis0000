using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stage3GivenHandler
{
    public static List<string> TrueGiven = new List<string>();
    public static List<string> FalseGiven = new List<string>();

    public static void InitializeGiven()
    {
        TrueGiven = new List<string>
        {
            "if(True && False)" +
            "\n\t print((False || True) &&  False)" +
            "\nelse" + //this
            "\n\t print(True || (False && False))",

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
            "if(False && False)" +
            "\n\t print(True && True)" +
            "\n else" + //this
            "\n\t print(!True)",

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
