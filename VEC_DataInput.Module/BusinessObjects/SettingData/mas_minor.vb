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
<XafDisplayName("สาขางาน")>
Public Class mas_minor ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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
    Private _major_Oid As mas_major
    <Association("majors-minors")>
    <XafDisplayName("สาขาวิชา"), ImmediatePostData()>
    Public Property ref_mas_major() As mas_major
        Get
            Return _major_Oid
        End Get
        Set(ByVal value As mas_major)
            SetPropertyValue("ref_mas_major", _major_Oid, value)

        End Set
    End Property

    Private privateminorId As String
    <XafDisplayName("รหัสสาขางาน"), Index(0), VisibleInListView(True), VisibleInDetailView(True), Size(6), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property minorId() As String
        Get
            Return privateminorId
        End Get
        Set(ByVal value As String)
            SetPropertyValue("minorId", privateminorId, value)
        End Set
    End Property
    Private _minorName As String
    <XafDisplayName("ชื่อสาขางาน"), Index(1), Size(200), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property minorName() As String
        Get
            Return _minorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("minorName", _minorName, value)
        End Set
    End Property


    Private _specialType_Oid As mas_specialIndustryTypes
    <XafDisplayName("ประเภทอุตสาหกรรมพิเศษ"), Index(3), ImmediatePostData()>
    <Association("specialIndustryTypes-minor")>
    Public Property ref_Mas_SpecialIndustryTypes() As mas_specialIndustryTypes
        Get
            Return _specialType_Oid
        End Get
        Set(ByVal value As mas_specialIndustryTypes)
            SetPropertyValue("ref_Mas_SpecialIndustryTypes", _specialType_Oid, value)
        End Set
    End Property

    Private _LastUpdated As DateTime
    <XafDisplayName("วันที่ Update ล่าสุด"), VisibleInListView(False), VisibleInDetailView(False)>
    Public Property LastUpdated() As DateTime
        Get
            Return _LastUpdated
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("LastUpdated", _LastUpdated, value)
        End Set
    End Property
End Class
