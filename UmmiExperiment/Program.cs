using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


public class MMIInterface
{
    static void Main()
    {
        Console.WriteLine("Working");
    }
    public List<Type> Interfaces = new();
}


[AttributeUsage(validOn: AttributeTargets.Method)]
public class MultimodalInterface : Attribute
{
    public string[] Utterances { get; }

    public MultimodalInterface(string utterance)
    {
        Utterances = new[] { utterance };
    }

    public MultimodalInterface(string[] utterances)
    {
        Utterances = utterances;
    }
}



public class Experiment : MMIInterface
{

    public void Start()
    {
        Interfaces.Add(typeof(TestInterface));
    }

    public static class TestInterface
    {
        [MultimodalInterface("Hi")]
        public static void Hi() { }

    }

}

