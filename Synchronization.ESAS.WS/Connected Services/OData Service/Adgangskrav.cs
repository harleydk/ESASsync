//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generation date: 03-03-2021 10:44:52
namespace esas.Dynamics.Models.Contracts
{
    /// <summary>
    /// There are no comments for AdgangskravSingle in the schema.
    /// </summary>
    public partial class AdgangskravSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Adgangskrav>
    {
        /// <summary>
        /// Initialize a new AdgangskravSingle object.
        /// </summary>
        public AdgangskravSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new AdgangskravSingle object.
        /// </summary>
        public AdgangskravSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new AdgangskravSingle object.
        /// </summary>
        public AdgangskravSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Adgangskrav> query)
            : base(query) {}

        /// <summary>
        /// There are no comments for list_esas_omraadenummeropsaetning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.Omraadenummeropsaetning> list_esas_omraadenummeropsaetning
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._list_esas_omraadenummeropsaetning == null))
                {
                    this._list_esas_omraadenummeropsaetning = Context.CreateQuery<global::esas.Dynamics.Models.Contracts.Omraadenummeropsaetning>(GetPath("list_esas_omraadenummeropsaetning"));
                }
                return this._list_esas_omraadenummeropsaetning;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.Omraadenummeropsaetning> _list_esas_omraadenummeropsaetning;
        /// <summary>
        /// There are no comments for list_esas_planlaegningsUddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> list_esas_planlaegningsUddannelseselement
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._list_esas_planlaegningsUddannelseselement == null))
                {
                    this._list_esas_planlaegningsUddannelseselement = Context.CreateQuery<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement>(GetPath("list_esas_planlaegningsUddannelseselement"));
                }
                return this._list_esas_planlaegningsUddannelseselement;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> _list_esas_planlaegningsUddannelseselement;
        /// <summary>
        /// There are no comments for list_esas_struktureltUddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> list_esas_struktureltUddannelseselement
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._list_esas_struktureltUddannelseselement == null))
                {
                    this._list_esas_struktureltUddannelseselement = Context.CreateQuery<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement>(GetPath("list_esas_struktureltUddannelseselement"));
                }
                return this._list_esas_struktureltUddannelseselement;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> _list_esas_struktureltUddannelseselement;
    }
    /// <summary>
    /// There are no comments for Adgangskrav in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_adgangskravId
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_adgangskravId")]
    public partial class Adgangskrav : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property esas_adgangskravId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_adgangskravId
        {
            get
            {
                return this._esas_adgangskravId;
            }
            set
            {
                this.Onesas_adgangskravIdChanging(value);
                this._esas_adgangskravId = value;
                this.Onesas_adgangskravIdChanged();
                this.OnPropertyChanged("esas_adgangskravId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_adgangskravId;
        partial void Onesas_adgangskravIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_adgangskravIdChanged();
        /// <summary>
        /// There are no comments for Property esas_adgangsgrundlag in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_adgangsgrundlag
        {
            get
            {
                return this._esas_adgangsgrundlag;
            }
            set
            {
                this.Onesas_adgangsgrundlagChanging(value);
                this._esas_adgangsgrundlag = value;
                this.Onesas_adgangsgrundlagChanged();
                this.OnPropertyChanged("esas_adgangsgrundlag");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_adgangsgrundlag;
        partial void Onesas_adgangsgrundlagChanging(string value);
        partial void Onesas_adgangsgrundlagChanged();
        /// <summary>
        /// There are no comments for Property esas_saerlige_krav in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_saerlige_krav
        {
            get
            {
                return this._esas_saerlige_krav;
            }
            set
            {
                this.Onesas_saerlige_kravChanging(value);
                this._esas_saerlige_krav = value;
                this.Onesas_saerlige_kravChanged();
                this.OnPropertyChanged("esas_saerlige_krav");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_saerlige_krav;
        partial void Onesas_saerlige_kravChanging(global::System.Nullable<bool> value);
        partial void Onesas_saerlige_kravChanged();
        /// <summary>
        /// There are no comments for Property esas_saerlige_krav_beskrivelse in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_saerlige_krav_beskrivelse
        {
            get
            {
                return this._esas_saerlige_krav_beskrivelse;
            }
            set
            {
                this.Onesas_saerlige_krav_beskrivelseChanging(value);
                this._esas_saerlige_krav_beskrivelse = value;
                this.Onesas_saerlige_krav_beskrivelseChanged();
                this.OnPropertyChanged("esas_saerlige_krav_beskrivelse");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_saerlige_krav_beskrivelse;
        partial void Onesas_saerlige_krav_beskrivelseChanging(string value);
        partial void Onesas_saerlige_krav_beskrivelseChanged();
        /// <summary>
        /// There are no comments for Property esas_dispensation_for_adgangskrav_muligt in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> esas_dispensation_for_adgangskrav_muligt
        {
            get
            {
                return this._esas_dispensation_for_adgangskrav_muligt;
            }
            set
            {
                this.Onesas_dispensation_for_adgangskrav_muligtChanging(value);
                this._esas_dispensation_for_adgangskrav_muligt = value;
                this.Onesas_dispensation_for_adgangskrav_muligtChanged();
                this.OnPropertyChanged("esas_dispensation_for_adgangskrav_muligt");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _esas_dispensation_for_adgangskrav_muligt;
        partial void Onesas_dispensation_for_adgangskrav_muligtChanging(global::System.Nullable<int> value);
        partial void Onesas_dispensation_for_adgangskrav_muligtChanged();
        /// <summary>
        /// There are no comments for Property esas_ansoegning_med_saerlig_tilladelse_muligt in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> esas_ansoegning_med_saerlig_tilladelse_muligt
        {
            get
            {
                return this._esas_ansoegning_med_saerlig_tilladelse_muligt;
            }
            set
            {
                this.Onesas_ansoegning_med_saerlig_tilladelse_muligtChanging(value);
                this._esas_ansoegning_med_saerlig_tilladelse_muligt = value;
                this.Onesas_ansoegning_med_saerlig_tilladelse_muligtChanged();
                this.OnPropertyChanged("esas_ansoegning_med_saerlig_tilladelse_muligt");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _esas_ansoegning_med_saerlig_tilladelse_muligt;
        partial void Onesas_ansoegning_med_saerlig_tilladelse_muligtChanging(global::System.Nullable<int> value);
        partial void Onesas_ansoegning_med_saerlig_tilladelse_muligtChanged();
        /// <summary>
        /// There are no comments for Property esas_navn in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_navn
        {
            get
            {
                return this._esas_navn;
            }
            set
            {
                this.Onesas_navnChanging(value);
                this._esas_navn = value;
                this.Onesas_navnChanged();
                this.OnPropertyChanged("esas_navn");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_navn;
        partial void Onesas_navnChanging(string value);
        partial void Onesas_navnChanged();
        /// <summary>
        /// There are no comments for Property CreatedBy in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> CreatedBy
        {
            get
            {
                return this._CreatedBy;
            }
            set
            {
                this.OnCreatedByChanging(value);
                this._CreatedBy = value;
                this.OnCreatedByChanged();
                this.OnPropertyChanged("CreatedBy");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _CreatedBy;
        partial void OnCreatedByChanging(global::System.Nullable<global::System.Guid> value);
        partial void OnCreatedByChanged();
        /// <summary>
        /// There are no comments for Property CreatedOn in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> CreatedOn
        {
            get
            {
                return this._CreatedOn;
            }
            set
            {
                this.OnCreatedOnChanging(value);
                this._CreatedOn = value;
                this.OnCreatedOnChanged();
                this.OnPropertyChanged("CreatedOn");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _CreatedOn;
        partial void OnCreatedOnChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void OnCreatedOnChanged();
        /// <summary>
        /// There are no comments for Property ModifiedBy in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> ModifiedBy
        {
            get
            {
                return this._ModifiedBy;
            }
            set
            {
                this.OnModifiedByChanging(value);
                this._ModifiedBy = value;
                this.OnModifiedByChanged();
                this.OnPropertyChanged("ModifiedBy");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _ModifiedBy;
        partial void OnModifiedByChanging(global::System.Nullable<global::System.Guid> value);
        partial void OnModifiedByChanged();
        /// <summary>
        /// There are no comments for Property ModifiedOn in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> ModifiedOn
        {
            get
            {
                return this._ModifiedOn;
            }
            set
            {
                this.OnModifiedOnChanging(value);
                this._ModifiedOn = value;
                this.OnModifiedOnChanged();
                this.OnPropertyChanged("ModifiedOn");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _ModifiedOn;
        partial void OnModifiedOnChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void OnModifiedOnChanged();
        /// <summary>
        /// There are no comments for Property OwningBusinessUnit in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> OwningBusinessUnit
        {
            get
            {
                return this._OwningBusinessUnit;
            }
            set
            {
                this.OnOwningBusinessUnitChanging(value);
                this._OwningBusinessUnit = value;
                this.OnOwningBusinessUnitChanged();
                this.OnPropertyChanged("OwningBusinessUnit");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _OwningBusinessUnit;
        partial void OnOwningBusinessUnitChanging(global::System.Nullable<global::System.Guid> value);
        partial void OnOwningBusinessUnitChanged();
        /// <summary>
        /// There are no comments for Property statecode in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> statecode
        {
            get
            {
                return this._statecode;
            }
            set
            {
                this.OnstatecodeChanging(value);
                this._statecode = value;
                this.OnstatecodeChanged();
                this.OnPropertyChanged("statecode");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _statecode;
        partial void OnstatecodeChanging(global::System.Nullable<int> value);
        partial void OnstatecodeChanged();
        /// <summary>
        /// There are no comments for Property statuscode in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> statuscode
        {
            get
            {
                return this._statuscode;
            }
            set
            {
                this.OnstatuscodeChanging(value);
                this._statuscode = value;
                this.OnstatuscodeChanged();
                this.OnPropertyChanged("statuscode");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _statuscode;
        partial void OnstatuscodeChanging(global::System.Nullable<int> value);
        partial void OnstatuscodeChanged();
        /// <summary>
        /// There are no comments for Property list_esas_omraadenummeropsaetning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Omraadenummeropsaetning> list_esas_omraadenummeropsaetning
        {
            get
            {
                return this._list_esas_omraadenummeropsaetning;
            }
            set
            {
                this.Onlist_esas_omraadenummeropsaetningChanging(value);
                this._list_esas_omraadenummeropsaetning = value;
                this.Onlist_esas_omraadenummeropsaetningChanged();
                this.OnPropertyChanged("list_esas_omraadenummeropsaetning");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Omraadenummeropsaetning> _list_esas_omraadenummeropsaetning = new global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Omraadenummeropsaetning>(null, global::Microsoft.OData.Client.TrackingMode.None);
        partial void Onlist_esas_omraadenummeropsaetningChanging(global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Omraadenummeropsaetning> value);
        partial void Onlist_esas_omraadenummeropsaetningChanged();
        /// <summary>
        /// There are no comments for Property list_esas_planlaegningsUddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> list_esas_planlaegningsUddannelseselement
        {
            get
            {
                return this._list_esas_planlaegningsUddannelseselement;
            }
            set
            {
                this.Onlist_esas_planlaegningsUddannelseselementChanging(value);
                this._list_esas_planlaegningsUddannelseselement = value;
                this.Onlist_esas_planlaegningsUddannelseselementChanged();
                this.OnPropertyChanged("list_esas_planlaegningsUddannelseselement");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> _list_esas_planlaegningsUddannelseselement = new global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement>(null, global::Microsoft.OData.Client.TrackingMode.None);
        partial void Onlist_esas_planlaegningsUddannelseselementChanging(global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> value);
        partial void Onlist_esas_planlaegningsUddannelseselementChanged();
        /// <summary>
        /// There are no comments for Property list_esas_struktureltUddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> list_esas_struktureltUddannelseselement
        {
            get
            {
                return this._list_esas_struktureltUddannelseselement;
            }
            set
            {
                this.Onlist_esas_struktureltUddannelseselementChanging(value);
                this._list_esas_struktureltUddannelseselement = value;
                this.Onlist_esas_struktureltUddannelseselementChanged();
                this.OnPropertyChanged("list_esas_struktureltUddannelseselement");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> _list_esas_struktureltUddannelseselement = new global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement>(null, global::Microsoft.OData.Client.TrackingMode.None);
        partial void Onlist_esas_struktureltUddannelseselementChanging(global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> value);
        partial void Onlist_esas_struktureltUddannelseselementChanged();
        /// <summary>
        /// This event is raised when the value of the property is changed
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The value of the property is changed
        /// </summary>
        /// <param name="property">property name</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
