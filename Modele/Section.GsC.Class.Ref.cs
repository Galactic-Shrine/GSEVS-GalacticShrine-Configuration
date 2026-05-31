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
using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.GsC {

  /**
   * <summary>
   *   [FR] Représente une section GsC.<br/>
   *   [EN] Represents a GsC section.
   * </summary>
   **/
  public class SectionGsC : ObjetGsC, ClonableInterface<SectionGsC> {

		/**
		 * <summary>
     *    [FR] Séparateur utilisé pour indiquer les niveaux de sous-sections dans les chemins de section GsC.<br/>
     *    [EN] Separator used to indicate sub-section levels in GsC section paths.
     * </summary>
     **/
		private const string SeparateurDeSousSection = "::";

		/**
     * <summary>
     *   [FR] Comparateur d'égalité utilisé pour les recherches de sections GsC par leur nom.<br/>
     *   [EN] Equality comparer used for GsC section lookups by their name.
     * </summary>
     **/
		private readonly IEqualityComparer<string> RechercherComparer;

		/**
     * <summary>
     *   [FR] Dictionnaire interne pour un accès rapide aux sous-sections GsC par leur nom.<br/>
     *   [EN] Internal dictionary for fast access to GsC sub-sections by their name.
     * </summary>
     **/
		private readonly Dictionary<string, SectionGsC> SousSectionsParNom;

		/**
     * <summary>
     *   [FR] Le nom de la section GsC.<br/>
     *   [EN] The name of the GsC section.
     * </summary>
     **/
		public string Nom { get; set; }

		/**
     * <summary>
     *   [FR] Liste de commentaires associés à cette section GsC.<br/>
     *   [EN] List of comments associated with this GsC section.
     * </summary>
     **/
		public List<string> Commentaire { get; set; }

		/**
     * <summary>
     *   [FR] Collection de sous-sections GsC contenues dans cette section GsC.<br/>
     *   [EN] Collection of GsC sub-sections contained within this GsC section.
     * </summary>
     **/
		public Collection<SectionGsC> SousSections { get; }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="SectionGsC"/> avec un nom spécifié et un comparateur de recherche optionnel.<br/>
     *   [EN] Initializes a new instance of the <see cref="SectionGsC"/> class with a specified name and an optional search comparer.
     * </summary>
     * <param name="Nom">
     *   [FR] Le nom de la section GsC.<br/>
     *   [EN] The name of the GsC section.
     * </param>
     * <param name="RechercherComparer">
     *   [FR] Un comparateur d'égalité pour les recherches de sections GsC, ou null pour utiliser le comparateur par défaut (Ordinal).<br/>
     *   [EN] An equality comparer for GsC section lookups, or null to use the default comparer (Ordinal).
     * </param>
     **/
		public SectionGsC(string Nom) : this(Nom: Nom, RechercherComparer: StringComparer.Ordinal) { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="SectionGsC"/> avec un nom spécifié et un comparateur de recherche.<br/>
     *   [EN] Initializes a new instance of the <see cref="SectionGsC"/> class with a specified name and search comparer.
     * </summary>
     * <param name="Nom">
     *   [FR] Le nom de la section GsC.<br/>
     *   [EN] The name of the GsC section.
     * </param>
     * <param name="RechercherComparer">
     *   [FR] Un comparateur d'égalité pour les recherches de sections GsC, ou null pour utiliser le comparateur par défaut (Ordinal).<br/>
     *   [EN] An equality comparer for GsC section lookups, or null to use the default comparer (Ordinal).
     * </param>
     **/
		public SectionGsC(string Nom, IEqualityComparer<string> RechercherComparer) : base(RechercherComparer: RechercherComparer) {

      if(string.IsNullOrWhiteSpace(value: Nom)) {

        throw new ArgumentException(message: "Le nom de la section GsC ne peut pas être vide.", paramName: nameof(Nom));
      }

      this.RechercherComparer = RechercherComparer ?? StringComparer.Ordinal;
      this.Nom = Nom;
      Commentaire = [];
      SousSections = [];
      SousSectionsParNom = new Dictionary<string, SectionGsC>(comparer: this.RechercherComparer);
    }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="SectionGsC"/> en copiant les propriétés d'une instance existante de 
     *        <see cref="SectionGsC"/>, avec un comparateur de recherche optionnel.<br/>
     *   [EN] Initializes a new instance of the <see cref="SectionGsC"/> class by copying properties from an existing instance of 
     *        <see cref="SectionGsC"/>, with an optional search comparer.
     * </summary>
     * <param name="SectionGsCInstance">
     *   [FR] L'instance de <see cref="SectionGsC"/> à copier.<br/>
     *   [EN] The instance of <see cref="SectionGsC"/> to copy.
     * </param>
     * <param name="RechercherComparer">
     *   [FR] Un comparateur d'égalité pour les recherches de sections GsC, 
     *        ou null pour utiliser le comparateur de l'instance source ou le comparateur par défaut (Ordinal).<br/>
     *   [EN] An equality comparer for GsC section lookups, 
     *        or null to use the source instance's comparer or the default comparer (Ordinal).
     * </param>
     **/
		public SectionGsC(SectionGsC SectionGsCInstance, IEqualityComparer<string> RechercherComparer = null) 
      : base(
          ObjetGsCInstance: SectionGsCInstance, 
          RechercherComparer: RechercherComparer ?? SectionGsCInstance.RechercherComparer ?? StringComparer.Ordinal
      ) {

      this.RechercherComparer = RechercherComparer ?? SectionGsCInstance.RechercherComparer ?? StringComparer.Ordinal;
      Nom = SectionGsCInstance.Nom;
      Commentaire = [.. SectionGsCInstance.Commentaire];
      SousSections = [];
      SousSectionsParNom = new Dictionary<string, SectionGsC>(comparer: this.RechercherComparer);

      foreach(SectionGsC sousSection in SectionGsCInstance.SousSections) {

        AjouterSection(Section: new SectionGsC(SectionGsCInstance: sousSection, RechercherComparer: this.RechercherComparer), Remplacer: true);
      }
    }

    /**
     * <summary>
     *   [FR] Ajoute une sous-section GsC à cette section GsC, avec une option pour remplacer une sous-section existante du même nom.<br/>
     *   [EN] Adds a GsC sub-section to this GsC section, with an option to replace an existing sub-section of the same name.
     * </summary>
     * <param name="Section">
     *   [FR] La sous-section GsC à ajouter.<br/>
     *   [EN] The GsC sub-section to add.
     * </param>
     * <param name="Remplacer">
     *   [FR] Indique si une sous-section existante du même nom doit être remplacée (true) ou si une exception doit être levée (false).<br/>
     *   [EN] Indicates whether an existing sub-section of the same name should be replaced (true) or if an exception should be thrown (false).
     * </param>
     **/
    public void AjouterSection(SectionGsC Section, bool Remplacer = true) {

      if(Section == null) {

        throw new ArgumentNullException(paramName: nameof(Section));
      }

      if(SousSectionsParNom.TryGetValue(key: Section.Nom, value: out SectionGsC sectionExistante)) {

        if(!Remplacer) {

          throw new InvalidOperationException(message: string.Format(format: "La sous-section GsC '{0}' existe déjà.", arg0: Section.Nom));
        }

        int index = SousSections.IndexOf(item: sectionExistante);
        SousSections[index] = Section;
        SousSectionsParNom[Section.Nom] = Section;
        return;
      }

      SousSections.Add(item: Section);
      SousSectionsParNom.Add(key: Section.Nom, value: Section);
    }

		/**
     * <summary>
     *   [FR] Vérifie si une section GsC existe à un chemin de section GsC spécifié.<br/>
     *   [EN] Checks if a GsC section exists at a specified GsC section path.
     * </summary>
     * <param name="NomDeLaSection">
     *   [FR] Le chemin de la section GsC à vérifier.<br/>
     *   [EN] The GsC section path to check.
     * </param>
     * <returns>
     *   [FR] true si une section GsC existe à ce chemin de section GsC, sinon false.<br/>
     *   [EN] true if a GsC section exists at that GsC section path, otherwise false.
     * </returns>
     **/
		public bool ContientSection(string NomDeLaSection) {

      string[] chemins = DecouperChemin(CheminDeSection: NomDeLaSection);

      if(chemins.Length == 0) {

        return false;
      }

      SectionGsC sectionActuelle = this;
      int index = RechercherComparer.Equals(chemins[0], Nom) ? 1 : 0;

      for(int i = index; i < chemins.Length; i++) {

        if(!sectionActuelle.SousSectionsParNom.TryGetValue(key: chemins[i], value: out sectionActuelle)) {

          return false;
        }
      }

      return true;
    }

		/**
     * <summary>
     *   [FR] Obtient la section GsC située à un chemin de section GsC spécifié.<br/>
     *   [EN] Gets the GsC section located at a specified GsC section path.
     * </summary>
     * <param name="NomDeLaSection">
     *   [FR] Le chemin de la section GsC à obtenir.<br/>
     *   [EN] The GsC section path to get.
     * </param>
     * <returns>
     *   [FR] La section GsC située à ce chemin de section GsC.<br/>
     *   [EN] The GsC section located at that GsC section path.
     * </returns>
     **/
		public SectionGsC ObtenirSection(string NomDeLaSection) {

      string[] chemins = DecouperChemin(CheminDeSection: NomDeLaSection);

      if(chemins.Length == 0) {

        throw new ArgumentException(message: "Le nom de la sous-section GsC ne peut pas être vide.", paramName: nameof(NomDeLaSection));
      }

      SectionGsC sectionActuelle = this;
      int index = RechercherComparer.Equals(chemins[0], Nom) ? 1 : 0;

      for(int i = index; i < chemins.Length; i++) {

        sectionActuelle = sectionActuelle.SousSectionsParNom[chemins[i]];
      }

      return sectionActuelle;
    }

        /**
     * <summary>
     *   [FR] Efface les commentaires, les propriétés et/ou les sous-sections de cette section GsC selon le mode demandé.<br/>
     *   [EN] Clears comments, properties and/or subsections from this GsC section according to the requested mode.
     * </summary>
     * <remarks>
     *   [FR] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/> efface le commentaire de la section, les commentaires des propriétés et les commentaires des sous-sections de manière récursive.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/> supprime les propriétés et les sous-sections, mais conserve le commentaire de la section.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/> supprime le commentaire de la section, les propriétés et les sous-sections.</para>
     *   [EN] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/> clears the section comment, property comments and subsection comments recursively.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/> removes properties and subsections while keeping the section comment.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/> removes the section comment, properties and subsections.</para>
     * </remarks>
     * <param name="Effacement">
     *   [FR] Mode d'effacement à appliquer. La valeur par défaut est <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/>.<br/>
     *   [EN] Clearing mode to apply. The default value is <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/>.
     * </param>
     **/

    public new void Effacer(Effacement Effacement = Effacement.Tout) {

      switch(Effacement) {

        case Effacement.Commentaires:

          Commentaire.Clear();
          base.Effacer(Effacement: Effacement.Commentaires);

          foreach(SectionGsC sousSection in SousSections) {

            sousSection.Effacer(Effacement: Effacement.Commentaires);
          }
          break;

        case Effacement.Proprietes:

          base.Effacer(Effacement: Effacement.Proprietes);
          SousSections.Clear();
          SousSectionsParNom.Clear();
          break;

        case Effacement.Tout:

          Commentaire.Clear();
          base.Effacer(Effacement: Effacement.Proprietes);
          SousSections.Clear();
          SousSectionsParNom.Clear();
          break;
      }
    }

		/**
     * <summary>
     *   [FR] Découpe un chemin de section GsC en segments individuels, en utilisant le séparateur de sous-section.<br/>
     *   [EN] Splits a GsC section path into individual segments using the sub-section separator.
     * </summary>
     * <param name="CheminDeSection">
     *   [FR] Le chemin de la section GsC à découper.<br/>
     *   [EN] The GsC section path to split.
     * </param>
     * <returns>
     *   [FR] Un tableau de segments individuels du chemin de la section GsC.<br/>
     *   [EN] An array of individual segments of the GsC section path.
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
     *   [FR] Crée une copie profonde de cette section GsC.<br/>
     *   [EN] Creates a deep copy of this GsC section.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="SectionGsC"/> qui est une copie profonde de cette section GsC.<br/>
     *   [EN] A new instance of <see cref="SectionGsC"/> that is a deep copy of this GsC section.
     * </returns>
     **/
		SectionGsC ClonableInterface<SectionGsC>.CloneEnProfondeur() => new(SectionGsCInstance: this);
  }
}
