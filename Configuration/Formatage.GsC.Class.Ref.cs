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
using System.Globalization;
using System.Text;
using GalacticShrine.Enumeration.Configuration.GsC;
using GalacticShrine.Modele.Configuration.GsC;

namespace GalacticShrine.Configuration.Configuration {

  /**
   * <summary>
   *   [FR] Convertit un modèle GsC en texte GsC canonique.<br/>
   *   [EN] Converts a GsC model into canonical GsC text.
   * </summary>
   **/
  public class FormatageGsC {

		/**
     * <summary>
     *   [FR] Le saut de ligne canonique utilisé pour la génération d'un fichier GsC.<br/>
     *   [EN] The canonical newline used for generating a GsC file.
     * </summary>
     **/
		public const string SautDeLigneCanonique = "\n";

		/**
     * <summary>
     *   [FR] L'indentation canonique utilisant des espaces pour la génération d'un fichier GsC.<br/>
     *   [EN] The canonical indentation using spaces for generating a GsC file.
     * </summary>
     **/
		public const string IndentationEspacesCanonique = "  ";

		/**
     * <summary>
     *   [FR] L'indentation canonique utilisant des tabulations pour la génération d'un fichier GsC.<br/>
     *   [EN] The canonical indentation using tabs for generating a GsC file.
     * </summary>
     **/
		public const string IndentationTabulationCanonique = "\t";

		/**
     * <summary>
     *   [FR] Le schéma de formatage GsC utilisé pour la génération d'un fichier GsC.<br/>
     *   [EN] The GsC formatting schema used for generating a GsC file.
     * </summary>
     **/
		private readonly SchemaGsC Schema;

