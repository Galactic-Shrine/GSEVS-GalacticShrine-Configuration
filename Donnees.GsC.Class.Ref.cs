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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalacticShrine.Configuration.Configuration;
using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Interface.Configuration;
using GalacticShrine.Modele.Configuration.GsC;

namespace GalacticShrine.Configuration {

  /**
   * <summary>
   *   [FR] Représente les données complètes d'un fichier GsC.<br/>
   *   [EN] Represents the complete data of a GsC file.
   * </summary>
   **/
  public class DonneesGsC : ClonableInterface<DonneesGsC> {

		/**
     * <summary>
     *   [FR] Séparateur utilisé pour les chemins de sections et de sous-sections dans le fichier GsC.<br/>
     *   [EN] Separator used for section and subsection paths in the GsC file.
     * </summary>
     **/
		private const string SeparateurDeSousSection = "::";

		/**
     * <summary>
     *   [FR] Comparer utilisé pour la recherche de sections et de propriétés, basé sur la configuration d'insensibilité à la casse.<br/>
     *   [EN] Comparer used for searching sections and properties, based on the case-insensitivity configuration.
     * </summary>
     **/
		private readonly IEqualityComparer<string> RechercherComparer;

		/**
     * <summary>
     *   [FR] Dictionnaire interne pour stocker les sections par leur chemin complet, facilitant la recherche rapide.<br/>
     *   [EN] Internal dictionary to store sections by their full path, facilitating quick lookup.
     * </summary>
     **/
		private readonly Dictionary<string, SectionGsC> SectionsParNom;

		/**
     * <summary>
     *   [FR] Schéma de validation pour les données GsC, utilisé pour vérifier la conformité des sections et des propriétés.<br/>
     *   [EN] Validation schema for GsC data, used to check the conformity of sections and properties.
     * </summary>
     **/
		public SchemaGsC Schema { get; set; }

		/**
     * <summary>
     *   [FR] Configuration d'analyse pour les données GsC, influençant la manière dont les sections et les propriétés sont traitées.<br/>
     *   [EN] Analysis configuration for GsC data, influencing how sections and properties are handled.
     * </summary>
     **/
		public AnalyseurGsC Configuration { get; set; }

		/**
     * <summary>
     *   [FR] Propriétés globales du fichier GsC, déclarées hors section.<br/>
     *   [EN] Global properties of the GsC file, declared outside any section.
     * </summary>
     **/
		public ObjetGsC ProprieteGlobales { get; set; }

    /**
     * <summary>
     *   [FR] Alias court vers les propriétés globales du fichier GsC.<br/>
     *   [EN] Short alias to the global properties of the GsC file.
     * </summary>
     **/
    public ObjetGsC Propriete => ProprieteGlobales;

		/**
     * <summary>
     *   [FR] Collection des sections racines du fichier GsC.<br/>
     *   [EN] Collection of root sections of the GsC file.
     * </summary>
     **/
		public Collection<SectionGsC> Sections { get; }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe DonneesGsC avec un schéma et une configuration d'analyse optionnels.<br/>
     *   [EN] Initializes a new instance of the DonneesGsC class with optional schema and analysis configuration.
     * </summary>
     * <param name="SchemaGsCInstance">
     *   [FR] Schéma de validation pour les données GsC. Si null, un schéma par défaut sera utilisé.<br/>
     *   [EN] Validation schema for GsC data. If null, a default schema will be used.
     * </param>
     * <param name="ConfigurationGsCInstance">
     *   [FR] Configuration d'analyse pour les données GsC. Si null, une configuration par défaut sera utilisée.<br/>
     *   [EN] Analysis configuration for GsC data. If null, a default configuration will be used.
     * </param>
     **/
		public DonneesGsC(SchemaGsC SchemaGsCInstance = null, AnalyseurGsC ConfigurationGsCInstance = null) {

      Schema = (SchemaGsCInstance ?? new SchemaGsC()).CloneEnProfondeur();
      Configuration = (ConfigurationGsCInstance ?? new AnalyseurGsC()).CloneEnProfondeur();
      RechercherComparer = Configuration.InsensibleA_LaCasse ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
      Sections = [];
      SectionsParNom = new Dictionary<string, SectionGsC>(comparer: RechercherComparer);
      ProprieteGlobales = new ObjetGsC(RechercherComparer: RechercherComparer);
    }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe DonneesGsC en copiant les données d'une instance existante, 
     *        créant ainsi une copie en profondeur.<br/>
     *   [EN] Initializes a new instance of the DonneesGsC class by copying data from an existing instance, 
     *        thus creating a deep copy.
     * </summary>
     * <param name="DonneesGsCInstance">
     *   [FR] Instance de DonneesGsC à copier. Ne peut pas être null.<br/>
     *   [EN] DonneesGsC instance to copy. Cannot be null.
     * </param>
     **/
		public DonneesGsC(DonneesGsC DonneesGsCInstance) 
      : this(SchemaGsCInstance: DonneesGsCInstance.Schema, ConfigurationGsCInstance: DonneesGsCInstance.Configuration) {

      ProprieteGlobales = DonneesGsCInstance.ProprieteGlobales.CloneEnProfondeur();

      foreach(SectionGsC section in DonneesGsCInstance.Sections) {

        AjouterSection(Section: new SectionGsC(SectionGsCInstance: section, RechercherComparer: RechercherComparer), Remplacer: true);
      }
    }

