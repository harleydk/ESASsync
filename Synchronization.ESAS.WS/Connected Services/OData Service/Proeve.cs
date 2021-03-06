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
    /// There are no comments for ProeveSingle in the schema.
    /// </summary>
    public partial class ProeveSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Proeve>
    {
        /// <summary>
        /// Initialize a new ProeveSingle object.
        /// </summary>
        public ProeveSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new ProeveSingle object.
        /// </summary>
        public ProeveSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new ProeveSingle object.
        /// </summary>
        public ProeveSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Proeve> query)
            : base(query) {}

        /// <summary>
        /// There are no comments for esas_ansoegning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.AnsoegningSingle esas_ansoegning
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._esas_ansoegning == null))
                {
                    this._esas_ansoegning = new global::esas.Dynamics.Models.Contracts.AnsoegningSingle(this.Context, GetPath("esas_ansoegning"));
                }
                return this._esas_ansoegning;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.AnsoegningSingle _esas_ansoegning;
    }
    /// <summary>
    /// There are no comments for Proeve in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_ansoegning_proeveid
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_ansoegning_proeveid")]
    public partial class Proeve : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property esas_ansoegning_proeveid in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_ansoegning_proeveid
        {
            get
            {
                return this._esas_ansoegning_proeveid;
            }
            set
            {
                this.Onesas_ansoegning_proeveidChanging(value);
                this._esas_ansoegning_proeveid = value;
                this.Onesas_ansoegning_proeveidChanged();
                this.OnPropertyChanged("esas_ansoegning_proeveid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_ansoegning_proeveid;
        partial void Onesas_ansoegning_proeveidChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_ansoegning_proeveidChanged();
        /// <summary>
        /// There are no comments for Property esas_ansoegning_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_ansoegning_id
        {
            get
            {
                return this._esas_ansoegning_id;
            }
            set
            {
                this.Onesas_ansoegning_idChanging(value);
                this._esas_ansoegning_id = value;
                this.Onesas_ansoegning_idChanged();
                this.OnPropertyChanged("esas_ansoegning_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_ansoegning_id;
        partial void Onesas_ansoegning_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_ansoegning_idChanged();
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
        /// There are no comments for Property esas_type in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_type
        {
            get
            {
                return this._esas_type;
            }
            set
            {
                this.Onesas_typeChanging(value);
                this._esas_type = value;
                this.Onesas_typeChanged();
                this.OnPropertyChanged("esas_type");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_type;
        partial void Onesas_typeChanging(string value);
        partial void Onesas_typeChanged();
        /// <summary>
        /// There are no comments for Property esas_fag in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_fag
        {
            get
            {
                return this._esas_fag;
            }
            set
            {
                this.Onesas_fagChanging(value);
                this._esas_fag = value;
                this.Onesas_fagChanged();
                this.OnPropertyChanged("esas_fag");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_fag;
        partial void Onesas_fagChanging(string value);
        partial void Onesas_fagChanged();
        /// <summary>
        /// There are no comments for Property esas_bestaaet in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_bestaaet
        {
            get
            {
                return this._esas_bestaaet;
            }
            set
            {
                this.Onesas_bestaaetChanging(value);
                this._esas_bestaaet = value;
                this.Onesas_bestaaetChanged();
                this.OnPropertyChanged("esas_bestaaet");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_bestaaet;
        partial void Onesas_bestaaetChanging(string value);
        partial void Onesas_bestaaetChanged();
        /// <summary>
        /// There are no comments for Property esas_bestaaet_aar in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_bestaaet_aar
        {
            get
            {
                return this._esas_bestaaet_aar;
            }
            set
            {
                this.Onesas_bestaaet_aarChanging(value);
                this._esas_bestaaet_aar = value;
                this.Onesas_bestaaet_aarChanged();
                this.OnPropertyChanged("esas_bestaaet_aar");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_bestaaet_aar;
        partial void Onesas_bestaaet_aarChanging(string value);
        partial void Onesas_bestaaet_aarChanged();
        /// <summary>
        /// There are no comments for Property esas_niveau in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_niveau
        {
            get
            {
                return this._esas_niveau;
            }
            set
            {
                this.Onesas_niveauChanging(value);
                this._esas_niveau = value;
                this.Onesas_niveauChanged();
                this.OnPropertyChanged("esas_niveau");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_niveau;
        partial void Onesas_niveauChanging(string value);
        partial void Onesas_niveauChanged();
        /// <summary>
        /// There are no comments for Property esas_skriftlig_karakter in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_skriftlig_karakter
        {
            get
            {
                return this._esas_skriftlig_karakter;
            }
            set
            {
                this.Onesas_skriftlig_karakterChanging(value);
                this._esas_skriftlig_karakter = value;
                this.Onesas_skriftlig_karakterChanged();
                this.OnPropertyChanged("esas_skriftlig_karakter");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_skriftlig_karakter;
        partial void Onesas_skriftlig_karakterChanging(string value);
        partial void Onesas_skriftlig_karakterChanged();
        /// <summary>
        /// There are no comments for Property esas_mundlig_karakter in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_mundlig_karakter
        {
            get
            {
                return this._esas_mundlig_karakter;
            }
            set
            {
                this.Onesas_mundlig_karakterChanging(value);
                this._esas_mundlig_karakter = value;
                this.Onesas_mundlig_karakterChanged();
                this.OnPropertyChanged("esas_mundlig_karakter");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_mundlig_karakter;
        partial void Onesas_mundlig_karakterChanging(string value);
        partial void Onesas_mundlig_karakterChanged();
        /// <summary>
        /// There are no comments for Property esas_from_eksamensdatabasen in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_from_eksamensdatabasen
        {
            get
            {
                return this._esas_from_eksamensdatabasen;
            }
            set
            {
                this.Onesas_from_eksamensdatabasenChanging(value);
                this._esas_from_eksamensdatabasen = value;
                this.Onesas_from_eksamensdatabasenChanged();
                this.OnPropertyChanged("esas_from_eksamensdatabasen");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_from_eksamensdatabasen;
        partial void Onesas_from_eksamensdatabasenChanging(global::System.Nullable<bool> value);
        partial void Onesas_from_eksamensdatabasenChanged();
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
        /// There are no comments for Property esas_ansoegning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.Ansoegning esas_ansoegning
        {
            get
            {
                return this._esas_ansoegning;
            }
            set
            {
                this.Onesas_ansoegningChanging(value);
                this._esas_ansoegning = value;
                this.Onesas_ansoegningChanged();
                this.OnPropertyChanged("esas_ansoegning");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.Ansoegning _esas_ansoegning;
        partial void Onesas_ansoegningChanging(global::esas.Dynamics.Models.Contracts.Ansoegning value);
        partial void Onesas_ansoegningChanged();
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