		/**
     * <summary>
     *   [FR] L'indentation canonique utilisée pour la génération d'un fichier GsC.<br/>
     *   [EN] The canonical indentation used for generating a GsC file.
     * </summary>
     **/
		private readonly string IndentationCanonique;

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="FormatageGsC"/> 
     *        avec le schéma de formatage GsC spécifié et l'indentation canonique par défaut (espaces).<br/>
     *   [EN] Initializes a new instance of the <see cref="FormatageGsC"/> class 
     *        with the specified GsC formatting schema and the default canonical indentation (spaces).
     * </summary>
     **/
		public FormatageGsC(SchemaGsC Schema) : this(Schema: Schema, IndentationCanonique: IndentationEspacesCanonique) { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="FormatageGsC"/> 
     *        avec le schéma de formatage GsC spécifié et l'indentation canonique définie dans la configuration d'analyseur GsC, 
     *        ou l'indentation canonique par défaut (espaces) si la configuration d'analyseur GsC est nulle.<br/>
     *   [EN] Initializes a new instance of the <see cref="FormatageGsC"/> class 
     *        with the specified GsC formatting schema and the canonical indentation defined in the GsC parser configuration, 
     *        or the default canonical indentation (spaces) if the GsC parser configuration is null.
     * </summary>
     **/
		public FormatageGsC(SchemaGsC Schema, AnalyseurGsC Configuration) 
      : this(Schema: Schema, IndentationCanonique: Configuration?.IndentationCanonique ?? IndentationEspacesCanonique) { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="FormatageGsC"/> 
     *        avec le schéma de formatage GsC spécifié et l'indentation canonique spécifiée.<br/>
     *   [EN] Initializes a new instance of the <see cref="FormatageGsC"/> class 
     *        with the specified GsC formatting schema and the specified canonical indentation.
     * </summary>
     **/
		public FormatageGsC(SchemaGsC Schema, string IndentationCanonique) {

      this.Schema = Schema ?? throw new ArgumentNullException(paramName: nameof(Schema));
      this.IndentationCanonique = NormaliserIndentation(IndentationCanonique: IndentationCanonique);
    }

		/**
     * <summary>
     *   [FR] Formate les données GsC spécifiées en texte GsC canonique selon le schéma de formatage 
     *        et l'indentation canonique définis dans cette instance.<br/>
     *   [EN] Formats the specified GsC data into canonical GsC text according to the formatting schema and canonical indentation defined in this instance.
     * </summary>
     **/
		public string Formater(DonneesGsC Donnees) {

      StringBuilder texte = new();

      foreach(ProprieteGsC propriete in Donnees.ProprieteGlobales.Proprietes) {

        EcrirePropriete(Texte: texte, Propriete: propriete, Niveau: 0);
      }

      if(Donnees.ProprieteGlobales.Proprietes.Count > 0 && Donnees.Sections.Count > 0) {

        EcrireSautDeLigne(Texte: texte);
      }

      for(int i = 0; i < Donnees.Sections.Count; i++) {

        if(i > 0) {

          EcrireSautDeLigne(Texte: texte);
        }

        EcrireSection(Texte: texte, Section: Donnees.Sections[i], Niveau: 0);
      }

      return texte.ToString();
    }

		/**
     * <summary>
     *   [FR] Écrit une section GsC dans le texte de sortie en respectant le schéma de formatage et l'indentation canonique définis dans cette instance.<br/>
     *   [EN] Writes a GsC section to the output text while respecting the formatting schema and canonical indentation defined in this instance.
     * </summary>
     **/
		private void EcrireSection(StringBuilder Texte, SectionGsC Section, int Niveau) {

      foreach(string commentaire in Section.Commentaire) {

        EcrireCommentaire(Texte: Texte, Commentaire: commentaire, Niveau: Niveau);
      }

      EcrireIndentation(Texte: Texte, Niveau: Niveau);
      Texte.Append(Schema.ChaineDeDebutDeSectionOfficielle);
      EcrireNom(Texte: Texte, Nom: Section.Nom);
      Texte.Append(Schema.ChaineDeFinDeSectionOfficielle);
      EcrireSautDeLigne(Texte: Texte);

      foreach(ProprieteGsC propriete in Section.Proprietes) {

        EcrirePropriete(Texte: Texte, Propriete: propriete, Niveau: Niveau);
      }

      for(int i = 0; i < Section.SousSections.Count; i++) {

        EcrireSautDeLigne(Texte: Texte);
        EcrireSection(Texte: Texte, Section: Section.SousSections[i], Niveau: Niveau + 1);
      }
    }

		/**
     * <summary>
     *   [FR] Écrit une propriété GsC dans le texte de sortie en respectant le schéma de formatage et l'indentation canonique définis dans cette instance.<br/>
     *   [EN] Writes a GsC property to the output text while respecting the formatting schema and canonical indentation defined in this instance.
     * </summary>
     **/
		private void EcrirePropriete(StringBuilder Texte, ProprieteGsC Propriete, int Niveau) {

      foreach(string commentaire in Propriete.Commentaire) {

        EcrireCommentaire(Texte: Texte, Commentaire: commentaire, Niveau: Niveau);
      }

      EcrireIndentation(Texte: Texte, Niveau: Niveau);
      EcrireNom(Texte: Texte, Nom: Propriete.Cle);
      Texte.Append(' ');
      Texte.Append(Schema.ChaineDattributionDesProprietesOfficielle);
      Texte.Append(' ');
      EcrireValeur(Texte: Texte, Valeur: Propriete.Valeur, Niveau: Niveau);
      Texte.Append(Schema.ChaineDeFinDesProprietesOfficielle);
      EcrireSautDeLigne(Texte: Texte);
    }

		/**
     * <summary>
     *   [FR] Écrit une valeur GsC dans le texte de sortie en respectant le schéma de formatage et l'indentation canonique définis dans cette instance.<br/>
     *   [EN] Writes a GsC value to the output text while respecting the formatting schema and canonical indentation defined in this instance.
     * </summary>
     **/
		private void EcrireValeur(StringBuilder Texte, ValeurGsC Valeur, int Niveau) {

      switch(Valeur.Type) {

        case TypeValeurGsC.Nul:
          Texte.Append("null");
          break;

        case TypeValeurGsC.Chaine:
          Texte.Append('"');
          Texte.Append(Echapper(Valeur: Valeur.EnChaine()));
          Texte.Append('"');
          break;

        case TypeValeurGsC.Entier:
          Texte.Append(Valeur.EnEntier().ToString(provider: CultureInfo.InvariantCulture));
          break;

        case TypeValeurGsC.Decimal:
          Texte.Append(Valeur.EnDecimal().ToString(provider: CultureInfo.InvariantCulture));
          break;

        case TypeValeurGsC.Booleen:
          Texte.Append(Valeur.EnBooleen() ? "true" : "false");
          break;

        case TypeValeurGsC.Liste:
          EcrireListe(Texte: Texte, Liste: Valeur.EnListe(), Niveau: Niveau);
          break;

        case TypeValeurGsC.Tableau:
          EcrireTableau(Texte: Texte, Tableau: Valeur.EnTableau(), Niveau: Niveau);
          break;

        case TypeValeurGsC.Objet:
          EcrireObjet(Texte: Texte, Objet: Valeur.EnObjet(), Niveau: Niveau);
          break;
      }
    }

		/**
     * <summary>
     *   [FR] Écrit une liste GsC dans le texte de sortie en respectant le schéma de formatage et l'indentation canonique définis dans cette instance.<br/>
     *   [EN] Writes a GsC list to the output text while respecting the formatting schema and canonical indentation defined in this instance.
     * </summary>
     **/
		private void EcrireListe(StringBuilder Texte, ListeGsC Liste, int Niveau) {

      if(Liste.Count == 0) {

        Texte.Append(Schema.ChaineDeDebutDeListeOfficielle);
        Texte.Append(Schema.ChaineDeFinDeListeOfficielle);
        return;
      }

      Texte.Append(Schema.ChaineDeDebutDeListeOfficielle);
      Texte.Append(' ');

      for(int i = 0; i < Liste.Count; i++) {

        EcrireValeur(Texte: Texte, Valeur: Liste[i], Niveau: Niveau);

        if(i < Liste.Count - 1) {

          Texte.Append(", ");
        }
      }

      Texte.Append(' ');
      Texte.Append(Schema.ChaineDeFinDeListeOfficielle);
    }

		/**
     * <summary>
     *   [FR] Écrit un tableau GsC dans le texte de sortie en respectant le schéma de formatage et l'indentation canonique définis dans cette instance.<br/>
     *   [EN] Writes a GsC array to the output text while respecting the formatting schema and canonical indentation defined in this instance.
     * </summary>
     **/
		private void EcrireTableau(StringBuilder Texte, TableauGsC Tableau, int Niveau) {

      if(Tableau.Count == 0) {

        Texte.Append(Schema.ChaineDeDebutDeTableauOfficielle);
        Texte.Append(Schema.ChaineDeFinDeTableauOfficielle);
        return;
      }

      Texte.Append(Schema.ChaineDeDebutDeTableauOfficielle);
      Texte.Append(' ');

      for(int i = 0; i < Tableau.Count; i++) {

        EcrireValeur(Texte: Texte, Valeur: Tableau[i], Niveau: Niveau);

        if(i < Tableau.Count - 1) {

          Texte.Append(", ");
        }
      }

      Texte.Append(' ');
      Texte.Append(Schema.ChaineDeFinDeTableauOfficielle);
    }

		/**
     * <summary>
     *   [FR] Écrit un objet GsC dans le texte de sortie en respectant le schéma de formatage et l'indentation canonique définis dans cette instance.<br/>
     *   [EN] Writes a GsC object to the output text while respecting the formatting schema and canonical indentation defined in this instance.
     * </summary>
     **/
		private void EcrireObjet(StringBuilder Texte, ObjetGsC Objet, int Niveau) {

      Texte.Append(Schema.ChaineDeDebutDObjetOfficielle);
      EcrireSautDeLigne(Texte: Texte);

      foreach(ProprieteGsC propriete in Objet.Proprietes) {

        EcrirePropriete(Texte: Texte, Propriete: propriete, Niveau: Niveau + 1);
      }

      EcrireIndentation(Texte: Texte, Niveau: Niveau);
      Texte.Append(Schema.ChaineDeFinDObjetOfficielle);
    }

		/**
     * <summary>
     *   [FR] Écrit un commentaire GsC dans le texte de sortie en respectant le schéma de formatage et l'indentation canonique définis dans cette instance.<br/>
     *   [EN] Writes a GsC comment to the output text while respecting the formatting schema and canonical indentation defined in this instance.
     * </summary>
     **/
		private void EcrireCommentaire(StringBuilder Texte, string Commentaire, int Niveau) {

      EcrireIndentation(Texte: Texte, Niveau: Niveau);
      Texte.Append(Schema.ChaineDattributionDuCommentaireOfficielle);
      Texte.Append(' ');
      Texte.Append(Commentaire);
      EcrireSautDeLigne(Texte: Texte);
    }

		/**
     * <summary>
     *   [FR] Écrit un saut de ligne canonique dans le texte de sortie.<br/>
     *   [EN] Writes a canonical newline to the output text.
     * </summary>
     **/
		private static void EcrireSautDeLigne(StringBuilder Texte) {

      Texte.Append(SautDeLigneCanonique);
    }

		/**
     * <summary>
     *   [FR] Écrit une indentation canonique dans le texte de sortie en fonction du niveau d'indentation spécifié.<br/>
     *   [EN] Writes a canonical indentation to the output text based on the specified indentation level.
     * </summary>
     **/
		private void EcrireIndentation(StringBuilder Texte, int Niveau) {

      for(int i = 0; i < Niveau; i++) {

        Texte.Append(value: IndentationCanonique);
      }
    }

		/**
     * <summary>
     *   [FR] Normalise l'indentation canonique en vérifiant qu'elle n'est pas vide et qu'elle ne contient que des espaces ou des tabulations.<br/>
     *   [EN] Normalizes the canonical indentation by ensuring it is not empty and contains only spaces or tabs.
     * </summary>
     **/
		private static string NormaliserIndentation(string IndentationCanonique) {

      if(string.IsNullOrEmpty(value: IndentationCanonique)) {

        throw new ArgumentException(message: "L'indentation canonique GsC ne peut pas être vide.", paramName: nameof(IndentationCanonique));
      }

      foreach(char caractere in IndentationCanonique) {

        if(caractere != ' ' && caractere != '\t') {

          throw new ArgumentException(
            message: "L'indentation canonique GsC doit contenir uniquement des espaces ou des tabulations.", paramName: nameof(IndentationCanonique)
          );
        }
      }

      return IndentationCanonique;
    }

		/**
     * <summary>
     *   [FR] Écrit un nom d'identifiant ou de section dans le texte de sortie en respectant les règles de formatage GsC.<br/>
     *   [EN] Writes an identifier or section name to the output text while respecting GsC formatting rules.
     * </summary>
     **/
		private static void EcrireNom(StringBuilder Texte, string Nom) {

      if(EstUnIdentifiantNonQuote(Nom: Nom)) {

        Texte.Append(Nom);
        return;
      }

      Texte.Append('"');
      Texte.Append(Echapper(Valeur: Nom ?? string.Empty));
      Texte.Append('"');
    }

		/**
     * <summary>
     *   [FR] Vérifie si un nom d'identifiant ou de section peut être écrit sans guillemets dans le texte de sortie en respectant les règles de formatage GsC.<br/>
     *   [EN] Checks if an identifier or section name can be written without quotes in the output text while respecting GsC formatting rules.
     * </summary>
     **/
		private static bool EstUnIdentifiantNonQuote(string Nom) {

      if(string.IsNullOrWhiteSpace(value: Nom)) {

        return false;
      }

      if(!(char.IsLetter(c: Nom[0]) || Nom[0] == '_' || Nom[0] == '$')) {

        return false;
      }

      for(int i = 1; i < Nom.Length; i++) {

        char caractere = Nom[i];

        if(!(char.IsLetterOrDigit(c: caractere) || caractere == '_' || caractere == '-' || caractere == '.' || caractere == ':' || caractere == '/')) {

          return false;
        }
      }

      return true;
    }

		/**
     * <summary>
     *   [FR] Échappe les caractères spéciaux dans une chaîne de caractères pour qu'elle puisse être écrite 
     *        entre guillemets dans le texte de sortie en respectant les règles de formatage GsC.<br/>
     *   [EN] Escapes special characters in a string so that it can be written between quotes in the output text while respecting GsC formatting rules.
     * </summary>
     **/
		private static string Echapper(string Valeur) 
      => Valeur.Replace(oldValue: "\\", newValue: "\\\\").Replace(oldValue: "\"", newValue: "\\\"")
               .Replace(oldValue: "\r", newValue: "\\r").Replace(oldValue: "\n", newValue: "\\n").Replace(oldValue: "\t", newValue: "\\t");
  }
}
