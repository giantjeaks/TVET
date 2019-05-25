Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()>
<XafDisplayName("ประเภทอุตสาหกรรมพิเศษ")>
Public Class mas_specialIndustryTypes ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Me.LastUpdated = Date.Now
    End Sub
    Private _IndustryCategoryID_Oid As Mas_SpecialIndustryGroups
    <Association("specialIndustryGroups-specialIndustryTypes")>
    <XafDisplayName("กลุ่มอุตสาหกรรมพิเศษ"), Index(0), ImmediatePostData()>
    Public Property ref_mas_specialIndustryGroups() As Mas_SpecialIndustryGroups
        Get
            Return _IndustryCategoryID_Oid
        End Get
        Set(ByVal value As Mas_SpecialIndustryGroups)
            SetPropertyValue("ref_mas_specialIndustryGroups", _IndustryCategoryID_Oid, value)
        End Set
    End Property

    Private privateIndustryTypeId As String
    <XafDisplayName("รหัสประเภทอุตสาหกรรม"), Index(1)>
    <Size(2), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property IndustryTypeId() As String
        Get
            Return privateIndustryTypeId
        End Get
        Set(ByVal value As String)
            SetPropertyValue("IndustryTypeId", privateIndustryTypeId, value)
        End Set
    End Property
    Private _IndustryTypeName As String
    <XafDisplayName("ชื่อประเภทอุตสาหกรรม"), Index(2)>
    <Size(200), VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property IndustryTypeName() As String
        Get
            Return _IndustryTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("IndustryTypeName", _IndustryTypeName, value)
        End Set
    End Property

    <XafDisplayName("สาขางาน"), ImmediatePostData(), Index(4)>
    <Association("specialIndustryTypes-minor", GetType(mas_minor))>
    Public ReadOnly Property ref_mas_minor() As XPCollection
        Get
            Return GetCollection("ref_mas_minor")
        End Get
    End Property

    Private _LastUpdated As DateTime
    <XafDisplayName("วันที่ Update ล่าสุด")>
    <VisibleInListView(False), VisibleInDetailView(False)>
    Public Property LastUpdated() As DateTime
        Get
            Return _LastUpdated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("LastUpdated", _LastUpdated, value)
        End Set
    End Property



End Class
