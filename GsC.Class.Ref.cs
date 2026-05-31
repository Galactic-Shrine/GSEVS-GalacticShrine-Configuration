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
using System.IO;
using System.Text;
using GalacticShrine.Configuration.Analyseur.GsC;
using GalacticShrine.Configuration.Configuration;
using GalacticShrine.Configuration.Securite.GsC;
using GalacticShrine.Configuration.Validation.GsC;
using GalacticShrine.Enumeration.Configuration.GsC;
using GalacticShrine.Exceptions.Configuration;
using GalacticShrine.Modele.Configuration.GsC;

namespace GalacticShrine.Configuration {

  /**
   * <summary>
   *   [FR] Galactic-Shrine Config (<see cref="GsC"/>) est un format de configuration structuré.<br/>
   *        GsCc est la variante chiffrée du même contenu GsC.<br/>
   *   [EN] Galactic-Shrine Config (<see cref="GsC"/>) is a structured configuration format.<br/>
   *        GsCc is the encrypted variant of the same GsC content.
   * </summary>
   **/
  public class GsC {

		/**
     * <summary>
     *   [FR] Le compartiment clair d'une configuration GsC compartimentée.<br/>
     *   [EN] The clear compartment of a compartmentalized GsC configuration.
     * </summary>
     **/
		public const string CompartimentClair = "GsC";

		/**
     * <summary>
     *   [FR] Le compartiment chiffré d'une configuration GsC compartimentée.<br/>
     *   [EN] The encrypted compartment of a compartmentalized GsC configuration.
     * </summary>
     **/
		public const string CompartimentCrypte = "GsCc";

		/**
     * <summary>
     *   [FR] Le schéma de validation utilisé pour analyser et formater les données GsC.<br/>
     *   [EN] The validation schema used to analyze and format GsC data.
     * </summary>
     **/
		private SchemaGsC SchemaInstance;

		/**
     * <summary>
     *   [FR] Le schéma de validation utilisé pour analyser et formater les données GsC.<br/>
     *   [EN] The validation schema used to analyze and format GsC data.
     * </summary>
     **/
		public SchemaGsC Schema {

      get {

        SchemaInstance ??= new SchemaGsC();

        return SchemaInstance;
      }

      protected set => SchemaInstance = value.CloneEnProfondeur();
    }

		/**
     * <summary>
     *   [FR] La configuration d'analyse utilisée pour analyser les données GsC.<br/>
     *   [EN] The analysis configuration used to analyze GsC data.
     * </summary>
     **/
		public virtual AnalyseurGsC Configuration { get; protected set; }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GsC"/> avec les paramètres par défaut.<br/>
     *   [EN] Initializes a new instance of the <see cref="GsC"/> class with default settings.
     * </summary>
     **/
		public GsC() {

      Schema = new SchemaGsC();
      Configuration = new AnalyseurGsC();
    }

		/**
     * <summary>
     *   [FR] Analyse une chaîne de caractères GsC et retourne les données correspondantes.<br/>
     *   [EN] Analyzes a GsC string and returns the corresponding data.
     * </summary>
     * <param name="ChaineGsC">
     *   [FR] La chaîne de caractères GsC à analyser.<br/>
     *   [EN] The GsC string to analyze.
     * </param>
     * <returns>
     *   [FR] Les données extraites de la chaîne GsC analysée.<br/>
     *   [EN] The data extracted from the analyzed GsC string.
     * </returns>
     **/
		public DonneesGsC Analyse(string ChaineGsC) {

      if(Configuration.LancerDesExceptionsEnCasDerreur) {

        return AnalyserInterne(ChaineGsC: ChaineGsC);
      }

      return EssayerAnalyser(ChaineGsC: ChaineGsC).Donnees;
    }

