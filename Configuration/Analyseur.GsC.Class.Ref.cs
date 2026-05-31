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
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Configuration.Configuration {

  /**
   * <summary>
   *   [FR] Définit les options d'analyse d'un fichier GsC.<br/>
   *   [EN] Defines parsing options for a GsC file.
   * </summary>
   **/
  public class AnalyseurGsC : ClonableInterface<AnalyseurGsC> {

		/**
     * <summary>
     *   [FR] La chaîne de caractères utilisée pour l'indentation canonique lors de la génération d'un fichier GsC.<br/>
     *   [EN] The string used for canonical indentation when generating a GsC file.
     * </summary>
     **/
		private string ChaineIndentationCanonique = "  ";

		/**
     * <summary>
     *   [FR] Indique si l'analyse doit être insensible à la casse pour les clés de propriété et les noms de section.<br/>
     *   [EN] Indicates whether the parsing should be case-insensitive for property keys and section names.
     * </summary>
     **/
		public bool InsensibleA_LaCasse { get; set; } = false;

		/**
     * <summary>
     *   [FR] Indique si les commentaires présents dans le fichier GsC doivent être analysés et inclus dans la structure de données résultante.<br/>
     *   [EN] Indicates whether comments present in the GsC file should be parsed and included in the resulting data structure.
     * </summary>
     **/
		public bool AnalyseDesCommentaires { get; set; } = true;

		/**
     * <summary>
     *   [FR] Indique si des exceptions doivent être lancées en cas d'erreur d'analyse, 
     *        ou si les erreurs doivent être simplement signalées dans la structure de données résultante.<br/>
     *   [EN] Indicates whether exceptions should be thrown in case of parsing errors, 
     *        or if errors should simply be reported in the resulting data structure.
     * </summary>
     **/
		public bool LancerDesExceptionsEnCasDerreur { get; set; } = true;

		/**
     * <summary>
     *   [FR] Indique si le format canonique doit être utilisé lors de la génération d'un fichier GsC, 
     *        en appliquant une indentation cohérente et en ordonnant les sections et les propriétés de manière standardisée.<br/>
     *   [EN] Indicates whether the canonical format should be used when generating a GsC file, 
     *        by applying consistent indentation and ordering sections and properties in a standardized way.
     * </summary>
     **/
		public bool FormatCanonique { get; set; } = true;

		/**
     * <summary>
     *   [FR] La chaîne de caractères utilisée pour l'indentation canonique lors de la génération d'un fichier GsC. 
     *        Par défaut, l'indentation canonique est définie sur deux espaces ("  ").<br/>
     *   [EN] The string used for canonical indentation when generating a GsC file. 
     *        By default, the canonical indentation is set to two spaces ("  ").
     * </summary>
     **/
		public string IndentationCanonique {

      get => ChaineIndentationCanonique;

      set {

        if(string.IsNullOrEmpty(value: value)) {

          throw new ArgumentException(message: "L'indentation canonique GsC ne peut pas être vide.", paramName: nameof(value));
        }

        foreach(char caractere in value) {

          if(caractere != ' ' && caractere != '\t') {

            throw new ArgumentException(message: "L'indentation canonique GsC doit contenir uniquement des espaces ou des tabulations.", paramName: nameof(value));
          }
        }

        ChaineIndentationCanonique = value;
      }
    }

		/**
     * <summary>
     *   [FR] Indique si les clés de propriété peuvent être définies en dehors de toute section dans un fichier GsC.<br/>
     *   [EN] Indicates whether property keys can be defined outside of any section in a GsC file.
     * </summary>
     **/
		public bool AutoriserLesClesSansSection { get; set; } = true;

		/**
		* <summary>
		*   [FR] Indique si les sections dupliquées sont autorisées dans un fichier GsC.<br/>
		*   [EN] Indicates whether duplicate sections are allowed in a GsC file.
		* </summary>
		**/
		public bool AutoriserLesSectionsDupliquees { get; set; } = false;

		/**
     * <summary>
     *   [FR] Indique si les propriétés dupliquées sont autorisées dans un fichier GsC.<br/>
     *   [EN] Indicates whether duplicate properties are allowed in a GsC file.
     * </summary>
     **/
		public bool AutoriserLesProprietesDupliquees { get; set; } = false;

		/**
     * <summary>
     *   [FR] Les options de chiffrement utilisées pour les fichiers GsCc. Si le chiffrement est actif, 
     *        les fichiers GsC seront traités comme des fichiers GsCc.<br/>
     *   [EN] The encryption options used for GsCc files. If encryption is active, 
     *        GsC files will be treated as GsCc files.
     * </summary>
     **/
		public OptionsCryptageGsC Cryptage { get; set; } = new();

		/**
     * <summary>
     *   [FR] Indique si le fichier GsC est traité comme un fichier GsCc chiffré, en fonction de l'état du chiffrement dans les options de chiffrement.<br/>
     *   [EN] Indicates whether the GsC file is treated as an encrypted GsCc file, based on the encryption state in the encryption options.
     * </summary>
     **/
		public bool FichierCrypte {

      get => Cryptage.EstActif;
      set => Cryptage.EstActif = value;
    }

		/**
     * <summary>
     *   [FR] Constructeur par défaut de la classe <see cref="AnalyseurGsC"/>.<br/>
     *   [EN] Default constructor for the <see cref="AnalyseurGsC"/> class.
     * </summary>
     **/
		public AnalyseurGsC() { }

		/**
     * <summary>
     *   [FR] Constructeur de copie pour la classe <see cref="AnalyseurGsC"/>.<br/>
     *   [EN] Copy constructor for the <see cref="AnalyseurGsC"/> class.
     * </summary>
     * <param name="AnalyseurGsCInstance">
     *   [FR] L'instance de <see cref="AnalyseurGsC"/> à copier.<br/>
     *   [EN] The instance of <see cref="AnalyseurGsC"/> to copy.
     * </param>
     **/
		public AnalyseurGsC(AnalyseurGsC AnalyseurGsCInstance) {

      InsensibleA_LaCasse = AnalyseurGsCInstance.InsensibleA_LaCasse;
      AnalyseDesCommentaires = AnalyseurGsCInstance.AnalyseDesCommentaires;
      LancerDesExceptionsEnCasDerreur = AnalyseurGsCInstance.LancerDesExceptionsEnCasDerreur;
      FormatCanonique = AnalyseurGsCInstance.FormatCanonique;
      IndentationCanonique = AnalyseurGsCInstance.IndentationCanonique;
      AutoriserLesClesSansSection = AnalyseurGsCInstance.AutoriserLesClesSansSection;
      AutoriserLesSectionsDupliquees = AnalyseurGsCInstance.AutoriserLesSectionsDupliquees;
      AutoriserLesProprietesDupliquees = AnalyseurGsCInstance.AutoriserLesProprietesDupliquees;
      Cryptage = AnalyseurGsCInstance.Cryptage.CloneEnProfondeur();
    }

		/**
     * <summary>
     *   [FR] Configure l'indentation canonique pour qu'elle utilise un nombre spécifié d'espaces.<br/>
     *   [EN] Configures the canonical indentation to use a specified number of spaces.
     * </summary>
     * <param name="NombreEspaces">
     *   [FR] Le nombre d'espaces à utiliser pour l'indentation canonique. Par défaut, 2 espaces sont utilisés.<br/>
     *   [EN] The number of spaces to use for canonical indentation. By default, 2 spaces are used.
     * </param>
     **/
		public void UtiliserEspacesPourIndentationCanonique(int NombreEspaces = 2) {

      if(NombreEspaces <= 0) {

        throw new ArgumentOutOfRangeException(
          paramName: nameof(NombreEspaces),
          message: "Le nombre d'espaces pour l'indentation canonique GsC doit être supérieur à zéro."
        );
      }

      IndentationCanonique = new string(c: ' ', count: NombreEspaces);
    }

		/**
     * <summary>
     *   [FR] Configure l'indentation canonique pour qu'elle utilise des tabulations.<br/>
     *   [EN] Configures the canonical indentation to use tabs.
     * </summary>
     **/
		public void UtiliserTabulationPourIndentationCanonique() => IndentationCanonique = "\t";

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de l'instance actuelle de <see cref="AnalyseurGsC"/>.<br/>
     *   [EN] Creates a deep copy of the current instance of <see cref="AnalyseurGsC"/>.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="AnalyseurGsC"/> qui est une copie en profondeur de l'instance actuelle.<br/>
     *   [EN] A new instance of <see cref="AnalyseurGsC"/> that is a deep copy of the current instance.
     * </returns>
     **/
		public AnalyseurGsC CloneEnProfondeur() => new(AnalyseurGsCInstance: this);
  }
}
