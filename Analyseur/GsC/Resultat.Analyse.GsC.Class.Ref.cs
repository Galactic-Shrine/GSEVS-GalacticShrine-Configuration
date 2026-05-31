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
using System.Collections.ObjectModel;
using System.Linq;
using GalacticShrine.Enumeration.Configuration.GsC;

namespace GalacticShrine.Configuration.Analyseur.GsC {

  /**
   * <summary>
   *   [FR] Représente le résultat non-exceptionnel d'une analyse GsC.<br/>
   *   [EN] Represents the non-exceptional result of a GsC analysis.
   * </summary>
   **/
  public class ResultatAnalyseGsC {

    public bool EstValide => Erreurs.Count == 0;

    public DonneesGsC Donnees { get; }

    public Collection<ErreurGsC> Erreurs { get; }

    public ErreurGsC PremiereErreur => Erreurs.FirstOrDefault();

    public ResultatAnalyseGsC(DonneesGsC Donnees, IEnumerable<ErreurGsC> Erreurs = null) {

      this.Donnees = Donnees;
      this.Erreurs = new Collection<ErreurGsC>(list: (Erreurs ?? Enumerable.Empty<ErreurGsC>()).ToList());
    }

    public AnalyseGsCException CreerException() {

      return new AnalyseGsCException(Erreur: PremiereErreur ?? new ErreurGsC(Code: CodeErreurGsC.ErreurInconnue, Message: "Erreur GsC inconnue."));
    }
  }
}
