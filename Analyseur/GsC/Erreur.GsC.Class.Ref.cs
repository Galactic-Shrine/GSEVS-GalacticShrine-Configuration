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

using GalacticShrine.Enumeration.Configuration.GsC;

namespace GalacticShrine.Configuration.Analyseur.GsC {

  /**
   * <summary>
   *   [FR] Représente une erreur riche produite pendant l'analyse GsC.<br/>
   *   [EN] Represents a rich error produced during GsC analysis.
   * </summary>
   **/
  public class ErreurGsC {

    public CodeErreurGsC Code { get; }

    public string CodeTexte => string.Format(format: "GSC{0:0000}", arg0: (int)Code);

    public string Message { get; }

    public int Ligne { get; }

    public int Colonne { get; }

    public string Extrait { get; }

    public string Jeton { get; }

    public ErreurGsC(CodeErreurGsC Code, string Message, int Ligne = 0, int Colonne = 0, string Extrait = "", string Jeton = "") {

      this.Code = Code;
      this.Message = Message ?? string.Empty;
      this.Ligne = Ligne;
      this.Colonne = Colonne;
      this.Extrait = Extrait ?? string.Empty;
      this.Jeton = Jeton ?? string.Empty;
    }

    public override string ToString() {

      string position = Ligne > 0 ? string.Format(format: " Ligne {0}, colonne {1}.", arg0: Ligne, arg1: Colonne) : string.Empty;
      string extrait = string.IsNullOrWhiteSpace(value: Extrait) ? string.Empty : string.Format(format: " Extrait : '{0}'.", arg0: Extrait);
      string jeton = string.IsNullOrWhiteSpace(value: Jeton) ? string.Empty : string.Format(format: " Jeton : '{0}'.", arg0: Jeton);

      return string.Format("[{0}] {1}{2}{3}{4}", CodeTexte, Message, position, jeton, extrait);
    }
  }
}