		/**
     * <summary>
     *   [FR] Tente d'analyser une chaîne de caractères GsC et retourne un résultat contenant les données extraites et les erreurs éventuelles.<br/>
     *   [EN] Tries to analyze a GsC string and returns a result containing the extracted data and any errors.
     * </summary>
     * <param name="ChaineGsC">
     *   [FR] La chaîne de caractères GsC à analyser.<br/>
     *   [EN] The GsC string to analyze.
     * </param>
     * <returns>
     *   [FR] Un objet contenant les données extraites de la chaîne GsC analysée et les erreurs éventuelles.<br/>
     *   [EN] An object containing the data extracted from the analyzed GsC string and any errors.
     * </returns>
     **/
		public ResultatAnalyseGsC EssayerAnalyser(string ChaineGsC) {

      try {

        return new ResultatAnalyseGsC(Donnees: AnalyserInterne(ChaineGsC: ChaineGsC));
      }
      catch(AnalyseGsCException exception) {

        return new ResultatAnalyseGsC(Donnees: new DonneesGsC(SchemaGsCInstance: Schema, ConfigurationGsCInstance: Configuration), Erreurs: [exception.Erreur]);
      }
      catch(InvalidOperationException exception) {

        return new ResultatAnalyseGsC(Donnees: new DonneesGsC(SchemaGsCInstance: Schema, ConfigurationGsCInstance: Configuration), Erreurs: [CreerErreurDepuisException(Code: CodeErreurGsC.ErreurStructure, Exception: exception)]);
      }
      catch(FormatException exception) {

        return new ResultatAnalyseGsC(Donnees: new DonneesGsC(SchemaGsCInstance: Schema, ConfigurationGsCInstance: Configuration), Erreurs: [CreerErreurDepuisException(Code: CodeErreurGsC.ErreurAnalyse, Exception: exception)]);
      }
      catch(System.Exception exception) {

        return new ResultatAnalyseGsC(Donnees: new DonneesGsC(SchemaGsCInstance: Schema, ConfigurationGsCInstance: Configuration), Erreurs: [CreerErreurDepuisException(Code: CodeErreurGsC.ErreurInconnue, Exception: exception)]);
      }
    }

		/**
     * <summary>
     *   [FR] Analyse le contenu d'un lecteur de texte GsC et retourne les données correspondantes.<br/>
     *   [EN] Analyzes the content of a GsC text reader and returns the corresponding data.
     * </summary>
     * <param name="LecteurDeTexte">
     *   [FR] Le lecteur de texte contenant le contenu GsC à analyser.<br/>
     *   [EN] The text reader containing the GsC content to analyze.
     * </param> 
     * <returns>
     *   [FR] Les données extraites du lecteur de texte GsC analysé.<br/>
     *   [EN] The data extracted from the analyzed GsC text reader.
     * </returns>
     **/
		public DonneesGsC Analyse(TextReader LecteurDeTexte) {

      if(LecteurDeTexte == null) {

        throw new ArgumentNullException(paramName: nameof(LecteurDeTexte));
      }

      return Analyse(ChaineGsC: LecteurDeTexte.ReadToEnd());
    }

		/**
     * <summary>
     *   [FR] Tente d'analyser le contenu d'un lecteur de texte GsC et retourne un résultat contenant les données extraites et les erreurs éventuelles.<br/>
     *   [EN] Tries to analyze the content of a GsC text reader and returns a result containing the extracted data and any errors.
     * </summary>
     * <param name="LecteurDeTexte">
     *   [FR] Le lecteur de texte contenant le contenu GsC à analyser.<br/>
     *   [EN] The text reader containing the GsC content to analyze.
     * </param>
     * <returns>
     *   [FR] Un objet contenant les données extraites du lecteur de texte GsC analysé et les erreurs éventuelles.<br/>
     *   [EN] An object containing the data extracted from the analyzed GsC text reader and any errors.
     * </returns>
     **/
		public ResultatAnalyseGsC EssayerAnalyser(TextReader LecteurDeTexte) {

      if(LecteurDeTexte == null) {

        return new ResultatAnalyseGsC(Donnees: new DonneesGsC(SchemaGsCInstance: Schema, ConfigurationGsCInstance: Configuration), Erreurs: [new ErreurGsC(Code: CodeErreurGsC.ErreurAnalyse, Message: "Le lecteur de texte GsC ne peut pas être null.")]);
      }

      return EssayerAnalyser(ChaineGsC: LecteurDeTexte.ReadToEnd());
    }

		/**
     * <summary>
     *   [FR] Ouvre un fichier GsC ou GsCc, analyse son contenu et retourne les données correspondantes.<br/>
     *   [EN] Opens a GsC or GsCc file, analyzes its content, and returns the corresponding data.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] Le chemin du fichier GsC ou GsCc à ouvrir et analyser.<br/>
     *   [EN] The path of the GsC or GsCc file to open and analyze.
     * </param>
     * <returns>
     *   [FR] Les données extraites du fichier GsC ou GsCc analysé.<br/>
     *   [EN] The data extracted from the analyzed GsC or GsCc file.
     * </returns>
     **/
		public DonneesGsC Ouvrir(string CheminDuFichier) {

      VerifierCheminDuFichier(CheminDuFichier: CheminDuFichier);

      if(!string.IsNullOrWhiteSpace(value: Path.GetExtension(path: CheminDuFichier))) {

        return OuvrirFichierUnique(CheminDuFichier: CheminDuFichier);
      }

      (string cheminGsC, string cheminGsCc, bool existeGsC, bool existeGsCc) = ObtenirEtatFichiersSansExtension(CheminDuFichier: CheminDuFichier);

      if(existeGsC && existeGsCc) {

        return OuvrirFichierCompartimente(CheminGsC: cheminGsC, CheminGsCc: cheminGsCc);
      }

      if(existeGsC) {

        return OuvrirFichierUnique(CheminDuFichier: cheminGsC);
      }

      if(existeGsCc) {

        return OuvrirFichierUnique(CheminDuFichier: cheminGsCc);
      }

      throw new GsCGsCcFileNotFoundException(CheminDemande: CheminDuFichier, CheminGsC: cheminGsC, CheminGsCc: cheminGsCc);
    }

