using System.Numerics;

public class MMInterfaceExample : MMInterface
{
    public override void Start()
    {
        Interfaces.Add(typeof(PutThatThereInterface));
    }

    public static class PutThatThereInterface
    {
        [UserAction("Create a cube there")]
        public static void CreateCube(Vector3 there)
        {
            Console.WriteLine($"Adding a cube over {there}...");
        }

    }

}