		/**
     * <summary>
     *   [FR] Indexeur pour accéder directement à une section par son nom ou son chemin, facilitant la navigation dans les données GsC.<br/>
     *   [EN] Indexer to directly access a section by its name or path, facilitating navigation through GsC data.
     * </summary>
     * <param name="NomDeLaSection">
     *   [FR] Nom ou chemin de la section à accéder. Peut inclure des séparateurs pour les sous-sections.<br/>
     *   [EN] Name or path of the section to access. Can include separators for subsections.
     * </param>
     * <returns>
     *   [FR] La section correspondante au nom ou chemin fourni. Si la section n'existe pas, une exception sera levée.<br/>
     *   [EN] The section corresponding to the provided name or path. If the section does not exist, an exception will be thrown.
     * </returns>
     **/
		public SectionGsC this[string NomDeLaSection] => ObtenirSection(NomDeLaSection: NomDeLaSection);

		/**
     * <summary>
     *   [FR] Ajoute une section à la collection de données GsC. Si une section avec le même nom existe déjà, 
     *        elle peut être remplacée ou une exception peut être levée en fonction du paramètre Remplacer.<br/>
     *   [EN] Adds a section to the GsC data collection. If a section with the same name already exists, 
     *        it can be replaced or an exception can be thrown based on the Remplacer parameter.
     * </summary>
     * <param name="Section">
     *   [FR] Section à ajouter. Ne peut pas être null.<br/>
     *   [EN] Section to add. Cannot be null.
     * </param>
     * <param name="Remplacer">
     *   [FR] Indique si une section existante avec le même nom doit être remplacée. Par défaut, true.<br/>
     *   [EN] Indicates whether an existing section with the same name should be replaced. Default is true.
     * </param>
     **/
		public void AjouterSection(SectionGsC Section, bool Remplacer = true) {

      if(Section == null) {

        throw new ArgumentNullException(paramName: nameof(Section));
      }

      if(Section.Nom.Contains(value: SeparateurDeSousSection)) {

        AjouterSectionParChemin(CheminDeSection: Section.Nom, Section: Section, Remplacer: Remplacer);
        return;
      }

      if(SectionsParNom.TryGetValue(key: Section.Nom, value: out SectionGsC sectionExistante) && Sections.Contains(item: sectionExistante)) {

        if(!Remplacer) {

          throw new InvalidOperationException(message: string.Format(format: "La section GsC '{0}' existe déjà.", arg0: Section.Nom));
        }

        RetirerCheminsDeSection(CheminDeSection: Section.Nom);
        int index = Sections.IndexOf(item: sectionExistante);
        Sections[index] = Section;
        EnregistrerSectionEtSousSections(CheminDeSection: Section.Nom, Section: Section);
        return;
      }

      Sections.Add(item: Section);
      EnregistrerSectionEtSousSections(CheminDeSection: Section.Nom, Section: Section);
    }

