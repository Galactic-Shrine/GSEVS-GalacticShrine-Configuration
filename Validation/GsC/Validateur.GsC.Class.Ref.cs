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
using GalacticShrine.Enumeration.Configuration.GsC;
using GalacticShrine.Modele.Configuration.GsC;

namespace GalacticShrine.Configuration.Validation.GsC {

  public static class ValidateurGsC {

    public const string CodeSectionManquante = "SECTION_MANQUANTE";

    public const string CodeProprieteManquante = "PROPRIETE_MANQUANTE";

    public const string CodeTypeInvalide = "TYPE_INVALIDE";

    public static ResultatValidationGsC Valider(DonneesGsC Donnees, SchemaValidationGsC Schema) {

      if(Donnees == null) {

        throw new ArgumentNullException(paramName: nameof(Donnees));
      }

      if(Schema == null) {

        throw new ArgumentNullException(paramName: nameof(Schema));
      }

      ResultatValidationGsC resultat = new();

      foreach(string cheminDeSection in Schema.SectionsObligatoires) {

        if(!Donnees.ContientSection(NomDeLaSection: cheminDeSection)) {

          resultat.AjouterErreur(Erreur: new ErreurValidationGsC(Code: CodeSectionManquante, Chemin: cheminDeSection, Message: string.Format(format: "La section GsC obligatoire '{0}' est absente.", arg0: cheminDeSection)));
        }
      }

      foreach(RegleValidationGsC regle in Schema.Regles) {

        ObjetGsC conteneur = ObtenirConteneur(Donnees: Donnees, Regle: regle, Resultat: resultat);

        if(conteneur == null) {

          continue;
        }

        if(!conteneur.Contient(Cle: regle.Cle)) {

          if(regle.Obligatoire) {

            resultat.AjouterErreur(Erreur: new ErreurValidationGsC(Code: CodeProprieteManquante, Chemin: regle.CheminComplet, Message: string.Format(format: "La propriété GsC obligatoire '{0}' est absente.", arg0: regle.CheminComplet), TypeAttendu: regle.Type));
          }

          continue;
        }

        ValeurGsC valeur = conteneur.ObtenirValeur(Cle: regle.Cle);

        if(valeur.Type == regle.Type) {

          continue;
        }

        if(valeur.Type == TypeValeurGsC.Nul && regle.AutoriserNul) {

          continue;
        }

        resultat.AjouterErreur(Erreur: new ErreurValidationGsC(Code: CodeTypeInvalide, Chemin: regle.CheminComplet, Message: string.Format(format: "La propriété GsC '{0}' est de type '{1}' au lieu de '{2}'.", arg0: regle.CheminComplet, arg1: valeur.Type, arg2: regle.Type), TypeAttendu: regle.Type, TypeActuel: valeur.Type));
      }

      return resultat;
    }

    private static ObjetGsC ObtenirConteneur(DonneesGsC Donnees, RegleValidationGsC Regle, ResultatValidationGsC Resultat) {

      if(string.IsNullOrWhiteSpace(value: Regle.CheminDeSection)) {

        return Donnees.ProprieteGlobales;
      }

      if(!Donnees.ContientSection(NomDeLaSection: Regle.CheminDeSection)) {

        if(Regle.Obligatoire) {

          Resultat.AjouterErreur(Erreur: new ErreurValidationGsC(Code: CodeSectionManquante, Chemin: Regle.CheminDeSection, Message: string.Format(format: "La section GsC '{0}' est absente.", arg0: Regle.CheminDeSection)));
        }

        return null;
      }

      return Donnees.ObtenirSection(NomDeLaSection: Regle.CheminDeSection);
    }
  }
}
