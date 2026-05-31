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

namespace GalacticShrine.Configuration.Analyseur.GsC {

  /**
   * <summary>
   *   [FR] Représente une exception d'analyse GsC avec code d'erreur et position.<br/>
   *   [EN] Represents a GsC analysis exception with error code and position.
   * </summary>
   **/
  public class AnalyseGsCException : System.FormatException {

		/**
		 * <summary>
     *   [FR] L'erreur GsC associée à cette exception.<br/>
     *   [EN] The GsC error associated with this exception.
     * </summary>
     **/
		public ErreurGsC Erreur { get; }

		/**
		 * <summary>
		 *   [FR] Le code d'erreur GsC associé à cette exception.<br/>
		 *   [EN] The GsC error code associated with this exception.
		 * </summary>
		 **/
		public CodeErreurGsC Code => Erreur.Code;

		/**
		 * <summary>
     *   [FR] Le message d'erreur GsC associé à cette exception.<br/>
     *   [EN] The GsC error message associated with this exception.
     * </summary>
     **/
		public string CodeTexte => Erreur.CodeTexte;

		/**
		 * <summary>
     *   [FR] La ligne où l'erreur GsC s'est produite.<br/>
     *   [EN] The line where the GsC error occurred.
     * </summary>
     **/
		public int Ligne => Erreur.Ligne;

		/**
		 * <summary>
     *   [FR] La colonne où l'erreur GsC s'est produite.<br/>
     *   [EN] The column where the GsC error occurred.
     * </summary>
     **/
		public int Colonne => Erreur.Colonne;

		/**
		 * <summary>
		 *   [FR] L'extrait de texte où l'erreur GsC s'est produite.<br/>
		 *   [EN] The text excerpt where the GsC error occurred.
		 * </summary>
		 **/
		public string Extrait => Erreur.Extrait;

		/**
		 * <summary>
     *   [FR] Le jeton où l'erreur GsC s'est produite.<br/>
     *   [EN] The token where the GsC error occurred.
     * </summary>
     **/
		public string Jeton => Erreur.Jeton;

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="AnalyseGsCException"/> avec une erreur GsC spécifiée.<br/>
     *   [EN] Initializes a new instance of the <see cref="AnalyseGsCException"/> class with a specified GsC error.
     * </summary>
     * <param name="Erreur">
     *   [FR] L'erreur GsC associée à cette exception.<br/>
     *   [EN] The GsC error associated with this exception.
     * </param>
     **/
		public AnalyseGsCException(ErreurGsC Erreur) : base(message: Erreur?.ToString() ?? string.Empty) 
      => this.Erreur = Erreur ?? new ErreurGsC(Code: CodeErreurGsC.ErreurInconnue, Message: "Erreur GsC inconnue.");

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="AnalyseGsCException"/> avec une erreur GsC spécifiée et une exception interne.<br/>
     *   [EN] Initializes a new instance of the <see cref="AnalyseGsCException"/> class with a specified GsC error and an inner exception.
     * </summary>
     * <param name="Erreur">
     *   [FR] L'erreur GsC associée à cette exception.<br/>
     *   [EN] The GsC error associated with this exception.
     * </param>
     * <param name="ExceptionInterne">
     *   [FR] L'exception interne qui a causé cette exception, le cas échéant.<br/>
     *   [EN] The inner exception that caused this exception, if any.
     * </param>
     **/
		public AnalyseGsCException(ErreurGsC Erreur, System.Exception ExceptionInterne) 
      : base(message: Erreur?.ToString() ?? string.Empty, innerException: ExceptionInterne) 
      => this.Erreur = Erreur ?? new ErreurGsC(Code: CodeErreurGsC.ErreurInconnue, Message: "Erreur GsC inconnue.");
	}
}