		/**
     * <summary>
     *   [FR] Ajoute une section à la collection de données GsC en utilisant un chemin de section,
     *        permettant d'ajouter des sections imbriquées facilement.<br/>
     *   [EN] Adds a section to the GsC data collection using a section path, 
     *        allowing for easy addition of nested sections.
     * </summary>
     * <param name="CheminDeSection">
     *   [FR] Chemin de la section à ajouter, utilisant le séparateur pour indiquer les niveaux de sous-sections. Ne peut pas être vide ou null.<br/>
     *   [EN] Path of the section to add, using the separator to indicate levels of subsections. Cannot be empty or null.
     * </param>
     * <param name="Section">
     *   [FR] Section à ajouter. Ne peut pas être null.<br/>
     *   [EN] Section to add. Cannot be null.
     * </param>
     * <param name="Remplacer">
     *   [FR] Indique si une section existante avec le même chemin doit être remplacée. Par défaut, true.<br/>
     *   [EN] Indicates whether an existing section with the same path should be replaced. Default is true.
     * </param>
     * <returns>
     *   [FR] La section ajoutée ou remplacée correspondant au chemin fourni.<br/>
     *   [EN] The added or replaced section corresponding to the provided path.
     * </returns>
     **/
		public SectionGsC AjouterSectionParChemin(string CheminDeSection, SectionGsC Section, bool Remplacer = true) {

      if(string.IsNullOrWhiteSpace(value: CheminDeSection)) {

        throw new ArgumentException(message: "Le chemin de section GsC ne peut pas être vide.", paramName: nameof(CheminDeSection));
      }

      if(Section == null) {

        throw new ArgumentNullException(paramName: nameof(Section));
      }

      string[] segments = DecouperChemin(CheminDeSection: CheminDeSection);

      if(segments.Length == 0) {

        throw new ArgumentException(message: "Le chemin de section GsC ne peut pas être vide.", paramName: nameof(CheminDeSection));
      }

      if(segments.Length == 1) {

        Section.Nom = segments[0];
        AjouterSection(Section: Section, Remplacer: Remplacer);
        return ObtenirSection(NomDeLaSection: segments[0]);
      }

      SectionGsC parent = ObtenirOuCreerSectionRacine(NomDeLaSection: segments[0]);
      string cheminParent = segments[0];

      for(int i = 1; i < segments.Length - 1; i++) {

        string nomDuSegment = segments[i];
        cheminParent = string.Concat(str0: cheminParent, str1: SeparateurDeSousSection, str2: nomDuSegment);

        if(!parent.ContientSection(NomDeLaSection: nomDuSegment)) {

          SectionGsC sectionIntermediaire = new(Nom: nomDuSegment, RechercherComparer: RechercherComparer);
          parent.AjouterSection(Section: sectionIntermediaire, Remplacer: true);
          EnregistrerSectionEtSousSections(CheminDeSection: cheminParent, Section: sectionIntermediaire);
        }

        parent = parent.ObtenirSection(NomDeLaSection: nomDuSegment);
      }

      Section.Nom = segments[^1];
      string cheminComplet = string.Concat(str0: cheminParent, str1: SeparateurDeSousSection, str2: Section.Nom);

      if(SectionsParNom.ContainsKey(key: cheminComplet) && !Remplacer) {

        throw new InvalidOperationException(message: string.Format(format: "La section GsC '{0}' existe déjà.", arg0: cheminComplet));
      }

      RetirerCheminsDeSection(CheminDeSection: cheminComplet);
      parent.AjouterSection(Section: Section, Remplacer: Remplacer);
      EnregistrerSectionEtSousSections(CheminDeSection: cheminComplet, Section: Section);
      return Section;
    }

		/**
     * <summary>
     *   [FR] Vérifie si une section existe dans les données GsC en utilisant son nom ou son chemin, 
     *        facilitant la validation de l'existence de sections avant d'y accéder.<br/>
     *   [EN] Checks if a section exists in the GsC data using its name or path, 
     *        facilitating validation of the existence of sections before accessing them.
     * </summary>
     * <param name="NomDeLaSection">
     *   [FR] Nom ou chemin de la section à vérifier. Peut inclure des séparateurs pour les sous-sections.<br/>
     *   [EN] Name or path of the section to check. Can include separators for subsections.
     * </param>
     * <returns>
     *   [FR] true si la section existe, sinon false.<br/>
     *   [EN] true if the section exists, otherwise false.
     * </returns>
     **/
		public bool ContientSection(string NomDeLaSection) => SectionsParNom.ContainsKey(key: NormaliserChemin(CheminDeSection: NomDeLaSection));

