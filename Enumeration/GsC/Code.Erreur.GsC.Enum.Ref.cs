/**
 * Copyright © 2017-2026, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2026, Galactic-Shrine - Tous droits réservés.
 * 
 * Mozilla Public License 2.0 / Licence Publique Mozilla 2.0
 *
 * This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
 * If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
 * Modifications to this file must be shared under the same Mozilla Public License, v. 2.0.
 *
 * Cette Forme de Code Source est soumise aux termes de la Licence Publique Mozilla, version 2.0.
 * Si une copie de la MPL ne vous a pas été distribuée avec ce fichier, vous pouvez en obtenir une à l'adresse suivante : https://mozilla.org/MPL/2.0/.
 * Les modifications apportées à ce fichier doivent être partagées sous la même Licence Publique Mozilla, v. 2.0.
 **/

namespace GalacticShrine.Enumeration.Configuration.GsC {

  /**
   * <summary>
   *   [FR] Définit les codes d'erreur officiels produits par l'analyse GsC.<br/>
   *   [EN] Defines official error codes produced by GsC analysis.
   * </summary>
   **/
  public enum CodeErreurGsC {

		/**
     * <summary>
     *   [FR] Aucun code d'erreur. Indique que l'analyse s'est déroulée sans problème.<br/>
     *   [EN] No error code. Indicates that the analysis completed without any issues.
     * </summary>
     **/
		Aucun = 0,

		/**
     * <summary>
     *   [FR] Erreur lexicale. Un problème a été rencontré lors de la lecture des caractères du fichier d'entrée.<br/>
     *   [EN] Lexical error. An issue was encountered while reading characters from the input file.
     * </summary>
     **/
		ErreurLexicale = 1000,

		/**
     * <summary>
     *   [FR] Caractère inattendu. Un caractère qui n'est pas reconnu comme valide dans le contexte actuel a été rencontré.<br/>
     *   [EN] Unexpected character. A character that is not recognized as valid in the current context was encountered.
     * </summary>
     **/
		CaractereInattendu = 1001,

		/**
     * <summary>
     *   [FR] Chaîne de caractères non terminée. Une chaîne de caractères a été commencée avec un guillemet d'ouverture, 
     *        mais n'a pas été fermée avec un guillemet de fermeture avant la fin du fichier ou la fin de la ligne.<br/>
     *   [EN] Unterminated string. 
     *        A string literal was started with an opening quote but was not closed with a closing quote before the end of the file or the end of the line.
     * </summary>
     **/
		ChaineNonTerminee = 1002,

		/**
     * <summary>
     *   [FR] Nombre mal formé. Un nombre a été rencontré qui ne respecte pas les formats numériques valides 
     *        (par exemple, un nombre avec des caractères non numériques ou une syntaxe incorrecte).<br/>
     *   [EN] Malformed number. A number was encountered that does not conform to valid numeric formats 
     *        (e.g., a number with non-numeric characters or incorrect syntax).
     * </summary>
     **/
		ErreurSyntaxe = 2000,

		/**
     * <summary>
     *   [FR] Début de section attendu. Le début d'une section (indiqué par un nom de section suivi d'une accolade ouvrante) était attendu, 
     *        mais n'a pas été trouvé.<br/>
     *   [EN] Section start expected. The start of a section (indicated by a section name followed by an opening brace) 
     *        was expected but not found.
     * </summary>
     **/
		DebutSectionAttendu = 2001,

		/**
     * <summary>
     *   [FR] Nom de section attendu. Un nom de section (un identifiant suivi d'une accolade ouvrante) était attendu, 
     *        mais n'a pas été trouvé.<br/>
     *   [EN] Section name expected. A section name (an identifier followed by an opening brace) was expected but not found.
     * </summary>
     **/
		NomSectionAttendu = 2002,

		/**
     * <summary>
     *   [FR] Fin de section attendue. La fin d'une section (indiquée par une accolade fermante) était attendue, 
     *        mais n'a pas été trouvée.<br/>
     *   [EN] Section end expected. The end of a section (indicated by a closing brace) was expected but not found.
     * </summary>
     **/
		FinSectionAttendue = 2003,

		/**
     * <summary>
     *   [FR] Clé de propriété attendue. Une clé de propriété (un identifiant suivi d'un signe égal) était attendue, 
     *        mais n'a pas été trouvée.<br/>
     *   [EN] Property key expected. A property key (an identifier followed by an equals sign) was expected but not found.
     * </summary>
     **/
		CleProprieteAttendue = 2004,

		/**
     * <summary>
     *   [FR] Signe égal attendu. Un signe égal (=) était attendu après une clé de propriété, 
     *        mais n'a pas été trouvé.<br/>
     *   [EN] Equals sign expected. An equals sign (=) was expected after a property key but was not found.
     * </summary>
     **/
		AttributionAttendue = 2005,

