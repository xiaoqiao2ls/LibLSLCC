#About LibLSLCC 
 
The LibLSLCC library is a compiler framework for writing LSL compilers.

The LibLSLCC code validator/syntax tree builder provides **full front end syntax checking of LSL**.
It even includes extended warnings that you would find in most compilers for other languages,
but that are not implemented in either the Linden compiler or current OpenSim compiler.


Warnings for things such as: 

 * Constant expressions in conditional/loop statements.
	
 * Un-used variables.
	
 * Dead code.
	
 * Deprecated function usage.
	
	
That's not everything just some common ones.. this list is pretty long.
I suggest you mess around with the editor releases to discover what all 
LibLSLCC can tell you about your code.
	
	
LibLSLCC can also be used for general purpose LSL parsing tasks, it provides
its own rich syntax tree (Created as LSL code is validated) that has been completely
abstracted from ANTLR.  The syntax tree is tailored specifically for dealing with LSL,
and there is an interface for every node so that you can implement your own code DOM 
if want to.


====


LibLSLCC includes a CSharp code generator that targets the OpenSim runtime.

The Code Validator and OpenSim code generator in LibLSLCC have both been designed
with the intent of implementing Linden LSL with 100 percent cross compatibility.

The project is basically a complete reverse engineering of the Linden compiler's
Grammar/Rules and generated code behavior.



I have integrated LibLSLCC into OpenSim, See Here:

       https://github.com/EriHoss/OpenSim_With_LibLSLCC 
 
       Or 
         
       https://gitlab.com/erihoss/OpenSim_With_LibLSLCC 


====   
	   
The Code Validator/OpenSim Code Generator Features:

 * Full front end Syntax Checking, including dead code detection.  
   no more esoteric CSharp compiler errors or line mapping funkyness.
	
 * Dead code elimination from generated code where applicable.
   This includes un-used functions and global variables, as well
   as any dead code in a function/event body that does not cause a compile 
   error and is safe to remove given its context.
	
 * Correct code generation for global variables that reference each other.
	
 * Symbol name mangling specific to globals/parameters/local variables/labels and 
   user defined functions.  This completely abstracts variable scoping rules from the CSharp 
   compiler underneath.  All variable scoping rules are handled by the front end LibLSLCC Code Validator.
   The scoping rules implemented are %100 true to LSL.  Symbol mangling also removes the possibility 
   of causing a CSharp syntax error by using a keyword/Class name as a variable or function name.
	  
	  
 * Correct Code generation for jumps over declared variables.
	
 * Jumps no longer require a "NoOp()" after them, LibLSLCC simply detects when
   they are not followed by any statements.
	
 * Full and optimized support for the CO-OP script stop strategy in OpenSim.
   An option to enable this is not in the editor yet, but the OpenSim fork enables it
   when it's seen in OpenSim.ini.
	
 * Correct order of operations via the use of operator function stubs
   that are generated on demand.  Old mono list optimizations will now
   port over without breaking, as well as other funky scripts that rely
   on Right to Left evaluation being the norm.
	  
 * Correct treatment of vectors/rotations/lists and strings as booleans.
	
 * Vectors and rotations can now be negated.
	
 * At this point I am scratching my head because I cannot remember what else...
   I put a lot of time into this.
	 
	
#About LibLSLCCEditor

  
The project also includes full featured LSL Editor that was originally built to test the compiler library.
It has since developed into a full blown multi-tabbed IDE that is built on top of LibLSLCC's parsing framework. 

It Features:
	
 * Syntax Highlighting.
	
 * Documentation tooltips.

 * Context aware auto complete.
	
 * Go to definition (navigation by symbol).
	
 * Code formatting.
	
 * Library data for both Linden and Opensim SecondLife servers. 
	
 * Compilation to CSharp code for OpenSim.
	 (CO-OP script stop mode cannot be enabled in the editor yet.)
	

 

#Project Dependencies

All binary dependencies are distributed with the build.
You should not need to add any binaries yourself.

===

The LibLSLCC library itself depends on the **CSharp ANTLR 4.5.1 Runtime**.

You can find a license for ANTLR 4 in the **ThirdPartyLicenses** folder
of the build directory.

For more information about **ANTLR** go here:

http://www.antlr.org/index.html


===

The ***LibraryDataScrapingTools** project depends on **System.Data.SQLite**
when compiled on Windows.  When its compiled on mono it uses **Mono.Data.Sqlite**.

see here: 

https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki


===

The LSLCCEditor application depends on **AvalonEdit**, and a few pieces
of code forked from **AvalonEdit**.  You can also find a license for **AvalonEdit**
in the **ThirdPartyLicenses** folder of the build directory.

 
For more information about **AvalonEdit** go here:

http://avalonedit.net/


=== 

 
 
#Building LibLSLCC and LSLCCEditor 
 
 
LibLSLCC requires Java to be installed so that the ANTLR 4 parser generator tool (Which is written in Java) can be run as
part of the pre-build step for the LibLSLCC Library.  The java executable will need to be in your PATH so that it can run from the
command prompt.  You can see if you have java installed by opening a command prompt/terminal and typing 'java', if you get an error
instead of information about the java executable's command line options then java is not installed and you will need to go
install it before the build will work.
 
You can build the entire solution on Windows using Microsoft Visual Studio express and up (2015 is what I am using).
LSLCCEditor, LSLCCEditor.CompletionUI and LSLCCEditorInstaller are only buildable on Windows as they depend on WPF, AvalonEdit
and WiX Installer framework,  but the LibLSLCC library is cross platform.
 
In order to build the LSLCCEditorInstaller project on Windows you must install the WiX Installer Toolset from http://wixtoolset.org/
after you have installed Microsoft Visual Studio.  Wix will integrate with Visual Studio and allow you to load the WiX Project
type so you can build the LSLCCEditorInstaller project.
 
If you do not have WiX installed the project will still load but Visual Studio will show an error about LSLCCEditorInstaller
being an unsupported project type.  If you don't care about building the installer for the editor just click through the error
and the project will be ignored when you try to build.
 
If you only wish to build the LibLSLCC library, you can go to Build -> Configuration Manager in Visual Studios and uncheck 'Build'
next to every project in the solution besides LibLSLCC.  You must first set the active solution configuration to the desired build
type (Release or Debug), and the active solution platform to the desired architecture before doing this, so that the settings actually
apply to the type of build you want.
 

 
#Building LibLSLCC on *Nix Platforms with xbuild/monodevelop 
 
 
Java is also required when building on *Nix platforms so that the ANTRL 4 tool can run.  Make sure you have the latest
version of Java available for your distribution installed and that it is runnable from the command line.
 
Only the LibLSLCC, lslcc_cmd and LibraryDataScrapingTools projects are buildable on Mono and the MonoDevelop-LibLSLCC.sln includes
only these projects for convenience.
 
You can open the provided MonoDevelop-LibLSLCC.sln solution on Linux using the latest version of MonoDevelop,
Or you can build it from the command line using the xbuild command.
 
Other than some of the projects in the solution being un-buildable under Mono, the build on *Nix platforms behaves the exact same way
under xbuild/monodevelop as it does under MSBuild on Windows.
