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
<XafDisplayName("ประเภทวิชา")>
Public Class mas_SubjectType ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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

    'Private _deegreeLevelOid As mas_degreeLevel
    '<Association("degreeLevels-SubjectTypes")>
    '<XafDisplayName("ระดับการศึกษา"), ImmediatePostData()>
    'Public Property deegreeLevelOid As mas_degreeLevel
    '    Get
    '        Return _deegreeLevelOid
    '    End Get
    '    Set(value As mas_degreeLevel)
    '        SetPropertyValue("deegreeLevelOid", _deegreeLevelOid, value)
    '    End Set
    'End Property


    Private privatesubjectTypeId As String
    <XafDisplayName("รหัสประเภทวิชา"), Index(0)>
    <Size(2), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property SubjectTypeId() As String
        Get
            Return privatesubjectTypeId
        End Get
        Set(ByVal value As String)
            privatesubjectTypeId = value
        End Set
    End Property

    Private _subjectTypeName As String
    <XafDisplayName("ชื่อประเภทวิชา"), Index(1)>
    <Size(50), VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save, CustomMessageTemplate:="ประเภทวิชานี้มีอยู่ในระบบแล้ว")>
    Public Property subjectTypeName() As String
        Get
            Return _subjectTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("subjectTypeName", _subjectTypeName, value)
        End Set
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

    <XafDisplayName("สาขาวิชา"), ImmediatePostData()>
    <Association("SubjectTypes-majors", GetType(mas_major))>
    Public ReadOnly Property majors() As XPCollection
        Get
            Return GetCollection("majors")
        End Get
    End Property
End Class
