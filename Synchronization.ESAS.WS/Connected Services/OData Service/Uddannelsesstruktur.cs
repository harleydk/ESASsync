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
    /// There are no comments for UddannelsesstrukturSingle in the schema.
    /// </summary>
    public partial class UddannelsesstrukturSingle : global::Microsoft.OData.Client.DataServiceQuerySingle<Uddannelsesstruktur>
    {
        /// <summary>
        /// Initialize a new UddannelsesstrukturSingle object.
        /// </summary>
        public UddannelsesstrukturSingle(global::Microsoft.OData.Client.DataServiceContext context, string path)
            : base(context, path) {}

        /// <summary>
        /// Initialize a new UddannelsesstrukturSingle object.
        /// </summary>
        public UddannelsesstrukturSingle(global::Microsoft.OData.Client.DataServiceContext context, string path, bool isComposable)
            : base(context, path, isComposable) {}

        /// <summary>
        /// Initialize a new UddannelsesstrukturSingle object.
        /// </summary>
        public UddannelsesstrukturSingle(global::Microsoft.OData.Client.DataServiceQuerySingle<Uddannelsesstruktur> query)
            : base(query) {}

        /// <summary>
        /// There are no comments for esas_uddannelsesaktivitet in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.UddannelsesaktivitetSingle esas_uddannelsesaktivitet
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._esas_uddannelsesaktivitet == null))
                {
                    this._esas_uddannelsesaktivitet = new global::esas.Dynamics.Models.Contracts.UddannelsesaktivitetSingle(this.Context, GetPath("esas_uddannelsesaktivitet"));
                }
                return this._esas_uddannelsesaktivitet;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.UddannelsesaktivitetSingle _esas_uddannelsesaktivitet;
        /// <summary>
        /// There are no comments for list_esas_studieforloeb in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.Studieforloeb> list_esas_studieforloeb
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._list_esas_studieforloeb == null))
                {
                    this._list_esas_studieforloeb = Context.CreateQuery<global::esas.Dynamics.Models.Contracts.Studieforloeb>(GetPath("list_esas_studieforloeb"));
                }
                return this._list_esas_studieforloeb;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.Studieforloeb> _list_esas_studieforloeb;
        /// <summary>
        /// There are no comments for list_esas_uddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> list_esas_uddannelseselement
        {
            get
            {
                if (!this.IsComposable)
                {
                    throw new global::System.NotSupportedException("The previous function is not composable.");
                }
                if ((this._list_esas_uddannelseselement == null))
                {
                    this._list_esas_uddannelseselement = Context.CreateQuery<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement>(GetPath("list_esas_uddannelseselement"));
                }
                return this._list_esas_uddannelseselement;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceQuery<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> _list_esas_uddannelseselement;
    }
    /// <summary>
    /// There are no comments for Uddannelsesstruktur in the schema.
    /// </summary>
    /// <KeyProperties>
    /// esas_uddannelsesstrukturId
    /// </KeyProperties>
    [global::Microsoft.OData.Client.Key("esas_uddannelsesstrukturId")]
    public partial class Uddannelsesstruktur : global::Microsoft.OData.Client.BaseEntityType, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// There are no comments for Property esas_uddannelsesstrukturId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_uddannelsesstrukturId
        {
            get
            {
                return this._esas_uddannelsesstrukturId;
            }
            set
            {
                this.Onesas_uddannelsesstrukturIdChanging(value);
                this._esas_uddannelsesstrukturId = value;
                this.Onesas_uddannelsesstrukturIdChanged();
                this.OnPropertyChanged("esas_uddannelsesstrukturId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_uddannelsesstrukturId;
        partial void Onesas_uddannelsesstrukturIdChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_uddannelsesstrukturIdChanged();
        /// <summary>
        /// There are no comments for Property esas_uddannelsesaktivitet_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_uddannelsesaktivitet_id
        {
            get
            {
                return this._esas_uddannelsesaktivitet_id;
            }
            set
            {
                this.Onesas_uddannelsesaktivitet_idChanging(value);
                this._esas_uddannelsesaktivitet_id = value;
                this.Onesas_uddannelsesaktivitet_idChanged();
                this.OnPropertyChanged("esas_uddannelsesaktivitet_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_uddannelsesaktivitet_id;
        partial void Onesas_uddannelsesaktivitet_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_uddannelsesaktivitet_idChanged();
        /// <summary>
        /// There are no comments for Property esas_antal_dage_foer_periodes_start in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> esas_antal_dage_foer_periodes_start
        {
            get
            {
                return this._esas_antal_dage_foer_periodes_start;
            }
            set
            {
                this.Onesas_antal_dage_foer_periodes_startChanging(value);
                this._esas_antal_dage_foer_periodes_start = value;
                this.Onesas_antal_dage_foer_periodes_startChanged();
                this.OnPropertyChanged("esas_antal_dage_foer_periodes_start");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _esas_antal_dage_foer_periodes_start;
        partial void Onesas_antal_dage_foer_periodes_startChanging(global::System.Nullable<int> value);
        partial void Onesas_antal_dage_foer_periodes_startChanged();
        /// <summary>
        /// There are no comments for Property esas_fejlbesked_fra_inrule in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_fejlbesked_fra_inrule
        {
            get
            {
                return this._esas_fejlbesked_fra_inrule;
            }
            set
            {
                this.Onesas_fejlbesked_fra_inruleChanging(value);
                this._esas_fejlbesked_fra_inrule = value;
                this.Onesas_fejlbesked_fra_inruleChanged();
                this.OnPropertyChanged("esas_fejlbesked_fra_inrule");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_fejlbesked_fra_inrule;
        partial void Onesas_fejlbesked_fra_inruleChanging(string value);
        partial void Onesas_fejlbesked_fra_inruleChanged();
        /// <summary>
        /// There are no comments for Property esas_field_of_study in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_field_of_study
        {
            get
            {
                return this._esas_field_of_study;
            }
            set
            {
                this.Onesas_field_of_studyChanging(value);
                this._esas_field_of_study = value;
                this.Onesas_field_of_studyChanged();
                this.OnPropertyChanged("esas_field_of_study");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_field_of_study;
        partial void Onesas_field_of_studyChanging(string value);
        partial void Onesas_field_of_studyChanged();
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
        /// There are no comments for Property esas_regeltjek_koert in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.DateTimeOffset> esas_regeltjek_koert
        {
            get
            {
                return this._esas_regeltjek_koert;
            }
            set
            {
                this.Onesas_regeltjek_koertChanging(value);
                this._esas_regeltjek_koert = value;
                this.Onesas_regeltjek_koertChanged();
                this.OnPropertyChanged("esas_regeltjek_koert");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.DateTimeOffset> _esas_regeltjek_koert;
        partial void Onesas_regeltjek_koertChanging(global::System.Nullable<global::System.DateTimeOffset> value);
        partial void Onesas_regeltjek_koertChanged();
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
        /// There are no comments for Property esas_institutionsafdeling_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_institutionsafdeling_id
        {
            get
            {
                return this._esas_institutionsafdeling_id;
            }
            set
            {
                this.Onesas_institutionsafdeling_idChanging(value);
                this._esas_institutionsafdeling_id = value;
                this.Onesas_institutionsafdeling_idChanged();
                this.OnPropertyChanged("esas_institutionsafdeling_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_institutionsafdeling_id;
        partial void Onesas_institutionsafdeling_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_institutionsafdeling_idChanged();
        /// <summary>
        /// There are no comments for Property esas_audd_kode_id in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<global::System.Guid> esas_audd_kode_id
        {
            get
            {
                return this._esas_audd_kode_id;
            }
            set
            {
                this.Onesas_audd_kode_idChanging(value);
                this._esas_audd_kode_id = value;
                this.Onesas_audd_kode_idChanged();
                this.OnPropertyChanged("esas_audd_kode_id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<global::System.Guid> _esas_audd_kode_id;
        partial void Onesas_audd_kode_idChanging(global::System.Nullable<global::System.Guid> value);
        partial void Onesas_audd_kode_idChanged();
        /// <summary>
        /// There are no comments for Property esas_studieretning in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_studieretning
        {
            get
            {
                return this._esas_studieretning;
            }
            set
            {
                this.Onesas_studieretningChanging(value);
                this._esas_studieretning = value;
                this.Onesas_studieretningChanged();
                this.OnPropertyChanged("esas_studieretning");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_studieretning;
        partial void Onesas_studieretningChanging(string value);
        partial void Onesas_studieretningChanged();
        /// <summary>
        /// There are no comments for Property esas_tjekstatus in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> esas_tjekstatus
        {
            get
            {
                return this._esas_tjekstatus;
            }
            set
            {
                this.Onesas_tjekstatusChanging(value);
                this._esas_tjekstatus = value;
                this.Onesas_tjekstatusChanged();
                this.OnPropertyChanged("esas_tjekstatus");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _esas_tjekstatus;
        partial void Onesas_tjekstatusChanging(global::System.Nullable<int> value);
        partial void Onesas_tjekstatusChanged();
        /// <summary>
        /// There are no comments for Property esas_tjekstatusbeskrivelse in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_tjekstatusbeskrivelse
        {
            get
            {
                return this._esas_tjekstatusbeskrivelse;
            }
            set
            {
                this.Onesas_tjekstatusbeskrivelseChanging(value);
                this._esas_tjekstatusbeskrivelse = value;
                this.Onesas_tjekstatusbeskrivelseChanged();
                this.OnPropertyChanged("esas_tjekstatusbeskrivelse");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_tjekstatusbeskrivelse;
        partial void Onesas_tjekstatusbeskrivelseChanging(string value);
        partial void Onesas_tjekstatusbeskrivelseChanged();
        /// <summary>
        /// There are no comments for Property esas_uddannelsestype in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::System.Nullable<int> esas_uddannelsestype
        {
            get
            {
                return this._esas_uddannelsestype;
            }
            set
            {
                this.Onesas_uddannelsestypeChanging(value);
                this._esas_uddannelsestype = value;
                this.Onesas_uddannelsestypeChanged();
                this.OnPropertyChanged("esas_uddannelsestype");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::System.Nullable<int> _esas_uddannelsestype;
        partial void Onesas_uddannelsestypeChanging(global::System.Nullable<int> value);
        partial void Onesas_uddannelsestypeChanged();
        /// <summary>
        /// There are no comments for Property esas_uddannelsens_hjemmeside_link in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual string esas_uddannelsens_hjemmeside_link
        {
            get
            {
                return this._esas_uddannelsens_hjemmeside_link;
            }
            set
            {
                this.Onesas_uddannelsens_hjemmeside_linkChanging(value);
                this._esas_uddannelsens_hjemmeside_link = value;
                this.Onesas_uddannelsens_hjemmeside_linkChanged();
                this.OnPropertyChanged("esas_uddannelsens_hjemmeside_link");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private string _esas_uddannelsens_hjemmeside_link;
        partial void Onesas_uddannelsens_hjemmeside_linkChanging(string value);
        partial void Onesas_uddannelsens_hjemmeside_linkChanged();
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
        /// There are no comments for Property esas_uddannelsesaktivitet in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::esas.Dynamics.Models.Contracts.Uddannelsesaktivitet esas_uddannelsesaktivitet
        {
            get
            {
                return this._esas_uddannelsesaktivitet;
            }
            set
            {
                this.Onesas_uddannelsesaktivitetChanging(value);
                this._esas_uddannelsesaktivitet = value;
                this.Onesas_uddannelsesaktivitetChanged();
                this.OnPropertyChanged("esas_uddannelsesaktivitet");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::esas.Dynamics.Models.Contracts.Uddannelsesaktivitet _esas_uddannelsesaktivitet;
        partial void Onesas_uddannelsesaktivitetChanging(global::esas.Dynamics.Models.Contracts.Uddannelsesaktivitet value);
        partial void Onesas_uddannelsesaktivitetChanged();
        /// <summary>
        /// There are no comments for Property list_esas_studieforloeb in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Studieforloeb> list_esas_studieforloeb
        {
            get
            {
                return this._list_esas_studieforloeb;
            }
            set
            {
                this.Onlist_esas_studieforloebChanging(value);
                this._list_esas_studieforloeb = value;
                this.Onlist_esas_studieforloebChanged();
                this.OnPropertyChanged("list_esas_studieforloeb");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Studieforloeb> _list_esas_studieforloeb = new global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Studieforloeb>(null, global::Microsoft.OData.Client.TrackingMode.None);
        partial void Onlist_esas_studieforloebChanging(global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.Studieforloeb> value);
        partial void Onlist_esas_studieforloebChanged();
        /// <summary>
        /// There are no comments for Property list_esas_uddannelseselement in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        public virtual global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> list_esas_uddannelseselement
        {
            get
            {
                return this._list_esas_uddannelseselement;
            }
            set
            {
                this.Onlist_esas_uddannelseselementChanging(value);
                this._list_esas_uddannelseselement = value;
                this.Onlist_esas_uddannelseselementChanged();
                this.OnPropertyChanged("list_esas_uddannelseselement");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.OData.Client.Design.T4", "#VersionNumber#")]
        private global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> _list_esas_uddannelseselement = new global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement>(null, global::Microsoft.OData.Client.TrackingMode.None);
        partial void Onlist_esas_uddannelseselementChanging(global::Microsoft.OData.Client.DataServiceCollection<global::esas.Dynamics.Models.Contracts.StruktureltUddannelseselement> value);
        partial void Onlist_esas_uddannelseselementChanged();
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