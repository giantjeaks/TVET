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
<XafDisplayName("ประเภทนวัตกรรม")>
Public Class mas_Innovation ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    End Sub

    Private _Innovation_name As String
    <XafDisplayName("ชื่อนวัตกรรม"), Index(1)>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), VisibleInLookupListView(True), RuleUniqueValue(DefaultContexts.Save, CustomMessageTemplate:="ชื่อนวัตกรรมนี้มีอยู่ในระบบแล้ว"), Size(200)>
    Public Property Innovation_name() As String
        Get
            Return _Innovation_name
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Innovation_name", _Innovation_name, value)
        End Set
    End Property
End Class
