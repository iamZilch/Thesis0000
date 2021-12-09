using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stage1GivenHandler : MonoBehaviour
{
    public static Stage1GivenHandler instance;
    List<string> StringGiven = new List<string>();
    List<string> IntGiven = new List<string>();
    List<string> FloatGiven = new List<string>();
    List<string> BoolGiven = new List<string>();
    List<string> CharGiven = new List<string>();

    List<string> DoubGiven = new List<string>();

    private void Start()
    {
        // StringGiven.Add("Hello");
        // StringGiven.Add("Fernan");
        // StringGiven.Add("I am a integer");
        // StringGiven.Add("Float");
        // StringGiven.Add("number variable");
        // StringGiven.Add("Not a string");
        // StringGiven.Add("Null");
        // StringGiven.Add("Message");
        // StringGiven.Add("Baguio");
        // StringGiven.Add("PNC");
        // StringGiven.Add("World of war");
        // StringGiven.Add("Graudation");
        // StringGiven.Add("character");
        // StringGiven.Add("Programming Language");
        // StringGiven.Add("PNC");
        // StringGiven.Add("Ethics");
        // StringGiven.Add("Nino");
        givenGenerator();
        instance = this;
    }

    public Stage1GivenHandler ReturnInstance()
    {
        return instance;
    }

    public string GenerateString()
    {

        return StringGiven[Random.Range(0, StringGiven.Count)];
    }

    public string GenerateInt()
    {
        return IntGiven[Random.Range(0, IntGiven.Count)];
    }
    public string GenerateDouble()
    {
        return DoubGiven[Random.Range(0, DoubGiven.Count)];
    }

    public string GenerateFloat()
    {
        return FloatGiven[Random.Range(0, FloatGiven.Count)];
    }

    public string GenerateBool()
    {
        return BoolGiven[Random.Range(0, BoolGiven.Count)];
    }

    public string GenerateChar()
    {
        return CharGiven[Random.Range(0, CharGiven.Count)];
    }

    public void givenGenerator()
    {
        FloatGiven.Add("The price of my favorite game 'Tux: Coding Penguin', which is $5.25.");
        FloatGiven.Add("A percentage value.");
        IntGiven.Add("Number of lives in a game.");
        IntGiven.Add("Pages in a Book");
        BoolGiven.Add("((True && False) || True)");
        BoolGiven.Add("!True");
        StringGiven.Add("Name of a student");
        StringGiven.Add("Name of a city");
        CharGiven.Add("Your middle initial");
        CharGiven.Add("String is composed of what data type of array?");
        DoubGiven.Add("This is Double");
    }
}
