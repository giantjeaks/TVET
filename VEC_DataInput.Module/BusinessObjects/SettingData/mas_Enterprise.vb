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
<XafDisplayName("ประเภทวิสาหกิจ")>
Public Class mas_Enterprise ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    End Sub

    Private _Enterprise_id As Integer
    <XafDisplayName("รหัสหมวดหมู่ประเภทวิสาหกิจ"), Index(0)>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property Enterprise_id() As Integer
        Get
            Return _Enterprise_id
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Enterprise_id", _Enterprise_id, value)
        End Set
    End Property

    Private _Enterprise_name As String
    <XafDisplayName("ชื่อหมวดหมู่วิสาหกิจ"), Index(1)>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), VisibleInLookupListView(True), RuleUniqueValue(DefaultContexts.Save), Size(200)>
    Public Property Enterprise_name() As String
        Get
            Return _Enterprise_name
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Enterprise_name", _Enterprise_name, value)
        End Set
    End Property
End Class
