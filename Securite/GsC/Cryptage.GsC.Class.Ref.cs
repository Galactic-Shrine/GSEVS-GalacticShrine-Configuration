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
using System.Security.Cryptography;
using System.Text;
using GalacticShrine.Configuration.Configuration;

namespace GalacticShrine.Configuration.Securite.GsC {

  /**
   * <summary>
   *   [FR] Fournit l'enveloppe de chiffrement et de déchiffrement du format GsCc.<br/>
   *   [EN] Provides the encryption and decryption envelope for the GsCc format.
   * </summary>
   **/
  public static class CryptageGsC {

		/**
     * <summary>
     *   [FR] Signature d'identification du format GsCc.<br/>
     *   [EN] Identification signature of the GsCc format.
     * </summary>
     **/
		public const string Signature = "GSCC";

		/**
     * <summary>
     *   [FR] Version du format GsCc.<br/>
     *   [EN] Version of the GsCc format.
     * </summary>
     **/
		public const string VersionFormat = "1";

		/**
     * <summary>
     *   [FR] En-tête complet attendu au début d'un contenu GsCc chiffré.<br/>
     *   [EN] Full header expected at the beginning of an encrypted GsCc content.
     * </summary>
     **/
		public const string EnteteFormat = Signature + ":" + VersionFormat;

		/**
     * <summary>
     *   [FR] KDF officiel utilisé pour le format GsCc.<br/>
     *   [EN] Official KDF used for the GsCc format.
     * </summary>
     **/
		public const string KdfOfficiel = "PBKDF2-SHA256";

		/**
     * <summary>
     *   [FR] Chiffrement officiel utilisé pour le format GsCc.<br/>
     *   [EN] Official cipher used for the GsCc format.
     * </summary>
     **/
		public const string ChiffrementOfficiel = "AES-256-GCM";

		/**
     * <summary>
     *   [FR] Extension de fichier recommandée pour les fichiers GsCc.<br/>
     *   [EN] Recommended file extension for GsCc files.
     * </summary>
     **/
		public static readonly string Extension = Fichier.Extension["Gs"][1];

		/**
     * <summary>
     *   [FR] Vérifie si un contenu donné est un contenu GsCc chiffré en vérifiant la présence de l'en-tête attendu.<br/>
     *   [EN] Checks if a given content is an encrypted GsCc content by verifying the presence of the expected header.
     * </summary>
     * <param name="Contenu">
     *   [FR] Le contenu à vérifier.<br/>
     *   [EN] The content to check.
     * </param>
     * <returns>
     *   [FR] Vrai si le contenu est un contenu GsCc chiffré, sinon faux.<br/>
     *   [EN] True if the content is an encrypted GsCc content, otherwise false.
     * </returns>
     **/
		public static bool EstUnContenuCrypte(string Contenu) 
      => Contenu != null && Contenu.StartsWith(value: EnteteFormat, comparisonType: StringComparison.Ordinal);

		/**
     * <summary>
     *   [FR] Chiffre un contenu clair en utilisant les options de chiffrement GsC spécifiées et retourne le contenu chiffré au format GsCc.<br/>
     *   [EN] Encrypts a plaintext content using the specified GsC encryption options and returns the encrypted content in GsCc format.
     * </summary>
     * <param name="ContenuClair">
     *   [FR] Le contenu clair à chiffrer.<br/>
     *   [EN] The plaintext content to encrypt.
     * </param>
     * <param name="Options">
     *   [FR] Les options de chiffrement GsC à utiliser pour le chiffrement.<br/>
     *   [EN] The GsC encryption options to use for encryption.
     * </param>
     * <returns>
     *   [FR] Le contenu chiffré au format GsCc.<br/>
     *   [EN] The encrypted content in GsCc format.
     * </returns>
     **/
		public static string Chiffrer(string ContenuClair, OptionsCryptageGsC Options) {

      VerifierOptions(Options: Options);

      byte[] sel = [];
      byte[] nonce = [];
      byte[] clair = [];
      byte[] chiffre = [];
      byte[] tag = [];
      byte[] cle = [];

      try {

        sel = RandomNumberGenerator.GetBytes(count: Options.TailleDuSel);
        nonce = RandomNumberGenerator.GetBytes(count: Options.TailleDuNonce);
        clair = Encoding.UTF8.GetBytes(s: ContenuClair ?? string.Empty);
        chiffre = new byte[clair.Length];
        tag = new byte[Options.TailleDuTag];
        cle = DeriverCle(MotDePasse: Options.MotDePasse, Sel: sel, Iterations: Options.Iterations);

        string selBase64 = Convert.ToBase64String(inArray: sel);
        string nonceBase64 = Convert.ToBase64String(inArray: nonce);
        byte[] donneesAssociees = CreerDonneesAssociees(
          Iterations: Options.Iterations, 
          SelBase64: selBase64, 
          NonceBase64: nonceBase64, 
          TailleDuTag: Options.TailleDuTag
        );

        using(AesGcm aes = new(key: cle, tagSizeInBytes: Options.TailleDuTag)) {

          aes.Encrypt(nonce: nonce, plaintext: clair, ciphertext: chiffre, tag: tag, associatedData: donneesAssociees);
        }

        return CreerEnveloppe(
          Iterations: Options.Iterations,
          SelBase64: selBase64,
          NonceBase64: nonceBase64,
          TagBase64: Convert.ToBase64String(inArray: tag),
          DonneesBase64: Convert.ToBase64String(inArray: chiffre),
          TailleDuTag: Options.TailleDuTag
        );
      }
      finally {

        Nettoyer(sel, nonce, clair, chiffre, tag, cle);
      }
    }

