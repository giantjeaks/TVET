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
<XafDisplayName("สถานศึกษา")>
Public Class Institution ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    End Sub


    Private _province_Oid As mas_province
    <XafDisplayName("จังหวัด"), ImmediatePostData(), Index(0)>
    <Association("Provinces-Institutions")>
    Public Property ref_mas_province() As mas_province
        Get
            Return _province_Oid
        End Get
        Set(ByVal value As mas_province)
            SetPropertyValue("ref_mas_province", _province_Oid, value)
        End Set
    End Property

    Private _InstitutionID As String
    <XafDisplayName("รหัสสถานศึกษา")>
    <Size(10), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save, CustomMessageTemplate:="รหัสสถานศึกษานี้มีอยู่ในระบบแล้ว")>
    Public Property InstitutionID As String
        Get
            Return _InstitutionID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InstitutionID", _InstitutionID, value)
        End Set
    End Property

    Private _InstitutionName As String
    <XafDisplayName("ชื่อสถานศึกษา")>
    <Size(200), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property InstitutionName As String
        Get
            Return _InstitutionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InstitutionName", _InstitutionName, value)
        End Set
    End Property
End Class
