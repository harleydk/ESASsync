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
    /// There are no comments for BetalingstilsagnSingle in the schema.
    /// </summary>
    public partial class BetalingstilsagnSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Betalingstilsagn>
    {
        /// <summary>
        /// Initialize a new BetalingstilsagnSingle object.
        /// </summary>
        public BetalingstilsagnSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new BetalingstilsagnSingle object.
        /// </summary>
        public BetalingstilsagnSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new BetalingstilsagnSingle object.
        /// </summary>
        public BetalingstilsagnSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Betalingstilsagn> query)
            : base(query) {}

        /// <summary>
        /// There are no comments for esas_faktureringsgrundlag in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.FaktureringsgrundlagSingle esas_faktureringsgrundlag
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._esas_faktureringsgrundlag == null))
                {
                    this._esas_faktureringsgrundlag = new global::esas.Dynamics.Models.Contracts.FaktureringsgrundlagSingle(this.Context, GetPath("esas_faktureringsgrundlag"));
                }
                return this._esas_faktureringsgrundlag;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.FaktureringsgrundlagSingle _esas_faktureringsgrundlag;
    }
    /// <summary>
    /// There are no comments for Betalingstilsagn in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_betalingstilsagnId
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_betalingstilsagnId")]
    public partial class Betalingstilsagn : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property esas_betalingstilsagnId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_betalingstilsagnId
        {
            get
            {
                return this._esas_betalingstilsagnId;
            }
            set
            {
                this.Onesas_betalingstilsagnIdChanging(value);
                this._esas_betalingstilsagnId = value;
                this.Onesas_betalingstilsagnIdChanged();
                this.OnPropertyChanged("esas_betalingstilsagnId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_betalingstilsagnId;
        partial void Onesas_betalingstilsagnIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_betalingstilsagnIdChanged();
        /// <summary>
        /// There are no comments for Property esas_faktureringsgrundlag_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_faktureringsgrundlag_id
        {
            get
            {
                return this._esas_faktureringsgrundlag_id;
            }
            set
            {
                this.Onesas_faktureringsgrundlag_idChanging(value);
                this._esas_faktureringsgrundlag_id = value;
                this.Onesas_faktureringsgrundlag_idChanged();
                this.OnPropertyChanged("esas_faktureringsgrundlag_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_faktureringsgrundlag_id;
        partial void Onesas_faktureringsgrundlag_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_faktureringsgrundlag_idChanged();
        /// <summary>
        /// There are no comments for Property esas_betalingstilsagnsaarsag in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> esas_betalingstilsagnsaarsag
        {
            get
            {
                return this._esas_betalingstilsagnsaarsag;
            }
            set
            {
                this.Onesas_betalingstilsagnsaarsagChanging(value);
                this._esas_betalingstilsagnsaarsag = value;
                this.Onesas_betalingstilsagnsaarsagChanged();
                this.OnPropertyChanged("esas_betalingstilsagnsaarsag");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _esas_betalingstilsagnsaarsag;
        partial void Onesas_betalingstilsagnsaarsagChanging(global::System.Nullable<int> value);
        partial void Onesas_betalingstilsagnsaarsagChanged();
        /// <summary>
        /// There are no comments for Property esas_betalingstilsagnsdato in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_betalingstilsagnsdato
        {
            get
            {
                return this._esas_betalingstilsagnsdato;
            }
            set
            {
                this.Onesas_betalingstilsagnsdatoChanging(value);
                this._esas_betalingstilsagnsdato = value;
                this.Onesas_betalingstilsagnsdatoChanged();
                this.OnPropertyChanged("esas_betalingstilsagnsdato");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_betalingstilsagnsdato;
        partial void Onesas_betalingstilsagnsdatoChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_betalingstilsagnsdatoChanged();
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
        /// There are no comments for Property esas_faktureringsgrundlag in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.Faktureringsgrundlag esas_faktureringsgrundlag
        {
            get
            {
                return this._esas_faktureringsgrundlag;
            }
            set
            {
                this.Onesas_faktureringsgrundlagChanging(value);
                this._esas_faktureringsgrundlag = value;
                this.Onesas_faktureringsgrundlagChanged();
                this.OnPropertyChanged("esas_faktureringsgrundlag");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.Faktureringsgrundlag _esas_faktureringsgrundlag;
        partial void Onesas_faktureringsgrundlagChanging(global::esas.Dynamics.Models.Contracts.Faktureringsgrundlag value);
        partial void Onesas_faktureringsgrundlagChanged();
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
