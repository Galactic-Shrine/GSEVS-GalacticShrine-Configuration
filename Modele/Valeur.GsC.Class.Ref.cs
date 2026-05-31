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
using GalacticShrine.Enumeration.Configuration.GsC;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.GsC {

  /**
   * <summary>
   *   [FR] Représente une valeur typée dans une configuration GsC.<br/>
   *   [EN] Represents a typed value in a GsC configuration.
   * </summary>
   **/
  public class ValeurGsC : ClonableInterface<ValeurGsC> {

		/**
     * <summary>
     *   [FR] Le type de la valeur GsC.<br/>
     *   [EN] The type of the GsC value.
     * </summary>
     **/
		public TypeValeurGsC Type { get; }

		/**
     * <summary>
     *   [FR] La valeur typée de la configuration GsC.<br/>
     *   [EN] The typed value of the GsC configuration.
     * </summary>
     **/
		public object Valeur { get; }

		/**
     * <summary>
     *   [FR] Représente une valeur GsC nulle, c'est-à-dire une valeur sans type et sans valeur.<br/>
     *   [EN] Represents a null GsC value, meaning a value with no type and no value.
     * </summary>
     **/
		public static ValeurGsC Nul => new(TypeValeurGsC.Nul, null);

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="ValeurGsC"/> avec un type et une valeur spécifiés.<br/>
     *   [EN] Initializes a new instance of the <see cref="ValeurGsC"/> class with a specified type and value.
     * </summary>
     * <param name="Type">
     *   [FR] Le type de la valeur GsC.<br/>
     *   [EN] The type of the GsC value.
     * </param>
     * <param name="Valeur">
     *   [FR] La valeur typée de la configuration GsC.<br/>
     *   [EN] The typed value of the GsC configuration.
     * </param>
     **/
		public ValeurGsC(TypeValeurGsC Type, object Valeur) {

      this.Type = Type;
      this.Valeur = Valeur;
    }

		/**
     * <summary>
     *   [FR] Obtient la valeur de la configuration GsC convertie en chaîne de caractères.<br/>
     *   [EN] Gets the value of the GsC configuration converted to a string.
     * </summary>
     **/
		public string Chaine => EnChaine();

		/**
     * <summary>
     *   [FR] Obtient la valeur de la configuration GsC convertie en entier.<br/>
     *   [EN] Gets the value of the GsC configuration converted to an integer.
     * </summary>
     **/
		public long Entier => EnEntier();

		/**
     * <summary>
     *   [FR] Obtient la valeur de la configuration GsC convertie en nombre décimal.<br/>
     *   [EN] Gets the value of the GsC configuration converted to a decimal number.
     * </summary>
     **/
		public decimal Decimal => EnDecimal();

		/**
     * <summary>
     *   [FR] Obtient la valeur de la configuration GsC convertie en booléen.<br/>
     *   [EN] Gets the value of the GsC configuration converted to a boolean.
     * </summary>
     **/
		public bool Booleen => EnBooleen();

		/**
     * <summary>
     *   [FR] Obtient la valeur de la configuration GsC convertie en liste de valeurs GsC.<br/>
     *   [EN] Gets the value of the GsC configuration converted to a list of GsC values.
     * </summary>
     **/
		public ListeGsC Liste => EnListe();

		/**
     * <summary>
     *   [FR] Obtient la valeur de la configuration GsC convertie en tableau de valeurs GsC.<br/>
     *   [EN] Gets the value of the GsC configuration converted to an array of GsC values.
     * </summary>
     **/
		public TableauGsC Tableau => EnTableau();

		/**
     * <summary>
     *   [FR] Obtient la valeur de la configuration GsC convertie en objet de propriétés GsC.<br/>
     *   [EN] Gets the value of the GsC configuration converted to an object of GsC properties.
     * </summary>
     **/
		public ObjetGsC Objet => EnObjet();

		/**
     * <summary>
     *   [FR] Permet d'accéder à une valeur GsC contenue dans un objet de propriétés GsC en utilisant une clé.<br/>
     *   [EN] Allows access to a GsC value contained in an object of GsC properties using a key.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de la valeur GsC à accéder dans l'objet de propriétés GsC.<br/>
     *   [EN] The key of the GsC value to access in the object of GsC properties.
     * </param>
     * <returns>
     *   [FR] La valeur GsC associée à la clé spécifiée dans l'objet de propriétés GsC.<br/>
     *   [EN] The GsC value associated with the specified key in the object of GsC properties.
     * </returns>
     * <exception cref="InvalidOperationException">
     *   [FR] Levée lorsque la valeur GsC n'est pas de type objet, ce qui rend l'indexation avec une clé invalide.<br/>
     *   [EN] Thrown when the GsC value is not of type object, making indexing with a key invalid.
     * </exception>
     **/
		public ValeurGsC this[string Cle] {

      get {

        if(Type != TypeValeurGsC.Objet) {

          throw new InvalidOperationException(
            message: string.Format(format: "La valeur GsC de type '{0}' ne peut pas être indexée avec une clé.", arg0: Type)
          );
        }

        return EnObjet()[Cle];
      }

      set {

        if(Type != TypeValeurGsC.Objet) {

          throw new InvalidOperationException(
            message: string.Format(format: "La valeur GsC de type '{0}' ne peut pas être indexée avec une clé.", arg0: Type)
          );
        }

        EnObjet()[Cle] = value;
      }
    }

		/**
     * <summary>
     *   [FR] Permet d'accéder à une valeur GsC contenue dans une liste ou un tableau de valeurs GsC en utilisant un index entier.<br/>
     *   [EN] Allows access to a GsC value contained in a list or array of GsC values using an integer index.
     * </summary>
     * <param name="Index">
     *   [FR] L'index de la valeur GsC à accéder dans la liste ou le tableau de valeurs GsC.<br/>
     *   [EN] The index of the GsC value to access in the list or array of GsC values.
     * </param>
     * <returns>
     *   [FR] La valeur GsC associée à l'index spécifié dans la liste ou le tableau de valeurs GsC.<br/>
     *   [EN] The GsC value associated with the specified index in the list or array of GsC values.
     * </returns>
     * <exception cref="InvalidOperationException">
     *   [FR] Levée lorsque la valeur GsC n'est pas de type liste ou tableau, ce qui rend l'indexation avec un entier invalide.<br/>
     *   [EN] Thrown when the GsC value is not of type list or array, making indexing with an integer invalid.
     * </exception>
     **/
		public ValeurGsC this[int Index] {

      get {

        return Type switch {

          TypeValeurGsC.Liste => EnListe()[Index],
          TypeValeurGsC.Tableau => EnTableau()[Index],
          _ => throw new InvalidOperationException(
            message: string.Format(format: "La valeur GsC de type '{0}' ne peut pas être indexée avec un entier.", arg0: Type)
          )
        };
      }

      set {

        switch(Type) {

          case TypeValeurGsC.Liste:
            EnListe()[Index] = value;
            return;

          case TypeValeurGsC.Tableau:
            EnTableau()[Index] = value;
            return;

          default:
            throw new InvalidOperationException(
              message: string.Format(format: "La valeur GsC de type '{0}' ne peut pas être indexée avec un entier.", arg0: Type)
            );
        }
      }
    }

		/**
     * <summary>
     *   [FR] Convertit la valeur de la configuration GsC en chaîne de caractères.<br/>
     *   [EN] Converts the value of the GsC configuration to a string.
     * </summary>
     * <returns>
     *   [FR] La valeur de la configuration GsC convertie en chaîne de caractères.<br/>
     *   [EN] The value of the GsC configuration converted to a string.
     * </returns>
     **/
		public string EnChaine() => Valeur as string ?? string.Empty;

		/**
     * <summary>
     *   [FR] Convertit la valeur de la configuration GsC en entier.<br/>
     *   [EN] Converts the value of the GsC configuration to an integer.
     * </summary>
     * <returns>
     *   [FR] La valeur de la configuration GsC convertie en entier.<br/>
     *   [EN] The value of the GsC configuration converted to an integer.
     * </returns>
     **/
		public long EnEntier() => Convert.ToInt64(value: Valeur, provider: System.Globalization.CultureInfo.InvariantCulture);

		/**
     * <summary>
     *   [FR] Convertit la valeur de la configuration GsC en nombre décimal.<br/>
     *   [EN] Converts the value of the GsC configuration to a decimal number.
     * </summary>
     * <returns>
     *   [FR] La valeur de la configuration GsC convertie en nombre décimal.<br/>
     *   [EN] The value of the GsC configuration converted to a decimal number.
     * </returns>
     **/
		public decimal EnDecimal() => Convert.ToDecimal(value: Valeur, provider: System.Globalization.CultureInfo.InvariantCulture);

		/**
     * <summary>
     *   [FR] Convertit la valeur de la configuration GsC en booléen.<br/>
     *   [EN] Converts the value of the GsC configuration to a boolean.
     * </summary>
     * <returns>
     *   [FR] La valeur de la configuration GsC convertie en booléen.<br/>
     *   [EN] The value of the GsC configuration converted to a boolean.
     * </returns>
     **/
		public bool EnBooleen() => Convert.ToBoolean(value: Valeur, provider: System.Globalization.CultureInfo.InvariantCulture);

		/**
     * <summary>
     *   [FR] Convertit la valeur de la configuration GsC en liste de valeurs GsC.<br/>
     *   [EN] Converts the value of the GsC configuration to a list of GsC values.
     * </summary>
     * <returns>
     *   [FR] La valeur de la configuration GsC convertie en liste de valeurs GsC.<br/>
     *   [EN] The value of the GsC configuration converted to a list of GsC values.
     * </returns>
     **/
		public ListeGsC EnListe() => (ListeGsC)Valeur;

		/**
     * <summary>
     *   [FR] Convertit la valeur de la configuration GsC en tableau de valeurs GsC.<br/>
     *   [EN] Converts the value of the GsC configuration to an array of GsC values.
     * </summary>
     * <returns>
     *   [FR] La valeur de la configuration GsC convertie en tableau de valeurs GsC.<br/>
     *   [EN] The value of the GsC configuration converted to an array of GsC values.
     * </returns>
     **/
		public TableauGsC EnTableau() => (TableauGsC)Valeur;

		/**
     * <summary>
     *   [FR] Convertit la valeur de la configuration GsC en objet de propriétés GsC.<br/>
     *   [EN] Converts the value of the GsC configuration to an object of GsC properties.
     * </summary>
     * <returns>
     *   [FR] La valeur de la configuration GsC convertie en objet de propriétés GsC.<br/>
     *   [EN] The value of the GsC configuration converted to an object of GsC properties.
     * </returns>
     **/
		public ObjetGsC EnObjet() => (ObjetGsC)Valeur;

		/**
     * <summary>
     *   [FR] Crée une nouvelle instance de <see cref="ValeurGsC"/> de type chaîne avec la valeur spécifiée.<br/>
     *   [EN] Creates a new instance of <see cref="ValeurGsC"/> of type string with the specified value.
     * </summary>
     * <param name="Valeur">
     *   [FR] La valeur de type chaîne à encapsuler dans l'instance de <see cref="ValeurGsC"/>.<br/>
     *   [EN] The string value to encapsulate in the instance of <see cref="ValeurGsC"/>.
     * </param>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ValeurGsC"/> de type chaîne contenant la valeur spécifiée.<br/>
     *   [EN] A new instance of <see cref="ValeurGsC"/> of type string containing the specified value.
     * </returns>
     **/
		public static ValeurGsC DepuisChaine(string Valeur) => new(TypeValeurGsC.Chaine, Valeur);

		/**
     * <summary>
     *   [FR] Crée une nouvelle instance de <see cref="ValeurGsC"/> de type entier avec la valeur spécifiée.<br/>
     *   [EN] Creates a new instance of <see cref="ValeurGsC"/> of type integer with the specified value.
     * </summary>
     * <param name="Valeur">
     *   [FR] La valeur de type entier à encapsuler dans l'instance de <see cref="ValeurGsC"/>.<br/>
     *   [EN] The integer value to encapsulate in the instance of <see cref="ValeurGsC"/>.
     * </param>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ValeurGsC"/> de type entier contenant la valeur spécifiée.<br/>
     *   [EN] A new instance of <see cref="ValeurGsC"/> of type integer containing the specified value.
     * </returns>
     **/
		public static ValeurGsC DepuisEntier(long Valeur) => new(TypeValeurGsC.Entier, Valeur);

		/**
     * <summary>
     *   [FR] Crée une nouvelle instance de <see cref="ValeurGsC"/> de type décimal avec la valeur spécifiée.<br/>
     *   [EN] Creates a new instance of <see cref="ValeurGsC"/> of type decimal with the specified value.
     * </summary>
     * <param name="Valeur">
     *   [FR] La valeur de type décimal à encapsuler dans l'instance de <see cref="ValeurGsC"/>.<br/>
     *   [EN] The decimal value to encapsulate in the instance of <see cref="ValeurGsC"/>.
     * </param>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ValeurGsC"/> de type décimal contenant la valeur spécifiée.<br/>
     *   [EN] A new instance of <see cref="ValeurGsC"/> of type decimal containing the specified value.
     * </returns>
     **/
		public static ValeurGsC DepuisDecimal(decimal Valeur) => new(TypeValeurGsC.Decimal, Valeur);

		/**
     * <summary>
     *   [FR] Crée une nouvelle instance de <see cref="ValeurGsC"/> de type booléen avec la valeur spécifiée.<br/>
     *   [EN] Creates a new instance of <see cref="ValeurGsC"/> of type boolean with the specified value.
     * </summary>
     * <param name="Valeur">
     *   [FR] La valeur de type booléen à encapsuler dans l'instance de <see cref="ValeurGsC"/>.<br/>
     *   [EN] The boolean value to encapsulate in the instance of <see cref="ValeurGsC"/>.
     * </param>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ValeurGsC"/> de type booléen contenant la valeur spécifiée.<br/>
     *   [EN] A new instance of <see cref="ValeurGsC"/> of type boolean containing the specified value.
     * </returns>
     **/
		public static ValeurGsC DepuisBooleen(bool Valeur) => new(TypeValeurGsC.Booleen, Valeur);

		/**
     * <summary>
     *   [FR] Crée une nouvelle instance de <see cref="ValeurGsC"/> de type liste avec la valeur spécifiée.<br/>
     *   [EN] Creates a new instance of <see cref="ValeurGsC"/> of type list with the specified value.
     * </summary>
     * <param name="Valeur">
     *   [FR] La valeur de type liste à encapsuler dans l'instance de <see cref="ValeurGsC"/>.<br/>
     *   [EN] The list value to encapsulate in the instance of <see cref="ValeurGsC"/>.
     * </param>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ValeurGsC"/> de type liste contenant la valeur spécifiée.<br/>
     *   [EN] A new instance of <see cref="ValeurGsC"/> of type list containing the specified value.
     * </returns>
     **/
		public static ValeurGsC DepuisListe(ListeGsC Valeur) => new(TypeValeurGsC.Liste, Valeur);

		/**
     * <summary>
     *   [FR] Crée une nouvelle instance de <see cref="ValeurGsC"/> de type tableau avec la valeur spécifiée.<br/>
     *   [EN] Creates a new instance of <see cref="ValeurGsC"/> of type array with the specified value.
     * </summary>
     * <param name="Valeur">
     *   [FR] La valeur de type tableau à encapsuler dans l'instance de <see cref="ValeurGsC"/>.<br/>
     *   [EN] The array value to encapsulate in the instance of <see cref="ValeurGsC"/>.
     * </param>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ValeurGsC"/> de type tableau contenant la valeur spécifiée.<br/>
     *   [EN] A new instance of <see cref="ValeurGsC"/> of type array containing the specified value.
     * </returns>
     **/
		public static ValeurGsC DepuisTableau(TableauGsC Valeur) => new(TypeValeurGsC.Tableau, Valeur);

		/**
     * <summary>
     *   [FR] Crée une nouvelle instance de <see cref="ValeurGsC"/> de type objet avec la valeur spécifiée.<br/>
     *   [EN] Creates a new instance of <see cref="ValeurGsC"/> of type object with the specified value.
     * </summary>
     * <param name="Valeur">
     *   [FR] La valeur de type objet à encapsuler dans l'instance de <see cref="ValeurGsC"/>.<br/>
     *   [EN] The object value to encapsulate in the instance of <see cref="ValeurGsC"/>.
     * </param>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ValeurGsC"/> de type objet contenant la valeur spécifiée.<br/>
     *   [EN] A new instance of <see cref="ValeurGsC"/> of type object containing the specified value.
     * </returns>
     **/
		public static ValeurGsC DepuisObjet(ObjetGsC Valeur) => new(TypeValeurGsC.Objet, Valeur);

		/**
     * <summary>
     *   [FR] Crée une copie en profondeur de l'instance actuelle de <see cref="ValeurGsC"/>.<br/>
     *   [EN] Creates a deep copy of the current instance of <see cref="ValeurGsC"/>.
     * </summary>
     * <returns>
     *   [FR] Une nouvelle instance de <see cref="ValeurGsC"/> qui est une copie en profondeur de l'instance actuelle.<br/>
     *   [EN] A new instance of <see cref="ValeurGsC"/> that is a deep copy of the current instance.
     * </returns>
     **/
		public ValeurGsC CloneEnProfondeur() {

      return Type switch {

        TypeValeurGsC.Liste => DepuisListe(EnListe().CloneEnProfondeur()),
        TypeValeurGsC.Tableau => DepuisTableau(EnTableau().CloneEnProfondeur()),
        TypeValeurGsC.Objet => DepuisObjet(EnObjet().CloneEnProfondeur()),
        _ => new ValeurGsC(Type: Type, Valeur: Valeur)
      };
    }
  }
}
