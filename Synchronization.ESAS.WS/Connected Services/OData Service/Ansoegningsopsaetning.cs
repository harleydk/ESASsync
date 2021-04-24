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
    /// There are no comments for AnsoegningsopsaetningSingle in the schema.
    /// </summary>
    public partial class AnsoegningsopsaetningSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Ansoegningsopsaetning>
    {
        /// <summary>
        /// Initialize a new AnsoegningsopsaetningSingle object.
        /// </summary>
        public AnsoegningsopsaetningSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new AnsoegningsopsaetningSingle object.
        /// </summary>
        public AnsoegningsopsaetningSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new AnsoegningsopsaetningSingle object.
        /// </summary>
        public AnsoegningsopsaetningSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Ansoegningsopsaetning> query)
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
    /// There are no comments for Ansoegningsopsaetning in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_ansoegningsopsaetningId
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_ansoegningsopsaetningId")]
    public partial class Ansoegningsopsaetning : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property esas_ansoegningsopsaetningId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_ansoegningsopsaetningId
        {
            get
            {
                return this._esas_ansoegningsopsaetningId;
            }
            set
            {
                this.Onesas_ansoegningsopsaetningIdChanging(value);
                this._esas_ansoegningsopsaetningId = value;
                this.Onesas_ansoegningsopsaetningIdChanged();
                this.OnPropertyChanged("esas_ansoegningsopsaetningId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_ansoegningsopsaetningId;
        partial void Onesas_ansoegningsopsaetningIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_ansoegningsopsaetningIdChanged();
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
        /// There are no comments for Property esas_optag_dk_optag in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_optag_dk_optag
        {
            get
            {
                return this._esas_optag_dk_optag;
            }
            set
            {
                this.Onesas_optag_dk_optagChanging(value);
                this._esas_optag_dk_optag = value;
                this.Onesas_optag_dk_optagChanged();
                this.OnPropertyChanged("esas_optag_dk_optag");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_optag_dk_optag;
        partial void Onesas_optag_dk_optagChanging(global::System.Nullable<bool> value);
        partial void Onesas_optag_dk_optagChanged();
        /// <summary>
        /// There are no comments for Property esas_optagelsesperiode_slut in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_optagelsesperiode_slut
        {
            get
            {
                return this._esas_optagelsesperiode_slut;
            }
            set
            {
                this.Onesas_optagelsesperiode_slutChanging(value);
                this._esas_optagelsesperiode_slut = value;
                this.Onesas_optagelsesperiode_slutChanged();
                this.OnPropertyChanged("esas_optagelsesperiode_slut");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_optagelsesperiode_slut;
        partial void Onesas_optagelsesperiode_slutChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_optagelsesperiode_slutChanged();
        /// <summary>
        /// There are no comments for Property esas_optagelsesperiode_start in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_optagelsesperiode_start
        {
            get
            {
                return this._esas_optagelsesperiode_start;
            }
            set
            {
                this.Onesas_optagelsesperiode_startChanging(value);
                this._esas_optagelsesperiode_start = value;
                this.Onesas_optagelsesperiode_startChanged();
                this.OnPropertyChanged("esas_optagelsesperiode_start");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_optagelsesperiode_start;
        partial void Onesas_optagelsesperiode_startChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_optagelsesperiode_startChanged();
        /// <summary>
        /// There are no comments for Property ownerid in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> ownerid
        {
            get
            {
                return this._ownerid;
            }
            set
            {
                this.OnowneridChanging(value);
                this._ownerid = value;
                this.OnowneridChanged();
                this.OnPropertyChanged("ownerid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _ownerid;
        partial void OnowneridChanging(global::System.Nullable<global::System.Guid> value);
        partial void OnowneridChanged();
        /// <summary>
        /// There are no comments for Property esas_klargjort in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_klargjort
        {
            get
            {
                return this._esas_klargjort;
            }
            set
            {
                this.Onesas_klargjortChanging(value);
                this._esas_klargjort = value;
                this.Onesas_klargjortChanged();
                this.OnPropertyChanged("esas_klargjort");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_klargjort;
        partial void Onesas_klargjortChanging(global::System.Nullable<bool> value);
        partial void Onesas_klargjortChanged();
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
