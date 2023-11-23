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
You state the type of data you need as parameters for your user actions, and Ummi handles the fusion.

For a class to be a usable as a parameter type, you need at least one modality processor to instantiate that class based on the modality itself. 

### Writing multimodal interfaces

Creating any multimodal interface with Ummi begins by declaring a C# class that implements `MMInterface`.

The `MMInterface` is used to register one (or more) sets of multimodal actions that a user should be capable of performing. Each user action is grouped within a set of actions, where a set is a class that needs to be both `public` and `static`, capable of grouping as many actions as needed. Each action is written as a `public` and `static` method, and it is registered with the `UserAction` annotation. The `UserAction` annotation takes a string parameter, which represents the typical sentence you would want your user to say in order to execute the associated action.

Once declared, a set of actions must be registered by adding the type of its class to the `UserActions` list inside the `Start` method of a `MMInterface` implementation.

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
As a Unity developer consulting for a furniture company, you have been tasked with creating an application that showcases the capabilities of your employer's headset in terms of Multimodal Interaction.
Recognizing the limitation faced by the company's clients, who can currently only view products through a traditional website, the application should enable clients to visualize furniture items through augmented reality.

### Interface description

To expedite the development process, you've chosen to utilize the Ummi fusion engine to demonstrate the user interface you intend to make. 
Users of the applicaton should be capable of doing actions described below, through Ummi actions. 

1. **Show the menu:**
   - Allow users to have access to a menu containing a list of furniture  items available for sale. 

2. **Hide the menu:**
   - Allow users to hide the menu containing the list of furniture items.

3. **Sort menu items:**
   - Allow users to reorganize items within the menu. They should be able to sort the items by price or by name. 

4. **Add an item from the menu around the user:**
   - Allow users to select an item within the menu and place it inside the environment.

5. **Move an item around:**
   - Allow users to move an item from one point to another within the environment.

6. **Invert the position of two items:**
   - Allow users to switch the position of two items within the environment.

7. **Delete an item:**
   - Allow users to delete an item from those that were previously added to the environment.


<!-- 6. **Add an item to user's Basket:**
   - Allow users to add an item to their basket.

7. **Show the basket:**
   - Allow users to display their basket.

8. **Buy the basket's products:**
   - Allow users to complete the purchase of items in their basket.  -->


### Types at your disposal
As you are not alone on this project, a colleague of yours will write the processors in charge of mapping modalities events to types. You can use these types freely for your Multimodal Interface. No other types will be emitted from the modalities processors. They are the following: 
- `Sort`:
    - `SortByPrice` which extends `Sort`,
    - `SortByName` which extends `Sort`,
- `Item` which represents any piece of furniture, 
- `Vector3` which represents a point in space.  


### Getting Started

Create a C# file within the `UmmiExperiment` folder and write the methods signatures (along with their required annotations) but ommit their body. 
You can run the following command to check wether or not your methods have been registered:
```bash
dotnet run
```
Once you are done (i.e. each method you declared was recognised by the program), you can save your file and call the experimenter to check if it is ok.

Then, answer the following [form](https://forms.gle/hHYb9UhvJNMWJzKf9).  
