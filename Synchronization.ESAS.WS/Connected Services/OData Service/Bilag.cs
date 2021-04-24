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
    /// There are no comments for BilagSingle in the schema.
    /// </summary>
    public partial class BilagSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Bilag>
    {
        /// <summary>
        /// Initialize a new BilagSingle object.
        /// </summary>
        public BilagSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new BilagSingle object.
        /// </summary>
        public BilagSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new BilagSingle object.
        /// </summary>
        public BilagSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Bilag> query)
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
    /// There are no comments for Bilag in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_bilagid
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_bilagid")]
    public partial class Bilag : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property esas_bilagid in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_bilagid
        {
            get
            {
                return this._esas_bilagid;
            }
            set
            {
                this.Onesas_bilagidChanging(value);
                this._esas_bilagid = value;
                this.Onesas_bilagidChanged();
                this.OnPropertyChanged("esas_bilagid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_bilagid;
        partial void Onesas_bilagidChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_bilagidChanged();
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
        /// There are no comments for Property esas_original_filnavn in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_original_filnavn
        {
            get
            {
                return this._esas_original_filnavn;
            }
            set
            {
                this.Onesas_original_filnavnChanging(value);
                this._esas_original_filnavn = value;
                this.Onesas_original_filnavnChanged();
                this.OnPropertyChanged("esas_original_filnavn");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_original_filnavn;
        partial void Onesas_original_filnavnChanging(string value);
        partial void Onesas_original_filnavnChanged();
        /// <summary>
        /// There are no comments for Property esas_fil_url in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_fil_url
        {
            get
            {
                return this._esas_fil_url;
            }
            set
            {
                this.Onesas_fil_urlChanging(value);
                this._esas_fil_url = value;
                this.Onesas_fil_urlChanged();
                this.OnPropertyChanged("esas_fil_url");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_fil_url;
        partial void Onesas_fil_urlChanging(string value);
        partial void Onesas_fil_urlChanged();
        /// <summary>
        /// There are no comments for Property esas_fil_content_type in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_fil_content_type
        {
            get
            {
                return this._esas_fil_content_type;
            }
            set
            {
                this.Onesas_fil_content_typeChanging(value);
                this._esas_fil_content_type = value;
                this.Onesas_fil_content_typeChanged();
                this.OnPropertyChanged("esas_fil_content_type");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_fil_content_type;
        partial void Onesas_fil_content_typeChanging(string value);
        partial void Onesas_fil_content_typeChanged();
        /// <summary>
        /// There are no comments for Property esas_sidst_hentet in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_sidst_hentet
        {
            get
            {
                return this._esas_sidst_hentet;
            }
            set
            {
                this.Onesas_sidst_hentetChanging(value);
                this._esas_sidst_hentet = value;
                this.Onesas_sidst_hentetChanged();
                this.OnPropertyChanged("esas_sidst_hentet");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_sidst_hentet;
        partial void Onesas_sidst_hentetChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_sidst_hentetChanged();
        /// <summary>
        /// There are no comments for Property esas_bilagskategorier in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_bilagskategorier
        {
            get
            {
                return this._esas_bilagskategorier;
            }
            set
            {
                this.Onesas_bilagskategorierChanging(value);
                this._esas_bilagskategorier = value;
                this.Onesas_bilagskategorierChanged();
                this.OnPropertyChanged("esas_bilagskategorier");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_bilagskategorier;
        partial void Onesas_bilagskategorierChanging(string value);
        partial void Onesas_bilagskategorierChanged();
        /// <summary>
        /// There are no comments for Property esas_laest in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_laest
        {
            get
            {
                return this._esas_laest;
            }
            set
            {
                this.Onesas_laestChanging(value);
                this._esas_laest = value;
                this.Onesas_laestChanged();
                this.OnPropertyChanged("esas_laest");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_laest;
        partial void Onesas_laestChanging(global::System.Nullable<bool> value);
        partial void Onesas_laestChanged();
        /// <summary>
        /// There are no comments for Property esas_optagelse_dk_fil_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_optagelse_dk_fil_id
        {
            get
            {
                return this._esas_optagelse_dk_fil_id;
            }
            set
            {
                this.Onesas_optagelse_dk_fil_idChanging(value);
                this._esas_optagelse_dk_fil_id = value;
                this.Onesas_optagelse_dk_fil_idChanged();
                this.OnPropertyChanged("esas_optagelse_dk_fil_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_optagelse_dk_fil_id;
        partial void Onesas_optagelse_dk_fil_idChanging(string value);
        partial void Onesas_optagelse_dk_fil_idChanged();
        /// <summary>
        /// There are no comments for Property esas_filstoerrelse_mb in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<double> esas_filstoerrelse_mb
        {
            get
            {
                return this._esas_filstoerrelse_mb;
            }
            set
            {
                this.Onesas_filstoerrelse_mbChanging(value);
                this._esas_filstoerrelse_mb = value;
                this.Onesas_filstoerrelse_mbChanged();
                this.OnPropertyChanged("esas_filstoerrelse_mb");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<double> _esas_filstoerrelse_mb;
        partial void Onesas_filstoerrelse_mbChanging(global::System.Nullable<double> value);
        partial void Onesas_filstoerrelse_mbChanged();
        /// <summary>
        /// There are no comments for Property esas_upload_dato in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_upload_dato
        {
            get
            {
                return this._esas_upload_dato;
            }
            set
            {
                this.Onesas_upload_datoChanging(value);
                this._esas_upload_dato = value;
                this.Onesas_upload_datoChanged();
                this.OnPropertyChanged("esas_upload_dato");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_upload_dato;
        partial void Onesas_upload_datoChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_upload_datoChanged();
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
