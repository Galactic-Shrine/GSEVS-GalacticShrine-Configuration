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
using System.Globalization;
using GalacticShrine.Configuration.Configuration;
using GalacticShrine.Enumeration.Configuration.GsC;
using GalacticShrine.Modele.Configuration.GsC;

namespace GalacticShrine.Configuration.Analyseur.GsC {

  /**
   * <summary>
   *   [FR] Analyse les jetons GsC et produit le modèle de données structuré.<br/>
   *   [EN] Parses GsC tokens and produces the structured data model.
   * </summary>
   **/
  public class ParseurGsC {

    private const string SeparateurDeSousSection = "::";

    private readonly List<JetonGsC> Jetons;
    private readonly SchemaGsC Schema;
    private readonly AnalyseurGsC Configuration;
    private readonly List<string> CommentairesEnAttente;
    private readonly List<ContexteSectionGsC> SectionsEnCours;
    private readonly string Source;
    private int Position;

    public ParseurGsC(List<JetonGsC> Jetons, SchemaGsC Schema, AnalyseurGsC Configuration, string Source = null) {

      this.Jetons = Jetons;
      this.Schema = Schema;
      this.Configuration = Configuration;
      this.Source = Source ?? string.Empty;
      CommentairesEnAttente = [];
      SectionsEnCours = [];
    }

    public DonneesGsC Analyser() {

      DonneesGsC donnees = new(SchemaGsCInstance: Schema, ConfigurationGsCInstance: Configuration);
      SectionGsC sectionActuelle = null;

      while(!Verifier(Type: TypeJetonGsC.FinDeFichier)) {

        if(Correspond(Type: TypeJetonGsC.Commentaire)) {

          if(Configuration.AnalyseDesCommentaires) {

            CommentairesEnAttente.Add(item: Precedent().Texte);
          }

          continue;
        }

        if(Verifier(Type: TypeJetonGsC.DebutSection) && EstUneSection()) {

          JetonGsC jetonDebutSection = Courant();
          SectionGsC sectionLue = LireSection();
          string cheminDeSection = ConstruireCheminDeSection(NomDeLaSection: sectionLue.Nom, Colonne: jetonDebutSection.Colonne);
          sectionActuelle = donnees.AjouterSectionParChemin(CheminDeSection: cheminDeSection, Section: sectionLue, Remplacer: Configuration.AutoriserLesSectionsDupliquees);
          ActualiserSectionsEnCours(CheminDeSection: cheminDeSection, Section: sectionActuelle, Colonne: jetonDebutSection.Colonne);
          continue;
        }

        JetonGsC jetonPropriete = Courant();
        sectionActuelle = SelectionnerSectionPourPropriete(Colonne: jetonPropriete.Colonne);
        ProprieteGsC propriete = LirePropriete();

        if(sectionActuelle == null) {

          if(!Configuration.AutoriserLesClesSansSection) {

            throw CreerException(Code: CodeErreurGsC.ProprieteGlobaleInterdite, Message: string.Format(format: "La propriété globale GsC '{0}' n'est pas autorisée avant une section.", arg0: propriete.Cle), Jeton: Precedent());
          }

          donnees.ProprieteGlobales.Ajouter(Propriete: propriete, Remplacer: Configuration.AutoriserLesProprietesDupliquees);
        }
        else {

          sectionActuelle.Ajouter(Propriete: propriete, Remplacer: Configuration.AutoriserLesProprietesDupliquees);
        }
      }

      return donnees;
    }

    private bool EstUneSection() {

      if(!Verifier(Type: TypeJetonGsC.DebutSection)) {

        return false;
      }

      if(Position + 2 >= Jetons.Count) {

        return false;
      }

      TypeJetonGsC typeNom = Jetons[Position + 1].Type;
      return (typeNom == TypeJetonGsC.Identifiant || typeNom == TypeJetonGsC.Chaine) && Jetons[Position + 2].Type == TypeJetonGsC.FinSection;
    }