		/**
     * <summary>
     *   [FR] Ouvre un fichier GsC ou GsCc, analyse son contenu et retourne les données correspondantes.<br/>
     *   [EN] Opens a GsC or GsCc file, analyzes its content, and returns the corresponding data.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] Le chemin du fichier GsC ou GsCc à ouvrir et analyser.<br/>
     *   [EN] The path of the GsC or GsCc file to open and analyze.
     * </param>
     * <returns>
     *   [FR] Les données extraites du fichier GsC ou GsCc analysé.<br/>
     *   [EN] The data extracted from the analyzed GsC or GsCc file.
     * </returns>
     **/
		public string ResoudreCheminDuFichierPourOuverture(string CheminDuFichier) {

      VerifierCheminDuFichier(CheminDuFichier: CheminDuFichier);

      if(!string.IsNullOrWhiteSpace(value: Path.GetExtension(path: CheminDuFichier))) {

        VerifierFichierExpliciteExiste(CheminDuFichier: CheminDuFichier);
        return CheminDuFichier;
      }

      (string cheminGsC, string cheminGsCc, bool existeGsC, bool existeGsCc) = ObtenirEtatFichiersSansExtension(CheminDuFichier: CheminDuFichier);

      if(existeGsC && existeGsCc) {

        return CheminDuFichier;
      }

      if(existeGsC) {

        return cheminGsC;
      }

      if(existeGsCc) {

        return cheminGsCc;
      }

      throw new GsCGsCcFileNotFoundException(CheminDemande: CheminDuFichier, CheminGsC: cheminGsC, CheminGsCc: cheminGsCc);
    }

		/**
     * <summary>
     *   [FR] Résout le chemin d'un fichier GsC ou GsCc à ouvrir, en vérifiant son existence et en tenant compte de la possibilité de 
     *        fichiers sans extension.<br/>
     *   [EN] Resolves the path of a GsC or GsCc file to open, by verifying its existence and considering the possibility of 
     *        files without extension.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] Le chemin du fichier GsC ou GsCc à résoudre pour ouverture.<br/>
     *   [EN] The path of the GsC or GsCc file to resolve for opening.
     * </param>
     * <returns>
     *   [FR] Le chemin résolu du fichier GsC ou GsCc à ouvrir.<br/>
     *   [EN] The resolved path of the GsC or GsCc file to open.
     * </returns>
     **/
		public string[] ResoudreCheminsDuFichierPourOuverture(string CheminDuFichier) {

      VerifierCheminDuFichier(CheminDuFichier: CheminDuFichier);

      if(!string.IsNullOrWhiteSpace(value: Path.GetExtension(path: CheminDuFichier))) {

        VerifierFichierExpliciteExiste(CheminDuFichier: CheminDuFichier);
        return [CheminDuFichier];
      }

      (string cheminGsC, string cheminGsCc, bool existeGsC, bool existeGsCc) = ObtenirEtatFichiersSansExtension(CheminDuFichier: CheminDuFichier);

      if(existeGsC && existeGsCc) {

        return [cheminGsC, cheminGsCc];
      }

      if(existeGsC) {

        return [cheminGsC];
      }

      if(existeGsCc) {

        return [cheminGsCc];
      }

      throw new GsCGsCcFileNotFoundException(CheminDemande: CheminDuFichier, CheminGsC: cheminGsC, CheminGsCc: cheminGsCc);
    }

