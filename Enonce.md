# Énoncé

## Introduction avec le Lore

Les organisateurs de la croisière veulent mettre en ligne leur toute nouvelle boutique. Ils veulent vérifier que le code est suffisamment de qualité, mais ils n'ont pas le temps de vérifier chaque ligne de code dans un audit. Ils ont donc prit la décision de vous mandater afin de développer un outil d'analyse statique qui va automatiquement reporter des problèmes dans le code source. L'outil demandé doit pouvoir analyser l'entièreté du programme avec la meilleure précision. Tentez de trouver le plus de défauts possible sans donner trop de faux positifs.

## Description du défi à réaliser

Votre objectif est d'implémenter une série d'analyses qui vont détecter des problèmes dans un projet C#. Les problèmes à résoudre sont les suivants

- I01 : La premiere lettre d'une propriete doit commencer par une majuscule
- I02 : Une instruction, une propriete ou un attribut doivent etre testes par un test
- I03 : Une variable locale ou un attribut qui n'est pas utilisé devrait être enlevé (Ignorez le code dans les tests)
- I04 : Une méthode ou une propriété qui n'utilise pas des données de l'instance devrait être statique (Ignorez le code dans les tests)
- I05 : Une fonction ne devrait pas faire plus de 30 lignes (Ignorez les lignes vides ou les lignes contenant seulement un commentaire). La longueur est mesurée de la première ligne de déclaration (protected void blabla()) jusqu'à la fin du block "}" inclusivement.
- I06 : Le code ne devrait pas être commenté

Le pointage pour chacun de ces problèmes sera calculé en fonction du nombre de vrais positifs, de faux positifs, de faux négatifs et de vrais négatifs. Il est possible qu'une analyse trop impossible se retrouve avec un score négatif, dans ce cas nous utiliserons un score de 0 afin de ne pas vous pénaliser.

Votre pointage pour chaque problème sera multiplié par un multiplicateur de difficulté, les voici:
- I01 : 1
- I02 : 10
- I03 : 5
- I04 : 3
- I05 : 1
- I06 : 2

Le pointage final sera simplement la somme de chaque problème normalisé dans la portée 0-100.

Dans le cas improbable où il y aurait égalité, la performance sera prise en compte.

## Instructions pour setuper le projet si nécéssaire

Vous aurez besoin d'avoir installé dotnet core (https://dotnet.microsoft.com/download)... 

Nous vous conseillons d'utiliser Visual Studio Code (Un IDE gratuit) dans cette compétition. Le IDE n'est pas nécessaire, vous pouvez aussi à la racine du projet (là où Analyzer.sln se retrouve) tapez dans la console les commandes "dotnet restore", "dotnet build" et "dotnet run".

Pour chaque problème, ajoutez une classe nommée dans le dossier "IssueWalker" qui hérite de "DefaultWalker". Copiez la classe DummyWalker.cs afin de vous faciliter la tâche. Ainsi votre analyse sera automatiquement éxécutée.

Utilisez les notions de base que vous avez appris dans vos cours de compilateur pour résoudre cette épreuve. Visitez l'Arbre de Syntaxe Abstrait (AST) et déterminez là où il y a des défauts. Une fois le défaut trouvé, vous n'avez qu'à reporter ce défaut à l'instance singleton du "IssueReporter".

Certains problèmes peuvent être simple et requérir une simple visite de l'AST alors que d'autres problèmes peuvent demander de visiter le Graphe de Flux de Controle (CFG) et peut-être même de surveiller le flux de données. Vous n'avez que trois heures alors choississez les problèmes que vous pensez pouvoir résoudre.

## Instructions de remise

Vous pouvez simplement archiver de nouveau le projet et le remettre. 

ATTENTION: Si vous avez effectué des modifications en dehors du dossier IssueWalkers, ajoutez un fichier REMISE.MD à la racine du projet pour expliquer la raison de vos modifications. Si elles sont valables et ne brisent pas le système de correction elles devraient être acceptées. Sans ce fichier nous risquons d'ignorer vos changements lors de la correction.

## Extras

* La ficher CFGWalker dans Walkers vous donnera une bonne idée de comment il est possible de parcourir le CFG.

* Pour tester plus rapidement, il vous est conseillé de créer un plus petit projet à analyser. (Par example votre propre projet)

* Évitez de modifier les classes en dehors du dossier IssueWalker, des modifications seront tolérées si elles sont pertinentes, mais refusées si elles tentent de manipuler le pointage.

* Le compiler que l'on utilise comme base pour le projet est mieux connu sous le nom de "Roslyn"
