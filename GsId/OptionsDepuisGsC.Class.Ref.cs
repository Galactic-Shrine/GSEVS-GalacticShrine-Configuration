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
using GalacticShrine.Enumeration.GsId;
using GalacticShrine.GsId;
using GalacticShrine.Modele.Configuration.GsC;

namespace GalacticShrine.Configuration.GsId {

	/**
	 * <summary>
	 *   [FR] Fournit les mécanismes de configuration des options GsId à partir d'un fichier de configuration GsC.
	 *   [EN] Provides mechanisms for configuring GsId options from a GsC configuration file.
	 * </summary>
	 **/
	public static class OptionsDepuisGsC {

		/**
		 * <summary>
		 *   [FR] Nom de section par défaut pour la configuration GsId dans un fichier GsC.
		 *   [EN] Default section name for GsId configuration in a GsC file.
		 * </summary>
		 **/
		public const string NomDeSectionParDefaut = "GsId";
		
		/**
		 * <summary>
		 *   [FR] Configure les options GsId à partir d'un fichier de configuration GsC.
		 *   [EN] Configures GsId options from a GsC configuration file.
		 * </summary>
		 * <param name="Chemin">
		 *   [FR] Chemin du fichier de configuration.
		 *   [EN] Path to the configuration file.
		 * </param>
		 * <param name="NomDeSection">
		 *   [FR] Nom de la section à utiliser.
		 *   [EN] Name of the section to use.
		 * </param>
		 **/
		public static void ConfigurerDepuisFichier(string Chemin, string NomDeSection = NomDeSectionParDefaut) {

			GsC GsC = new();
			DonneesGsC Donnees = GsC.Ouvrir(Chemin);

			Configurer(Donnees, NomDeSection);
		}

		/**
		 * <summary>
		 *   [FR] Configure les options GsId à partir d'une instance de GsC.
		 *   [EN] Configures GsId options from a GsC instance.
		 * </summary>
		 * <param name="GsC">
		 *   [FR] Instance de GsC à utiliser pour la configuration.
		 *   [EN] GsC instance to use for configuration.
		 * </param>
		 * <param name="Chemin">
		 *   [FR] Chemin du fichier de configuration.
		 *   [EN] Path to the configuration file.
		 * </param>
		 * <param name="NomDeSection">
		 *   [FR] Nom de la section à utiliser.
		 *   [EN] Name of the section to use.
		 * </param>
		 **/
		public static void ConfigurerDepuisFichier(GsC GsC, string Chemin, string NomDeSection = NomDeSectionParDefaut) {

			ArgumentNullException.ThrowIfNull(GsC);

			DonneesGsC Donnees = GsC.Ouvrir(Chemin);

			Configurer(Donnees, NomDeSection);
		}

		/**
		 * <summary>
		 *   [FR] Configure les options GsId à partir d'une instance de données GsC.
		 *   [EN] Configures GsId options from a GsC data instance.
		 * </summary>
		 * <param name="Donnees">
		 *   [FR] Instance de données GsC à utiliser pour la configuration.
		 *   [EN] GsC data instance to use for configuration.
		 * </param>
		 * <param name="NomDeSection">
		 *   [FR] Nom de la section à utiliser.
		 *   [EN] Name of the section to use.
		 * </param>
		 **/
		public static void Configurer(DonneesGsC Donnees, string NomDeSection = NomDeSectionParDefaut) {

			ArgumentNullException.ThrowIfNull(Donnees);

			SectionGsC Section = Donnees[NomDeSection];
			ObjetGsC Formats = Section.Contient("Format") ? Section.ObtenirValeur("Format").Objet : new ObjetGsC();

			string Casses = Section.Contient("Casse") ? Section.ObtenirValeur("Casse").Chaine : "";
			string Texte = Formats.Contient("Texte") ? Formats.ObtenirValeur("Texte").Chaine : "";
			string Json = Formats.Contient("Json") ? Formats.ObtenirValeur("Json").Chaine : "";
			string BaseDeDonnees = Formats.Contient("BaseDeDonnees") ? Formats.ObtenirValeur("BaseDeDonnees").Chaine : "";

			bool Verrouiller = Section.Contient("Verrouiller") && Section.ObtenirValeur("Verrouiller").Booleen;

			Options.Configurer(
				Casse: Casse(Casses),
				FormatTexte: Format(Texte, Options.FormatTexteParDefaut),
				FormatJson: Format(Json, Options.FormatJsonParDefaut),
				FormatBaseDeDonnees: Format(BaseDeDonnees, Options.FormatBaseDeDonneesParDefaut)
			);

			if(Verrouiller) {

				Options.Verrouiller();
			}
		}

		/**
		 * <summary>
		 *   [FR] Lit la casse d'identifiant à partir d'une chaîne de caractères, en acceptant plusieurs représentations courantes.
		 *   [EN] Reads the identifier case from a string, accepting multiple common representations.
		 * </summary>
		 * <param name="Valeur">
		 *   [FR] Chaîne de caractères à analyser.
		 *   [EN] String to parse.
		 * </param>
		 * <returns>
		 *   [FR] La casse d'identifiant correspondante.
		 *   [EN] The corresponding identifier case.
		 * </returns>
		 **/
		private static CasseDIdentifiant Casse(string Valeur) {

			if(string.IsNullOrWhiteSpace(Valeur)) {

				return Options.CasseParDefaut;
			}

			string ValeurNormalisee = Valeur.Trim();

			return ValeurNormalisee.ToLowerInvariant() switch {

				"majuscule" or "majuscules" or "upper" or "uppercase" => CasseDIdentifiant.Majuscules,
				"minuscule" or "minuscules" or "lower" or "lowercase" => CasseDIdentifiant.Minuscules,
				_ => throw new FormatException($"La casse GsId '{Valeur}' est invalide.")
			};
		}

		/**
		 * <summary>
		 *   [FR] Lit le format d'identifiant à partir d'une chaîne de caractères, en acceptant plusieurs représentations courantes.
		 *   [EN] Reads the identifier format from a string, accepting multiple common representations.
		 * </summary>
		 * <param name="Valeur">
		 *   [FR] Chaîne de caractères à analyser.
		 *   [EN] String to parse.
		 * </param>
		 * <returns>
		 *   [FR] Le format d'identifiant correspondant.
		 *   [EN] The corresponding identifier format.
		 * </returns>
		 **/
		private static FormatDIdentifiant Format(string Valeur, FormatDIdentifiant Defaut) {

			if(string.IsNullOrWhiteSpace(Valeur)) {

				return Defaut;
			}

			string ValeurNormalisee = Valeur.Trim();

			return ValeurNormalisee.ToUpperInvariant() switch {

				"N" => FormatDIdentifiant.N,
				"D" => FormatDIdentifiant.D,
				_ => throw new FormatException($"Le format GsId '{Valeur}' est invalide.")
			};
		}
	}
}
