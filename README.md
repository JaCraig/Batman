Batman
------

One of the things that has always annoyed me about C#, .Net, etc. is the glue code. There have been days that I've spent more time plugging in various libraries into my application than actually building the application. I'm tired of this and decided to do something about it. This framework is designed to handle the grunt work, the boring bits of code that no one should have to write.

I am vengence, I am the night, I am Batman
------------------------------------------
Batman is based around a few key principles:

### Libraries should be pluggable
With that in mind, let's look at how Batman works. Let's say you download the Batman MVC core project from NuGet (when that's up). With this plugged into your project, you gain very little but it sets the stage for the awesomesauce. Want to set up serialization using ServiceStack.Text? Just download the nuget package and it's set up for you. Want to replace it with Json.Net? Just remove the one package and replace it with the other. Your code doesn't need to change at all. Want to switch between NLog and log4net to see which you like more? Just switch the package. And from the shadows, Batman will make it work.

Batman takes care of all of that for you and gives a unified layer for dealing with the various libraries. Let's see an example:
**Logging with NLog**

```
    public class DefaultController : Batman.MVC.BaseClasses.ControllerBase
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            Log("This is a test", MessageType.Info);
		}
	}
```

**Logging with log4net**

```
    public class DefaultController : Batman.MVC.BaseClasses.ControllerBase
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            Log("This is a test", MessageType.Info);
		}
	}
```

### Convention over configuration
One of the main things that Batman is designed to do is to make it so that you don't have to worry about the small stuff. In keeping with that goal, when a task is straightforward and Batman has enough info to act, it acts. When serializing an object, if the client specifies a type that it wants, Batman will automatically handle the content negotiation and return what is requested. By default Batman handles JSON/JSONP, CSV, and XML without issue but other formats can easily be added.

### Speed is key
Batman itself is a thin layer that goes on top of 3rd party libraries. As such, what is important are those libraries. If you choose the default package, Batman comes with some of the fastest libraries around. We want to make sure that you end up with the best experience overall and your code needs to be fast for that to happen. So if we find a library that has similar features that is faster, we'll switch ours out for the better library.

To the Batmobile
----------------
The basic makeup of Batman can be seen in its various namespaces:
* **Bootstrapper** - This is the DI layer of Batman. It connects everything within Batman. It's the heart. Without it, there is no Batman.
* **Communication** - This is how Batman communicates with the outside world. Want to send an email? This is the place to look. It houses a system that allows templates to be created, similar to Ruby on Rails, thus allowing reuse and better maintainability of your communications. By default it uses the Razor engine for this but can be set up to use others.
* **FileSystem** - This is how Batman finds and opens a file. It's a unified system that allows you to specify a string and it figures out how to get you that file. For instance, "~/Test.txt","C:\\Test.txt", and "http://www.google.com/Test.txt" can all be specified to the system and it will figure out where it is and how to download the data for you.
* **Logging** - Batman offers a unified experience for various logging frameworks.
* **Profiling** - Batman automatically sets up your system so you can easily profile your code, find the bottlenecks, and fix them.
* **Tasks** - Batman offers an easy way to tie into various events within its system.
* **Assets** - Batman's MVC addons add automatic bundling and minification of your JS/CSS files. It also adds support for LESS. The system lays on top of Microsoft's System.Web.Optimization framework and adds a pipeline to the process, thus allowing a lot more control for the minification process. It also fixes a number of issues, such as importing CSS and JS files appropriately (the system respects the @import and /// <reference...> items when building your bundled file), and adds improvements, such as reducing calls to the server by embedding images within CSS files.
* **Serialization** - Batman automatically deals with content negotiation and will serialize your data based on what is requested by the end user. And adding new formats is a snap.

The true crime fighter always carries everything he needs in his utility belt
-----------------------------------------------------------------------------
The one thing that Batman comes with, is utilities. First it starts off with one of the largest utility libraries for .Net in [Craig's Utility Library](https://cul.codeplex.com). Then Batman adds on to it with platform specific utilities (at present that just means MVC).

Why so serious?
---------------
OK, now for the boring stuff:
* Maintained by [James Craig](https://github.com/JaCraig)
* Uses the MIT license.
* If you would like to contribute, please contact me as help is always appreciated.