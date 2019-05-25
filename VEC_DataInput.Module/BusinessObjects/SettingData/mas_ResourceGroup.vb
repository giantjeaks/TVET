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
<XafDisplayName("หมวดหมู่ทรัพยากร")>
Public Class mas_ResourceGroup ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    End Sub


    Private _ResourceGroup_ID As String
    <XafDisplayName("รหัสหมวดหมู่ทรัพยากร"), Index(0)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property ResourceGroup_ID As String
        Get
            Return _ResourceGroup_ID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ResourceGroup_ID", _ResourceGroup_ID, value)
        End Set
    End Property

    Private _ResourceGroup_Name As String
    <XafDisplayName("ชื่อหมวดหมู่ทรัพยากร"), Index(1)>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), VisibleInLookupListView(True), RuleUniqueValue(DefaultContexts.Save, CustomMessageTemplate:="หมวดหมู่ทรัพยากรนี้มีอยู่ในระบบแล้ว"), Size(200)>
    Public Property ResourceGroup_Name As String
        Get
            Return _ResourceGroup_Name
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ResourceGroup_Name", _ResourceGroup_Name, value)
        End Set
    End Property

    <XafDisplayName("ประเภททรัพยากร"), ImmediatePostData()>
    <Association("Res-ResTypes", GetType(mas_ResourceType))>
    Public ReadOnly Property ResourceTypes() As XPCollection
        Get
            Return GetCollection("ResourceTypes")
        End Get
    End Property
End Class
