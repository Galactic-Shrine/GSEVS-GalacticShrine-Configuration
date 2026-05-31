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
using System.Text;
using GalacticShrine.Configuration.Configuration;
using GalacticShrine.Enumeration.Configuration.GsC;

namespace GalacticShrine.Configuration.Analyseur.GsC {

  /**
   * <summary>
   *   [FR] Transforme le texte GsC en jetons syntaxiques.<br/>
   *   [EN] Turns GsC text into syntax tokens.
   * </summary>
   **/
  public class LexeurGsC {

    private readonly string Source;
    private readonly SchemaGsC Schema;
    private int Position;
    private int Ligne;
    private int Colonne;

    public LexeurGsC(string Source, SchemaGsC Schema) {

      this.Source = Source ?? string.Empty;
      this.Schema = Schema;
      Ligne = 1;
      Colonne = 1;
    }

    public List<JetonGsC> Analyser() {

      List<JetonGsC> jetons = [];

      while(!EstALaFin()) {

        char caractere = Lire();

        if(char.IsWhiteSpace(c: caractere)) {

          AvancerEspace();
          continue;
        }

        int ligne = Ligne;
        int colonne = Colonne;

        if(Correspond(Schema.ChaineDeDebutDeSectionOfficielle)) {

          Avancer(longueur: Schema.ChaineDeDebutDeSectionOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.DebutSection, Texte: Schema.ChaineDeDebutDeSectionOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDeFinDeSectionOfficielle)) {

          Avancer(longueur: Schema.ChaineDeFinDeSectionOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.FinSection, Texte: Schema.ChaineDeFinDeSectionOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDeDebutDObjetOfficielle)) {

          Avancer(longueur: Schema.ChaineDeDebutDObjetOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.DebutObjet, Texte: Schema.ChaineDeDebutDObjetOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDeFinDObjetOfficielle)) {

          Avancer(longueur: Schema.ChaineDeFinDObjetOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.FinObjet, Texte: Schema.ChaineDeFinDObjetOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDattributionDesProprietesOfficielle)) {

          Avancer(longueur: Schema.ChaineDattributionDesProprietesOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.Attribution, Texte: Schema.ChaineDattributionDesProprietesOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDattributionDuCommentaireOfficielle)) {

          jetons.Add(item: LireCommentaire());
          continue;
        }

        if(Correspond(Schema.ChaineDeFinDesProprietesOfficielle)) {

          Avancer(longueur: Schema.ChaineDeFinDesProprietesOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.FinInstruction, Texte: Schema.ChaineDeFinDesProprietesOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDeDebutDeListeOfficielle)) {

          Avancer(longueur: Schema.ChaineDeDebutDeListeOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.DebutListe, Texte: Schema.ChaineDeDebutDeListeOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDeFinDeListeOfficielle)) {

          Avancer(longueur: Schema.ChaineDeFinDeListeOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.FinListe, Texte: Schema.ChaineDeFinDeListeOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDeDebutDeTableauOfficielle)) {

          Avancer(longueur: Schema.ChaineDeDebutDeTableauOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.DebutTableau, Texte: Schema.ChaineDeDebutDeTableauOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(Correspond(Schema.ChaineDeFinDeTableauOfficielle)) {

          Avancer(longueur: Schema.ChaineDeFinDeTableauOfficielle.Length);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.FinTableau, Texte: Schema.ChaineDeFinDeTableauOfficielle, Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(caractere == ',') {

          Avancer(longueur: 1);
          jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.Virgule, Texte: ",", Ligne: ligne, Colonne: colonne));
          continue;
        }

        if(caractere == '"' || caractere == '\'') {

          jetons.Add(item: LireChaine());
          continue;
        }

        if(char.IsDigit(c: caractere) || caractere == '-') {

          jetons.Add(item: LireNombre());
          continue;
        }

        if(EstDebutIdentifiant(Caractere: caractere)) {

          jetons.Add(item: LireIdentifiant());
          continue;
        }

        throw CreerException(Code: CodeErreurGsC.CaractereInattendu, Message: string.Format(format: "Caractère GsC inattendu '{0}'.", arg0: caractere), Ligne: ligne, Colonne: colonne, Jeton: caractere.ToString());
      }

      jetons.Add(item: new JetonGsC(Type: TypeJetonGsC.FinDeFichier, Texte: string.Empty, Ligne: Ligne, Colonne: Colonne));
      return jetons;
    }

    private JetonGsC LireCommentaire() {

      int ligne = Ligne;
      int colonne = Colonne;
      Avancer(longueur: Schema.ChaineDattributionDuCommentaireOfficielle.Length);
      StringBuilder texte = new();

      while(!EstALaFin() && Lire() != '\r' && Lire() != '\n') {

        texte.Append(value: Lire());
        Avancer(longueur: 1);
      }

      return new JetonGsC(Type: TypeJetonGsC.Commentaire, Texte: texte.ToString().Trim(), Ligne: ligne, Colonne: colonne);
    }

    private JetonGsC LireChaine() {

      int ligne = Ligne;
      int colonne = Colonne;
      char delimiteur = Lire();
      Avancer(longueur: 1);
      StringBuilder texte = new();

      while(!EstALaFin()) {

        char caractere = Lire();

        if(caractere == delimiteur) {

          Avancer(longueur: 1);
          return new JetonGsC(Type: TypeJetonGsC.Chaine, Texte: texte.ToString(), Ligne: ligne, Colonne: colonne);
        }

        if(caractere == '\\') {

          Avancer(longueur: 1);

          if(EstALaFin()) {

            break;
          }

          char echappe = Lire();
          texte.Append(value: echappe switch {

            'n' => '\n',
            'r' => '\r',
            't' => '\t',
            '"' => '"',
            '\'' => '\'',
            '\\' => '\\',
            _ => echappe
          });
          Avancer(longueur: 1);
          continue;
        }

        texte.Append(value: caractere);
        Avancer(longueur: 1);
      }

      throw CreerException(Code: CodeErreurGsC.ChaineNonTerminee, Message: "Chaîne GsC non terminée.", Ligne: ligne, Colonne: colonne);
    }

    private JetonGsC LireNombre() {

      int ligne = Ligne;
      int colonne = Colonne;
      int debut = Position;

      if(Lire() == '-') {

        Avancer(longueur: 1);
      }

      while(!EstALaFin() && char.IsDigit(c: Lire())) {

        Avancer(longueur: 1);
      }

      if(!EstALaFin() && Lire() == '.') {

        Avancer(longueur: 1);

        while(!EstALaFin() && char.IsDigit(c: Lire())) {

          Avancer(longueur: 1);
        }
      }

      return new JetonGsC(Type: TypeJetonGsC.Nombre, Texte: Source[debut..Position], Ligne: ligne, Colonne: colonne);
    }

    private JetonGsC LireIdentifiant() {

      int ligne = Ligne;
      int colonne = Colonne;
      int debut = Position;

      while(!EstALaFin() && EstCaractereIdentifiant(Caractere: Lire())) {

        Avancer(longueur: 1);
      }

      string texte = Source[debut..Position];

      if(string.Equals(a: texte, b: "true", comparisonType: StringComparison.OrdinalIgnoreCase) || string.Equals(a: texte, b: "vrai", comparisonType: StringComparison.OrdinalIgnoreCase)) {

        return new JetonGsC(Type: TypeJetonGsC.Booleen, Texte: "true", Ligne: ligne, Colonne: colonne);
      }

      if(string.Equals(a: texte, b: "false", comparisonType: StringComparison.OrdinalIgnoreCase) || string.Equals(a: texte, b: "faux", comparisonType: StringComparison.OrdinalIgnoreCase)) {

        return new JetonGsC(Type: TypeJetonGsC.Booleen, Texte: "false", Ligne: ligne, Colonne: colonne);
      }

      if(string.Equals(a: texte, b: "null", comparisonType: StringComparison.OrdinalIgnoreCase) || string.Equals(a: texte, b: "nul", comparisonType: StringComparison.OrdinalIgnoreCase)) {

        return new JetonGsC(Type: TypeJetonGsC.Nul, Texte: "null", Ligne: ligne, Colonne: colonne);
      }

      return new JetonGsC(Type: TypeJetonGsC.Identifiant, Texte: texte, Ligne: ligne, Colonne: colonne);
    }

    private bool EstALaFin() => Position >= Source.Length;

    private char Lire() => Source[Position];

    private bool Correspond(string Texte) {

      if(string.IsNullOrEmpty(value: Texte) || Position + Texte.Length > Source.Length) {

        return false;
      }

      return string.Compare(strA: Source, indexA: Position, strB: Texte, indexB: 0, length: Texte.Length, comparisonType: StringComparison.Ordinal) == 0;
    }

    private void AvancerEspace() {

      if(Lire() == '\r') {

        Avancer(longueur: 1);

        if(!EstALaFin() && Lire() == '\n') {

          Avancer(longueur: 1);
        }

        Ligne++;
        Colonne = 1;
        return;
      }

      if(Lire() == '\n') {

        Avancer(longueur: 1);
        Ligne++;
        Colonne = 1;
        return;
      }

      Avancer(longueur: 1);
    }

    private void Avancer(int longueur) {

      for(int i = 0; i < longueur && Position < Source.Length; i++) {

        Position++;
        Colonne++;
      }
    }

    private bool EstDebutIdentifiant(char Caractere) => char.IsLetter(c: Caractere) || Caractere == '_' || Caractere == '$';

    private bool EstCaractereIdentifiant(char Caractere) => char.IsLetterOrDigit(c: Caractere) || Caractere == '_' || Caractere == '-' || Caractere == '.' || Caractere == ':' || Caractere == '/';

    private AnalyseGsCException CreerException(CodeErreurGsC Code, string Message, int Ligne, int Colonne, string Jeton = "") {

      return new AnalyseGsCException(Erreur: new ErreurGsC(Code: Code, Message: Message, Ligne: Ligne, Colonne: Colonne, Extrait: ObtenirExtrait(LigneCible: Ligne), Jeton: Jeton));
    }

    private string ObtenirExtrait(int LigneCible) {

      if(LigneCible <= 0) {

        return string.Empty;
      }

      string[] lignes = Source.Replace(oldValue: "\r\n", newValue: "\n").Replace(oldValue: "\r", newValue: "\n").Split(separator: '\n');
      return LigneCible <= lignes.Length ? lignes[LigneCible - 1] : string.Empty;
    }
  }
}
