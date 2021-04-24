﻿//------------------------------------------------------------------------------
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
    /// There are no comments for RekvirenttypeSingle in the schema.
    /// </summary>
    public partial class RekvirenttypeSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Rekvirenttype>
    {
        /// <summary>
        /// Initialize a new RekvirenttypeSingle object.
        /// </summary>
        public RekvirenttypeSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new RekvirenttypeSingle object.
        /// </summary>
        public RekvirenttypeSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new RekvirenttypeSingle object.
        /// </summary>
        public RekvirenttypeSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Rekvirenttype> query)
            : base(query) {}

        /// <summary>
        /// There are no comments for list_esas_ansoegning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.Ansoegning> list_esas_ansoegning
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._list_esas_ansoegning == null))
                {
                    this._list_esas_ansoegning = Context.CreateQuery<global::esas.Dynamics.Models.Contracts.Ansoegning>(GetPath("list_esas_ansoegning"));
                }
                return this._list_esas_ansoegning;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.Ansoegning> _list_esas_ansoegning;
    }
    /// <summary>
    /// There are no comments for Rekvirenttype in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_rekvirenttypeId
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_rekvirenttypeId")]
    public partial class Rekvirenttype : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new Rekvirenttype object.
        /// </summary>
        /// <param name="esas_ressourceudloesende">Initial value of esas_ressourceudloesende.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public static Rekvirenttype CreateRekvirenttype(bool esas_ressourceudloesende)
        {
            Rekvirenttype rekvirenttype = new Rekvirenttype();
            rekvirenttype.esas_ressourceudloesende = esas_ressourceudloesende;
            return rekvirenttype;
        }
        /// <summary>
        /// There are no comments for Property esas_rekvirenttypeId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_rekvirenttypeId
        {
            get
            {
                return this._esas_rekvirenttypeId;
            }
            set
            {
                this.Onesas_rekvirenttypeIdChanging(value);
                this._esas_rekvirenttypeId = value;
                this.Onesas_rekvirenttypeIdChanged();
                this.OnPropertyChanged("esas_rekvirenttypeId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_rekvirenttypeId;
        partial void Onesas_rekvirenttypeIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_rekvirenttypeIdChanged();
        /// <summary>
        /// There are no comments for Property esas_kategorisering in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_kategorisering
        {
            get
            {
                return this._esas_kategorisering;
            }
            set
            {
                this.Onesas_kategoriseringChanging(value);
                this._esas_kategorisering = value;
                this.Onesas_kategoriseringChanged();
                this.OnPropertyChanged("esas_kategorisering");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_kategorisering;
        partial void Onesas_kategoriseringChanging(string value);
        partial void Onesas_kategoriseringChanged();
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
        /// There are no comments for Property esas_blanketkode in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_blanketkode
        {
            get
            {
                return this._esas_blanketkode;
            }
            set
            {
                this.Onesas_blanketkodeChanging(value);
                this._esas_blanketkode = value;
                this.Onesas_blanketkodeChanged();
                this.OnPropertyChanged("esas_blanketkode");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_blanketkode;
        partial void Onesas_blanketkodeChanging(string value);
        partial void Onesas_blanketkodeChanged();
        /// <summary>
        /// There are no comments for Property esas_udloebsdato in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_udloebsdato
        {
            get
            {
                return this._esas_udloebsdato;
            }
            set
            {
                this.Onesas_udloebsdatoChanging(value);
                this._esas_udloebsdato = value;
                this.Onesas_udloebsdatoChanged();
                this.OnPropertyChanged("esas_udloebsdato");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_udloebsdato;
        partial void Onesas_udloebsdatoChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_udloebsdatoChanged();
        /// <summary>
        /// There are no comments for Property esas_aktoer in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_aktoer
        {
            get
            {
                return this._esas_aktoer;
            }
            set
            {
                this.Onesas_aktoerChanging(value);
                this._esas_aktoer = value;
                this.Onesas_aktoerChanged();
                this.OnPropertyChanged("esas_aktoer");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_aktoer;
        partial void Onesas_aktoerChanging(string value);
        partial void Onesas_aktoerChanged();
        /// <summary>
        /// There are no comments for Property esas_rekvirenttype in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_rekvirenttype
        {
            get
            {
                return this._esas_rekvirenttype;
            }
            set
            {
                this.Onesas_rekvirenttypeChanging(value);
                this._esas_rekvirenttype = value;
                this.Onesas_rekvirenttypeChanged();
                this.OnPropertyChanged("esas_rekvirenttype");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_rekvirenttype;
        partial void Onesas_rekvirenttypeChanging(string value);
        partial void Onesas_rekvirenttypeChanged();
        /// <summary>
        /// There are no comments for Property esas_ressourceudloesende in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual bool esas_ressourceudloesende
        {
            get
            {
                return this._esas_ressourceudloesende;
            }
            set
            {
                this.Onesas_ressourceudloesendeChanging(value);
                this._esas_ressourceudloesende = value;
                this.Onesas_ressourceudloesendeChanged();
                this.OnPropertyChanged("esas_ressourceudloesende");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private bool _esas_ressourceudloesende;
        partial void Onesas_ressourceudloesendeChanging(bool value);
        partial void Onesas_ressourceudloesendeChanged();
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
        /// There are no comments for Property list_esas_ansoegning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Ansoegning> list_esas_ansoegning
        {
            get
            {
                return this._list_esas_ansoegning;
            }
            set
            {
                this.Onlist_esas_ansoegningChanging(value);
                this._list_esas_ansoegning = value;
                this.Onlist_esas_ansoegningChanged();
                this.OnPropertyChanged("list_esas_ansoegning");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Ansoegning> _list_esas_ansoegning = new global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Ansoegning>(null, global::Microsoft.OData.Client.TrackingMode.None);
        partial void Onlist_esas_ansoegningChanging(global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Ansoegning> value);
        partial void Onlist_esas_ansoegningChanged();
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