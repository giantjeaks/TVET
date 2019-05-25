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
<XafDisplayName("ระดับการศึกษา/หลักสูตร")>
Public Class mas_degreeLevel ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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

    Private privatedegreeLevelId As String
    <XafDisplayName("รหัสระดับการศึกษา")>
    <Index(0)>
    <Size(2), RuleUniqueValue(DefaultContexts.Save)>
    Public Property degreeLevelId() As String
        Get
            Return privatedegreeLevelId
        End Get
        Set(ByVal value As String)
            SetPropertyValue("degreeLevelId", privatedegreeLevelId, value)
        End Set
    End Property

    Private _degreeLevelName As String
    <XafDisplayName("ชื่อระดับการศึกษา"), Index(1)>
    <Size(50), VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property degreeLevelName() As String
        Get
            Return _degreeLevelName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("degreeLevelName", _degreeLevelName, value)
        End Set
    End Property

    Private _LastUpdated As DateTime
    <XafDisplayName("เวลาที่ Update ล่าสุด")>
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