		/**
     * <summary>
     *   [FR] Déchiffre un contenu chiffré au format GsCc en utilisant les options de chiffrement GsC spécifiées et retourne le contenu clair.<br/>
     *   [EN] Decrypts an encrypted content in GsCc format using the specified GsC encryption options and returns the plaintext content.
     * </summary>
     * <param name="ContenuCrypte">
     *   [FR] Le contenu chiffré au format GsCc à déchiffrer.<br/>
     *   [EN] The encrypted content in GsCc format to decrypt.
     * </param>
     * <param name="Options">
     *   [FR] Les options de chiffrement GsC à utiliser pour le déchiffrement.<br/>
     *   [EN] The GsC encryption options to use for decryption.
     * </param>
     * <returns>
     *   [FR] Le contenu clair résultant du déchiffrement du contenu chiffré.<br/>
     *   [EN] The plaintext content resulting from decrypting the encrypted content.
     * </returns>
     **/
		public static string Dechiffrer(string ContenuCrypte, OptionsCryptageGsC Options) {

      VerifierOptions(Options: Options);

      if(!EstUnContenuCrypte(Contenu: ContenuCrypte)) {

        throw new InvalidOperationException(message: "Le contenu GsCc ne possède pas l'en-tête de chiffrement attendu.");
      }

      string contenuNormalise = NormaliserSautsDeLigne(Contenu: ContenuCrypte);
      string[] lignes = contenuNormalise.Split(separator: '\n', options: StringSplitOptions.None);

      if(lignes.Length < 8) {

        throw new FormatException(message: "Le contenu GsCc est incomplet ou utilise une enveloppe obsolète non prise en charge.");
      }

      Dictionary<string, string> champs = LireChamps(ContenuNormalise: contenuNormalise);
      string kdf = LireChamp(Champs: champs, Nom: "KDF");
      string chiffrement = LireChamp(Champs: champs, Nom: "CIPHER");

      if(!string.Equals(a: kdf, b: KdfOfficiel, comparisonType: StringComparison.OrdinalIgnoreCase)) {

        throw new InvalidOperationException(message: "Le KDF du contenu GsCc n'est pas pris en charge.");
      }

      if(!string.Equals(a: chiffrement, b: ChiffrementOfficiel, comparisonType: StringComparison.OrdinalIgnoreCase)) {

        throw new InvalidOperationException(message: "Le chiffrement du contenu GsCc n'est pas pris en charge.");
      }

      int iterations = int.Parse(s: LireChamp(Champs: champs, Nom: "ITERATIONS"), provider: CultureInfo.InvariantCulture);
      string selBase64 = LireChamp(Champs: champs, Nom: "SALT");
      string nonceBase64 = LireChamp(Champs: champs, Nom: "NONCE");
      int tailleDuTag = int.Parse(s: LireChamp(Champs: champs, Nom: "TAG-SIZE"), provider: CultureInfo.InvariantCulture);

      byte[] sel = [];
      byte[] nonce = [];
      byte[] tag = [];
      byte[] chiffre = [];
      byte[] clair = [];
      byte[] cle = [];

      try {

        sel = Convert.FromBase64String(s: selBase64);
        nonce = Convert.FromBase64String(s: nonceBase64);
        tag = Convert.FromBase64String(s: LireChamp(Champs: champs, Nom: "TAG"));

        if(tag.Length != tailleDuTag) {

          throw new InvalidOperationException(message: "La taille du tag GsCc ne correspond pas à la métadonnée TAG-SIZE.");
        }

        chiffre = Convert.FromBase64String(s: LireChamp(Champs: champs, Nom: "DATA"));
        clair = new byte[chiffre.Length];
        cle = DeriverCle(MotDePasse: Options.MotDePasse, Sel: sel, Iterations: iterations);

        byte[] donneesAssociees = CreerDonneesAssociees(Iterations: iterations, SelBase64: selBase64, NonceBase64: nonceBase64, TailleDuTag: tailleDuTag);

        using(AesGcm aes = new(key: cle, tagSizeInBytes: tailleDuTag)) {

          aes.Decrypt(nonce: nonce, ciphertext: chiffre, tag: tag, plaintext: clair, associatedData: donneesAssociees);
        }

        return Encoding.UTF8.GetString(bytes: clair);
      }
      finally {

        Nettoyer(sel, nonce, tag, chiffre, clair, cle);
      }
    }

