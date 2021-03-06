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
    /// There are no comments for PubliceringSingle in the schema.
    /// </summary>
    public partial class PubliceringSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Publicering>
    {
        /// <summary>
        /// Initialize a new PubliceringSingle object.
        /// </summary>
        public PubliceringSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new PubliceringSingle object.
        /// </summary>
        public PubliceringSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new PubliceringSingle object.
        /// </summary>
        public PubliceringSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Publicering> query)
            : base(query) {}

        /// <summary>
        /// There are no comments for esas_ansoegningskortopsaetning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.AnsoegningskortOpsaetningSingle esas_ansoegningskortopsaetning
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._esas_ansoegningskortopsaetning == null))
                {
                    this._esas_ansoegningskortopsaetning = new global::esas.Dynamics.Models.Contracts.AnsoegningskortOpsaetningSingle(this.Context, GetPath("esas_ansoegningskortopsaetning"));
                }
                return this._esas_ansoegningskortopsaetning;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.AnsoegningskortOpsaetningSingle _esas_ansoegningskortopsaetning;
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
        /// There are no comments for list_esas_planlaegningsuddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> list_esas_planlaegningsuddannelseselement
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._list_esas_planlaegningsuddannelseselement == null))
                {
                    this._list_esas_planlaegningsuddannelseselement = Context.CreateQuery<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement>(GetPath("list_esas_planlaegningsuddannelseselement"));
                }
                return this._list_esas_planlaegningsuddannelseselement;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> _list_esas_planlaegningsuddannelseselement;
    }
    /// <summary>
    /// There are no comments for Publicering in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_publiceringid
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_publiceringid")]
    public partial class Publicering : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property esas_publiceringid in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_publiceringid
        {
            get
            {
                return this._esas_publiceringid;
            }
            set
            {
                this.Onesas_publiceringidChanging(value);
                this._esas_publiceringid = value;
                this.Onesas_publiceringidChanged();
                this.OnPropertyChanged("esas_publiceringid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_publiceringid;
        partial void Onesas_publiceringidChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_publiceringidChanged();
        /// <summary>
        /// There are no comments for Property esas_ansoegningskortopsaetning_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_ansoegningskortopsaetning_id
        {
            get
            {
                return this._esas_ansoegningskortopsaetning_id;
            }
            set
            {
                this.Onesas_ansoegningskortopsaetning_idChanging(value);
                this._esas_ansoegningskortopsaetning_id = value;
                this.Onesas_ansoegningskortopsaetning_idChanged();
                this.OnPropertyChanged("esas_ansoegningskortopsaetning_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_ansoegningskortopsaetning_id;
        partial void Onesas_ansoegningskortopsaetning_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_ansoegningskortopsaetning_idChanged();
        /// <summary>
        /// There are no comments for Property esas_aktiver_ansoegningssynkronisering in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_aktiver_ansoegningssynkronisering
        {
            get
            {
                return this._esas_aktiver_ansoegningssynkronisering;
            }
            set
            {
                this.Onesas_aktiver_ansoegningssynkroniseringChanging(value);
                this._esas_aktiver_ansoegningssynkronisering = value;
                this.Onesas_aktiver_ansoegningssynkroniseringChanged();
                this.OnPropertyChanged("esas_aktiver_ansoegningssynkronisering");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_aktiver_ansoegningssynkronisering;
        partial void Onesas_aktiver_ansoegningssynkroniseringChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_aktiver_ansoegningssynkroniseringChanged();
        /// <summary>
        /// There are no comments for Property esas_ansoegningssynkronisering_aktiv in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_ansoegningssynkronisering_aktiv
        {
            get
            {
                return this._esas_ansoegningssynkronisering_aktiv;
            }
            set
            {
                this.Onesas_ansoegningssynkronisering_aktivChanging(value);
                this._esas_ansoegningssynkronisering_aktiv = value;
                this.Onesas_ansoegningssynkronisering_aktivChanged();
                this.OnPropertyChanged("esas_ansoegningssynkronisering_aktiv");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_ansoegningssynkronisering_aktiv;
        partial void Onesas_ansoegningssynkronisering_aktivChanging(global::System.Nullable<bool> value);
        partial void Onesas_ansoegningssynkronisering_aktivChanged();
        /// <summary>
        /// There are no comments for Property esas_beskeder_tilladt_indtil in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_beskeder_tilladt_indtil
        {
            get
            {
                return this._esas_beskeder_tilladt_indtil;
            }
            set
            {
                this.Onesas_beskeder_tilladt_indtilChanging(value);
                this._esas_beskeder_tilladt_indtil = value;
                this.Onesas_beskeder_tilladt_indtilChanged();
                this.OnPropertyChanged("esas_beskeder_tilladt_indtil");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_beskeder_tilladt_indtil;
        partial void Onesas_beskeder_tilladt_indtilChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_beskeder_tilladt_indtilChanged();
        /// <summary>
        /// There are no comments for Property esas_bilagsupload_tilladt_indtil in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_bilagsupload_tilladt_indtil
        {
            get
            {
                return this._esas_bilagsupload_tilladt_indtil;
            }
            set
            {
                this.Onesas_bilagsupload_tilladt_indtilChanging(value);
                this._esas_bilagsupload_tilladt_indtil = value;
                this.Onesas_bilagsupload_tilladt_indtilChanged();
                this.OnPropertyChanged("esas_bilagsupload_tilladt_indtil");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_bilagsupload_tilladt_indtil;
        partial void Onesas_bilagsupload_tilladt_indtilChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_bilagsupload_tilladt_indtilChanged();
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
        /// There are no comments for Property esas_supplerende_oplysninger in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_supplerende_oplysninger
        {
            get
            {
                return this._esas_supplerende_oplysninger;
            }
            set
            {
                this.Onesas_supplerende_oplysningerChanging(value);
                this._esas_supplerende_oplysninger = value;
                this.Onesas_supplerende_oplysningerChanged();
                this.OnPropertyChanged("esas_supplerende_oplysninger");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_supplerende_oplysninger;
        partial void Onesas_supplerende_oplysningerChanging(string value);
        partial void Onesas_supplerende_oplysningerChanged();
        /// <summary>
        /// There are no comments for Property esas_publiceringsmuligheder in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_publiceringsmuligheder
        {
            get
            {
                return this._esas_publiceringsmuligheder;
            }
            set
            {
                this.Onesas_publiceringsmulighederChanging(value);
                this._esas_publiceringsmuligheder = value;
                this.Onesas_publiceringsmulighederChanged();
                this.OnPropertyChanged("esas_publiceringsmuligheder");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_publiceringsmuligheder;
        partial void Onesas_publiceringsmulighederChanging(string value);
        partial void Onesas_publiceringsmulighederChanged();
        /// <summary>
        /// There are no comments for Property esas_publiceringsmulighed_institutionsspecifik in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_publiceringsmulighed_institutionsspecifik
        {
            get
            {
                return this._esas_publiceringsmulighed_institutionsspecifik;
            }
            set
            {
                this.Onesas_publiceringsmulighed_institutionsspecifikChanging(value);
                this._esas_publiceringsmulighed_institutionsspecifik = value;
                this.Onesas_publiceringsmulighed_institutionsspecifikChanged();
                this.OnPropertyChanged("esas_publiceringsmulighed_institutionsspecifik");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_publiceringsmulighed_institutionsspecifik;
        partial void Onesas_publiceringsmulighed_institutionsspecifikChanging(string value);
        partial void Onesas_publiceringsmulighed_institutionsspecifikChanged();
        /// <summary>
        /// There are no comments for Property esas_publiceringsperiode_fra in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_publiceringsperiode_fra
        {
            get
            {
                return this._esas_publiceringsperiode_fra;
            }
            set
            {
                this.Onesas_publiceringsperiode_fraChanging(value);
                this._esas_publiceringsperiode_fra = value;
                this.Onesas_publiceringsperiode_fraChanged();
                this.OnPropertyChanged("esas_publiceringsperiode_fra");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_publiceringsperiode_fra;
        partial void Onesas_publiceringsperiode_fraChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_publiceringsperiode_fraChanged();
        /// <summary>
        /// There are no comments for Property esas_publiceringsperiode_til in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_publiceringsperiode_til
        {
            get
            {
                return this._esas_publiceringsperiode_til;
            }
            set
            {
                this.Onesas_publiceringsperiode_tilChanging(value);
                this._esas_publiceringsperiode_til = value;
                this.Onesas_publiceringsperiode_tilChanged();
                this.OnPropertyChanged("esas_publiceringsperiode_til");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_publiceringsperiode_til;
        partial void Onesas_publiceringsperiode_tilChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_publiceringsperiode_tilChanged();
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
        /// There are no comments for Property esas_ansoegningskortopsaetning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.AnsoegningskortOpsaetning esas_ansoegningskortopsaetning
        {
            get
            {
                return this._esas_ansoegningskortopsaetning;
            }
            set
            {
                this.Onesas_ansoegningskortopsaetningChanging(value);
                this._esas_ansoegningskortopsaetning = value;
                this.Onesas_ansoegningskortopsaetningChanged();
                this.OnPropertyChanged("esas_ansoegningskortopsaetning");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.AnsoegningskortOpsaetning _esas_ansoegningskortopsaetning;
        partial void Onesas_ansoegningskortopsaetningChanging(global::esas.Dynamics.Models.Contracts.AnsoegningskortOpsaetning value);
        partial void Onesas_ansoegningskortopsaetningChanged();
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
        /// There are no comments for Property list_esas_planlaegningsuddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> list_esas_planlaegningsuddannelseselement
        {
            get
            {
                return this._list_esas_planlaegningsuddannelseselement;
            }
            set
            {
                this.Onlist_esas_planlaegningsuddannelseselementChanging(value);
                this._list_esas_planlaegningsuddannelseselement = value;
                this.Onlist_esas_planlaegningsuddannelseselementChanged();
                this.OnPropertyChanged("list_esas_planlaegningsuddannelseselement");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> _list_esas_planlaegningsuddannelseselement = new global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement>(null, global::Microsoft.OData.Client.TrackingMode.None);
        partial void Onlist_esas_planlaegningsuddannelseselementChanging(global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.PlanlaegningsUddannelseselement> value);
        partial void Onlist_esas_planlaegningsuddannelseselementChanged();
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