		/**
     * <summary>
     *   [FR] Valeur attendue. Une valeur (une chaîne, un nombre, un booléen, une liste, un tableau ou un objet)
     *        était attendue après le signe égal d'une propriété, mais n'a pas été trouvée.<br/>
     *   [EN] Value expected. A value (a string, number, boolean, list, array, or object) was expected after the equals sign of a property but was not found.
     * </summary>
     **/
		ValeurAttendue = 2006,

		/**
     * <summary>
     *   [FR] Fin d'instruction attendue. La fin d'une instruction (indiquée par la fin de la ligne ou la fin du fichier) était attendue, 
     *        mais n'a pas été trouvée.<br/>
     *   [EN] End of statement expected. The end of a statement (indicated by the end of the line or the end of the file) was expected but not found.
     * </summary>
     **/
		FinInstructionAttendue = 2007,

		/**
     * <summary>
     *   [FR] Fin de liste attendue. La fin d'une liste (indiquée par une parenthèse fermante) était attendue, 
     *        mais n'a pas été trouvée.<br/>
     *   [EN] List end expected. The end of a list (indicated by a closing parenthesis) was expected but not found.
     * </summary>
     **/
		FinListeAttendue = 2008,

		/**
     * <summary>
     *   [FR] Valeur après virgule attendue. Après une virgule dans une liste ou un tableau, une valeur était attendue, 
     *        mais n'a pas été trouvée.<br/>
     *   [EN] Value after comma expected. After a comma in a list or array, a value was expected but not found.
     * </summary>
     **/
		ValeurApresVirguleListeAttendue = 2009,

		/**
     * <summary>
     *   [FR] Fin de tableau attendue. La fin d'un tableau (indiquée par une accolade fermante) était attendue, 
     *        mais n'a pas été trouvée.<br/>
     *   [EN] Array end expected. The end of an array (indicated by a closing brace) was expected but not found.
     * </summary>
     **/
		FinTableauAttendue = 2010,

		/**
     * <summary>
     *   [FR] Valeur après virgule dans un tableau attendue. Après une virgule dans un tableau, une valeur était attendue, 
     *        mais n'a pas été trouvée.<br/>
     *   [EN] Value after comma in array expected. After a comma in an array, a value was expected but not found.
     * </summary>
     **/
		ValeurApresVirguleTableauAttendue = 2011,

		/**
     * <summary>
     *   [FR] Fin d'objet attendue. La fin d'un objet (indiquée par une accolade fermante) était attendue, 
     *        mais n'a pas été trouvée.<br/>
     *   [EN] Object end expected. The end of an object (indicated by a closing brace) was expected but not found.
     * </summary>
     **/
		FinObjetAttendue = 2012,

		/**
     * <summary>
     *   [FR] Propriété globale interdite. Une propriété a été définie en dehors de toute section, ce qui n'est pas autorisé.<br/>
     *   [EN] Global property forbidden. A property was defined outside of any section, which is not allowed.
     * </summary>
     **/
		ProprieteGlobaleInterdite = 2013,
		/**
     * <summary>
     *   [FR] Erreur structurelle. Un problème structurel a été rencontré dans le fichier de configuration, comme des accolades non appariées, 
     *        un mauvais imbriquement des sections ou d'autres problèmes qui violent la structure attendue d'un fichier GsC.<br/>
     *   [EN] Structural error. A structural problem was encountered in the configuration file, such as mismatched braces, 
     *        incorrect nesting of sections, or other issues that violate the expected structure of a GsC file.
     * </summary>
     **/
		ErreurStructure = 3000,

		/**
     * <summary>
     *   [FR] Section dupliquée. Une section avec le même nom a été définie plus d'une fois dans le même contexte.<br/>
     *   [EN] Duplicate section. A section with the same name was defined more than once in the same context.
     * </summary>
     **/
		SectionDupliquee = 3001,

		/**
     * <summary>
     *   [FR] Propriété dupliquée. Une propriété avec la même clé a été définie plus d'une fois dans la même section.<br/>
     *   [EN] Duplicate property. A property with the same key was defined more than once in the same section.
     * </summary>
     **/
		ProprieteDupliquee = 3002,

		/**
     * <summary>
     *   [FR] Type de valeur incompatible. La valeur d'une propriété ne correspond pas au type de valeur attendu pour cette propriété.<br/>
     *   [EN] Incompatible value type. The value of a property does not match the expected value type for that property.
     * </summary>
     **/
		ErreurAnalyse = 9000,

		/**
     * <summary>
     *   [FR] Erreur inconnue. Une erreur non spécifiée ou inattendue s'est produite pendant l'analyse.<br/>
     *   [EN] Unknown error. An unspecified or unexpected error occurred during analysis.
     * </summary>
     **/
		ErreurInconnue = 9999
  }
}
