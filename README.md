# Ummi Usability Testing

This repository is part of the [Ummi](https://github.com/tsepton/ummi) project. 

## Getting started
### Requirements
- [.NET SDK](https://learn.microsoft.com/en-us/dotnet/core/install/) (version 7)

Just run the following if you are on linux (and use apt): 
```bash
sudo apt update && sudo apt install dotnet7
```


### Environment 
Use the following:
- [Visual Studio Code](https://visualstudio.microsoft.com/) 
- [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) extension

### Instructions
Use the following to run the program and see its output.
```bash
git clone https://github.com/tsepton/ummi_usability.git
cd ummi_usability/UmmiExperiment
dotnet run
```

## Documentation

### Core concepts
With Ummi, a multimodal interface is decomposed as sets of user actions.
As Ummi uses the Object-Oriented paradigm in order to model and abstract multimodal interaction, you can think of any multimodal interface as being a class, while multimodal user actions are methods.   

With Ummi, you don't need to think about specifying the modalities you want to use, and how to handle their data, while writing a multimodal interface. 
You state the type of data you need as parameter for your user actions, and Ummi handles the fusion.

For a class to be a usable as a parameter type, you need at least one modality processor to instantiate that class based on the modality itself. 

### Writing multimodal interfaces

Creating any multimodal interface with Ummi begins by declaring a C# class that implements the `MMInterface`.

The `MMInterface` is used to register one (or more) sets of multimodal actions that a user should be capable of performing. Each user action is grouped within a set of actions, where a set is a class that needs to be both `public` and `static`, capable of grouping as many actions as needed. Each action is written as a `public` and `static` method, and it is registered with the `UserAction` annotation. The `UserAction` annotation takes a string parameter, which represents the typical sentence you would want your user to say in order to execute the associated action.

Once declared, a set of actions must be registered by adding the type of its class to the `Actions` list inside the `Start` method of a `MMInterface` implementation.

### Put-that-there example
The following code illustrates how you can use Ummi to recreate Bolt's put-that-there example:

```C#
public class MMInterfaceExample : MMInterface {

    public override void Start() {
        UserActions.Add(typeof(PutThatThereActions));
    }

    public static class PutThatThereActions {

        [UserAction("Create a cube there")]
        public static void CreateCubeThere(Vector3 there) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = there;
        }
        
        [UserAction("Put that there")]
        public static void PutThatThere(GameObject that, Vector3 there) {
            that.transform.position = there;
        }
    }
}
```

## Instructions
TODO