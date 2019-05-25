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
<XafDisplayName("กลุ่มอุตสาหกรรมพิเศษ")>
Public Class Mas_SpecialIndustryGroups ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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
    Private privateIndustryCategoryID As Integer
    <XafDisplayName("รหัสกลุ่มอุตสาหกรรม"), Index(0)>
    <RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property IndustryCategoryID() As Integer
        Get
            Return privateIndustryCategoryID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("IndustryCategoryID", privateIndustryCategoryID, value)
        End Set
    End Property

    Private _IndustryCategoryName As String
    <XafDisplayName("ชื่อกลุ่มอุตสาหกรรม"), Index(1)>
    <Size(200), VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)>
    Public Property IndustryCategoryName() As String
        Get
            Return _IndustryCategoryName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("IndustryCategoryName", _IndustryCategoryName, value)
        End Set
    End Property

    <XafDisplayName("ประเภทสถานศึกษา"), ImmediatePostData()>
    <Association("specialIndustryGroups-specialIndustryTypes", GetType(mas_specialIndustryTypes))>
    Public ReadOnly Property specialIndustryTypes() As XPCollection
        Get
            Return GetCollection("specialIndustryTypes")
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
