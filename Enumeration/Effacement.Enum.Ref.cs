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

namespace GalacticShrine.Enumeration.Configuration {

  /**
   * <summary>
   *   [FR] Définit le type d'effacement à appliquer sur une structure de configuration.<br/>
   *   [EN] Defines the clearing mode to apply to a configuration structure.
   * </summary>
   **/
  public enum Effacement {

    /**
     * <summary>
     *   [FR] Efface uniquement les commentaires, en conservant les propriétés, sections et valeurs.<br/>
     *   [EN] Clears comments only, while keeping properties, sections and values.
     * </summary>
     **/
    Commentaires,

    /**
     * <summary>
     *   [FR] Efface les propriétés ou les données principales de l'objet ciblé, en conservant les commentaires lorsque le type le permet.<br/>
     *   [EN] Clears the properties or main data of the target object, while keeping comments when the type allows it.
     * </summary>
     **/
    Proprietes,

    /**
     * <summary>
     *   [FR] Efface les commentaires et les propriétés ou données principales de l'objet ciblé.<br/>
     *   [EN] Clears both comments and properties or main data of the target object.
     * </summary>
     **/
    Tout
  }
}
