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
        // interfaces.Add(typeof(MMInterfaceExample.PutThatThereInterface));

        var mminterfaces = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(typeof(MMInterface).IsAssignableFrom)
            .Select(c => Activator.CreateInstance(c) as MMInterface);

        foreach (var item in mminterfaces)
        {
            if (item == null) continue;
            item.Start();
            interfaces.AddRange(item.Interfaces);
        }


        var parser = new AttributeParser(interfaces.ToArray());

        Console.WriteLine($"Number of registered user action: {parser.Methods.Length}");
        foreach (var item in parser.Methods)
        {
            Console.WriteLine(item);
        }
    }
}




; public class AttributeParser
{
    private readonly RegisteredMMIMethod[] _methods;

    public RegisteredMMIMethod[] Methods => _methods;
    public RegisteredMMIMethod[] AbstractMethods => _methods.Where(m => m.Info.IsAbstract).ToArray();
    public RegisteredMMIMethod[] StaticMethods => _methods.Where(m => m.Info.IsStatic).ToArray();
    public RegisteredMMIMethod[] ConcreteMethods => _methods.Where(m => !m.Info.IsAbstract).ToArray();

    public AttributeParser(Type[] classes)
    {
        //   var extractedMethods = TypeCache
        // .GetMethodsWithAttribute<UserAction>()
        // .Where(method => classes.Contains(method.DeclaringType));

        var extractedMethods = new List<MethodInfo>();
        foreach (var item in classes)
        {
            extractedMethods.AddRange(item.GetMethods().Where(method => method.GetCustomAttributes<UserAction>() != null));
        }

        List<RegisteredMMIMethod> methods = new List<RegisteredMMIMethod>();
        var x = extractedMethods
          .Where(m => !m.IsPrivate)
          .Where(m => m.ReturnType == typeof(void))
          .Where(m => !m.IsAbstract)
          .Where(m => m.IsStatic);
        foreach (var method in x)
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
