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
<XafDisplayName("จังหวัด")>
Public Class mas_province ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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

    Private _centerId_Oid As mas_vecCenter

    <XafDisplayName("ศูนย์ฯ กลุ่มจังหวัด"), ImmediatePostData(), Index(0)>
    <Association("vecCenters-Provinces")>
    Public Property ref_mas_vecCenter() As mas_vecCenter
        Get
            Return _centerId_Oid
        End Get
        Set(ByVal value As mas_vecCenter)
            SetPropertyValue("ref_mas_vecCenter", _centerId_Oid, value)
        End Set
    End Property

    Private privateprovinceId As Integer
    <XafDisplayName("รหัสจังหวัด"), Index(1)>
    <VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property provinceId() As Integer
        Get
            Return privateprovinceId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("provinceId", privateprovinceId, value)
        End Set
    End Property

    Private _provinceName As String
    <XafDisplayName("ชื่อจังหวัด"), Index(2)>
    <Size(100), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property provinceName() As String
        Get
            Return _provinceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("provinceName", _provinceName, value)
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

    <XafDisplayName("สถานศึกษา"), ImmediatePostData()>
    <Association("Provinces-Institutions", GetType(Institution))>
    Public ReadOnly Property Institutions() As XPCollection
        Get
            Return GetCollection("Institutions")
        End Get
    End Property
End Class