    private SectionGsC LireSection() {

      Consommer(Type: TypeJetonGsC.DebutSection, Message: "Début de section GsC attendu.", Code: CodeErreurGsC.DebutSectionAttendu);
      JetonGsC nom = ConsommerNom(Message: "Nom de section GsC attendu.", Code: CodeErreurGsC.NomSectionAttendu);
      Consommer(Type: TypeJetonGsC.FinSection, Message: "Fin de section GsC attendue.", Code: CodeErreurGsC.FinSectionAttendue);

      SectionGsC section = new(Nom: nom.Texte, RechercherComparer: Configuration.InsensibleA_LaCasse ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);

      if(Configuration.AnalyseDesCommentaires && CommentairesEnAttente.Count > 0) {

        section.Commentaire.AddRange(collection: CommentairesEnAttente);
        CommentairesEnAttente.Clear();
      }

      return section;
    }

    private string ConstruireCheminDeSection(string NomDeLaSection, int Colonne) {

      if(NomDeLaSection.Contains(value: SeparateurDeSousSection)) {

        return NormaliserCheminDeSection(CheminDeSection: NomDeLaSection);
      }

      while(SectionsEnCours.Count > 0 && SectionsEnCours[^1].Colonne >= Colonne) {

        SectionsEnCours.RemoveAt(index: SectionsEnCours.Count - 1);
      }

      if(SectionsEnCours.Count == 0) {

        return NomDeLaSection;
      }

      return string.Concat(str0: SectionsEnCours[^1].CheminDeSection, str1: SeparateurDeSousSection, str2: NomDeLaSection);
    }

    private void ActualiserSectionsEnCours(string CheminDeSection, SectionGsC Section, int Colonne) {

      while(SectionsEnCours.Count > 0 && SectionsEnCours[^1].Colonne >= Colonne) {

        SectionsEnCours.RemoveAt(index: SectionsEnCours.Count - 1);
      }

      SectionsEnCours.Add(item: new ContexteSectionGsC(CheminDeSection: CheminDeSection, Section: Section, Colonne: Colonne));
    }

    private SectionGsC SelectionnerSectionPourPropriete(int Colonne) {

      while(SectionsEnCours.Count > 1 && SectionsEnCours[^1].Colonne > Colonne) {

        SectionsEnCours.RemoveAt(index: SectionsEnCours.Count - 1);
      }

      return SectionsEnCours.Count == 0 ? null : SectionsEnCours[^1].Section;
    }

    private static string NormaliserCheminDeSection(string CheminDeSection) => string.Join(separator: SeparateurDeSousSection, value: DecouperCheminDeSection(CheminDeSection: CheminDeSection));

