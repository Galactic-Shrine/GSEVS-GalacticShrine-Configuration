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

using System.Collections.ObjectModel;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.GsC {

  /**
   * <summary>
   *   [FR] Représente une liste de valeurs GsC.<br/>
   *   [EN] Represents a list of GsC values.
   * </summary>
   **/
  public class ListeGsC : Collection<ValeurGsC>, ClonableInterface<ListeGsC> {

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="ListeGsC"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="ListeGsC"/> class.
     * </summary>
     **/
		public ListeGsC() { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="ListeGsC"/> en copiant les éléments d'une autre instance.<br/>
     *   [EN] Initializes a new instance of the <see cref="ListeGsC"/> class by copying the elements from another instance.
     * </summary>
     * <param name="ListeGsCInstance">
     *   [FR] L'instance de <see cref="ListeGsC"/> à copier.<br/>
     *   [EN] The <see cref="ListeGsC"/> instance to copy.
     * </param>
     **/
		public ListeGsC(ListeGsC ListeGsCInstance) {

      foreach(ValeurGsC valeur in ListeGsCInstance) {

        Add(item: valeur.CloneEnProfondeur());
      }
    }

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de l'instance actuelle de <see cref="ListeGsC"/>.<br/>
     *   [EN] Creates a deep copy of the current instance of <see cref="ListeGsC"/>.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ListeGsC"/> qui est une copie en profondeur de l'instance actuelle.<br/>
     *   [EN] A new instance of <see cref="ListeGsC"/> that is a deep copy of the current instance.
     * </returns>
     **/
		public ListeGsC CloneEnProfondeur() => new(ListeGsCInstance: this);
  }
}
