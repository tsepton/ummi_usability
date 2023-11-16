using System.Numerics;

public class MMInterfaceExample : MMInterface
{
    public override void Start()
    {
        UserActions.Add(typeof(PutThatThereActions));
    }

    public static class PutThatThereActions
    {
        [UserAction("Create a cube there")]
        public static void CreateCube(Vector3 there)
        {
            Console.WriteLine($"Adding a cube over {there}...");
        }
    }
}