		/**
     * <summary>
     *   [FR] Obtient une section à partir de son nom ou de son chemin. Si la section n'existe pas, une exception est levée.<br/>
     *   [EN] Retrieves a section by its name or path. If the section does not exist, an exception is thrown.
     * </summary>
     * <param name="NomDeLaSection">
     *   [FR] Nom ou chemin de la section à obtenir. Peut inclure des séparateurs pour les sous-sections.<br/>
     *   [EN] Name or path of the section to retrieve. Can include separators for subsections.
     * </param>
     * <returns>
     *   [FR] La section correspondante au nom ou chemin fourni.<br/>
     *   [EN] The section corresponding to the provided name or path.
     * </returns>
     **/
		public SectionGsC ObtenirSection(string NomDeLaSection) => SectionsParNom[NormaliserChemin(CheminDeSection: NomDeLaSection)];

		    /**
     * <summary>
     *   [FR] Efface les commentaires, les propriétés globales et/ou les sections de ces données GsC selon le mode demandé.<br/>
     *   [EN] Clears comments, global properties and/or sections from these GsC data according to the requested mode.
     * </summary>
     * <remarks>
     *   [FR] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/> efface les commentaires des propriétés globales, des sections et des valeurs imbriquées, sans supprimer les données.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/> supprime les propriétés globales et toutes les sections.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/> a le même effet au niveau racine, car les sections supprimées emportent aussi leurs commentaires.</para>
     *   [EN] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/> clears comments from global properties, sections and nested values without removing data.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/> removes global properties and all sections.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/> has the same effect at root level because removed sections also remove their comments.</para>
     * </remarks>
     * <param name="Effacement">
     *   [FR] Mode d'effacement à appliquer. La valeur par défaut est <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>.<br/>
     *   [EN] Clearing mode to apply. The default value is <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>.
     * </param>
     **/

		public void Effacer(Effacement Effacement = Effacement.Proprietes) {

      switch(Effacement) {

        case Effacement.Commentaires:

          ProprieteGlobales.Effacer(Effacement: Effacement.Commentaires);

          foreach(SectionGsC section in Sections) {

            section.Effacer(Effacement: Effacement.Commentaires);
          }
          break;

        case Effacement.Proprietes:
        case Effacement.Tout:

          ProprieteGlobales = new ObjetGsC(RechercherComparer: RechercherComparer);
          Sections.Clear();
          SectionsParNom.Clear();
          break;
      }
    }

		/**
     * <summary>
     *   [FR] Obtient une section racine existante ou en crée une nouvelle si elle n'existe pas, 
     *        facilitant la gestion des sections principales dans les données GsC.<br/>
     *   [EN] Retrieves an existing root section or creates a new one if it does not exist, 
     *        facilitating the management of main sections in GsC data.
     * </summary>
     * <param name="NomDeLaSection">
     *   [FR] Nom de la section racine à obtenir ou créer. Ne peut pas être vide ou null.<br/>
     *   [EN] Name of the root section to retrieve or create. Cannot be empty or null.
     * </param>
     * <returns>
     *   [FR] La section racine correspondante au nom fourni, qu'elle ait été existante ou nouvellement créée.<br/>
     *   [EN] The root section corresponding to the provided name, whether it was existing or newly created.
     * </returns>
     **/
		private SectionGsC ObtenirOuCreerSectionRacine(string NomDeLaSection) {

      if(SectionsParNom.TryGetValue(key: NomDeLaSection, value: out SectionGsC sectionExistante) && Sections.Contains(item: sectionExistante)) {

        return sectionExistante;
      }

      SectionGsC section = new(Nom: NomDeLaSection, RechercherComparer: RechercherComparer);
      AjouterSection(Section: section, Remplacer: true);
      return section;
    }

		/**
     * <summary>
     *   [FR] Enregistre une section et toutes ses sous-sections dans le dictionnaire interne, 
     *        permettant une recherche rapide des sections par leur chemin complet.<br/>
     *   [EN] Registers a section and all its subsections in the internal dictionary, 
     *        allowing for quick lookup of sections by their full path.
     * </summary>
     * <param name="CheminDeSection">
     *   [FR] Chemin complet de la section à enregistrer, incluant les séparateurs pour les sous-sections.<br/>
     *   [EN] Full path of the section to register, including separators for subsections.
     * </param>
     * <param name="Section">
     *   [FR] Section à enregistrer. Ne peut pas être null.<br/>
     *   [EN] Section to register. Cannot be null.
     * </param>
     **/
		private void EnregistrerSectionEtSousSections(string CheminDeSection, SectionGsC Section) {

      SectionsParNom[CheminDeSection] = Section;

      foreach(SectionGsC sousSection in Section.SousSections) {

        EnregistrerSectionEtSousSections(
          CheminDeSection: string.Concat(str0: CheminDeSection, str1: SeparateurDeSousSection, str2: sousSection.Nom), 
          Section: sousSection
        );
      }
    }

