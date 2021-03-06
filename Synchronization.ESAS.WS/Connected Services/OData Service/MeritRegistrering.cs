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
    /// There are no comments for MeritRegistreringSingle in the schema.
    /// </summary>
    public partial class MeritRegistreringSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<MeritRegistrering>
    {
        /// <summary>
        /// Initialize a new MeritRegistreringSingle object.
        /// </summary>
        public MeritRegistreringSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new MeritRegistreringSingle object.
        /// </summary>
        public MeritRegistreringSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new MeritRegistreringSingle object.
        /// </summary>
        public MeritRegistreringSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<MeritRegistrering> query)
            : base(query) {}

        /// <summary>
        /// There are no comments for esas_godkender in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.PersonoplysningSingle esas_godkender
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._esas_godkender == null))
                {
                    this._esas_godkender = new global::esas.Dynamics.Models.Contracts.PersonoplysningSingle(this.Context, GetPath("esas_godkender"));
                }
                return this._esas_godkender;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.PersonoplysningSingle _esas_godkender;
        /// <summary>
        /// There are no comments for esas_gennemfoerelsesuddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.GennemfoerelsesUddannelseselementSingle esas_gennemfoerelsesuddannelseselement
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._esas_gennemfoerelsesuddannelseselement == null))
                {
                    this._esas_gennemfoerelsesuddannelseselement = new global::esas.Dynamics.Models.Contracts.GennemfoerelsesUddannelseselementSingle(this.Context, GetPath("esas_gennemfoerelsesuddannelseselement"));
                }
                return this._esas_gennemfoerelsesuddannelseselement;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.GennemfoerelsesUddannelseselementSingle _esas_gennemfoerelsesuddannelseselement;
        /// <summary>
        /// There are no comments for esas_karakter in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.KarakterSingle esas_karakter
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._esas_karakter == null))
                {
                    this._esas_karakter = new global::esas.Dynamics.Models.Contracts.KarakterSingle(this.Context, GetPath("esas_karakter"));
                }
                return this._esas_karakter;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.KarakterSingle _esas_karakter;
    }
    /// <summary>
    /// There are no comments for MeritRegistrering in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_meritregistreringId
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_meritregistreringId")]
    public partial class MeritRegistrering : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property esas_meritregistreringId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_meritregistreringId
        {
            get
            {
                return this._esas_meritregistreringId;
            }
            set
            {
                this.Onesas_meritregistreringIdChanging(value);
                this._esas_meritregistreringId = value;
                this.Onesas_meritregistreringIdChanged();
                this.OnPropertyChanged("esas_meritregistreringId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_meritregistreringId;
        partial void Onesas_meritregistreringIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_meritregistreringIdChanged();
        /// <summary>
        /// There are no comments for Property esas_godkender_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_godkender_id
        {
            get
            {
                return this._esas_godkender_id;
            }
            set
            {
                this.Onesas_godkender_idChanging(value);
                this._esas_godkender_id = value;
                this.Onesas_godkender_idChanged();
                this.OnPropertyChanged("esas_godkender_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_godkender_id;
        partial void Onesas_godkender_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_godkender_idChanged();
        /// <summary>
        /// There are no comments for Property esas_gennemfoerelsesuddannelseselement_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_gennemfoerelsesuddannelseselement_id
        {
            get
            {
                return this._esas_gennemfoerelsesuddannelseselement_id;
            }
            set
            {
                this.Onesas_gennemfoerelsesuddannelseselement_idChanging(value);
                this._esas_gennemfoerelsesuddannelseselement_id = value;
                this.Onesas_gennemfoerelsesuddannelseselement_idChanged();
                this.OnPropertyChanged("esas_gennemfoerelsesuddannelseselement_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_gennemfoerelsesuddannelseselement_id;
        partial void Onesas_gennemfoerelsesuddannelseselement_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_gennemfoerelsesuddannelseselement_idChanged();
        /// <summary>
        /// There are no comments for Property esas_karakter_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_karakter_id
        {
            get
            {
                return this._esas_karakter_id;
            }
            set
            {
                this.Onesas_karakter_idChanging(value);
                this._esas_karakter_id = value;
                this.Onesas_karakter_idChanged();
                this.OnPropertyChanged("esas_karakter_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_karakter_id;
        partial void Onesas_karakter_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_karakter_idChanged();
        /// <summary>
        /// There are no comments for Property esas_aktivitetstype_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_aktivitetstype_id
        {
            get
            {
                return this._esas_aktivitetstype_id;
            }
            set
            {
                this.Onesas_aktivitetstype_idChanging(value);
                this._esas_aktivitetstype_id = value;
                this.Onesas_aktivitetstype_idChanged();
                this.OnPropertyChanged("esas_aktivitetstype_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_aktivitetstype_id;
        partial void Onesas_aktivitetstype_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_aktivitetstype_idChanged();
        /// <summary>
        /// There are no comments for Property esas_startdato in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_startdato
        {
            get
            {
                return this._esas_startdato;
            }
            set
            {
                this.Onesas_startdatoChanging(value);
                this._esas_startdato = value;
                this.Onesas_startdatoChanged();
                this.OnPropertyChanged("esas_startdato");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_startdato;
        partial void Onesas_startdatoChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_startdatoChanged();
        /// <summary>
        /// There are no comments for Property esas_slutdato in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_slutdato
        {
            get
            {
                return this._esas_slutdato;
            }
            set
            {
                this.Onesas_slutdatoChanging(value);
                this._esas_slutdato = value;
                this.Onesas_slutdatoChanged();
                this.OnPropertyChanged("esas_slutdato");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_slutdato;
        partial void Onesas_slutdatoChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_slutdatoChanged();
        /// <summary>
        /// There are no comments for Property esas_bedoemmelsesdato in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_bedoemmelsesdato
        {
            get
            {
                return this._esas_bedoemmelsesdato;
            }
            set
            {
                this.Onesas_bedoemmelsesdatoChanging(value);
                this._esas_bedoemmelsesdato = value;
                this.Onesas_bedoemmelsesdatoChanged();
                this.OnPropertyChanged("esas_bedoemmelsesdato");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_bedoemmelsesdato;
        partial void Onesas_bedoemmelsesdatoChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_bedoemmelsesdatoChanged();
        /// <summary>
        /// There are no comments for Property esas_fra_institution_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_fra_institution_id
        {
            get
            {
                return this._esas_fra_institution_id;
            }
            set
            {
                this.Onesas_fra_institution_idChanging(value);
                this._esas_fra_institution_id = value;
                this.Onesas_fra_institution_idChanged();
                this.OnPropertyChanged("esas_fra_institution_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_fra_institution_id;
        partial void Onesas_fra_institution_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_fra_institution_idChanged();
        /// <summary>
        /// There are no comments for Property esas_bedoemmelse_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_bedoemmelse_id
        {
            get
            {
                return this._esas_bedoemmelse_id;
            }
            set
            {
                this.Onesas_bedoemmelse_idChanging(value);
                this._esas_bedoemmelse_id = value;
                this.Onesas_bedoemmelse_idChanged();
                this.OnPropertyChanged("esas_bedoemmelse_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_bedoemmelse_id;
        partial void Onesas_bedoemmelse_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_bedoemmelse_idChanged();
        /// <summary>
        /// There are no comments for Property esas_titel_dansk in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_titel_dansk
        {
            get
            {
                return this._esas_titel_dansk;
            }
            set
            {
                this.Onesas_titel_danskChanging(value);
                this._esas_titel_dansk = value;
                this.Onesas_titel_danskChanged();
                this.OnPropertyChanged("esas_titel_dansk");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_titel_dansk;
        partial void Onesas_titel_danskChanging(string value);
        partial void Onesas_titel_danskChanged();
        /// <summary>
        /// There are no comments for Property esas_titel_engelsk in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_titel_engelsk
        {
            get
            {
                return this._esas_titel_engelsk;
            }
            set
            {
                this.Onesas_titel_engelskChanging(value);
                this._esas_titel_engelsk = value;
                this.Onesas_titel_engelskChanged();
                this.OnPropertyChanged("esas_titel_engelsk");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_titel_engelsk;
        partial void Onesas_titel_engelskChanging(string value);
        partial void Onesas_titel_engelskChanged();
        /// <summary>
        /// There are no comments for Property esas_studietidsforkortende in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_studietidsforkortende
        {
            get
            {
                return this._esas_studietidsforkortende;
            }
            set
            {
                this.Onesas_studietidsforkortendeChanging(value);
                this._esas_studietidsforkortende = value;
                this.Onesas_studietidsforkortendeChanged();
                this.OnPropertyChanged("esas_studietidsforkortende");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_studietidsforkortende;
        partial void Onesas_studietidsforkortendeChanging(global::System.Nullable<bool> value);
        partial void Onesas_studietidsforkortendeChanged();
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
        /// There are no comments for Property esas_studieforloeb_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_studieforloeb_id
        {
            get
            {
                return this._esas_studieforloeb_id;
            }
            set
            {
                this.Onesas_studieforloeb_idChanging(value);
                this._esas_studieforloeb_id = value;
                this.Onesas_studieforloeb_idChanged();
                this.OnPropertyChanged("esas_studieforloeb_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_studieforloeb_id;
        partial void Onesas_studieforloeb_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_studieforloeb_idChanged();
        /// <summary>
        /// There are no comments for Property esas_godkendelsesdato in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_godkendelsesdato
        {
            get
            {
                return this._esas_godkendelsesdato;
            }
            set
            {
                this.Onesas_godkendelsesdatoChanging(value);
                this._esas_godkendelsesdato = value;
                this.Onesas_godkendelsesdatoChanged();
                this.OnPropertyChanged("esas_godkendelsesdato");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_godkendelsesdato;
        partial void Onesas_godkendelsesdatoChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_godkendelsesdatoChanged();
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
        /// There are no comments for Property esas_godkender in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.Personoplysning esas_godkender
        {
            get
            {
                return this._esas_godkender;
            }
            set
            {
                this.Onesas_godkenderChanging(value);
                this._esas_godkender = value;
                this.Onesas_godkenderChanged();
                this.OnPropertyChanged("esas_godkender");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.Personoplysning _esas_godkender;
        partial void Onesas_godkenderChanging(global::esas.Dynamics.Models.Contracts.Personoplysning value);
        partial void Onesas_godkenderChanged();
        /// <summary>
        /// There are no comments for Property esas_gennemfoerelsesuddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.GennemfoerelsesUddannelseselement esas_gennemfoerelsesuddannelseselement
        {
            get
            {
                return this._esas_gennemfoerelsesuddannelseselement;
            }
            set
            {
                this.Onesas_gennemfoerelsesuddannelseselementChanging(value);
                this._esas_gennemfoerelsesuddannelseselement = value;
                this.Onesas_gennemfoerelsesuddannelseselementChanged();
                this.OnPropertyChanged("esas_gennemfoerelsesuddannelseselement");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.GennemfoerelsesUddannelseselement _esas_gennemfoerelsesuddannelseselement;
        partial void Onesas_gennemfoerelsesuddannelseselementChanging(global::esas.Dynamics.Models.Contracts.GennemfoerelsesUddannelseselement value);
        partial void Onesas_gennemfoerelsesuddannelseselementChanged();
        /// <summary>
        /// There are no comments for Property esas_karakter in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.Karakter esas_karakter
        {
            get
            {
                return this._esas_karakter;
            }
            set
            {
                this.Onesas_karakterChanging(value);
                this._esas_karakter = value;
                this.Onesas_karakterChanged();
                this.OnPropertyChanged("esas_karakter");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.Karakter _esas_karakter;
        partial void Onesas_karakterChanging(global::esas.Dynamics.Models.Contracts.Karakter value);
        partial void Onesas_karakterChanged();
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
