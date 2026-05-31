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
   *   [FR] Définit les types de jetons reconnus par le lexeur GsC.<br/>
   *   [EN] Defines token types recognized by the GsC lexer.
   * </summary>
   **/
  public enum TypeJetonGsC {

		/**
     * <summary>
     *   [FR] Représente la fin du fichier d'entrée.<br/>
     *   [EN] Represents the end of the input file.
     * </summary>
     **/
		FinDeFichier,

		/**
     * <summary>
     *   [FR] Représente un identifiant, qui peut être une clé de propriété ou un nom de section.<br/>
     *   [EN] Represents an identifier, which can be a property key or a section name.
     * </summary>
     **/
		Identifiant,

		/**
     * <summary>
     *   [FR] Représente une chaîne de caractères, entourée de guillemets.<br/>
     *   [EN] Represents a string literal, enclosed in quotes.
     * </summary>
     **/
		Chaine,

		/**
     * <summary>
     *   [FR] Représente un nombre entier ou à virgule flottante.<br/>
     *   [EN] Represents an integer or floating-point number.
     * </summary>
     **/
		Nombre,

		/**
     * <summary>
     *   [FR] Représente une valeur booléenne (vrai ou faux).<br/>
     *   [EN] Represents a boolean value (true or false).
     * </summary>
     **/
		Booleen,

		/**
     * <summary>
     *   [FR] Représente une valeur nulle ou absente.<br/>
     *   [EN] Represents a null or absent value.
     * </summary>
     **/
		Nul,

		/**
     * <summary>
     *   [FR] Représente le début d'une section, indiqué par une accolade ouvrante '<{'.<br/>
     *   [EN] Represents the beginning of a section, indicated by an opening brace '<{'.
     * </summary>
     **/
		DebutSection,

		/**
     * <summary>
     *   [FR] Représente la fin d'une section, indiqué par une accolade fermante '}>'.<br/>
     *   [EN] Represents the end of a section, indicated by a closing brace '}>'.
     * </summary>
     **/
		FinSection,

		/**
     * <summary>
     *   [FR] Représente le début d'un objet, indiqué par une accolade ouvrante '<'.<br/>
     *   [EN] Represents the beginning of an object, indicated by an opening brace '<'.
     * </summary>
     **/
		DebutObjet,

		/**
     * <summary>
     *   [FR] Représente la fin d'un objet, indiqué par une accolade fermante '>'.<br/>
     *   [EN] Represents the end of an object, indicated by a closing brace '>'.
     * </summary>
     **/
		FinObjet,

		/**
     * <summary>
     *   [FR] Représente le début d'une liste, indiqué par une parenthèse ouvrante '{'.<br/>
     *   [EN] Represents the beginning of a list, indicated by an opening parenthesis '{'.
     * </summary>
     **/
		DebutListe,

		/**
     * <summary>
     *   [FR] Représente la fin d'une liste, indiqué par une parenthèse fermante '}'.<br/>
     *   [EN] Represents the end of a list, indicated by a closing parenthesis '}'.
     * </summary>
     **/
		FinListe,

		/**
     * <summary>
     *   [FR] Représente le début d'un tableau, indiqué par une crochets ouvrant '['.<br/>
     *   [EN] Represents the beginning of an array, indicated by an opening bracket '['.
     * </summary>
     **/
		DebutTableau,

		/**
     * <summary>
     *   [FR] Représente la fin d'un tableau, indiqué par une crochets fermant ']'.<br/>
     *   [EN] Represents the end of an array, indicated by a closing bracket ']'.
     * </summary>
     **/
		FinTableau,

		/**
     * <summary>
     *   [FR] Représente le symbole '~>', utilisé pour séparer les clés et les valeurs des propriétés.<br/>
     *   [EN] Represents the symbol '~>', used to separate property keys and values.
     * </summary>
     **/
		Attribution,

		/**
     * <summary>
     *   [FR] Représente le symbole ';', utilisé pour séparer les éléments d'une liste.<br/>
     *   [EN] Represents the symbol ';', used to separate elements in a list.
     * </summary>
     **/
		FinInstruction,

		/**
     * <summary>
     *   [FR] Représente le symbole ',', utilisé pour séparer les éléments d'un tableau ou d'un objet.<br/>
     *   [EN] Represents the symbol ',', used to separate elements in an array or object.
     * </summary>
     **/
		Virgule,

		/**
     * <summary>
     *   [FR] Représente un commentaire, qui commence par le symbole '#' et se termine à la fin de la ligne.<br/>
     *   [EN] Represents a comment, which starts with the symbol '#' and ends at the end of the line.
     * </summary>
     **/
		Commentaire
	}
}
