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
   *   [FR] Définit les types de valeurs supportés par une configuration GsC.<br/>
   *   [EN] Defines value types supported by a GsC configuration.
   * </summary>
   **/
  public enum TypeValeurGsC {

		/**
     * <summary>
     *   [FR] Représente une valeur nulle ou absente.<br/>
     *   [EN] Represents a null or absent value.
     * </summary>
     **/
		Nul,

		/**
     * <summary>
     *   [FR] Représente une chaîne de caractères.<br/>
     *   [EN] Represents a string of characters.
     * </summary>
     **/
		Chaine,

		/**
     * <summary>
     *   [FR] Représente un nombre entier.<br/>
     *   [EN] Represents an integer number.
     * </summary>
     **/
		Entier,

		/**
     * <summary>
     *   [FR] Représente un nombre à virgule flottante.<br/>
     *   [EN] Represents a floating-point number.
     * </summary>
     **/
		Decimal,

		/**
     * <summary>
     *   [FR] Représente une valeur booléenne (vrai ou faux).<br/>
     *   [EN] Represents a boolean value (true or false).
     * </summary>
     **/
		Booleen,

		/**
     * <summary>
     *   [FR] Représente une liste de valeurs.<br/>
     *   [EN] Represents a list of values.
     * </summary>
     **/
		Liste,

		/**
     * <summary>
     *   [FR] Représente un tableau de valeurs.<br/>
     *   [EN] Represents an array of values.
     * </summary>
     **/
		Tableau,

		/**
     * <summary>
     *   [FR] Représente un objet complexe avec des propriétés.<br/>
     *   [EN] Represents a complex object with properties.
     * </summary>
     **/
		Objet
	}
}
