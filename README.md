# Compiler

Un compilateur est grossièrement un traducteur qui recoit du code source et le traduit en un autre language ciblé.

![Compiler Steps](https://brainmass.com/hubsimg/1511787/Compilador.gif)

Alors pour une compétition de compilateur, on peut s'attendre à ce que l'une ou plusieurs de ces étapes soit le cible de la compétition. Par example, lors des CS-Games il y a quelques années il fallait créer un interpréteur qui lisait le code source, le parsait en AST et finalement faisait une analyse sémantique (interprétation) pour éxécuter le code.

Alors moi je voudrais faire différent. Je souhaite ignorer les premières étapes concernant la construction de l'AST et me concentrer sur les dernières étapes du compilateur. Je suis plus particulièrement intéressé à travailler sur l'analyse statique du code en termes de qualité et de sécurité.

## Description Générale

C'est quoi un analyseur statique? Puisqu'une image vaut mille mots, voici des images de résultats d'analyse statique:

![Coverity](https://user-images.githubusercontent.com/5842757/28995243-82c9cfa6-79e4-11e7-93e3-af9a91f376b6.png)
![BetterCodeHub](https://files.realpython.com/media/bch1.f5045e9b9392.png)

En gros, le logiciel analyse le code (Analyse sémantiques) et cherche simplement a en extraire des informations pour l'usager (contrairement a chercher de générer ou interpréter le code). Puisqu'il existe des tonnes d'informations différentes à extirper, il est possible de créer une compétition auquel tous peuvent participer et ramasser des points, mais seul les meilleurs peuvent vraiment se démarquer.

## Déroulement

Une base sera déjà fournie aux participants, ils travailleront tous sur la même base de code à analyser (probablement un projet open source) et vont tous avoir un squelette capable d'analyser le code et de fournir un arbre abstrait de syntaxes.

Plusieurs défauts seront introduits dans le code ou simplement identifier par nous. Les participants auront le travail de créer un programme capable d'identifier le défaut et de générer un rapport (fichier texte avec un format spécifique) avec la liste de tous ces défauts. 

Puisque nous travaillons sur un arbre, les étudiants auront principalement a créer des visiteurs. Un exemple facile serait de détecter que tous les attributs privés commencent par le préfixe "m_", dans ce cas un visiteur est implémenté et les participants vont parcourir tous les noeuds et si un noeud est de type Field (whatever) et que la string pour le nom ne commence pas par m_, ils doivent sortir l'erreur: (exemple préliminaire) foo/bar.txt Line 32 : Defined field foobar does not start with prefix m_. Je considère ce genre d'erreurs comme étant facile et un participant n'aillant aucune connaissance devrait etre capable de faire cela.

Par contre, certaines erreurs peuvent facilement être plus difficile, voir impossible a réaliser en trois heures. Par exemple faire une analyse qui "simule" l'éxécution du code pour réaliser qu'il y a une fuite de mémoire ou du code mort / inutile exige plus de connaissances.

Donc potentiellement,
Analyses simples (visiteurs)
Analyses de flux sur le CFG (Dataflow, SSA, Points-fixes)
Analyses interprocédurales
Etc.

## Technologies / Modules

Todo

## Systeme de correction

Une solution serait d'avoir une liste de tous les défauts dans le même format que les étudiants et de simplement sort les lignes et appliquer un diff pour avoir le nombre de lignes différentes. On peut donner ce programme aux étudiants pour qu'ils testent en temps réel pendant la compé et apprennent leur score. Il ne manquerait qu'à vérifier le code des participants pour éviter de la triche (genre ils font un programme qui fait simplement output le résultat sans rien analyser).

On peut aussi etre plus sophistiqué, nous pouvons ajouter dans le code a analyser des annotations, du genre:

int foo; // ERROR: MISSING_ATTRIBUTE_PREFIX

Dans le projet, on ajoute une classe statique Results avec un API pour envoyer directement les défauts:
Results.Instance.pushError(String Category, String File, int loc, String ErrorMessage); ou quelque chose du genre.

On ajoute un visiteur qui parcours le code et qui cherche nos annotations et le compare aux résultats générés dans l'instance. Ainsi a chaque execution les étudiants peuvent avoir le résultat

## Pointage

Il existe quatre types de "résultats":
True positive: Erreur existante et reportée (Bien)
False positive: Erreur inexistante et reportée (Mal)
True négative: Erreur inexistante et non reportée (Bien)
False négative: Erreur existante et non reportée (Mal)

Alors, nous pourrions utiliser une formule d'ici: https://en.wikipedia.org/wiki/Precision_and_recall

La F-Measure semble intéressante, c'est une valeur qui va de 0 pour une classification imprécise et 1 pour des résultats parfaits: 

![FMeasure](https://wikimedia.org/api/rest_v1/media/math/render/svg/dd577aee2dd35c5b0e349327528a5ac606c7bbbf)

On peut faire une mesure globale sur tous les erreurs reportés ou nous pouvons donner des poids aux catégories et controler le résultat pour que les analyses difficiles aillent plus de valeurs


## ToDo List

- [ ] Faire un énoncé
- [ ] Choisir le langage analysé et l'engin de base
- [ ] Coder le squelette de la compé
- [ ] Coder le correcteur automatisé
- [ ] Tester
- [ ] Compléter la ToDo List
