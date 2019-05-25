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
<XafDisplayName("สาขาวิชา"), XafDefaultProperty("DispayName")>
Public Class mas_major ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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

    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(True)>
    Public ReadOnly Property DispayName() As String
        Get
            Return majorId & " : " & majorName & " หลักสูตรปี " & majorYear
        End Get
    End Property


    Private _DeegreeLevelName As String
    <XafDisplayName("ระดับชั้น"), ImmediatePostData()>
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property DeegreeLevelName() As String
        Get
            Return _DeegreeLevelName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DeegreeLevelName", _DeegreeLevelName, value)

        End Set
    End Property

    Private _subjectTypeId_Oid As mas_SubjectType
    <Association("SubjectTypes-majors")>
    <XafDisplayName("ประเภทวิชา"), ImmediatePostData()>
    Public Property ref_mas_subjectType() As mas_SubjectType
        Get
            Return _subjectTypeId_Oid
        End Get
        Set(ByVal value As mas_SubjectType)
            SetPropertyValue("ref_mas_subjectType", _subjectTypeId_Oid, value)

        End Set
    End Property

    Private privatemajorId As String
    <XafDisplayName("รหัสสาขาวิชา"), Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True), Size(4), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property majorId() As String
        Get
            Return privatemajorId
        End Get
        Set(ByVal value As String)
            SetPropertyValue("majorId", privatemajorId, value)
        End Set
    End Property

    Private _majorName As String
    <XafDisplayName("ชื่อสาขาวิชา"), Index(1), Size(200), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property majorName() As String
        Get
            Return _majorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("majorName", _majorName, value)
        End Set
    End Property


    Private _majorYear As String
    <XafDisplayName("ปีที่เปิดหลักสูตร")>
    <Size(4), VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property majorYear As String
        Get
            Return _majorYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("majorYear", _majorYear, value)
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

    <XafDisplayName("สาขางาน"), ImmediatePostData()>
    <Association("majors-minors", GetType(mas_minor))>
    Public ReadOnly Property minors() As XPCollection
        Get
            Return GetCollection("minors")
        End Get
    End Property


End Class