		/**
     * <summary>
     *   [FR] Sauvegarde les données GsC dans un fichier spécifié, en gérant les cas de fichiers uniques ou compartimentés, 
     *        et en appliquant le chiffrement si nécessaire.<br/>
     *   [EN] Saves the GsC data to a specified file, handling cases of single or compartmentalized files, 
     *        and applying encryption if necessary.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] Le chemin du fichier GsC ou GsCc où sauvegarder les données.<br/>
     *   [EN] The path of the GsC or GsCc file where to save the data.
     * </param>
     * <param name="Donnees">
     *   [FR] Les données GsC à sauvegarder dans le fichier.<br/>
     *   [EN] The GsC data to save to the file.
     * </param>
     **/
		public void Sauvegarder(string CheminDuFichier, DonneesGsC Donnees) {

      VerifierCheminDuFichier(CheminDuFichier: CheminDuFichier);

      if(Donnees == null) {

        throw new ArgumentNullException(paramName: nameof(Donnees));
      }

      if(!string.IsNullOrWhiteSpace(value: Path.GetExtension(path: CheminDuFichier))) {

        SauvegarderFichierUnique(CheminDuFichier: CheminDuFichier, Donnees: Donnees, ForcerChiffrement: null);
        return;
      }

      (string cheminGsC, string cheminGsCc, bool existeGsC, bool existeGsCc) = ObtenirEtatFichiersSansExtension(CheminDuFichier: CheminDuFichier);

      if(!existeGsC && !existeGsCc) {

        throw new GsCGsCcFileNotFoundException(CheminDemande: CheminDuFichier, CheminGsC: cheminGsC, CheminGsCc: cheminGsCc);
      }

      if(existeGsC && existeGsCc) {

        SauvegarderFichierCompartimente(CheminGsC: cheminGsC, CheminGsCc: cheminGsCc, Donnees: Donnees);
        return;
      }

      if(existeGsC) {

        SauvegarderFichierUnique(CheminDuFichier: cheminGsC, Donnees: Donnees, ForcerChiffrement: false);
        return;
      }

      SauvegarderFichierUnique(CheminDuFichier: cheminGsCc, Donnees: Donnees, ForcerChiffrement: true);
    }

		/**
     * <summary>
     *   [FR] Formate les données GsC en une chaîne de caractères selon le schéma et la configuration spécifiés.<br/>
     *   [EN] Formats the GsC data into a string according to the specified schema and configuration.
     * </summary>
     * <param name="Donnees">
     *   [FR] Les données GsC à formater.<br/>
     *   [EN] The GsC data to format.
     * </param>
     * <returns>
     *   [FR] La chaîne de caractères résultante du formatage des données GsC.<br/>
     *   [EN] The resulting string from formatting the GsC data.
     * </returns>
     **/

    /**
     * <summary>
     *   [FR] Valide des données GsC avec un schéma de validation.<br/>
     *   [EN] Validates GsC data with a validation schema.
     * </summary>
     **/
    public ResultatValidationGsC Valider(DonneesGsC Donnees, SchemaValidationGsC SchemaValidation) => ValidateurGsC.Valider(Donnees: Donnees, Schema: SchemaValidation);

    /**
     * <summary>
     *   [FR] Ouvre puis valide un fichier GsC ou GsCc avec un schéma de validation.<br/>
     *   [EN] Opens and validates a GsC or GsCc file with a validation schema.
     * </summary>
     **/
    public ResultatValidationGsC Valider(string CheminDuFichier, SchemaValidationGsC SchemaValidation) => Valider(Donnees: Ouvrir(CheminDuFichier: CheminDuFichier), SchemaValidation: SchemaValidation);

		public string Formater(DonneesGsC Donnees) => FormaterCanonique(Donnees: Donnees);

		/**
     * <summary>
     *   [FR] Formate les données GsC en une chaîne de caractères canonique selon le schéma et la configuration spécifiés.<br/>
     *   [EN] Formats the GsC data into a canonical string according to the specified schema and configuration.
     * </summary>
     * <param name="Donnees">
     *   [FR] Les données GsC à formater en canonique.<br/>
     *   [EN] The GsC data to format into canonical.
     * </param>
     * <returns>
     *   [FR] La chaîne de caractères canonique résultante du formatage des données GsC.<br/>
     *   [EN] The resulting canonical string from formatting the GsC data.
     * </returns>
     **/
		public string FormaterCanonique(DonneesGsC Donnees) {

      if(Donnees == null) {

        throw new ArgumentNullException(paramName: nameof(Donnees));
      }

      return new FormatageGsC(Schema: Schema, Configuration: Configuration).Formater(Donnees: Donnees);
    }

