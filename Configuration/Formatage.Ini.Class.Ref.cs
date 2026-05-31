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

using GalacticShrine.Enumeration;
using GalacticShrine.Interface.Configuration;
using GalacticShrine.Outils;

namespace GalacticShrine.Configuration.Configuration {

  internal class FormatageIni : ClonableInterface<FormatageIni> {

    private uint NombreEspaceEntreLaCleEtAffectation;

    private uint NombreEspaceEntreAffectationEtLaValeur;

    public FormatageIni() {

      NombreEspacesEntreLaCleEtAffectation = 1;
      NombreEspacesEntreAffectationEtLaValeur = 1;
    }

    public string EspaceEntreLaCleEtAffectation { get; private set; }

    public string EspaceEntreAffectationEtLaValeur { get; private set; }

    public bool NouvelleLigneAvantLaSection { get; set; } = false;

    public bool NouvelleLigneApresLaSection { get; set; } = false;

    public bool NouvelleLigneAvantLaPropriete { get; set; } = false;

    public bool NouvelleLigneApresLaPropriete { get; set; } = false;

    public string NouvelleLigne {

      get {

        switch(OS.ObtenirIdCourantes) {

          case SystemeExploitation.Windows:
            return "\r\n";

          case SystemeExploitation.Linux:
          case SystemeExploitation.Mac:
          default:
            return "\n";
        }
      }
    }

    public uint NombreEspacesEntreLaCleEtAffectation {

      set {

        NombreEspaceEntreLaCleEtAffectation = value;
        EspaceEntreLaCleEtAffectation       = new string(' ', (int)value);
      }
    }

    public uint NombreEspacesEntreAffectationEtLaValeur {

      set {

        NombreEspaceEntreAffectationEtLaValeur = value;
        EspaceEntreAffectationEtLaValeur       = new string(' ', (int)value);
      }
    }

    public FormatageIni CloneEnProfondeur() => MemberwiseClone() as FormatageIni;
  }
}