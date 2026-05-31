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

using System.Collections.Generic;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.GsC {

  /**
   * <summary>
   *   [FR] Représente une propriété GsC composée d'une clé et d'une valeur typée.<br/>
   *   [EN] Represents a GsC property composed of a key and a typed value.
   * </summary>
   **/
  public class ProprieteGsC : ClonableInterface<ProprieteGsC> {

		/**
     * <summary>
     *   [FR] La clé de la propriété GsC.<br/>
     *   [EN] The key of the GsC property.
     * </summary>
     **/
		public string Cle { get; set; }

		/**
     * <summary>
     *   [FR] La valeur de la propriété GsC.<br/>
     *   [EN] The value of the GsC property.
     * </summary>
     **/
		public ValeurGsC Valeur { get; set; }

		/**
     * <summary>
     *   [FR] Les commentaires associés à la propriété GsC.<br/>
     *   [EN] The comments associated with the GsC property.
     * </summary>
     **/
		public List<string> Commentaire { get; set; }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="ProprieteGsC"/> avec une clé et une valeur spécifiées.<br/>
     *   [EN] Initializes a new instance of the <see cref="ProprieteGsC"/> class with a specified key and value.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de la propriété GsC.<br/>
     *   [EN] The key of the GsC property.
     * </param>
     * <param name="Valeur">
     *   [FR] La valeur de la propriété GsC.<br/>
     *   [EN] The value of the GsC property.
     * </param>
     **/
		public ProprieteGsC(string Cle, ValeurGsC Valeur) {

      this.Cle = Cle;
      this.Valeur = Valeur;
      Commentaire = [];
    }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="ProprieteGsC"/> en copiant les propriétés d'une autre instance.<br/>
     *   [EN] Initializes a new instance of the <see cref="ProprieteGsC"/> class by copying the properties from another instance.
     * </summary>
     * <param name="ProprieteGsCInstance">
     *   [FR] L'instance de <see cref="ProprieteGsC"/> à copier.<br/>
     *   [EN] The <see cref="ProprieteGsC"/> instance to copy.
     * </param>
     **/
		public ProprieteGsC(ProprieteGsC ProprieteGsCInstance) {

      Cle = ProprieteGsCInstance.Cle;
      Valeur = ProprieteGsCInstance.Valeur.CloneEnProfondeur();
      Commentaire = [.. ProprieteGsCInstance.Commentaire];
    }

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de l'instance actuelle de <see cref="ProprieteGsC"/>.<br/>
     *   [EN] Creates a deep copy of the current instance of <see cref="ProprieteGsC"/>.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ProprieteGsC"/> qui est une copie en profondeur de l'instance actuelle.<br/>
     *   [EN] A new instance of <see cref="ProprieteGsC"/> that is a deep copy of the current instance.
     * </returns>
     **/
		public ProprieteGsC CloneEnProfondeur() => new(ProprieteGsCInstance: this);
  }
}