		/**
     * <summary>
     *   [FR] Formate les données GsC en une chaîne de caractères canonique avec une indentation personnalisée, 
     *        selon le schéma et la configuration spécifiés.<br/>
     *   [EN] Formats the GsC data into a canonical string with custom indentation, 
     *        according to the specified schema and configuration.
     * </summary>
     * <param name="Donnees">
     *   [FR] Les données GsC à formater en canonique.<br/>
     *   [EN] The GsC data to format into canonical.
     * </param>
     * <param name="IndentationCanonique">
     *   [FR] La chaîne d'indentation à utiliser pour le formatage canonique.<br/>
     *   [EN] The indentation string to use for canonical formatting.
     * </param>
     * <returns>
     *   [FR] La chaîne de caractères canonique résultante du formatage des données GsC avec l'indentation personnalisée.<br/>
     *   [EN] The resulting canonical string from formatting the GsC data with custom indentation.
     * </returns>
     **/
		public string FormaterCanonique(DonneesGsC Donnees, string IndentationCanonique) {

      if(Donnees == null) {

        throw new ArgumentNullException(paramName: nameof(Donnees));
      }

      return new FormatageGsC(Schema: Schema, IndentationCanonique: IndentationCanonique).Formater(Donnees: Donnees);
    }

		/**
     * <summary>
     *   [FR] Vérifie si le fichier spécifié est un fichier GsCc chiffré, en se basant sur son extension.<br/>
     *   [EN] Checks if the specified file is an encrypted GsCc file, based on its extension.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] Le chemin du fichier à vérifier.<br/>
     *   [EN] The path of the file to check.
     * </param>
     * <returns>
     *   [FR] Vrai si le fichier est un fichier GsCc chiffré, sinon faux.<br/>
     *   [EN] True if the file is an encrypted GsCc file, otherwise false.
     * </returns>
     **/
		public bool EstUnFichierCrypte(string CheminDuFichier) {

      string extension = Path.GetExtension(path: CheminDuFichier);
      return string.Equals(a: extension, b: Configuration.Cryptage.ExtensionCryptee, comparisonType: StringComparison.OrdinalIgnoreCase);
    }

		/**
     * <summary>
     *   [FR] Ouvre un fichier GsC ou GsCc unique, analyse son contenu et retourne les données correspondantes, en gérant le chiffrement si nécessaire.<br/>
     *   [EN] Opens a single GsC or GsCc file, analyzes its content, and returns the corresponding data, handling encryption if necessary.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] The path of the single GsC or GsCc file to open and analyze.<br/>
     *   [EN] The path of the single GsC or GsCc file to open and analyze.
     * </param>
     * <returns>
     *   [FR] The data extracted from the analyzed single GsC or GsCc file.<br/>
     *   [EN] The data extracted from the analyzed single GsC or GsCc file.
     * </returns>
     **/
		private DonneesGsC OuvrirFichierUnique(string CheminDuFichier) {

      string contenu = File.ReadAllText(path: CheminDuFichier, encoding: Encoding.UTF8);

      if(EstUnFichierCrypte(CheminDuFichier: CheminDuFichier) || CryptageGsC.EstUnContenuCrypte(Contenu: contenu)) {

        contenu = CryptageGsC.Dechiffrer(ContenuCrypte: contenu, Options: Configuration.Cryptage);
      }

      return Analyse(ChaineGsC: contenu);
    }

		/**
     * <summary>
     *   [FR] Ouvre deux fichiers GsC et GsCc compartimentés, analyse leur contenu, et retourne les données correspondantes combinées en un seul objet, 
     *        en gérant le chiffrement si nécessaire.<br/>
     *   [EN] Opens two compartmentalized GsC and GsCc files, analyzes their content, and returns the corresponding combined data in a single object, 
     *        handling encryption if necessary.
     * </summary>
     * <param name="CheminGsC">
     *   [FR] The path of the clear compartment GsC file to open and analyze.<br/>
     *   [EN] The path of the clear compartment GsC file to open and analyze.
     * </param>
     * <param name="CheminGsCc">
     *   [FR] The path of the encrypted compartment GsCc file to open and analyze.<br/>
     *   [EN] The path of the encrypted compartment GsCc file to open and analyze.
     * </param>
     * <returns>
     *   [FR] The combined data extracted from the analyzed compartmentalized GsC and GsCc files.<br/>
     *   [EN] The combined data extracted from the analyzed compartmentalized GsC and GsCc files.
     * </returns>
     **/
		private DonneesGsC OuvrirFichierCompartimente(string CheminGsC, string CheminGsCc) {

      DonneesGsC donneesCompartimentees = new(SchemaGsCInstance: Schema, ConfigurationGsCInstance: Configuration);
      donneesCompartimentees.AjouterSection(
        Section: CreerCompartiment(
          NomDuCompartiment: CompartimentClair, 
          Donnees: OuvrirFichierUnique(CheminDuFichier: CheminGsC)
        ), 
        Remplacer: true
      );
      donneesCompartimentees.AjouterSection(
        Section: CreerCompartiment(
          NomDuCompartiment: CompartimentCrypte, 
          Donnees: OuvrirFichierUnique(CheminDuFichier: CheminGsCc)
        ), 
        Remplacer: true
      );
      return donneesCompartimentees;
    }

