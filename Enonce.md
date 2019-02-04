# Énoncé

## Mise en contexte

TODO

## Objectif

Votre objectif est d'implémenter une série d'analyses qui vont détecter des problèmes dans un projet C#. Les problèmes à résoudre sont les suivants

* I01 : La premiere lettre d'une propriete doit commencer par une majuscule
* I02 : Une instruction, une propriete ou un attribut doivent etre testés par un test
* I03 : Un attribut doit etre utilisé

Le pointage pour chacun de ces problèmes sera calculé en fonction du nombre de vrais positifs, de faux positifs, de faux négatifs et de vrais négatifs.

Votre pointage pour chaque problème sera pondéré en fonction du succès de chaque équipe pour un problème afin de donner plus de poids aux problèmes difficiles.

Dans le cas improbable où il y a égalité, la performance sera prise en compte.

## Déroulement

Pour chaque problème, ajoutez une classe nommée dans le dossier "IssueWalker" qui hérite de "DefaultWalker". Ainsi votre analyse sera automatiquement éxécutée.

Utilisez les notions de base que vous avez appris dans vos cours de compilateur pour résourdre cette épreuve. Visitez l'Arbre de Syntaxe Abstrait (AST) et déterminez là où il y a des défauts. Une fois le défaut trouvé, vous n'avez qu'à reporter ce défaut à l'instance singleton du "IssueReporter".

Certains problèmes peuvent être simple et requérir une simple visite de l'AST alors que d'autres problèmes peuvent demander de visiter le Graphe de Flux de Controle (CFG) et peut-être même de surveiller le flux de données. Vous n'avez que trois heures alors choississez les problèmes que vous pensez pouvoir résoudre.

## Extras

* La classe CFGWalker vous donnera une bonne idée de comment il est possible de parcourir le CFG.

* Pour tester plus rapidement, il vous est conseillé de créer un plus petit projet à analyser.

* Évitez de modifier les classes en dehors du dossier IssueWalker, des modifications seront tolérées si elles sont pertinentes, mais refusées si elles tentent de manipuler le pointage.
