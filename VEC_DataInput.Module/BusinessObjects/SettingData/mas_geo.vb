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
<XafDisplayName("ศูนย์ฯ ภาค")>
Public Class mas_geo ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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
    Private privateId As String
    <XafDisplayName("รหัสศูนย์ฯ ภาค"), Index(0)>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save, CustomMessageTemplate:="รหัสศูนย์ฯ ภาคนี้มีอยู่ในระบบแล้ว")>
    Public Property geoId() As String
        Get
            Return privateId
        End Get
        Set(ByVal value As String)
            SetPropertyValue("geoId", privateId, value)
        End Set
    End Property


    Private _geoName As String
    <XafDisplayName("ชื่อศูนย์ฯ ภาค"), Index(1)>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), VisibleInLookupListView(True), RuleUniqueValue(DefaultContexts.Save, CustomMessageTemplate:="ชื่อศูนย์ฯ ภาคนี้มีอยู่ในระบบแล้ว"), Size(200)>
    Public Property geoName() As String
        Get
            Return _geoName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("geoName", _geoName, value)
        End Set
    End Property

    <XafDisplayName("ศูนย์ฯ กลุ่มจังหวัด"), ImmediatePostData()>
    <Association("geo-vecCenters", GetType(mas_vecCenter))>
    Public ReadOnly Property vecCenters() As XPCollection
        Get
            Return GetCollection("vecCenters")
        End Get
    End Property

    Private _LastUpdated As DateTime
    <XafDisplayName("วันที่ Update ล่าสุด"), Index(2)>
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
