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
   *   [FR] Représente un tableau de valeurs GsC.<br/>
   *   [EN] Represents an array of GsC values.
   * </summary>
   **/
  public class TableauGsC : Collection<ValeurGsC>, ClonableInterface<TableauGsC> {

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="TableauGsC"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="TableauGsC"/> class.
     * </summary>
     **/
		public TableauGsC() { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="TableauGsC"/> en copiant les éléments d'une autre instance.<br/>
     *   [EN] Initializes a new instance of the <see cref="TableauGsC"/> class by copying the elements from another instance.
     * </summary>
     * <param name="TableauGsCInstance">
     *   [FR] L'instance de <see cref="TableauGsC"/> à copier.<br/>
     *   [EN] The <see cref="TableauGsC"/> instance to copy.
     * </param>
     **/
		public TableauGsC(TableauGsC TableauGsCInstance) {

      foreach(ValeurGsC valeur in TableauGsCInstance) {

        Add(item: valeur.CloneEnProfondeur());
      }
    }

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de l'instance actuelle de <see cref="TableauGsC"/>.<br/>
     *   [EN] Creates a deep copy of the current instance of <see cref="TableauGsC"/>.
     * </summary>
     * <returns>
     *   [FR] Une copie en profondeur de l'instance actuelle de <see cref="TableauGsC"/>.<br/>
     *   [EN] A deep copy of the current instance of <see cref="TableauGsC"/>.
     * </returns>
     **/
		public TableauGsC CloneEnProfondeur() => new(TableauGsCInstance: this);
  }
}
