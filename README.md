# Hibzz.Dropl
![LICENSE](https://img.shields.io/badge/LICENSE-CC--BY--4.0-ee5b32?style=for-the-badge) [![Twitter Follow](https://img.shields.io/badge/follow-%40hibzzgames-1DA1f2?logo=twitter&style=for-the-badge)](https://twitter.com/hibzzgames) [![Discord](https://img.shields.io/discord/695898694083412048?color=788bd9&label=DIscord&style=for-the-badge)](https://discord.gg/YXdJ8cZngB) ![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

***A library used to execute instructions over a period of time***

The Deferred Runtime Operation Library (Dropl) provides users with a variety of tools and utilities to defer any operations such that they can be executed over time or at a later point in time.

## Installation
**Via Github**
This package can be installed in the Unity Package Manager using the following git URL.
```
https://github.com/hibzzgames/Hibzz.Dropl.git
```

This package additionally requires the [Hibzz.Singletons](https://github.com/hibzzgames/Hibzz.Singletons) package to be installed in the project as a dependency. Hopefully, 2023 is the year Unity finally adds support for git dependencies in the package manager. Until then, this package can be installed in the Unity Package Manager using the following git URL.
```
https://github.com/hibzzgames/Hibzz.Singletons.git
```

Alternatively, you can download the latest release from the [releases page](https://github.com/hibzzgames/Hibzz.Dropl/releases) and manually import the package into your project.

<br>

## Key Features
- Operations can be executed over time or at a later point in time
- Interpolation support for common easing functions + custom animation curves
- Sequence to string together multiple operations
- Lambda operation to execute any instructions without creating a new class
- Property operation to execute any property changes over time
- Flexible and customizable rules for when an operation should be executed and expired
- Built-in operations for common use cases
- Delay support for any operation
- Filter support to select operations based on a set of rules
- Customizable executer to control how operations are executed

<br>

## Usage
The Dropl library provides game developers with a variety of tools and utilities to defer any set of operations such that it can be executed over time or at a later point in time from the time of the request. This is useful for a variety of use cases such as UI animations, game logic, and more.

Detailed documentation can be found on [docs.hibzz.games](https://docs.hibzz.games/dropl/getting-started/), but here is a quick overview of the library.

There are two main components to the library, the `Operation`, and the `Executer`. The abstract `Operation` class is used to define how a set of instructions need to be executed over time and the `Executer` is used to manage the execution of the defined operations.

For example, let's take this simple operation that moves a `Transform` from its current position to a target position over some time.

```csharp
// move the current transform to the position (0, 5, 0) over 5 seconds using a linear interpolation
// additionally, dropl provides a variety of other interpolation methods to work with
var moveOperation = new MoveOperation(transform, expectedPosition: new vector3(0, 5, 0), duration: 5f, easing: Interpolations.LINEAR);
```

The next step would be to add this operation to an `Executer`. The users can either create custom `Executer`s or use the default `Executer` provided by the library.

```csharp
// add the defined operation to the default executer
Executer.DefaultExecuter.Add(moveOperation);

// or alternatively, use the helper method AddToDefaultExecuter
moveOperation.AddToDefaultExecuter();
```

<br>

The `MoveOperation` is one of the few built-in operations provided by the library for common use cases and reference. However, the real power of the library comes from the ability to create custom operations. Let's take a look at how we can create a custom operation.

```csharp
using Hibzz.Dropl;

// create a custom operation that handles the highlight of a given object and display the text of the object
public class HighlightOperation : Operation
{
    // set to this color to highlight
    Color targetColor;

    // store the current color (useful for interpolating from the current color to the target color)
    Color currentColor;

    // stores the target UI text of the object
    String targetText;

    // stores the current UI text of the object
    String currentText;


    public HighlightOperation (Highlightable highlightable, Color highlightColor, float duration) : base(target: highlightable)
    {
        ExpirationTime = duration;
        Easing = INTERPOLATIONS.IN_SINE;
        color = targetColor;
    }

    protected override void OnOperationStart()
    {
        currentColor = ((Highlightable)target).Color;
        currentText =  ((Highlightable)target).Text;
        targetText =   ((Highlightable)target).gameObject.name;
    }

    private string FancyStringInterpolation(float progress) 
    {
        // imagine some fancy string interpolation here from currentText to targetText using the progress
        string result = ... ;
        return result;
    }

    protected override void OnOperationTick(float progress) 
    {
        // interpolate the color from the current color to the target color
        ((Highlightable)target).Color = Color.Lerp(currentColor, targetColor, progress);

        // interpolate the text from the current text to the target text
        ((Highlightable)target).Text = FancyStringInterpolation(progress);
    }

    protected override void OnOperationEnd()
    {
        // set the final color and text, and mark the object as highlighted
        ((Highlightable)target).Color = targetColor;
        ((Highlightable)target).Text = targetText;
        ((Highlightable)target).isHighlighted = true;
    }
}

// now we can use this operation in the same way as the built-in operations
var highlightOperation = new HighlightOperation(highlightable, highlightColor: Color.red, duration: 5f);
highlightOperation.AddToDefaultExecuter();
```

<br>

The library additionally provides a variety of core operations that can be used to create more complex operations. 
- **Sequence** - Executes a set of operations in a sequence
- **LambdaOperation** - Executes any instructions provided by the user without creating a new class
- **PropertyOperation** - Execute any property changes over a period of time

That's it! We just scratched the surface of what the library can do. For more information, check out the [documentation](https://docs.hibzz.games/dropl/getting-started/)

<br>

## Have a question or want to contribute?
If you have any questions or want to contribute, feel free to join the [Discord server](https://discord.gg/YXdJ8cZngB) or [Twitter](https://twitter.com/hibzzgames). I'm always looking for feedback and ways to improve this tool. Thanks!

Additionally, you can support the development of these open-source projects via [GitHub Sponsors](https://github.com/sponsors/sliptrixx) and gain early access to the projects.