		/**
     * <summary>
     *   [FR] Sauvegarde les données GsC dans deux fichiers GsC et GsCc compartimentés, en gérant le chiffrement si nécessaire.<br/>
     *   [EN] Saves the GsC data into two compartmentalized GsC and GsCc files, handling encryption if necessary.
     * </summary>
     * <param name="CheminGsC">
     *   [FR] The path of the clear compartment GsC file to save the data to.<br/>
     *   [EN] The path of the clear compartment GsC file to save the data to.
     * </param>
     * <param name="CheminGsCc">
     *   [FR] The path of the encrypted compartment GsCc file to save the data to.<br/>
     *   [EN] The path of the encrypted compartment GsCc file to save the data to.
     * </param>
     * <param name="Donnees">
     *   [FR] The GsC data to save into the compartmentalized files.<br/>
     *   [EN] The GsC data to save into the compartmentalized files.
     * </param>
     **/
		private void SauvegarderFichierCompartimente(string CheminGsC, string CheminGsCc, DonneesGsC Donnees) {

      if(!Donnees.ContientSection(NomDeLaSection: CompartimentClair) || !Donnees.ContientSection(NomDeLaSection: CompartimentCrypte)) {

        throw new InvalidOperationException(
          message: string.Format(
            format: "La sauvegarde compartimentée exige deux sections racines : '{0}' et '{1}'.", 
            arg0: CompartimentClair, 
            arg1: CompartimentCrypte
          )
        );
      }

      SauvegarderFichierUnique(
        CheminDuFichier: CheminGsC, 
        Donnees: ExtraireCompartiment(Section: Donnees.ObtenirSection(NomDeLaSection: CompartimentClair)), 
        ForcerChiffrement: false
      );
      SauvegarderFichierUnique(
        CheminDuFichier: CheminGsCc, 
        Donnees: ExtraireCompartiment(Section: Donnees.ObtenirSection(NomDeLaSection: CompartimentCrypte)), 
        ForcerChiffrement: true
      );
    }

		/**
     * <summary>
     *   [FR] Sauvegarde les données GsC dans un fichier unique, en gérant le chiffrement si nécessaire ou forcé.<br/>
     *   [EN] Saves the GsC data into a single file, handling encryption if necessary or forced.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] The path of the single GsC or GsCc file to save the data to.<br/>
     *   [EN] The path of the single GsC or GsCc file to save the data to.
     * </param>
     * <param name="Donnees">
     *   [FR] The GsC data to save into the single file.<br/>
     *   [EN] The GsC data to save into the single file.
     * </param>
     * <param name="ForcerChiffrement">
     *   [FR] A boolean indicating whether to force encryption regardless of configuration or file type.<br/>
     *   [EN] A boolean indicating whether to force encryption regardless of configuration or file type.
     * </param>
     **/
		private void SauvegarderFichierUnique(string CheminDuFichier, DonneesGsC Donnees, bool? ForcerChiffrement) {

      string contenu = Formater(Donnees: Donnees);
      bool chiffrer = ForcerChiffrement ?? (Configuration.Cryptage.EstActif || EstUnFichierCrypte(CheminDuFichier: CheminDuFichier));

      if(chiffrer) {

        contenu = CryptageGsC.Chiffrer(ContenuClair: contenu, Options: Configuration.Cryptage);
      }

      EcrireFichierAtomiquement(CheminDuFichier: CheminDuFichier, Contenu: contenu);
    }

		/**
     * <summary>
     *   [FR] Crée une section de compartiment à partir des données fournies, en copiant les propriétés globales et les sections.<br/>
     *   [EN] Creates a compartment section from the provided data, by copying global properties and sections.
     * </summary>
     * <param name="NomDuCompartiment">
     *   [FR] The name of the compartment section to create.<br/>
     *   [EN] The name of the compartment section to create.
     * </param>
     * <param name="Donnees">
     *   [FR] The GsC data to extract and copy into the compartment section.<br/>
     *   [EN] The GsC data to extract and copy into the compartment section.
     * </param>
     * <returns>
     *   [FR] The created compartment section containing the copied data.<br/>
     *   [EN] The created compartment section containing the copied data.
     * </returns>
     **/
		private SectionGsC CreerCompartiment(string NomDuCompartiment, DonneesGsC Donnees) {

      SectionGsC compartiment = new(Nom: NomDuCompartiment);

      foreach(ProprieteGsC propriete in Donnees.ProprieteGlobales.Proprietes) {

        compartiment.Ajouter(Propriete: propriete.CloneEnProfondeur(), Remplacer: true);
      }

      foreach(SectionGsC section in Donnees.Sections) {

        compartiment.AjouterSection(Section: new SectionGsC(SectionGsCInstance: section), Remplacer: true);
      }

      return compartiment;
    }

