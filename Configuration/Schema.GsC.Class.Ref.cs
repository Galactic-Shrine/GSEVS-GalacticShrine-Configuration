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

using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Configuration.Configuration {

  /**
   * <summary>
   *   [FR] Cette structure définit le format du fichier GsC en personnalisant les chaînes utilisées<br/>
   *        pour définir les clés/valeurs, les sections, les objets, les listes, les tableaux ou les commentaires.<br/>
   *   [EN] This structure defines the GsC file format by customizing the strings used<br/>
   *        to define key/value pairs, sections, objects, lists, arrays or comments.
   * </summary>
   **/
  public class SchemaGsC : ClonableInterface<SchemaGsC> {

    /**
     * <summary>
     *   [FR] Chaîne de début de section.<br/>
     *   [EN] Section start string.
     * </summary>
     **/
    private string ChaineDeDebutDeSection = "<{";

    /**
     * <summary>
     *   [FR] Chaîne de fin de section.<br/>
     *   [EN] Section end string.
     * </summary>
     **/
    private string ChaineDeFinDeSection = "}>";

    /**
     * <summary>
     *   [FR] Chaîne de début d'objet.<br/>
     *   [EN] Object start string.
     * </summary>
     **/
    private string ChaineDeDebutDObjet = "<";

    /**
     * <summary>
     *   [FR] Chaîne de fin d'objet.<br/>
     *   [EN] Object end string.
     * </summary>
     **/
    private string ChaineDeFinDObjet = ">";

    /**
     * <summary>
     *   [FR] Chaîne d'attribution des propriétés.<br/>
     *   [EN] Property attribution string.
     * </summary>
     **/
    private string ChaineDattributionDesProprietes = "~>";

    /**
     * <summary>
     *   [FR] Chaîne de fin des propriétés.<br/>
     *   [EN] End of properties string.
     * </summary>
     **/
    private string ChaineDeFinDesProprietes = ";";

    /**
     * <summary>
     *   [FR] Chaîne de début de liste.<br/>
     *   [EN] List start string.
     * </summary>
     **/
    private string ChaineDeDebutDeListe = "{";

    /**
     * <summary>
     *   [FR] Chaîne de fin de liste.<br/>
     *   [EN] List end string.
     * </summary>
     **/
    private string ChaineDeFinDeListe = "}";

    /**
     * <summary>
     *   [FR] Chaîne de début de tableau.<br/>
     *   [EN] Array start string.
     * </summary>
     **/
    private string ChaineDeDebutDeTableau = "[";

    /**
     * <summary>
     *   [FR] Chaîne de fin de tableau.<br/>
     *   [EN] Array end string.
     * </summary>
     **/
    private string ChaineDeFinDeTableau = "]";

    /**
     * <summary>
     *   [FR] Chaîne d'attribution du commentaire.<br/>
     *   [EN] Comment attribution string.
     * </summary>
     **/
    private string ChaineDattributionDuCommentaire = "#";

    /**
     * <summary>
     *   [FR] Chaîne de séparation des sous-sections.<br/>
     *   [EN] Subsection separator string.
     * </summary>
     **/
    private string ChaineDeSeparateurDeSousSection = "::";

		/**
     * <summary>
     *   [FR] Chaîne de début de section officielle.<br/>
     *   [EN] Official section start string.
     * </summary>
     **/
		public string ChaineDeDebutDeSectionOfficielle => ChaineDeDebutDeSection;

		/**
     * <summary>
     *   [FR] Chaîne de fin de section officielle.<br/>
     *   [EN] Official section end string.
     * </summary>
     **/
		public string ChaineDeFinDeSectionOfficielle => ChaineDeFinDeSection;

		/**
     * <summary>
     *   [FR] Chaîne de début d'objet officielle.<br/>
     *   [EN] Official object start string.
     * </summary>
     **/
		public string ChaineDeDebutDObjetOfficielle => ChaineDeDebutDObjet;

		/**
     * <summary>
     *   [FR] Chaîne de fin d'objet officielle.<br/>
     *   [EN] Official object end string.
     * </summary>
     **/
		public string ChaineDeFinDObjetOfficielle => ChaineDeFinDObjet;

		/**
     * <summary>
     *   [FR] Chaîne d'attribution des propriétés officielle.<br/>
     *   [EN] Official property attribution string.
     * </summary>
     **/
		public string ChaineDattributionDesProprietesOfficielle => ChaineDattributionDesProprietes;

		/**
     * <summary>
     *   [FR] Chaîne de fin des propriétés officielle.<br/>
     *   [EN] Official end of properties string.
     * </summary>
     **/
		public string ChaineDeFinDesProprietesOfficielle => ChaineDeFinDesProprietes;

		/**
     * <summary>
     *   [FR] Chaîne de début de liste officielle.<br/>
     *   [EN] Official list start string.
     * </summary>
     **/
		public string ChaineDeDebutDeListeOfficielle => ChaineDeDebutDeListe;

		/**
     * <summary>
     *   [FR] Chaîne de fin de liste officielle.<br/>
     *   [EN] Official list end string.
     * </summary>
     **/
		public string ChaineDeFinDeListeOfficielle => ChaineDeFinDeListe;

		/**
     * <summary>
     *   [FR] Chaîne de début de tableau officielle.<br/>
     *   [EN] Official array start string.
     * </summary>
     **/
		public string ChaineDeDebutDeTableauOfficielle => ChaineDeDebutDeTableau;

		/**
     * <summary>
     *   [FR] Chaîne de fin de tableau officielle.<br/>
     *   [EN] Official array end string.
     * </summary>
     **/
		public string ChaineDeFinDeTableauOfficielle => ChaineDeFinDeTableau;

		/**
     * <summary>
     *   [FR] Chaîne d'attribution du commentaire officielle.<br/>
     *   [EN] Official comment attribution string.
     * </summary>
     **/
		public string ChaineDattributionDuCommentaireOfficielle => ChaineDattributionDuCommentaire;

		/**
     * <summary>
     *   [FR] Chaîne de séparation des sous-sections officielle.<br/>
     *   [EN] Official subsection separator string.
     * </summary>
     **/
		public string ChaineDeSeparateurDeSousSectionOfficielle => ChaineDeSeparateurDeSousSection;

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="SchemaGsC"/> avec les chaînes par défaut.<br/>
     *   [EN] Initializes a new instance of the <see cref="SchemaGsC"/> class with default strings.
     * </summary>
     **/
		public SchemaGsC() { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="SchemaGsC"/> en copiant les chaînes d'une autre instance.<br/>
     *   [EN] Initializes a new instance of the <see cref="SchemaGsC"/> class by copying the strings from another instance.
     * </summary>
     * <param name="SchemaGsCInstance">
     *   [FR] L'instance de <see cref="SchemaGsC"/> à copier.<br/>
     *   [EN] The <see cref="SchemaGsC"/> instance to copy.
     * </param>
     **/
		public SchemaGsC(SchemaGsC SchemaGsCInstance) {

      ChaineDeDebutDeSection = SchemaGsCInstance.ChaineDeDebutDeSection;
      ChaineDeFinDeSection = SchemaGsCInstance.ChaineDeFinDeSection;
      ChaineDeDebutDObjet = SchemaGsCInstance.ChaineDeDebutDObjet;
      ChaineDeFinDObjet = SchemaGsCInstance.ChaineDeFinDObjet;
      ChaineDattributionDesProprietes = SchemaGsCInstance.ChaineDattributionDesProprietes;
      ChaineDeFinDesProprietes = SchemaGsCInstance.ChaineDeFinDesProprietes;
      ChaineDeDebutDeListe = SchemaGsCInstance.ChaineDeDebutDeListe;
      ChaineDeFinDeListe = SchemaGsCInstance.ChaineDeFinDeListe;
      ChaineDeDebutDeTableau = SchemaGsCInstance.ChaineDeDebutDeTableau;
      ChaineDeFinDeTableau = SchemaGsCInstance.ChaineDeFinDeTableau;
      ChaineDattributionDuCommentaire = SchemaGsCInstance.ChaineDattributionDuCommentaire;
      ChaineDeSeparateurDeSousSection = SchemaGsCInstance.ChaineDeSeparateurDeSousSection;
    }

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de l'instance actuelle de <see cref="SchemaGsC"/>.<br/>
     *   [EN] Creates a deep copy of the current instance of <see cref="SchemaGsC"/>.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="SchemaGsC"/> qui est une copie en profondeur de l'instance actuelle.<br/>
     *   [EN] A new instance of <see cref="SchemaGsC"/> that is a deep copy of the current instance.
     * </returns>
     **/
		public SchemaGsC CloneEnProfondeur() => new(SchemaGsCInstance: this);
  }
}