		/**
     * <summary>
     *   [FR] Retire une section et toutes ses sous-sections du dictionnaire interne, 
     *        assurant que les sections supprimées ne sont plus accessibles par leur chemin.<br/>
     *   [EN] Removes a section and all its subsections from the internal dictionary, 
     *        ensuring that removed sections are no longer accessible by their path.
     * </summary>
     * <param name="CheminDeSection">
     *   [FR] Chemin complet de la section à retirer, incluant les séparateurs pour les sous-sections.<br/>
     *   [EN] Full path of the section to remove, including separators for subsections.
     * </param>
     **/
		private void RetirerCheminsDeSection(string CheminDeSection) {

      List<string> clesA_Retirer = [];
      string prefixe = string.Concat(str0: CheminDeSection, str1: SeparateurDeSousSection);

      foreach(string cle in SectionsParNom.Keys) {

        if(RechercherComparer.Equals(cle, CheminDeSection) || cle.StartsWith(
            value: prefixe, 
            comparisonType: Configuration.InsensibleA_LaCasse ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal
          )
        ) {

          clesA_Retirer.Add(item: cle);
        }
      }

      foreach(string cle in clesA_Retirer) {

        SectionsParNom.Remove(key: cle);
      }
    }

		/**
     * <summary>
     *   [FR] Normalise un chemin de section en supprimant les espaces inutiles et en utilisant le séparateur standard, 
     *        assurant que les chemins sont cohérents pour la recherche dans le dictionnaire.<br/>
     *   [EN] Normalizes a section path by removing unnecessary spaces and using the standard separator, 
     *        ensuring that paths are consistent for lookup in the dictionary.
     * </summary>
     * <param name="CheminDeSection">
     *   [FR] Chemin de section à normaliser. Peut inclure des espaces et des séparateurs multiples.<br/>
     *   [EN] Section path to normalize. Can include spaces and multiple separators.
     * </param>
     * <returns>
     *   [FR] Chemin de section normalisé, prêt à être utilisé pour la recherche dans le dictionnaire.<br/>
     *   [EN] Normalized section path, ready to be used for lookup in the dictionary.
     * </returns>
     **/
		private static string NormaliserChemin(string CheminDeSection) 
      => string.Join(separator: SeparateurDeSousSection, value: DecouperChemin(CheminDeSection: CheminDeSection));

		/**
     * <summary>
     *   [FR] Découpe un chemin de section en segments individuels en utilisant le séparateur, 
     *        tout en supprimant les segments vides et en trimant les espaces, 
     *        facilitant ainsi la manipulation des chemins de sections.<br/>
     *   [EN] Splits a section path into individual segments using the separator, 
     *        while removing empty segments and trimming spaces, 
     *        thus facilitating the handling of section paths.
     * </summary>
     * <param name="CheminDeSection">
     *   [FR] Chemin de section à découper. Peut inclure des espaces et des séparateurs multiples.<br/>
     *   [EN] Section path to split. Can include spaces and multiple separators.
     * </param>
     * <returns>
     *   [FR] Tableau de segments individuels extraits du chemin de section, prêts à être utilisés pour la navigation dans les sections.<br/>
     *   [EN] Array of individual segments extracted from the section path, ready to be used for navigating through sections.
     * </returns>
     **/
		private static string[] DecouperChemin(string CheminDeSection) {

      if(string.IsNullOrWhiteSpace(value: CheminDeSection)) {

        return [];
      }

      return CheminDeSection.Split(separator: SeparateurDeSousSection, options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de l'instance actuelle de DonneesGsC, 
     *        permettant de dupliquer les données sans partager les références.<br/>
     *   [EN] Creates a deep copy of the current instance of DonneesGsC, 
     *        allowing for duplication of data without sharing references.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de DonneesGsC contenant une copie en profondeur des données actuelles.<br/>
     *   [EN] A new instance of DonneesGsC containing a deep copy of the current data.
     * </returns>
     **/
		public DonneesGsC CloneEnProfondeur() => new(DonneesGsCInstance: this);
	}
}