		/**
     * <summary>
     *   [FR] Extrait les données d'une section de compartiment pour les sauvegarder dans un fichier unique, 
     *        en copiant les propriétés globales et les sections.<br/>
     *   [EN] Extracts the data from a compartment section to save it into a single file, 
     *        by copying global properties and sections.
     * </summary>
     * <param name="Section">
     *   [FR] The compartment section to extract data from.<br/>
     *   [EN] The compartment section to extract data from.
     * </param>
     * <returns>
     *   [FR] The extracted GsC data from the compartment section.<br/>
     *   [EN] The extracted GsC data from the compartment section.
     * </returns>
     **/
		private DonneesGsC ExtraireCompartiment(SectionGsC Section) {

      DonneesGsC donnees = new(SchemaGsCInstance: Schema, ConfigurationGsCInstance: Configuration);

      foreach(ProprieteGsC propriete in Section.Proprietes) {

        donnees.ProprieteGlobales.Ajouter(Propriete: propriete.CloneEnProfondeur(), Remplacer: true);
      }

      foreach(SectionGsC sousSection in Section.SousSections) {

        donnees.AjouterSection(Section: new SectionGsC(SectionGsCInstance: sousSection), Remplacer: true);
      }

      return donnees;
    }

		/**
     * <summary>
     *   [FR] Écrit le contenu dans un fichier de manière atomique, 
     *        en utilisant un fichier temporaire et en gérant les exceptions pour assurer l'intégrité des données.<br/>
     *   [EN] Writes the content to a file atomically, 
     *        using a temporary file and handling exceptions to ensure data integrity.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] The path of the file to write the content to.<br/>
     *   [EN] The path of the file to write the content to.
     * </param>
     * <param name="Contenu">
     *   [FR] The content to write to the file.<br/>
     *   [EN] The content to write to the file.
     * </param>
     **/
		private static void EcrireFichierAtomiquement(string CheminDuFichier, string Contenu) {

      string dossier = Path.GetDirectoryName(path: CheminDuFichier);

      if(!string.IsNullOrWhiteSpace(value: dossier)) {

        Directory.CreateDirectory(path: dossier);
      }

      string dossierTemporaire = string.IsNullOrWhiteSpace(value: dossier) ? Directory.GetCurrentDirectory() : dossier;
      string fichierTemporaire = Path.Combine(
        path1: dossierTemporaire, 
        path2: string.Concat(".", Path.GetFileName(path: CheminDuFichier), ".", Guid.NewGuid().ToString(format: "N"), ".tmp")
      );
      byte[] contenuEncode = Encoding.UTF8.GetBytes(s: Contenu ?? string.Empty);

      try {

        using(FileStream flux = new(path: fichierTemporaire, mode: FileMode.CreateNew, access: FileAccess.Write, share: FileShare.None)) {

          flux.Write(buffer: contenuEncode, offset: 0, count: contenuEncode.Length);
          flux.Flush(flushToDisk: true);
        }

        File.Move(sourceFileName: fichierTemporaire, destFileName: CheminDuFichier, overwrite: true);
      }
      finally {

        if(File.Exists(path: fichierTemporaire)) {

          File.Delete(path: fichierTemporaire);
        }

        if(contenuEncode.Length > 0) {

          System.Security.Cryptography.CryptographicOperations.ZeroMemory(buffer: contenuEncode);
        }
      }
    }

		/**
     * <summary>
     *   [FR] Analyse une chaîne de caractères GsC en utilisant le lexeur et le parseur internes, et retourne les données correspondantes.<br/>
     *   [EN] Analyzes a GsC string using the internal lexer and parser, and returns the corresponding data.
     * </summary>
     * <param name="ChaineGsC">
     *   [FR] The GsC string to analyze internally.<br/>
     *   [EN] The GsC string to analyze internally.
     * </param>
     * <returns>
     *   [FR] The data extracted from the internally analyzed GsC string.<br/>
     *   [EN] The data extracted from the internally analyzed GsC string.
     * </returns>
     **/
		private DonneesGsC AnalyserInterne(string ChaineGsC) {

      string source = ChaineGsC ?? string.Empty;
      LexeurGsC lexeur = new(Source: source, Schema: Schema);
      ParseurGsC parseur = new(Jetons: lexeur.Analyser(), Schema: Schema, Configuration: Configuration, Source: source);
      return parseur.Analyser();
    }

