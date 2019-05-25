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
<XafDisplayName("ประเภททรัพยากร")>
Public Class mas_ResourceType ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    End Sub


    Private _ResourceGroup_Oid As mas_ResourceGroup
    <Association("Res-ResTypes")>
    <XafDisplayName("หมวดหมู่ทรัพยากร"), Index(0), RuleRequiredField(DefaultContexts.Save)>
    Public Property ResourceGroup_Oid As mas_ResourceGroup
        Get
            Return _ResourceGroup_Oid
        End Get
        Set(ByVal value As mas_ResourceGroup)
            SetPropertyValue("ResourceGroup_Oid", _ResourceGroup_Oid, value)
        End Set
    End Property


    Private _ResourceType_ID As String
    <XafDisplayName("รหัสประเภททรัพยากร"), Index(1)>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property ResourceType_ID As String
        Get
            Return _ResourceType_ID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ResourceType_ID", _ResourceType_ID, value)
        End Set
    End Property

    Private _ResourceType_Name As String
    <XafDisplayName("ชื่อประเภททรัพยากร"), Index(2)>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), VisibleInLookupListView(True), RuleUniqueValue(DefaultContexts.Save), Size(200)>
    Public Property ResourceType_Name As String
        Get
            Return _ResourceType_Name
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ResourceType_Name", _ResourceType_Name, value)
        End Set
    End Property
End Class
