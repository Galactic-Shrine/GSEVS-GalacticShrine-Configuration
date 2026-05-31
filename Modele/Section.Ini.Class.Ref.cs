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
using GalacticShrine.Configuration.Properties;
using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.Ini {

  /**
   * <summary>
   *   [FR] Informations associées à une propriété provenant d'une Configuration INI.<br/>
   *   [EN] Information associated with a property from an INI Configuration.
   * </summary>
   **/
  public class Section : ClonableInterface<Section> {

		/**
     * <summary>
     *   [FR] Le nom de la section.<br/>
     *   [EN] The section name.
     * </summary>
     **/
		private string Noms;

    /**
     * <summary>
     *   [FR] Liste des lignes de commentaires associées à cette propriété.<br/>
     *   [EN] List of comment lines associated with this property.
     * </summary>
     **/
    private List<string> Commentaires;

		/**
     * <summary>
     *   [FR] Comparateur d'égalité pour la recherche de propriétés dans cette section.<br/>
     *   [EN] Equality comparer for searching properties in this section.
     * </summary>
     **/
		private readonly IEqualityComparer<string> RechercherComparer;

    /**
     * <summary>
     *   [FR] Obtient ou définit le nom de la section.<br/>
     *   [EN] Gets or sets the section name.
     * </summary>
     * <value>
     *   [FR] Le nom de la section<br/>
     *   [EN] Section name
     * </value>
     **/
    public string Nom {

      get => Noms;
    
      set {

        if(!string.IsNullOrEmpty(value: value))
          Noms = value;
      }
    }

    /**
     * <summary>
     *   [FR] Obtient ou définit la liste de commentaires associée à cette section.<br/>
     *   [EN] Gets or sets the list of comments associated with this section.
     * </summary>
     * <value>
     *   [FR] Une liste de chaînes.<br/>
     *   [EN] A list of strings.
     * </value>
     **/
    public List<string> Commentaire {

      get {

        Commentaires ??= new List<string>();
        return Commentaires;
      }

      set {

        Commentaires ??= new List<string>();
        Commentaires.Clear();
        Commentaires.AddRange(value);
      }
    }

    /**
     * <summary>
     *   [FR] Obtient ou définit les propriétés associées à cette section.<br/>
     *   [EN] Gets or sets properties associated with this section.
     * </summary>
     * <value>
     *   [FR] Une collection d'objets de propriété.<br/>
     *   [EN] A collection of Property objects.
     * </value>
     **/
    public ProprieteCollection Proprietes { get; set; }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class.
     * </summary>
     **/
    public Section(string Nom) : this(Nom: Nom, RechercherComparer: EqualityComparer<string>.Default) { }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class.
     * </summary>
     **/
    public Section(string Nom, IEqualityComparer<string> RechercherComparer) {

      this.RechercherComparer = RechercherComparer;

      if(string.IsNullOrEmpty(value: Nom)) {

        throw new ArgumentException(message: Resources.LeNomDeLaSectionNePeutPasEtreVide, paramName: nameof(Nom));
      }

      Proprietes = new ProprieteCollection(RechercherComparer: RechercherComparer);
      this.Nom = Nom;
    }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> à partir de l'instance précédente.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class from its previous instance.
     * </summary>
     * <remarks>
     *   [FR] Les données sont copiées en profondeur<br/>
     *   [EN] Data is copied in depth
     * </remarks>
     * <param name="SectionInstance">
     *   [FR] L'instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> utilisée pour créer la nouvelle instance.<br/>
     *   [EN] The instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class used to create the new instance.
     * </param>
     * <param name="RechercherComparer">
     *   [FR] Rechercher et comparer.<br/>
     *   [EN] Search comparer.
     * </param>
     **/
    public Section(Section SectionInstance, IEqualityComparer<string> RechercherComparer = null) {

      Nom = SectionInstance.Nom;
      Commentaire = SectionInstance.Commentaire;
      Proprietes = new ProprieteCollection(ProprieteCollectionInstance: SectionInstance.Proprietes, RechercherComparer: RechercherComparer ?? SectionInstance.RechercherComparer);
      this.RechercherComparer = RechercherComparer;
    }

        /**
     * <summary>
     *   [FR] Efface les commentaires et/ou les propriétés de cette section INI selon le mode demandé.<br/>
     *   [EN] Clears comments and/or properties from this INI section according to the requested mode.
     * </summary>
     * <remarks>
     *   [FR] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/> efface le commentaire de la section et les commentaires des propriétés.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/> supprime les propriétés, mais conserve le commentaire de la section.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/> supprime le commentaire de la section et les propriétés.</para>
     *   [EN] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/> clears the section comment and property comments.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/> removes properties while keeping the section comment.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/> removes the section comment and properties.</para>
     * </remarks>
     * <param name="Effacement">
     *   [FR] Mode d'effacement à appliquer. La valeur par défaut est <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/>.<br/>
     *   [EN] Clearing mode to apply. The default value is <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/>.
     * </param>
     **/

    public void Effacer(Effacement Effacement = Effacement.Tout) {

      switch (Effacement) {

        case Effacement.Commentaires:

          Commentaire.Clear();
          Proprietes.Effacer(Effacement: Effacement.Commentaires);
          break;

        case Effacement.Proprietes:

          Proprietes.Effacer();
          break;

        case Effacement.Tout:

          Proprietes.Effacer();
          Proprietes.Effacer(Effacement: Effacement.Commentaires);
          Commentaire.Clear();
          break;
      }
    }

    /**
     * <summary>
     *   [FR] Fusionne d'autres sections dans celle-ci,<br/>
     *        en ajoutant de nouvelles propriétés si elles n'existaient pas,<br/>
     *        ou en remplaçant des valeurs si les propriétés existaient déjà.<br/>
     *   [EN] Merges other sections into this one,<br/>
     *        adding new properties if they didn't exist,<br/>
     *        or replacing values if the properties already existed.
     * </summary>
     * <param name="FusionnerLaSection"></param>
     * <remarks>
     *   [FR] Les commentaires sont également fusionnés, mais ils sont toujours ajoutés et ne sont pas écrasés.
     *   [EN] Comments are also merged, but are still added and not overwritten.
     * </remarks>
     **/
    public void Fusionner(Section FusionnerLaSection) {

      Proprietes.Fusionner(ProprieteA_Fusionner: FusionnerLaSection.Proprietes);

      foreach(var Commentaires in FusionnerLaSection.Commentaire)
        Commentaire.Add(item: Commentaires);
    }

    /**
     * <summary>
     *   [FR] Crée un nouvel objet qui est une copie de l'instance actuelle.<br/>
     *   [EN] Creates a new object that is a copy of the current instance.
     * </summary>
     * <remarks>
     *   [FR] Un nouvel objet qui est une copie de cette instance.<br/>
     *   [EN] A new object that is a copy of this instance.
     * </remarks>
     **/
    public Section CloneEnProfondeur() => new (SectionInstance: this);
  }
}