		/**
     * <summary>
     *   [FR] Crée l'enveloppe de contenu chiffré au format GsCc en assemblant les différentes métadonnées et données chiffrées.<br/>
     *   [EN] Creates the encrypted content envelope in GsCc format by assembling the various metadata and encrypted data.
     * </summary>
     * <param name="Iterations">
     *   [FR] Le nombre d'itérations utilisé pour la dérivation de clé.<br/>
     *   [EN] The number of iterations used for key derivation.
     * </param>
     * <param name="SelBase64">
     *   [FR] Le sel utilisé pour la dérivation de clé, encodé en Base64.<br/>
     *   [EN] The salt used for key derivation, encoded in Base64.
     * </param>
     * <param name="NonceBase64">
     *   [FR] Le nonce utilisé pour le chiffrement, encodé en Base64.<br/>
     *   [EN] The nonce used for encryption, encoded in Base64.
     * </param>
     * <param name="TagBase64">
     *   [FR] Le tag d'authentification généré lors du chiffrement, encodé en Base64.<br/>
     *   [EN] The authentication tag generated during encryption, encoded in Base64.
     * </param>
     * <param name="DonneesBase64">
     *   [FR] Les données chiffrées, encodées en Base64.<br/>
     *   [EN] The encrypted data, encoded in Base64.
     * </param>
     * <param name="TailleDuTag">
     *   [FR] La taille du tag d'authentification en octets.<br/>
     *   [EN] The size of the authentication tag in bytes.
     * </param>
     * <returns>
     *   [FR] L'enveloppe de contenu chiffré au format GsCc.<br/>
     *   [EN] The encrypted content envelope in GsCc format.
     * </returns>
     **/
		private static string CreerEnveloppe(int Iterations, string SelBase64, string NonceBase64, string TagBase64, string DonneesBase64, int TailleDuTag) {

      return string.Join("\n", new[] {
        EnteteFormat,
        "KDF:" + KdfOfficiel,
        "ITERATIONS:" + Iterations.ToString(CultureInfo.InvariantCulture),
        "CIPHER:" + ChiffrementOfficiel,
        "SALT:" + SelBase64,
        "NONCE:" + NonceBase64,
        "TAG-SIZE:" + TailleDuTag.ToString(CultureInfo.InvariantCulture),
        "TAG:" + TagBase64,
        "DATA:" + DonneesBase64
      });
    }

		/**
     * <summary>
     *   [FR] Lit les champs de métadonnées d'un contenu GsCc normalisé et les retourne sous forme de dictionnaire.<br/>
     *   [EN] Reads the metadata fields from a normalized GsCc content and returns them as a dictionary.
     * </summary>
     * <param name="ContenuNormalise">
     *   [FR] Le contenu GsCc normalisé à partir duquel lire les champs de métadonnées.<br/>
     *   [EN] The normalized GsCc content from which to read the metadata fields.
     * </param>
     * <returns>
     *   [FR] Un dictionnaire contenant les champs de métadonnées lus du contenu GsCc, avec une comparaison de clés insensible à la casse.<br/>
     *   [EN] A dictionary containing the metadata fields read from the GsCc content, with case-insensitive key comparison.
     * </returns>
     **/
		private static Dictionary<string, string> LireChamps(string ContenuNormalise) {

      Dictionary<string, string> champs = new(comparer: StringComparer.OrdinalIgnoreCase);
      string[] lignes = ContenuNormalise.Split(separator: '\n', options: StringSplitOptions.None);

      for(int i = 1; i < lignes.Length; i++) {

        string ligne = lignes[i].Trim();

        if(string.IsNullOrWhiteSpace(value: ligne)) {

          continue;
        }

        int index = ligne.IndexOf(value: ':');

        if(index <= 0) {

          throw new FormatException(message: "Le contenu GsCc contient une métadonnée invalide ou une enveloppe obsolète non prise en charge.");
        }

        string nom = ligne[..index].Trim();
        string valeur = ligne[(index + 1)..].Trim();
        champs[nom] = valeur;
      }

      return champs;
    }