		/**
     * <summary>
     *   [FR] Obtient les chemins complets des fichiers GsC et GsCc associés à un chemin de fichier donné sans extension, ainsi que leur existence.<br/>
     *   [EN] Gets the full paths of the GsC and GsCc files associated with a given file path without extension, as well as their existence.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] The file path without extension to check for associated GsC and GsCc files.<br/>
     *   [EN] The file path without extension to check for associated GsC and GsCc files.
     * </param>
     * <returns>
     *   [FR] A tuple containing the full paths of the associated GsC and GsCc files, and booleans indicating their existence.<br/>
     *   [EN] A tuple containing the full paths of the associated GsC and GsCc files, and booleans indicating their existence.
     * </returns>
     **/
		private (string CheminGsC, string CheminGsCc, bool ExisteGsC, bool ExisteGsCc) ObtenirEtatFichiersSansExtension(string CheminDuFichier) {

      string cheminGsC = string.Concat(str0: CheminDuFichier, str1: Configuration.Cryptage.ExtensionClaire);
      string cheminGsCc = string.Concat(str0: CheminDuFichier, str1: Configuration.Cryptage.ExtensionCryptee);
      return (cheminGsC, cheminGsCc, File.Exists(path: cheminGsC), File.Exists(path: cheminGsCc));
    }

		/**
     * <summary>
     *   [FR] Vérifie que le fichier spécifié existe, en tenant compte de la possibilité que le chemin soit un chemin sans extension et en 
     *        vérifiant l'existence du dossier si nécessaire.<br/>
     *   [EN] Verifies that the specified file exists, considering the possibility that the path is a path without extension and 
     *        checking the existence of the folder if necessary.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] The file path to verify for existence.<br/>
     *   [EN] The file path to verify for existence.
     * </param>
     **/
		private static void VerifierFichierExpliciteExiste(string CheminDuFichier) {

      if(File.Exists(path: CheminDuFichier)) {

        return;
      }

      string dossier = Path.GetDirectoryName(path: CheminDuFichier);

      if(!string.IsNullOrWhiteSpace(value: dossier) && !Directory.Exists(path: dossier)) {

        throw new DirectoryNotFoundException(message: string.Format(format: "Le dossier du fichier GsC/GsCc est introuvable : '{0}'.", arg0: dossier));
      }

      throw new FileNotFoundException(message: "Le fichier GsC/GsCc demandé est introuvable.", fileName: CheminDuFichier);
    }

		/**
		 * <summary>
		 *   [FR] Vérifie que le chemin du fichier spécifié n'est pas null, vide ou composé uniquement d'espaces blancs.<br/>
		 *   [EN] Verifies that the specified file path is not null, empty, or consists only of whitespace characters.
		 * </summary>
		 * <param name="CheminDuFichier">
		 *   [FR] The file path to verify.<br/>
		 *   [EN] The file path to verify.
		 * </param>
		 **/
		private static void VerifierCheminDuFichier(string CheminDuFichier) {

      if(string.IsNullOrWhiteSpace(value: CheminDuFichier)) {

        throw new ArgumentException(message: "Le chemin du fichier GsC ne peut pas être vide.", paramName: nameof(CheminDuFichier));
      }
    }

		/**
     * <summary>
     *   [FR] Crée une erreur GsC à partir d'une exception, en utilisant un code d'erreur générique et le message de l'exception.<br/>
     *   [EN] Creates a GsC error from an exception, using a generic error code and the message from the exception.
     * </summary>
     * <param name="Code">
     *   [FR] The generic error code to use for the created GsC error.<br/>
     *   [EN] The generic error code to use for the created GsC error.
     * </param>
     * <param name="Exception">
     *   [FR] The exception to create the GsC error from.<br/>
     *   [EN] The exception to create the GsC error from.
     * </param>
     * <returns>
     *   [FR] The created GsC error containing the provided code and the message from the exception.<br/>
     *   [EN] The created GsC error containing the provided code and the message from the exception.
     * </returns>
     **/
		private static ErreurGsC CreerErreurDepuisException(CodeErreurGsC Code, System.Exception Exception) => new(Code: Code, Message: Exception.Message);
	}
}
