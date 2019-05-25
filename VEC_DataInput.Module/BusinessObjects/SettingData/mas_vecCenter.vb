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
<XafDisplayName("ศูนย์ฯ กลุ่มจังหวัด")>
Public Class mas_vecCenter ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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


    Private _geoId_Oid As mas_geo
    <XafDisplayName("ศูนย์ฯ ภาค"), ImmediatePostData(), Index(0), RuleRequiredField(DefaultContexts.Save)>
    <Association("geo-vecCenters")>
    Public Property ref_mas_geo() As mas_geo
        Get
            Return _geoId_Oid
        End Get
        Set(ByVal value As mas_geo)
            SetPropertyValue("ref_mas_geo", _geoId_Oid, value)

        End Set
    End Property

    Private privatecenterId As String
    <XafDisplayName("รหัสศูนย์ฯ กลุ่มจังหวัด"), Index(1)>
    <VisibleInDetailView(True), VisibleInListView(True), Size(10), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save, CustomMessageTemplate:="รหัสศูนย์ฯ กลุ่มจังหวัดนี้มีอยู่ในระบบแล้ว")>
    Public Property centerId() As String
        Get
            Return privatecenterId
        End Get
        Set(ByVal value As String)
            SetPropertyValue("centerId", privatecenterId, value)
        End Set
    End Property

    Private _centerName As String
    <XafDisplayName("ชื่อศูนย์ฯ กลุ่มจังหวัด"), Index(2)>
    <Size(200), VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property centerName() As String
        Get
            Return _centerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("centerName", _centerName, value)
        End Set
    End Property


    <XafDisplayName("จังหวัด"), ImmediatePostData()>
    <Association("vecCenters-Provinces", GetType(mas_province))>
    Public ReadOnly Property Provinces() As XPCollection
        Get
            Return GetCollection("Provinces")
        End Get
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
End Class
