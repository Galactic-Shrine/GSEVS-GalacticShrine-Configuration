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
using System.Reflection;
using GalacticShrine.Configuration.Properties;

namespace GalacticShrine.Exceptions.Configuration {

  /**
   * <summary>
   *   [FR] Représente une erreur survenue lors de l'analyse des données.
   *   [EN] Represents an error during data analysis.
   * </summary>
   **/
  public class AnalyseException : System.Exception {

		/**
     * <summary>
     *   [FR] La version de la bibliothèque où l'erreur s'est produite.
     *   [EN] The version of the library where the error occurred.
     * </summary>
     **/
		public Version VersionLib { get;}

		/**
     * <summary>
     *   [FR] Le numéro de la ligne où l'erreur s'est produite.
     *   [EN] The line number where the error occurred.
     * </summary>
     **/
		public uint NumeroDeLaLigne {get;}

		/**
     * <summary>
     *   [FR] Le contenu de la ligne où l'erreur s'est produite.
     *   [EN] The content of the line where the error occurred.
     * </summary>
     **/
		public string ContenuDeLaLigne { get;}

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="AnalyseException"/> avec un message d'erreur spécifié.<br/>
     *   [EN] Initializes a new instance of the <see cref="AnalyseException"/> class with a specified error message.
     * </summary>
     * <param name="Message">
     *   [FR] Le message d'erreur décrivant la nature de l'erreur.<br/>
     *   [EN] The error message describing the nature of the error.
     * </param>
     **/
		public AnalyseException(string Message, uint NumeroDeLigne) : this(Message: Message, NumeroDeLigne: NumeroDeLigne, ContenuDeLigne: string.Empty, ExceptionInterne: null) { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="AnalyseException"/> 
     *        avec un message d'erreur spécifié et une exception interne.<br/>
     *   [EN] Initializes a new instance of the <see cref="AnalyseException"/> class 
     *        with a specified error message and an inner exception.
     * </summary>
     * <param name="Message">
     *   [FR] Le message d'erreur décrivant la nature de l'erreur.<br/>
     *   [EN] The error message describing the nature of the error.
     * </param>
     * <param name="ExceptionInterne">
     *   [FR] L'exception interne qui a causé l'erreur, si disponible.<br/>
     *   [EN] The inner exception that caused the error, if available.
     * </param>
     **/
		public AnalyseException(string Message, System.Exception ExceptionInterne) : this(Message: Message, NumeroDeLigne: 0, ContenuDeLigne: string.Empty, ExceptionInterne: ExceptionInterne) { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="AnalyseException"/> avec un message d'erreur spécifié, 
     *        un numéro de ligne et le contenu de la ligne.<br/>
     *   [EN] Initializes a new instance of the <see cref="AnalyseException"/> class with a specified error message, 
     *        line number, and line content.
     * </summary>
     * <param name="Message">
     *   [FR] Le message d'erreur décrivant la nature de l'erreur.<br/>
     *   [EN] The error message describing the nature of the error.
     * </param>
     * <param name="NumeroDeLigne">
     *   [FR] Le numéro de la ligne où l'erreur s'est produite.<br/>
     *   [EN] The line number where the error occurred.
     * </param>
     * <param name="ContenuDeLigne">
     *   [FR] Le contenu de la ligne où l'erreur s'est produite.<br/>
     *   [EN] The content of the line where the error occurred.
     * </param>
     **/
		public AnalyseException(string Message, uint NumeroDeLigne, string ContenuDeLigne) : this(Message: Message, NumeroDeLigne: NumeroDeLigne, ContenuDeLigne: ContenuDeLigne, ExceptionInterne: null) { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="AnalyseException"/> avec un message d'erreur spécifié, 
     *        un numéro de ligne, le contenu de la ligne et une exception interne.<br/>
     *   [EN] Initializes a new instance of the <see cref="AnalyseException"/> class with a specified error message, 
     *        line number, line content, and an inner exception.
     * </summary>
     * <param name="Message">
     *   [FR] Le message d'erreur décrivant la nature de l'erreur.<br/>
     *   [EN] The error message describing the nature of the error.
     * </param>
     * <param name="NumeroDeLigne">
     *   [FR] Le numéro de la ligne où l'erreur s'est produite.<br/>
     *   [EN] The line number where the error occurred.
     * </param>
     * <param name="ContenuDeLigne">
     *   [FR] Le contenu de la ligne où l'erreur s'est produite.<br/>
     *   [EN] The content of the line where the error occurred.
     * </param>
     * <param name="ExceptionInterne">
     *   [FR] L'exception interne qui a causé l'erreur, si disponible.<br/>
     *   [EN] The inner exception that caused the error, if available.
     * </param>
     **/
		public AnalyseException(string Message, uint NumeroDeLigne, string ContenuDeLigne, System.Exception ExceptionInterne) : base(message: $"{Message} {Resources.MessageExceptionErreurAnalyse0} {NumeroDeLigne} {Resources.MessageExceptionErreurAnalyse1} '{ContenuDeLigne}'", innerException: ExceptionInterne) {//Resources.MessageExceptionErreurAnalyse,

      VersionLib = ObtenirLaVersionDeL_Assemblage();
      NumeroDeLaLigne = NumeroDeLigne;
      ContenuDeLaLigne = ContenuDeLigne;
    }

		/**
     * <summary>
     *   [FR] Obtient la version de l'assemblage en cours d'exécution.<br/>
     *   [EN] Gets the version of the currently executing assembly.
     * </summary>
     * <returns>
     *   [FR] La version de l'assemblage.<br/>
     *   [EN] The version of the assembly.
     * </returns>
     **/
		private Version ObtenirLaVersionDeL_Assemblage() => Assembly.GetExecutingAssembly().GetName().Version;
  }
}
