using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Object = System.Object;



public class Program
{
    static void Main()
    {
        var interfaces = new List<Type>();

        var mminterfaces = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(type => typeof(MMInterface).IsAssignableFrom(type) && type != typeof(MMInterface))
            .Select(c => Activator.CreateInstance(c) as MMInterface);

        foreach (var item in mminterfaces)
        {
            if (item == null) continue;
            item.Start();
            interfaces.AddRange(
                item.UserActions
                    .Where(type => type.IsAbstract && type.IsSealed)
                    .Where(type => type.IsNestedPublic || type.IsPublic)
            );
        }

        var parser = new AttributeParser(interfaces.ToArray());
        Console.WriteLine($"Number of registered user action: {parser.Methods.Length}");
        foreach (var item in parser.Methods)
        {
            Console.WriteLine(item);
        }
    }
}


public class AttributeParser
{
    private readonly RegisteredMMIMethod[] _methods;

    public RegisteredMMIMethod[] Methods => _methods;
    public RegisteredMMIMethod[] AbstractMethods => _methods.Where(m => m.Info.IsAbstract).ToArray();
    public RegisteredMMIMethod[] StaticMethods => _methods.Where(m => m.Info.IsStatic).ToArray();
    public RegisteredMMIMethod[] ConcreteMethods => _methods.Where(m => !m.Info.IsAbstract).ToArray();

    public AttributeParser(Type[] classes)
    {
        var extractedMethods = new List<MethodInfo>();
        foreach (var item in classes)
        {
            var filteredM = item.GetMethods()
                .Where(method => method.GetCustomAttributes<UserAction>() != null)
                .Where(method => method.IsPublic)
                .Where(method => method.ReturnType == typeof(void))
                .Where(method => method.GetParameters()
                    .All(p => typeof(TypesForExperiment).IsAssignableFrom(p.ParameterType))
                );


            extractedMethods.AddRange(filteredM);
        }

        List<RegisteredMMIMethod> methods = new List<RegisteredMMIMethod>();
        foreach (var method in extractedMethods)
        {
            var attribute = method.GetCustomAttribute<UserAction>();
            if (attribute == null) continue;
            string[] utters = attribute.Utterances;
            methods.Add(new RegisteredMMIMethod(method, utters));
        }

        _methods = methods.ToArray();
    }

    public class RegisteredMMIMethod
    {
        public MethodInfo Info { get; }
        public string[] Utters { get; }


        public RegisteredMMIMethod(MethodInfo method, string[] utters)
        {
            Info = method;
            Utters = utters;
        }

        public override string ToString()
        {
            return $"Registered UserAction: {Info.Name}";
        }
    }
}
