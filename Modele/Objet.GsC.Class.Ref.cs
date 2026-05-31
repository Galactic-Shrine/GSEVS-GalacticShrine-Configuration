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
using System.Collections.ObjectModel;
using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Enumeration.Configuration.GsC;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.GsC {

  /**
   * <summary>
   *   [FR] Représente un objet GsC contenant des propriétés nommées.<br/>
   *   [EN] Represents a GsC object containing named properties.
   * </summary>
   **/
  public class ObjetGsC : ClonableInterface<ObjetGsC> {

		/**
     * [FR] Dictionnaire interne pour un accès rapide aux propriétés par leur clé.<br/>
     * [EN] Internal dictionary for quick access to properties by their key.
     **/
		private readonly Dictionary<string, ProprieteGsC> ProprietesParCle;

		/**
     * [FR] Collection des propriétés de cet objet GsC.<br/>
     * [EN] Collection of properties of this GsC object.
     **/
		public Collection<ProprieteGsC> Proprietes { get; }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="ObjetGsC"/> avec une comparaison de clés spécifique.<br/>
     *   [EN] Initializes a new instance of the <see cref="ObjetGsC"/> class with a specific key comparer.
     * </summary>
     * <param name="RechercherComparer">
     *   [FR] Comparateur d'égalité pour les clés des propriétés.<br/>
     *   [EN] Equality comparer for property keys.
     * </param>
     **/
		public ObjetGsC() : this(RechercherComparer: StringComparer.Ordinal) { }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="ObjetGsC"/> avec un comparateur d'égalité pour les clés des propriétés.<br/>
     *   [EN] Initializes a new instance of the <see cref="ObjetGsC"/> class with an equality comparer for property keys.
     * </summary>
     * <param name="RechercherComparer">
     *   [FR] Comparateur d'égalité pour les clés des propriétés.<br/>
     *   [EN] Equality comparer for property keys.
     * </param>
     **/
		public ObjetGsC(IEqualityComparer<string> RechercherComparer) {

      Proprietes = [];
      ProprietesParCle = new Dictionary<string, ProprieteGsC>(comparer: RechercherComparer);
    }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="ObjetGsC"/> en copiant les propriétés d'une autre instance.<br/>
     *   [EN] Initializes a new instance of the <see cref="ObjetGsC"/> class by copying the properties from another instance.
     * </summary>
     * <param name="ObjetGsCInstance">
     *   [FR] L'instance de <see cref="ObjetGsC"/> à copier.<br/>
     *   [EN] The <see cref="ObjetGsC"/> instance to copy.
     * </param>
     * <param name="RechercherComparer">
     *   [FR] Comparateur d'égalité pour les clés des propriétés. Si null, le comparateur par défaut est utilisé.<br/>
     *   [EN] Equality comparer for property keys. If null, the default comparer is used.
     * </param>
     **/
		public ObjetGsC(ObjetGsC ObjetGsCInstance, IEqualityComparer<string> RechercherComparer = null) 
      : this(RechercherComparer: RechercherComparer ?? StringComparer.Ordinal) {

      foreach(ProprieteGsC propriete in ObjetGsCInstance.Proprietes) {

        Ajouter(Propriete: propriete.CloneEnProfondeur(), Remplacer: true);
      }
    }

		/**
     * <summary>
     *   [FR] Accède à la valeur d'une propriété par sa clé.<br/>
     *   [EN] Accesses the value of a property by its key.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de la propriété à accéder.<br/>
     *   [EN] The key of the property to access.
     * </param>
     * <returns>
     *   [FR] La valeur de la propriété associée à la clé spécifiée.<br/>
     *   [EN] The value of the property associated with the specified key.
     * </returns>
     **/
		public ValeurGsC this[string Cle] {

      get => ObtenirValeur(Cle: Cle);
      set => Ajouter(Cle: Cle, Valeur: value, Remplacer: true);
    }

		/**
     * <summary>
     *   [FR] Ajoute ou remplace une propriété dans cet objet GsC.<br/>
     *   [EN] Adds or replaces a property in this GsC object.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de la propriété à ajouter ou remplacer.<br/>
     *   [EN] The key of the property to add or replace.
     * </param>
     * <param name="Valeur">
     *   [FR] La valeur de la propriété à ajouter ou remplacer.<br/>
     *   [EN] The value of the property to add or replace.
     * </param>
     * <param name="Remplacer">
     *   [FR] Indique si une propriété existante avec la même clé doit être remplacée. Par défaut, true.<br/>
     *   [EN] Indicates whether an existing property with the same key should be replaced. Default is true.
     * </param>
     **/
		public void Ajouter(string Cle, ValeurGsC Valeur, bool Remplacer = true) 
      => Ajouter(Propriete: new ProprieteGsC(Cle: Cle, Valeur: Valeur), Remplacer: Remplacer);

		/**
     * <summary>
     *   [FR] Ajoute ou remplace une propriété dans cet objet GsC.<br/>
     *   [EN] Adds or replaces a property in this GsC object.
     * </summary>
     * <param name="Propriete">
     *   [FR] La propriété à ajouter ou remplacer.<br/>
     *   [EN] The property to add or replace.
     * </param>
     * <param name="Remplacer">
     *   [FR] Indique si une propriété existante avec la même clé doit être remplacée. Par défaut, true.<br/>
     *   [EN] Indicates whether an existing property with the same key should be replaced. Default is true.
     * </param>
     **/
		public void Ajouter(ProprieteGsC Propriete, bool Remplacer = true) {

      if(ProprietesParCle.TryGetValue(key: Propriete.Cle, value: out ProprieteGsC proprieteExistante)) {

        if(!Remplacer) {

          throw new InvalidOperationException(message: string.Format(format: "La propriété GsC '{0}' existe déjà.", arg0: Propriete.Cle));
        }

        int index = Proprietes.IndexOf(item: proprieteExistante);
        Proprietes[index] = Propriete;
        ProprietesParCle[Propriete.Cle] = Propriete;
        return;
      }

      Proprietes.Add(item: Propriete);
      ProprietesParCle.Add(key: Propriete.Cle, value: Propriete);
    }

    /**
     * <summary>
     *   [FR] Vérifie si une propriété avec la clé spécifiée existe dans cet objet GsC.<br/>
     *   [EN] Checks if a property with the specified key exists in this GsC object.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de la propriété à vérifier.<br/>
     *   [EN] The key of the property to check.
     * </param>
     * <returns>
     *   [FR] true si une propriété avec la clé spécifiée existe, sinon false.<br/>
     *   [EN] true if a property with the specified key exists, otherwise false.
     * </returns>
     **/
    public bool Contient(string Cle) => ProprietesParCle.ContainsKey(key: Cle);

		/**
     * <summary>
     *   [FR] Obtient une propriété par sa clé.<br/>
     *   [EN] Gets a property by its key.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de la propriété à obtenir.<br/>
     *   [EN] The key of the property to get.
     * </param>
     * <returns>
     *   [FR] La propriété associée à la clé spécifiée.<br/>
     *   [EN] The property associated with the specified key.
     * </returns>
     **/
		public ProprieteGsC ObtenirPropriete(string Cle) => ProprietesParCle[Cle];

		/**
     * <summary>
     *   [FR] Obtient la valeur d'une propriété par sa clé.<br/>
     *   [EN] Gets the value of a property by its key.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de la propriété dont la valeur doit être obtenue.<br/>
     *   [EN] The key of the property whose value is to be obtained.
     * </param>
     * <returns>
     *   [FR] La valeur de la propriété associée à la clé spécifiée.<br/>
     *   [EN] The value of the property associated with the specified key.
     * </returns>
     **/
		public ValeurGsC ObtenirValeur(string Cle) => ObtenirPropriete(Cle: Cle).Valeur;

        /**
     * <summary>
     *   [FR] Efface les commentaires ou les propriétés de cet objet GsC selon le mode demandé.<br/>
     *   [EN] Clears comments or properties from this GsC object according to the requested mode.
     * </summary>
     * <remarks>
     *   [FR] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/> efface les commentaires des propriétés et des valeurs imbriquées.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/> supprime toutes les propriétés de l'objet.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/> supprime toutes les propriétés de l'objet.</para>
     *   [EN] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/> clears comments from properties and nested values.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/> removes all properties from the object.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/> removes all properties from the object.</para>
     * </remarks>
     * <param name="Effacement">
     *   [FR] Mode d'effacement à appliquer. La valeur par défaut est <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>.<br/>
     *   [EN] Clearing mode to apply. The default value is <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>.
     * </param>
     **/

    public void Effacer(Effacement Effacement = Effacement.Proprietes) {

      switch(Effacement) {

        case Effacement.Commentaires:

          foreach(ProprieteGsC propriete in Proprietes) {

            propriete.Commentaire.Clear();
            EffacerCommentairesDansValeur(Valeur: propriete.Valeur);
          }
          break;

        case Effacement.Proprietes:
        case Effacement.Tout:

          Proprietes.Clear();
          ProprietesParCle.Clear();
          break;
      }
    }

		/**
     * <summary>
     *   [FR] Efface les commentaires dans une valeur GsC de manière récursive.<br/>
     *   [EN] Clears comments in a GsC value recursively.
     * </summary>
     * <param name="Valeur">
     *   [FR] La valeur GsC dont les commentaires doivent être effacés.<br/>
     *   [EN] The GsC value whose comments should be cleared.
     * </param>
     **/
		private static void EffacerCommentairesDansValeur(ValeurGsC Valeur) {

      if(Valeur == null) {

        return;
      }

      switch(Valeur.Type) {

        case TypeValeurGsC.Objet:

          Valeur.EnObjet().Effacer(Effacement: Effacement.Commentaires);
          break;

        case TypeValeurGsC.Liste:

          foreach(ValeurGsC valeur in Valeur.EnListe()) {

            EffacerCommentairesDansValeur(Valeur: valeur);
          }
          break;

        case TypeValeurGsC.Tableau:

          foreach(ValeurGsC valeur in Valeur.EnTableau()) {

            EffacerCommentairesDansValeur(Valeur: valeur);
          }
          break;
      }
    }

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de cet objet GsC.<br/>
     *   [EN] Creates a deep copy of this GsC object.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ObjetGsC"/> qui est une copie en profondeur de cet objet.<br/>
     *   [EN] A new instance of <see cref="ObjetGsC"/> that is a deep copy of this object.
     * </returns>
     **/
		public ObjetGsC CloneEnProfondeur() => new(ObjetGsCInstance: this);
  }
}
