# Changelog

Toutes les modifications notables du dépôt `GSEVS-GalacticShrine-Configuration` doivent être documentées dans ce fichier.

Format inspiré de [Keep a Changelog](https://keepachangelog.com/fr/1.1.0/).
Ce projet utilise une logique de versionnement alignée sur les versions d'assembly du projet `.csproj`.

---

## [1.1.0.112] - 2026-05-31

### Résumé

Cette version transforme `GalacticShrine.Configuration` d'une base initiale centrée sur `Ini` et un squelette `GsC` vers une vraie bibliothèque de configuration structurée avec prise en charge complète de `GsC` et `GsCc`.

`GsC` devient le format de configuration structuré Galactic-Shrine.
`GsCc` devient la variante chiffrée du même contenu `GsC`.

### Ajouté

#### GsC - Format structuré

- Ajout d'une nouvelle implémentation complète de `GsC`.
- Ajout du modèle de données `DonneesGsC`.
- Ajout du modèle `SectionGsC`.
- Ajout du modèle `ProprieteGsC`.
- Ajout du modèle `ValeurGsC`.
- Ajout du modèle `ObjetGsC`.
- Ajout du modèle `ListeGsC`.
- Ajout du modèle `TableauGsC`.
- Ajout de la gestion des propriétés globales.
- Ajout de la gestion des sections.
- Ajout de la gestion des sous-sections.
- Ajout de la gestion des sous-sections par chemin explicite avec `::`.
- Ajout de la gestion des sous-sections par indentation.
- Ajout de la gestion des objets imbriqués.
- Ajout de la gestion des listes.
- Ajout de la gestion des tableaux.
- Ajout de la gestion des commentaires.
- Ajout de la gestion des commentaires attachés aux sections et propriétés.
- Ajout de la gestion des données insensibles à la casse lorsque l'option est activée.
- Ajout de la prise en charge des clés globales sans section lorsque l'option est activée.
- Ajout de la gestion configurable des sections dupliquées.
- Ajout de la gestion configurable des propriétés dupliquées.

#### GsC - Syntaxe officielle

- Ajout de la syntaxe officielle des sections : `<{Nom}>`.
- Ajout de la syntaxe officielle des objets : `< ... >`.
- Ajout de la syntaxe officielle des listes : `{ ... }`.
- Ajout de la syntaxe officielle des tableaux : `[ ... ]`.
- Ajout de l'opérateur d'attribution officiel : `~>`.
- Ajout du terminateur de propriété officiel : `;`.
- Ajout du commentaire officiel : `#`.
- Ajout du séparateur de sous-section officiel : `::`.

#### GsC - Types de valeurs

- Ajout de `TypeValeurGsC.Nul`.
- Ajout de `TypeValeurGsC.Chaine`.
- Ajout de `TypeValeurGsC.Entier`.
- Ajout de `TypeValeurGsC.Decimal`.
- Ajout de `TypeValeurGsC.Booleen`.
- Ajout de `TypeValeurGsC.Liste`.
- Ajout de `TypeValeurGsC.Tableau`.
- Ajout de `TypeValeurGsC.Objet`.

#### GsC - Analyse

- Ajout du dossier `Analyseur/GsC`.
- Ajout de `LexeurGsC`.
- Ajout de `ParseurGsC`.
- Ajout de `JetonGsC`.
- Ajout de `ErreurGsC`.
- Ajout de `ResultatAnalyseGsC`.
- Ajout de `AnalyseGsCException`.
- Ajout de `TypeJetonGsC`.
- Ajout de `CodeErreurGsC`.
- Ajout d'erreurs riches contenant code, ligne, colonne et extrait du contenu.
- Ajout de l'analyse depuis une chaîne.
- Ajout de l'analyse depuis un `TextReader`.
- Ajout du mode `EssayerAnalyser` pour retourner un résultat sans exception.
- Ajout de l'option `LancerDesExceptionsEnCasDerreur`.
- Ajout de l'option `AnalyseDesCommentaires`.
- Ajout de l'option `InsensibleA_LaCasse`.
- Ajout de l'option `AutoriserLesClesSansSection`.
- Ajout de l'option `AutoriserLesSectionsDupliquees`.
- Ajout de l'option `AutoriserLesProprietesDupliquees`.

#### GsC - Formatage

- Ajout de `FormatageGsC`.
- Ajout du formatage standard des données `GsC`.
- Ajout du formatage canonique.
- Ajout de la normalisation des sauts de ligne en `LF` pour le format canonique.
- Ajout de l'indentation canonique par espaces.
- Ajout de l'indentation canonique par tabulation.
- Ajout de l'échappement des chaînes.
- Ajout du formatage des listes, tableaux et objets.
- Ajout du round-trip lecture -> modèle -> formatage -> relecture.

#### GsC - API principale

- Ajout de `GsC.Analyse(string)`.
- Ajout de `GsC.Analyse(TextReader)`.
- Ajout de `GsC.EssayerAnalyser(string)`.
- Ajout de `GsC.EssayerAnalyser(TextReader)`.
- Ajout de `GsC.Ouvrir(string)`.
- Ajout de `GsC.Sauvegarder(string, DonneesGsC)`.
- Ajout de `GsC.Formater(DonneesGsC)`.
- Ajout de `GsC.FormaterCanonique(DonneesGsC)`.
- Ajout de `GsC.Valider(DonneesGsC, SchemaValidationGsC)`.
- Ajout de `GsC.Valider(string, SchemaValidationGsC)`.
- Ajout de `GsC.EstUnFichierCrypte(string)`.
- Ajout de `GsC.ResoudreCheminDuFichierPourOuverture(string)`.
- Ajout de `GsC.ResoudreCheminsDuFichierPourOuverture(string)`.

#### GsC - Fichiers

- Ajout de la lecture des fichiers `.GsC`.
- Ajout de la sauvegarde des fichiers `.GsC`.
- Ajout de la création automatique des dossiers parents lors de la sauvegarde.
- Ajout de la résolution des fichiers lorsque le chemin est fourni sans extension.
- Ajout de la détection des extensions `.GsC` et `.GsCc` sans respecter la casse.
- Ajout du mode compartimenté quand un fichier clair et un fichier chiffré existent pour le même chemin logique.
- Ajout du compartiment clair `GsC`.
- Ajout du compartiment chiffré `GsCc`.

#### GsCc - Configuration chiffrée

- Ajout de `OptionsCryptageGsC`.
- Ajout de `CryptageGsC`.
- Ajout de la prise en charge de l'extension chiffrée `.GsCc`.
- Ajout de la sauvegarde chiffrée en `.GsCc`.
- Ajout de l'ouverture chiffrée en `.GsCc`.
- Ajout de la détection automatique du chiffrement par extension.
- Ajout de la détection automatique du chiffrement par en-tête.
- Ajout de l'option `Configuration.Cryptage.EstActif`.
- Ajout de l'option `Configuration.Cryptage.MotDePasse`.
- Ajout de l'option `Configuration.Cryptage.ExtensionClaire`.
- Ajout de l'option `Configuration.Cryptage.ExtensionCryptee`.
- Ajout de l'option `Configuration.Cryptage.Iterations`.
- Ajout de l'option `Configuration.Cryptage.TailleDuSel`.
- Ajout de l'option `Configuration.Cryptage.TailleDuNonce`.
- Ajout de l'option `Configuration.Cryptage.TailleDuTag`.

#### GsCc - Sécurité

- Ajout de la signature officielle `GSCC`.
- Ajout de la version de format `1`.
- Ajout de l'en-tête officiel `GSCC:1`.
- Ajout du KDF officiel `PBKDF2-SHA256`.
- Ajout du chiffrement officiel `AES-256-GCM`.
- Ajout de l'enveloppe textuelle chiffrée.
- Ajout des champs d'enveloppe :
  - `KDF`;
  - `ITERATIONS`;
  - `CIPHER`;
  - `SALT`;
  - `NONCE`;
  - `TAG-SIZE`;
  - `TAG`;
  - `DATA`.
- Ajout de l'encodage Base64 pour les données cryptographiques.
- Ajout du refus des enveloppes incomplètes.
- Ajout du refus des anciens formats d'enveloppe.
- Ajout de la levée d'erreur en cas de mauvais mot de passe.
- Ajout de la levée d'erreur si un mot de passe est requis mais absent.

#### Validation GsC

- Ajout du dossier `Validation/GsC`.
- Ajout de `SchemaValidationGsC`.
- Ajout de `RegleValidationGsC`.
- Ajout de `ValidateurGsC`.
- Ajout de `ResultatValidationGsC`.
- Ajout de `ErreurValidationGsC`.
- Ajout de la validation des sections obligatoires.
- Ajout de la validation des propriétés obligatoires.
- Ajout de la validation des types de valeurs.
- Ajout de la possibilité d'autoriser ou non les valeurs nulles.

#### Exceptions

- Ajout du dossier `Exceptions`.
- Déplacement / remplacement de `Exception/AnalyseException.Class.Ref.cs` vers `Exceptions/AnalyseException.Class.Ref.cs`.
- Ajout de `GsCGsCcFileNotFoundException`.
- Ajout d'exceptions plus précises pour les erreurs d'analyse, de fichier et de chiffrement.

#### Ini

- Ajout / amélioration de la gestion avancée du modèle `Ini`.
- Ajout / amélioration de la fusion des données `Ini`.
- Ajout / amélioration de l'effacement sélectif des commentaires, propriétés et sections.
- Ajout de l'énumération `Effacement`.
- Ajout / amélioration de `ComportementDesProprietesDupliquees`.
- Amélioration de la gestion des sections et propriétés globales.
- Amélioration des collections `SectionCollection` et `ProprieteCollection`.
- Amélioration du comportement insensible à la casse.

### Modifié

- Remplacement de l'ancien fichier `GsC.Class.Ref.cs.cs` par `GsC.Class.Ref.cs`.
- Transformation de `GsC` d'un squelette minimal vers une API complète.
- Extension de `SchemaGsC` pour couvrir toute la syntaxe officielle `GsC`.
- Passage de `SchemaGsC` vers une structure clonable.
- Mise à jour de `GalacticShrine.Configuration.csproj`.
- Passage de la version projet de `1.0.1.95` à `1.1.0.112`.
- Mise à jour des copyrights vers `2017-2026`.
- Harmonisation de la licence MPL 2.0 dans les nouveaux fichiers.
- Réorganisation des espaces de noms liés aux exceptions.
- Amélioration du tampon de chaîne utilisé par les analyseurs.
- Amélioration de la configuration de l'analyseur `Ini`.
- Amélioration du formatage `Ini`.
- Amélioration du schéma `Ini`.
- Amélioration du modèle `Ini`.

### Supprimé

- Suppression de l'ancien fichier `GsC.Class.Ref.cs.cs`.
- Suppression / remplacement de l'ancien dossier `Exception` par `Exceptions`.
- Suppression de l'ancien squelette `GsC` qui ne contenait que `Schema`.

### Corrigé

- Correction de la confusion historique où `GsC` était décrit comme un fichier chiffré.
- Clarification : `GsC` est le format structuré clair, `GsCc` est sa variante chiffrée.
- Correction du modèle pour séparer clairement :
  - format clair ;
  - format chiffré ;
  - analyse ;
  - formatage ;
  - validation ;
  - chiffrement.
- Correction du comportement attendu en cas de valeurs manquantes, fin d'instruction absente, fin de liste/tableau/objet absente et nom de section manquant.
- Correction / amélioration des messages d'erreur avec ligne et colonne.
- Correction de la gestion des cultures pour les décimaux, qui doivent rester au format invariant.
- Correction des cas de mauvais mot de passe `GsCc`.
- Correction des cas de fichiers `.GsC` / `.GsCc` introuvables quand un chemin logique sans extension est utilisé.

### Notes techniques

- Le fichier `CHANGELOG.md` présent dans l'archive locale était vide.
- Le dépôt public GitHub contient encore un `CHANGELOG.md` générique hérité, qui ne décrit pas correctement l'état réel de `GalacticShrine.Configuration`.
- L'archive contient un état de travail local non poussé avec de nombreux fichiers `GsC` / `GsCc` ajoutés.
- Le dépôt Git local de `GalacticShrine.Configuration` dans l'archive pointe encore sur le commit initial `Init`.
- Le dépôt public actuel expose encore une base `GsC` minimale, alors que l'archive contient l'implémentation complète.

---

## [1.0.1.95] - 2026-2-3

### Ajouté

- Base initiale du projet `GalacticShrine.Configuration`.
- Prise en charge historique de `Ini`.
- Présence d'un premier squelette `GsC`.
- Présence de `SchemaGsC` avec les marqueurs de base :
  - début de section `<{` ;
  - fin de section `}>` ;
  - attribution `~>` ;
  - commentaire `#`.

### Limites connues

- `GsC` n'était pas encore un vrai parser complet.
- `GsCc` n'était pas encore implémenté.
- Le modèle de données `GsC` structuré n'était pas encore présent.
- Les listes, tableaux, objets et sous-sections n'étaient pas encore entièrement modélisés.
- La validation `GsC` n'était pas encore présente.
- Le chiffrement `GsCc` n'était pas encore présent.
- Les tests `GsC` / `GsCc` n'étaient pas encore intégrés dans le dépôt public.

---

## [2023-08-15] - Création initiale

### Ajouté

- Création du projet `GalacticShrine.Configuration`.
