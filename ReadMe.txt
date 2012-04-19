Spaghetti Detector is a simple tool for generating maps of class dependencies
for .NET assemblies.

It records a dependency in the event of the following:

- Class inheritance and interface implementation
- Properties, fields
- Method arguments and local variables
- Attributes on classes, properties, fields and methods
- Generic arguments


USAGE

SpaghettiDetector [-s] [-i:NAMESPACE0,NAMESPACE1] [-d:MAX_DEPTH] ASSEMBLY

-s   Suppress default ignore namespaces (currently System.* and Microsoft.*)
-i   Provide a list of namespaces to ignore, separated by commas. Includes 
     children, so ignoring Colourblind will also ignore Colourblind.Web
-d   Maximum depth to search to. Defaults to 3


OUTPUT

The output of the sample application is currently in JSON, and may look a 
little zany (colour, for example). This is because it's designed to work in 
tandem with avin_a_graph.js, my Javascript graph rendering code. Other 
formats will follow (XML, YAML, more general JSON).


TODO

- More serialisation options
- <Module>? What are you doing there?
- Better test coverage


MISC

We're using a Mono.Cecil assembly built using VS2010 rather than the Mono built
one because otherwise it breaks under NUnit on x64.

https://gist.github.com/1485605