    private static string[] DecouperCheminDeSection(string CheminDeSection) => CheminDeSection.Split(separator: SeparateurDeSousSection, options: StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    private ProprieteGsC LirePropriete() {

      JetonGsC cle = ConsommerNom(Message: "Clé de propriété GsC attendue.", Code: CodeErreurGsC.CleProprieteAttendue);
      Consommer(Type: TypeJetonGsC.Attribution, Message: "Attribution GsC attendue.", Code: CodeErreurGsC.AttributionAttendue);
      ValeurGsC valeur = LireValeur();
      Consommer(Type: TypeJetonGsC.FinInstruction, Message: "Fin d'instruction GsC attendue.", Code: CodeErreurGsC.FinInstructionAttendue);

      ProprieteGsC propriete = new(Cle: cle.Texte, Valeur: valeur);

      if(Configuration.AnalyseDesCommentaires && CommentairesEnAttente.Count > 0) {

        propriete.Commentaire.AddRange(collection: CommentairesEnAttente);
        CommentairesEnAttente.Clear();
      }

      return propriete;
    }

    private ValeurGsC LireValeur() {

      if(Correspond(Type: TypeJetonGsC.Chaine)) {

        return ValeurGsC.DepuisChaine(Valeur: Precedent().Texte);
      }

      if(Correspond(Type: TypeJetonGsC.Nombre)) {

        string texte = Precedent().Texte;

        try {

          if(texte.Contains(value: ".")) {

            return ValeurGsC.DepuisDecimal(Valeur: decimal.Parse(s: texte, provider: CultureInfo.InvariantCulture));
          }

          return ValeurGsC.DepuisEntier(Valeur: long.Parse(s: texte, provider: CultureInfo.InvariantCulture));
        }
        catch(FormatException exception) {

          throw CreerExceptionDepuisException(Code: CodeErreurGsC.ValeurAttendue, Message: string.Format(format: "Nombre GsC invalide '{0}'.", arg0: texte), Jeton: Precedent(), Exception: exception);
        }
        catch(OverflowException exception) {

          throw CreerExceptionDepuisException(Code: CodeErreurGsC.ValeurAttendue, Message: string.Format(format: "Nombre GsC hors limites '{0}'.", arg0: texte), Jeton: Precedent(), Exception: exception);
        }
      }

      if(Correspond(Type: TypeJetonGsC.Booleen)) {

        return ValeurGsC.DepuisBooleen(Valeur: string.Equals(a: Precedent().Texte, b: "true", comparisonType: StringComparison.OrdinalIgnoreCase));
      }

      if(Correspond(Type: TypeJetonGsC.Nul)) {

        return ValeurGsC.Nul;
      }

      if(Correspond(Type: TypeJetonGsC.DebutListe)) {

        return ValeurGsC.DepuisListe(Valeur: LireListe());
      }

      if(Correspond(Type: TypeJetonGsC.DebutTableau)) {

        return ValeurGsC.DepuisTableau(Valeur: LireTableau());
      }

      if(Correspond(Type: TypeJetonGsC.DebutObjet)) {

        return ValeurGsC.DepuisObjet(Valeur: LireObjet());
      }

      throw CreerException(Code: CodeErreurGsC.ValeurAttendue, Message: "Valeur GsC attendue.", Jeton: Courant());
    }

    private ListeGsC LireListe() {

      ListeGsC liste = [];

      while(!Verifier(Type: TypeJetonGsC.FinListe) && !Verifier(Type: TypeJetonGsC.FinDeFichier)) {

        if(Correspond(Type: TypeJetonGsC.Commentaire)) {

          continue;
        }

        liste.Add(item: LireValeur());

        if(!Correspond(Type: TypeJetonGsC.Virgule)) {

          break;
        }

        IgnorerCommentairesDansCollection();

        if(Verifier(Type: TypeJetonGsC.FinListe) || Verifier(Type: TypeJetonGsC.FinDeFichier)) {

          throw CreerException(Code: CodeErreurGsC.ValeurApresVirguleListeAttendue, Message: "Valeur GsC attendue après une virgule dans une liste.", Jeton: Courant());
        }
      }

      Consommer(Type: TypeJetonGsC.FinListe, Message: "Fin de liste GsC attendue.", Code: CodeErreurGsC.FinListeAttendue);
      return liste;
    }

    private TableauGsC LireTableau() {

      TableauGsC tableau = [];

      while(!Verifier(Type: TypeJetonGsC.FinTableau) && !Verifier(Type: TypeJetonGsC.FinDeFichier)) {

        if(Correspond(Type: TypeJetonGsC.Commentaire)) {

          continue;
        }

        tableau.Add(item: LireValeur());

        if(!Correspond(Type: TypeJetonGsC.Virgule)) {

          break;
        }

        IgnorerCommentairesDansCollection();

        if(Verifier(Type: TypeJetonGsC.FinTableau) || Verifier(Type: TypeJetonGsC.FinDeFichier)) {

          throw CreerException(Code: CodeErreurGsC.ValeurApresVirguleTableauAttendue, Message: "Valeur GsC attendue après une virgule dans un tableau.", Jeton: Courant());
        }
      }

      Consommer(Type: TypeJetonGsC.FinTableau, Message: "Fin de tableau GsC attendue.", Code: CodeErreurGsC.FinTableauAttendue);
      return tableau;
    }

    private void IgnorerCommentairesDansCollection() {

      while(Correspond(Type: TypeJetonGsC.Commentaire)) {

      }
    }

    private ObjetGsC LireObjet() {

      ObjetGsC objet = new(RechercherComparer: Configuration.InsensibleA_LaCasse ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);

      while(!Verifier(Type: TypeJetonGsC.FinObjet) && !Verifier(Type: TypeJetonGsC.FinDeFichier)) {

        if(Correspond(Type: TypeJetonGsC.Commentaire)) {

          if(Configuration.AnalyseDesCommentaires) {

            CommentairesEnAttente.Add(item: Precedent().Texte);
          }

          continue;
        }

        objet.Ajouter(Propriete: LirePropriete(), Remplacer: Configuration.AutoriserLesProprietesDupliquees);
      }

      Consommer(Type: TypeJetonGsC.FinObjet, Message: "Fin d'objet GsC attendue.", Code: CodeErreurGsC.FinObjetAttendue);
      return objet;
    }

    private JetonGsC ConsommerNom(string Message, CodeErreurGsC Code) {

      if(Verifier(Type: TypeJetonGsC.Identifiant) || Verifier(Type: TypeJetonGsC.Chaine)) {

        return Avancer();
      }

      throw CreerException(Code: Code, Message: Message, Jeton: Courant());
    }

    private JetonGsC Consommer(TypeJetonGsC Type, string Message, CodeErreurGsC Code) {

      if(Verifier(Type: Type)) {

        return Avancer();
      }

      throw CreerException(Code: Code, Message: Message, Jeton: Courant());
    }

    private bool Correspond(TypeJetonGsC Type) {

      if(!Verifier(Type: Type)) {

        return false;
      }

      Avancer();
      return true;
    }

    private bool Verifier(TypeJetonGsC Type) => Courant().Type == Type;

    private JetonGsC Avancer() {

      if(!Verifier(Type: TypeJetonGsC.FinDeFichier)) {

        Position++;
      }

      return Precedent();
    }

    private JetonGsC Courant() => Jetons[Position];

    private JetonGsC Precedent() => Jetons[Position - 1];

    private AnalyseGsCException CreerException(CodeErreurGsC Code, string Message, JetonGsC Jeton) {

      return new AnalyseGsCException(Erreur: new ErreurGsC(Code: Code, Message: Message, Ligne: Jeton.Ligne, Colonne: Jeton.Colonne, Extrait: ObtenirExtrait(LigneCible: Jeton.Ligne), Jeton: Jeton.Texte));
    }


    private AnalyseGsCException CreerExceptionDepuisException(CodeErreurGsC Code, string Message, JetonGsC Jeton, System.Exception Exception) {

      return new AnalyseGsCException(Erreur: new ErreurGsC(Code: Code, Message: Message, Ligne: Jeton.Ligne, Colonne: Jeton.Colonne, Extrait: ObtenirExtrait(LigneCible: Jeton.Ligne), Jeton: Jeton.Texte), ExceptionInterne: Exception);
    }
    private string ObtenirExtrait(int LigneCible) {

      if(LigneCible <= 0 || string.IsNullOrEmpty(value: Source)) {

        return string.Empty;
      }

      string[] lignes = Source.Replace(oldValue: "\r\n", newValue: "\n").Replace(oldValue: "\r", newValue: "\n").Split(separator: '\n');
      return LigneCible <= lignes.Length ? lignes[LigneCible - 1] : string.Empty;
    }

    private sealed class ContexteSectionGsC {

      public string CheminDeSection { get; }

      public SectionGsC Section { get; }

      public int Colonne { get; }

      public ContexteSectionGsC(string CheminDeSection, SectionGsC Section, int Colonne) {

        this.CheminDeSection = CheminDeSection;
        this.Section = Section;
        this.Colonne = Colonne;
      }
    }
  }
}
