# Énoncé

## Introduction avec le Lore

The organizers of the cruise want to put in production their new online shop. They first want to ensure that the source code is of good enough quality. However, they lack enough time to audit every line of code. They choose to mandate you to create a new static analysis tool that will automatically report issues in the source code. This new tool must be able to analyze the whole application (the shop) with the best precision. You must try to report defects as much as possible without having too much false positives.

## Description du défi à réaliser

Your goal is to implement multiple static analyses that can detect defects in a C# application. The problems to solve are the following:

- I01 : The first letter of a property should start with an uppercase
- I02 : An instruction, method, field or property should be covered by a test
- I03 : A local variable or a field should be used (Ignore code in tests)
- I04 : A method or property which doesn't use instance data should be static (Ignore code in tests)
- I05 : A function should not be more than 30 lines of codes. Ignore blank lines and lines containing comments. The lenght is mesured by including both the declaring line (protected void blabla()) and the ending curly brace "}".
- I06 : Code should not be commented

- I01 : La premiere lettre d'une propriete doit commencer par une majuscule
- I02 : Une instruction, une fonction, une propriete ou un attribut doivent etre testes par un test
- I03 : Une variable locale ou un attribut qui n'est pas utilisé devrait être enlevé (Ignorez le code dans les tests)
- I04 : Une méthode ou une propriété qui n'utilise pas des données de l'instance devrait être statique (Ignorez le code dans les tests)
- I05 : Une fonction ne devrait pas faire plus de 30 lignes (Ignorez les lignes vides ou les lignes contenant seulement un commentaire). La longueur est mesurée de la première ligne de déclaration (protected void blabla()) jusqu'à la fin du block "}" inclusivement.
- I06 : Le code ne devrait pas être commenté

You have a score for every kind of defects. It is computed in function of the number of true positives, false positives, false negatives and true negatives. If your analyis is too imprecise (a lot of false positives), you may get a negative score. In this case we will use a score of 0 for this analysis.

The score of every analysis is multiplied by a difficulty multiplicator, here they are:
- I01 : 1
- I02 : 10
- I03 : 5
- I04 : 3
- I05 : 1
- I06 : 2

The final score will simply by the sum of the score of every problem normalized in the range 0-100.

In the improbable case where there is an equality, performance will be taken into account for tie-breakers.

## Instructions pour setuper le projet si nécéssaire

You will need to install dotnet core (https://dotnet.microsoft.com/download)... 

We recommend that you use Visual Studio Code (A free IDE) for this competition. However it is not mandatory, you can also go to the root of the project (the location of Analyzer.sln) and use the command line. You will need to use "dotnet restore", "dotnet build" et "dotnet run".

For every problem, you will need to add a class in the folder "IssueWalker" which inherits from "DefaultWalker". The easiest way would be to copy the class DummyWalker.cs. This file should be automatically executed without having to change the main function.

Use the notions you have learn in your class of Compiler theory to solve this competition. Visit the Abstract Syntax Tree (AST) and determine where are the defects. When you have found a defect, simply report it using the singleton instance of the class IssueReporter.

Some problems might be simple to solve and require only a visit of the AST. However some other problems can be much more complex and require to visit the Control-Flow Graph (CFG) and maybe even visit the DataFlow. You have only three hours, so choose carefully which problems you want to solve.

## Instructions de remise

You can simply zip again the project and submit it.

WARNING: If you have done any kind of modification outside of the directory IssueWalkers, add a file REMISE.MD at the root of the project to explain what are your modifications. If they are reasonable and doesn't break the correction system, they should be accepted. Without this file, we might ignore your changes when correcting.

## Extras

* The file CFGWalker in Walkers should give you some tips about how you can visit the CFG.

* To test faster, you can analyze a smaller project (Like this analysis project). Just change the path in the main function.

* The compiler that we are using as a framework for this competion is better known under the name of "Roslyn".
