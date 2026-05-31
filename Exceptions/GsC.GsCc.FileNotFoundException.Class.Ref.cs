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

using System.IO;

namespace GalacticShrine.Exceptions.Configuration {

  /**
   * <summary>
   *   [FR] Représente l'absence simultanée d'un fichier GsC et d'un fichier GsCc pour un même nom de base.<br/>
   *   [EN] Represents the simultaneous absence of a GsC file and a GsCc file for the same base name.
   * </summary>
   **/
  public class GsCGsCcFileNotFoundException : FileNotFoundException {

		/**
     * <summary>
     *   [FR] Le chemin du fichier de demande pour lequel aucun fichier GsC ou GsCc n'a été trouvé.<br/>
     *   [EN] The path of the request file for which no GsC or GsCc file was found.
     * </summary>
     **/
		public string CheminDemande { get; }

		/**
     * <summary>
     *   [FR] Le chemin du fichier GsC testé.<br/>
     *   [EN] The path of the tested GsC file.
     * </summary>
     **/
		public string CheminGsC { get; }

		/**
     * <summary>
     *   [FR] Le chemin du fichier GsCc testé.<br/>
     *   [EN] The path of the tested GsCc file.
     * </summary>
     **/
		public string CheminGsCc { get; }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GsCGsCcFileNotFoundException"/> avec les chemins de demande, GsC et GsCc spécifiés.<br/>
     *   [EN] Initializes a new instance of the <see cref="GsCGsCcFileNotFoundException"/> class with the specified request, GsC and GsCc paths.
     * </summary>
     * <param name="CheminDemande">
     *   [FR] Le chemin du fichier de demande pour lequel aucun fichier GsC ou GsCc n'a été trouvé.<br/>
     *   [EN] The path of the request file for which no GsC or GsCc file was found.
     * </param>
     * <param name="CheminGsC">
     *   [FR] Le chemin du fichier GsC testé.<br/>
     *   [EN] The path of the tested GsC file.
     * </param>
     * <param name="CheminGsCc">
     *   [FR] Le chemin du fichier GsCc testé.<br/>
     *   [EN] The path of the tested GsCc file.
     * </param>
     **/
		public GsCGsCcFileNotFoundException(string CheminDemande, string CheminGsC, string CheminGsCc) 
      : base(message: ConstruireMessage(CheminDemande: CheminDemande, CheminGsC: CheminGsC, CheminGsCc: CheminGsCc), fileName: CheminDemande) {

      this.CheminDemande = CheminDemande;
      this.CheminGsC = CheminGsC;
      this.CheminGsCc = CheminGsCc;
    }

		/**
     * <summary>
     *   [FR] Construit un message d'erreur détaillé indiquant les chemins testés pour le fichier de demande, le fichier GsC et le fichier GsCc.<br/>
     *   [EN] Constructs a detailed error message indicating the paths tested for the request file, the GsC file and the GsCc file.
     * </summary>
     * <param name="CheminDemande">
     *   [FR] Le chemin du fichier de demande pour lequel aucun fichier GsC ou GsCc n'a été trouvé.<br/>
     *   [EN] The path of the request file for which no GsC or GsCc file was found.
     * </param>
     * <param name="CheminGsC">
     *   [FR] Le chemin du fichier GsC testé.<br/>
     *   [EN] The path of the tested GsC file.
     * </param>
     * <param name="CheminGsCc">
     *   [FR] Le chemin du fichier GsCc testé.<br/>
     *   [EN] The path of the tested GsCc file.
     * </param>
     * <returns>
     *   [FR] Un message d'erreur formaté indiquant les chemins testés pour le fichier de demande, le fichier GsC et le fichier GsCc.<br/>
     *   [EN] A formatted error message indicating the paths tested for the request file, the GsC file and the GsCc file.
     * </returns>
     **/
		private static string ConstruireMessage(string CheminDemande, string CheminGsC, string CheminGsCc) 
      => string.Format(
        format: "Aucun fichier GsC/GsCc n'a été trouvé pour '{0}'. Chemins testés : '{1}' et '{2}'.", 
        arg0: CheminDemande, 
        arg1: CheminGsC, 
        arg2: CheminGsCc
      );
  }
}
