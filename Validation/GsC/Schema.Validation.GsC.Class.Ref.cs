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

using System;
using System.Collections.ObjectModel;
using GalacticShrine.Enumeration.Configuration.GsC;

namespace GalacticShrine.Configuration.Validation.GsC {

  public class SchemaValidationGsC {

    public Collection<string> SectionsObligatoires { get; } = [];

    public Collection<RegleValidationGsC> Regles { get; } = [];

    public SchemaValidationGsC AjouterSectionObligatoire(string CheminDeSection) {

      if(string.IsNullOrWhiteSpace(value: CheminDeSection)) {

        throw new ArgumentException(message: "Le chemin de section obligatoire GsC ne peut pas être vide.", paramName: nameof(CheminDeSection));
      }

      SectionsObligatoires.Add(item: CheminDeSection);
      return this;
    }

    public SchemaValidationGsC AjouterPropriete(string Cle, TypeValeurGsC Type, bool Obligatoire = true, bool AutoriserNul = false) {

      Regles.Add(item: new RegleValidationGsC(CheminDeSection: string.Empty, Cle: Cle, Type: Type, Obligatoire: Obligatoire, AutoriserNul: AutoriserNul));
      return this;
    }

    public SchemaValidationGsC Ajouter(string CheminDeSection, string Cle, TypeValeurGsC Type, bool Obligatoire = true, bool AutoriserNul = false) {

      Regles.Add(item: new RegleValidationGsC(CheminDeSection: CheminDeSection, Cle: Cle, Type: Type, Obligatoire: Obligatoire, AutoriserNul: AutoriserNul));
      return this;
    }
  }
}