		/**
     * <summary>
     *   [FR] Lit une métadonnée spécifique à partir du dictionnaire de champs d'un contenu GsCc et retourne sa valeur.<br/>
     *   [EN] Reads a specific metadata from the fields dictionary of a GsCc content and returns its value.
     * </summary>
     * <param name="Champs">
     *   [FR] Le dictionnaire de champs de métadonnées d'un contenu GsCc.<br/>
     *   [EN] The dictionary of metadata fields from a GsCc content.
     * </param>
     * <param name="Nom">
     *   [FR] Le nom de la métadonnée à lire.<br/>
     *   [EN] The name of the metadata to read.
     * </param>
     * <returns>
     *   [FR] La valeur de la métadonnée spécifiée lue à partir du dictionnaire de champs.<br/>
     *   [EN] The value of the specified metadata read from the fields dictionary.
     * </returns>
     **/
		private static string LireChamp(Dictionary<string, string> Champs, string Nom) {

      if(!Champs.TryGetValue(key: Nom, value: out string valeur)) {

        throw new FormatException(message: string.Format(format: "Le contenu GsCc ne contient pas la métadonnée obligatoire '{0}'.", arg0: Nom));
      }

      if(!string.Equals(a: Nom, b: "DATA", comparisonType: StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(value: valeur)) {

        throw new FormatException(message: string.Format(format: "La métadonnée obligatoire GsCc '{0}' ne peut pas être vide.", arg0: Nom));
      }

      return valeur;
    }

		/**
     * <summary>
     *   [FR] Crée les données associées utilisées pour l'authentification lors du chiffrement et du déchiffrement d'un contenu GsCc, 
     *        en assemblant les différentes métadonnées pertinentes.<br/>
     *   [EN] Creates the associated data used for authentication during encryption and decryption of a GsCc content, 
     *        by assembling the various relevant metadata.
     * </summary>
     * <param name="Iterations">
     *   [FR] Le nombre d'itérations utilisé pour la dérivation de clé.<br/>
     *   [EN] The number of iterations used for key derivation.
     * </param>
     * <param name="SelBase64">
     *   [FR] Le sel utilisé pour la dérivation de clé, encodé en Base64.<br/>
     *   [EN] The salt used for key derivation, encoded in Base64.
     * </param>
     * <param name="NonceBase64">
     *   [FR] Le nonce utilisé pour le chiffrement, encodé en Base64.<br/>
     *   [EN] The nonce used for encryption, encoded in Base64.
     * </param>
     * <param name="TailleDuTag">
     *   [FR] La taille du tag d'authentification en octets.<br/>
     *   [EN] The size of the authentication tag in bytes.
     * </param>
     * <returns>
     *   [FR] Les données associées sous forme de tableau d'octets, 
     *        prêtes à être utilisées pour l'authentification lors du chiffrement et du déchiffrement d'un contenu GsCc.<br/>
     *   [EN] The associated data as a byte array, 
     *        ready to be used for authentication during encryption and decryption of a GsCc content.
     * </returns>
     **/
		private static byte[] CreerDonneesAssociees(int Iterations, string SelBase64, string NonceBase64, int TailleDuTag) {

      string texte = string.Join("\n", new[] {
        EnteteFormat,
        "KDF:" + KdfOfficiel,
        "ITERATIONS:" + Iterations.ToString(CultureInfo.InvariantCulture),
        "CIPHER:" + ChiffrementOfficiel,
        "SALT:" + SelBase64,
        "NONCE:" + NonceBase64,
        "TAG-SIZE:" + TailleDuTag.ToString(CultureInfo.InvariantCulture)
      });

      return Encoding.UTF8.GetBytes(s: texte);
    }

		/**
     * <summary>
     *   [FR] Dérive la clé de chiffrement à partir du mot de passe et du sel spécifiés en 
     *        utilisant le KDF officiel du format GsCc (PBKDF2 avec HMAC-SHA256).<br/>
     *   [EN] Derives the encryption key from the specified password and salt using the official KDF of the GsCc format (PBKDF2 with HMAC-SHA256).
     * </summary>
     * <param name="MotDePasse">
     *   [FR] Le mot de passe à partir duquel dériver la clé de chiffrement.<br/>
     *   [EN] The password from which to derive the encryption key.
     * </param>
     * <param name="Sel">
     *   [FR] Le sel à utiliser pour la dérivation de clé.<br/>
     *   [EN] The salt to use for key derivation.
     * </param>
     * <param name="Iterations">
     *   [FR] Le nombre d'itérations à utiliser pour la dérivation de clé.<br/>
     *   [EN] The number of iterations to use for key derivation.
     * </param>
     * <returns>
     *   [FR] La clé de chiffrement dérivée à partir du mot de passe et du sel spécifiés,
     *        prête à être utilisée pour le chiffrement ou le déchiffrement d'un contenu GsCc.<br/>
     *   [EN] The encryption key derived from the specified password and salt, 
     *        ready to be used for encryption or decryption of a GsCc content.
     * </returns>
     **/
		private static byte[] DeriverCle(string MotDePasse, byte[] Sel, int Iterations) {

      return Rfc2898DeriveBytes.Pbkdf2(
        password: MotDePasse,
        salt: Sel,
        iterations: Iterations,
        hashAlgorithm: HashAlgorithmName.SHA256,
        outputLength: 32
      );
    }

		/**
     * <summary>
     *   [FR] Normalise les sauts de ligne d'un contenu en remplaçant les différentes variations de sauts de ligne par un saut de ligne standard '\n'.<br/>
     *   [EN] Normalizes the line breaks of a content by replacing the different variations of line breaks with a standard line break '\n'.
     * </summary>
     * <param name="Contenu">
     *   [FR] Le contenu dont les sauts de ligne doivent être normalisés.<br/>
     *   [EN] The content whose line breaks need to be normalized.
     * </param>
     * <returns>
     *   [FR] Le contenu avec les sauts de ligne normalisés, 
     *        où toutes les variations de sauts de ligne ont été remplacées par un saut de ligne standard '\n'.<br/>
     *   [EN] The content with normalized line breaks, 
     *        where all variations of line breaks have been replaced with a standard line break '\n'.
     * </returns>
     **/
		private static string NormaliserSautsDeLigne(string Contenu) 
      => (Contenu ?? string.Empty).Replace(oldValue: "\r\n", newValue: "\n").Replace(oldValue: "\r", newValue: "\n");

		/**
     * <summary>
     *   [FR] Nettoie de manière sécurisée les tableaux d'octets spécifiés en utilisant la méthode ZeroMemory de CryptographicOperations, 
     *        afin d'effacer les données sensibles de la mémoire après leur utilisation.<br/>
     *   [EN] Securely cleans the specified byte arrays using the ZeroMemory method of CryptographicOperations, 
     *        to erase sensitive data from memory after use.
     * </summary>
     * <param name="Tableaux">
     *   [FR] Les tableaux d'octets à nettoyer de manière sécurisée.<br/>
     *   [EN] The byte arrays to securely clean.
     * </param>
     **/
		private static void Nettoyer(params byte[][] Tableaux) {

      foreach(byte[] tableau in Tableaux) {

        if(tableau != null && tableau.Length > 0) {

          CryptographicOperations.ZeroMemory(buffer: tableau);
        }
      }
    }

		/**
     * <summary>
     *   [FR] Vérifie la validité des options de chiffrement GsC spécifiées en s'assurant que les options 
     *        ne sont pas nulles et que le mot de passe est présent et non vide.<br/>
     *   [EN] Validates the specified GsC encryption options by ensuring that the options 
     *        are not null and that the password is present and not empty.
     * </summary>
     * <param name="Options">
     *   [FR] Les options de chiffrement GsC à vérifier.<br/>
     *   [EN] The GsC encryption options to validate.
     * </param>
     **/
		private static void VerifierOptions(OptionsCryptageGsC Options) {

      if(Options == null) {

        throw new ArgumentNullException(paramName: nameof(Options));
      }

      if(string.IsNullOrWhiteSpace(value: Options.MotDePasse)) {

        throw new InvalidOperationException(message: "Le mot de passe de chiffrement GsC est obligatoire pour lire ou écrire un fichier GsCc.");
      }
    }
  }
}
