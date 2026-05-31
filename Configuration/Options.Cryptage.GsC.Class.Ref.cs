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

using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Configuration.Configuration {

  /**
   * <summary>
   *   [FR] Définit les options de chiffrement utilisées pour les fichiers GsCc.<br/>
   *   [EN] Defines encryption options used by GsCc files.
   * </summary>
   **/
  public class OptionsCryptageGsC : ClonableInterface<OptionsCryptageGsC> {

		/**
     * <summary>
     *   [FR] Indique si le chiffrement est actif pour les fichiers GsCc.<br/>
     *   [EN] Indicates whether encryption is active for GsCc files.
     * </summary>
     **/
		public bool EstActif { get; set; } = false;

		/**
     * <summary>
     *   [FR] Le mot de passe utilisé pour le chiffrement et le déchiffrement des fichiers GsCc.<br/>
     *   [EN] The password used for encrypting and decrypting GsCc files.
     * </summary>
     **/
		public string MotDePasse { get; set; }

		/**
     * <summary>
     *   [FR] L'extension de fichier utilisée pour les fichiers GsC non chiffrés.<br/>
     *   [EN] The file extension used for unencrypted GsC files.
     * </summary>
     * <remarks>
     *   [FR] Par défaut, l'extension de fichier pour les fichiers GsC non chiffrés est définie sur la première extension de la liste d'extensions 
     *        associée à "Gs" dans la classe <see cref="Fichier"/>.<br/>
     *   [EN] By default, the file extension for unencrypted GsC files is set to the first extension in the list of extensions 
     *        associated with "Gs" in the <see cref="Fichier"/> class.
     * </remarks>
     **/
		public string ExtensionClaire { get; set; } = Fichier.Extension["Gs"][0];

		/**
     * <summary>
     *   [FR] L'extension de fichier utilisée pour les fichiers GsC chiffrés (GsCc).<br/>
     *   [EN] The file extension used for encrypted GsC files (GsCc).
     * </summary>
     * <remarks>
     *   [FR] Par défaut, l'extension de fichier pour les fichiers GsC chiffrés est définie sur la deuxième extension de la liste d'extensions 
     *        associée à "Gs" dans la classe <see cref="Fichier"/>.<br/>
     *   [EN] By default, the file extension for encrypted GsC files is set to the second extension in the list of extensions 
     *        associated with "Gs" in the <see cref="Fichier"/> class.
     * </remarks>
     **/
		public string ExtensionCryptee { get; set; } = Fichier.Extension["Gs"][1];

		/**
     * <summary>
     *   [FR] Le nombre d'itérations utilisé pour la dérivation de clé dans le processus de chiffrement et de déchiffrement des fichiers GsCc.<br/>
     *   [EN] The number of iterations used for key derivation in the encryption and decryption process of GsCc files.
     * </summary>
     **/
		public int Iterations { get; set; } = 200000;

		/**
     * <summary>
     *   [FR] La taille du sel (en octets) utilisée pour la dérivation de clé dans le processus de chiffrement et de déchiffrement des fichiers GsCc.<br/>
     *   [EN] The size of the salt (in bytes) used for key derivation in the encryption and decryption process of GsCc files.
     * </summary>
     **/
		public int TailleDuSel { get; set; } = 16;

		/**
     * <summary>
     *   [FR] La taille du nonce (en octets) utilisée pour le chiffrement et le déchiffrement des fichiers GsCc.<br/>
     *   [EN] The size of the nonce (in bytes) used for encrypting and decrypting GsCc files.
     * </summary>
     **/
		public int TailleDuNonce { get; set; } = 12;

		/**
     * <summary>
     *   [FR] La taille du tag d'authentification (en octets) utilisée pour le chiffrement et le déchiffrement des fichiers GsCc.<br/>
     *   [EN] The size of the authentication tag (in bytes) used for encrypting and decrypting GsCc files.
     * </summary>
     **/
		public int TailleDuTag { get; set; } = 16;

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="OptionsCryptageGsC"/> avec les valeurs par défaut.<br/>
     *   [EN] Initializes a new instance of the <see cref="OptionsCryptageGsC"/> class with default values.
     * </summary>
     **/
		public OptionsCryptageGsC() { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="OptionsCryptageGsC"/> en copiant les valeurs d'une autre instance.<br/>
     *   [EN] Initializes a new instance of the <see cref="OptionsCryptageGsC"/> class by copying the values from another instance.
     * </summary>
     * <param name="OptionsCryptageGsCInstance">
     *   [FR] L'instance de <see cref="OptionsCryptageGsC"/> à copier.<br/>
     *   [EN] The <see cref="OptionsCryptageGsC"/> instance to copy.
     * </param>
     **/
		public OptionsCryptageGsC(OptionsCryptageGsC OptionsCryptageGsCInstance) {

      EstActif = OptionsCryptageGsCInstance.EstActif;
      MotDePasse = OptionsCryptageGsCInstance.MotDePasse;
      ExtensionClaire = OptionsCryptageGsCInstance.ExtensionClaire;
      ExtensionCryptee = OptionsCryptageGsCInstance.ExtensionCryptee;
      Iterations = OptionsCryptageGsCInstance.Iterations;
      TailleDuSel = OptionsCryptageGsCInstance.TailleDuSel;
      TailleDuNonce = OptionsCryptageGsCInstance.TailleDuNonce;
      TailleDuTag = OptionsCryptageGsCInstance.TailleDuTag;
    }

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de l'instance actuelle de <see cref="OptionsCryptageGsC"/>.<br/>
     *   [EN] Creates a deep copy of the current instance of <see cref="OptionsCryptageGsC"/>.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="OptionsCryptageGsC"/> qui est une copie en profondeur de l'instance actuelle.<br/>
     *   [EN] A new instance of <see cref="OptionsCryptageGsC"/> that is a deep copy of the current instance.
     * </returns>
     **/
		public OptionsCryptageGsC CloneEnProfondeur() => new(OptionsCryptageGsCInstance: this);
  }
}
