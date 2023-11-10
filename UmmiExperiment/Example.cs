public class Experiment : MMInterface
{

    public void Start()
    {
        Interfaces.Add(typeof(TestInterface));
    }

    public static class TestInterface
    {
        [UserAction("Hi")]
        public static void Hi() { }

    }

}

