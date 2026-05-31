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

  public enum ComportementDesProprietesDupliquees {

    /**
     * <summary>
     *   [FR] Les clés dupliquées ne sont pas autorisées. Lorsqu'une clé dupliquée est trouvée, l'analyseur s'arrête avec une erreur.<br/>
     *   [EN] Duplicate keys are not allowed. When a duplicate key is found, the analyzer stops with an error.
     * </summary>
     **/
    DesactiverEtArretAvecErreur,

    /**
     * <summary>
     *   [FR] Les clés dupliquées sont autorisées. La valeur de la clé dupliquée sera la première valeur trouvée dans l'ensemble des noms de clés dupliquées.<br/>
     *   [EN] Duplicate keys are allowed. The value of the duplicated key will be the first value found in the set of duplicated key names.
     * </summary>
     **/
    AutoriserEtConserverLaPremiereValeur,

    /**
     * <summary>
     *   [FR] Les clés dupliquées sont autorisées. La valeur de la clé dupliquée sera la dernière valeur trouvée dans l'ensemble des noms de clés dupliquées.<br/>
     *   [EN] Duplicate keys are allowed. The value of the duplicated key will be the last value found in the set of duplicated key names.
     * </summary>
     **/
    AutoriserEtConserverLaDerniereValeur,

    /**
     * <summary>
     *   [FR] Les clés dupliquées sont autorisées. La valeur des clés dupliquées sera une chaîne qui résulte de la concaténation de toutes les valeurs dupliquées trouvées, séparées par le caractère.<br/>
     *   [EN] Duplicate keys are allowed. The value of duplicate keys will be a string resulting from the concatenation of all duplicate values found, separated by the character.
     * </summary>
     **/
    AutoriserEtConcatenerLesValeurs
  }
}
