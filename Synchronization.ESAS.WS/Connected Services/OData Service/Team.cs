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
    /// There are no comments for TeamSingle in the schema.
    /// </summary>
    public partial class TeamSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Team>
    {
        /// <summary>
        /// Initialize a new TeamSingle object.
        /// </summary>
        public TeamSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new TeamSingle object.
        /// </summary>
        public TeamSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new TeamSingle object.
        /// </summary>
        public TeamSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Team> query)
            : base(query) {}

    }
    /// <summary>
    /// There are no comments for Team in the schema.
    /// </summary>
    /// <KeyProperties>
    /// TeamId
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("TeamId")]
    public partial class Team : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property TeamId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> TeamId
        {
            get
            {
                return this._TeamId;
            }
            set
            {
                this.OnTeamIdChanging(value);
                this._TeamId = value;
                this.OnTeamIdChanged();
                this.OnPropertyChanged("TeamId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _TeamId;
        partial void OnTeamIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void OnTeamIdChanged();
        /// <summary>
        /// There are no comments for Property name in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this.OnnameChanging(value);
                this._name = value;
                this.OnnameChanged();
                this.OnPropertyChanged("name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _name;
        partial void OnnameChanging(string value);
        partial void OnnameChanged();
        /// <summary>
        /// There are no comments for Property description in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string description
        {
            get
            {
                return this._description;
            }
            set
            {
                this.OndescriptionChanging(value);
                this._description = value;
                this.OndescriptionChanged();
                this.OnPropertyChanged("description");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _description;
        partial void OndescriptionChanging(string value);
        partial void OndescriptionChanged();
        /// <summary>
        /// There are no comments for Property esas_integrationsteam in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_integrationsteam
        {
            get
            {
                return this._esas_integrationsteam;
            }
            set
            {
                this.Onesas_integrationsteamChanging(value);
                this._esas_integrationsteam = value;
                this.Onesas_integrationsteamChanged();
                this.OnPropertyChanged("esas_integrationsteam");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_integrationsteam;
        partial void Onesas_integrationsteamChanging(global::System.Nullable<bool> value);
        partial void Onesas_integrationsteamChanged();
        /// <summary>
        /// There are no comments for Property esas_standard_esas_team in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<bool> esas_standard_esas_team
        {
            get
            {
                return this._esas_standard_esas_team;
            }
            set
            {
                this.Onesas_standard_esas_teamChanging(value);
                this._esas_standard_esas_team = value;
                this.Onesas_standard_esas_teamChanged();
                this.OnPropertyChanged("esas_standard_esas_team");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<bool> _esas_standard_esas_team;
        partial void Onesas_standard_esas_teamChanging(global::System.Nullable<bool> value);
        partial void Onesas_standard_esas_teamChanged();
        /// <summary>
        /// There are no comments for Property esas_status in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> esas_status
        {
            get
            {
                return this._esas_status;
            }
            set
            {
                this.Onesas_statusChanging(value);
                this._esas_status = value;
                this.Onesas_statusChanged();
                this.OnPropertyChanged("esas_status");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _esas_status;
        partial void Onesas_statusChanging(global::System.Nullable<int> value);
        partial void Onesas_statusChanged();
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
