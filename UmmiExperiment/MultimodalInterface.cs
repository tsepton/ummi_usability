[AttributeUsage(validOn: AttributeTargets.Method)]
public class UserAction : Attribute
{
    public string[] Utterances { get; }

    public UserAction(string utterance)
    {
        Utterances = new[] { utterance };
    }

    public UserAction(string[] utterances)
    {
        Utterances = utterances;
    }
}

